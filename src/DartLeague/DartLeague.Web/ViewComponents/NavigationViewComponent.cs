using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DartLeague.Web.ViewComponents.Models.Navigation;
using EFLeagueData = DartLeague.Repositories.LeagueData;
using EFSeasonData = DartLeague.Repositories.SeasonData;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Collections.Generic;

namespace DartLeague.Web.Controllers.Components
{
    public class NavigationViewComponent : ViewComponent
    {
        private readonly EFLeagueData.LeagueContext _leagueContext;
        private readonly EFSeasonData.SeasonContext _seasonContext;

        public NavigationViewComponent(EFLeagueData.LeagueContext leagueContext, EFSeasonData.SeasonContext seasonContext)
        {
            _leagueContext = leagueContext;
            _seasonContext = seasonContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(string template)
        {
            var model = await NavBuilder(Url, template);
            return View(model);
        }
        
        public async Task<SiteNavigationViewModel> NavBuilder(IUrlHelper url, string name = "Default")
        {
            switch (name)
            {
                case "DartsForDreams":
                    return await DartsForDreamsNavigation(url);
                case "Manage":
                    return await ManagementNavigation(url);
                default:
                    return await DefaultNavigation(url);
            }
        }

        private async Task<SiteNavigationViewModel> DefaultNavigation(IUrlHelper url)
        {
            return new SiteNavigationViewModel
            {
                ParentNavigations = {
                    new NavigationViewModel
                    {
                        Title = "Current",
                        SubNavigations =
                        {
                            new NavigationViewModel
                            {
                                Title = "Weekly Standings",
                                Href = url.Action("Index", "Home", null)
                            },
                            new NavigationViewModel
                            {
                                Title = "Full Schedule"
                            },
                            new NavigationViewModel
                            {
                                Title = "Stats"
                            },
                            new NavigationViewModel
                            {
                                Title = "Leaderboards"
                            },
                            new NavigationViewModel
                            {
                                Title = "Awards"
                            },
                            new NavigationViewModel
                            {
                                Title = "Teams"
                            }
                        }
                    },
                    await BuildSeasonActivitiesMenu(),
                    new NavigationViewModel
                    {
                        Title="League",
                        SubNavigations =
                        {
                            new NavigationViewModel
                            {
                                Title="Where we Play",
                                Href = url.Action("Index", "WhereWePlay")
                            },
                            new NavigationViewModel
                            {
                                Title="Sponsors and Partners",
                                Href = url.Action("Index", "Sponsor")
                            },
                            new NavigationViewModel
                            {
                                Title = "Darts in the Region",
                                Href = url.Action("Index", "DartsInRegion")
                            },
                            new NavigationViewModel
                            {
                                Title = "Board Members",
                                Href = url.Action("Index", "BoardMembers")
                            },
                            new NavigationViewModel
                            {
                                Title="Contact",
                                Href = url.Action("Index", "Contact")
                            }
                        }
                    },
                    new NavigationViewModel
                    {
                        Title="History",
                        SubNavigations = await BuildHistoryMenu(url)
                    },
                    await BuildOtherMenu()
                }
            };
        }

        private async Task<List<NavigationViewModel>> BuildHistoryMenu(IUrlHelper url)
        {
            var today = DateTime.Now.Date;
            return await _seasonContext.Seasons
                .Where(x => x.EndDate < today)
                .OrderByDescending(x => x.StartDate)
                .Select(x =>
                    new NavigationViewModel
                    {
                        Title = x.Title,
                        Href = url.Action("Index", "History", new {title = x.Title})
                    }).ToListAsync();
        }

        private async Task<NavigationViewModel> BuildSeasonActivitiesMenu()
        {
            var nav = new NavigationViewModel
            {
                Title = "Activities and Events"
            };

            var season = await CurrentSeason();
            if (season == null)
                return nav;

            foreach (var link in season.SeasonLinks.OrderBy(x => x.Order).ToList())
            {
                nav.SubNavigations.Add(new NavigationViewModel
                {
                    Title = link.Title,
                    Href = link.Url
                });
            }

            return nav;
        }

        private async Task<EFSeasonData.Season> CurrentSeason()
        {
            var today = DateTime.Now.Date;
            return await _seasonContext.Seasons
                .Include("SeasonLinks")
                .FirstOrDefaultAsync(x => x.StartDate <= today && today <= x.EndDate);
        }

        private async Task<NavigationViewModel> BuildOtherMenu()
        {
            var nav = new NavigationViewModel
            {
                Title = "Other"
            };

            foreach (var link in await _leagueContext.LeagueLinks.OrderBy(x => x.Order).ToListAsync())
            {
                nav.SubNavigations.Add(new NavigationViewModel
                {
                    Title = link.Title,
                    Href = link.Url
                });
            }

            return nav;
        }

        private async Task<SiteNavigationViewModel> ManagementNavigation(IUrlHelper url)
        {
            return await Task.FromResult(new SiteNavigationViewModel
            {
                ParentNavigations =
                {
                    new NavigationViewModel
                    {
                        Title = "Home",
                        Href = url.Action("Index", "Home", new {Area = "Manage"})
                    },
                    new NavigationViewModel
                    {
                        Title = "Site",
                        Href = "#",
                        SubNavigations = {
                            new NavigationViewModel{ Title = "Dart Events", Href = url.Action("Index", "DartEvent", new { Area = "Site" }) },
                            new NavigationViewModel{ Title = "Page Content", Href =url.Action("Index", "PagePart", new { Area = "Site" }) },
                            new NavigationViewModel{ Title = "Luck of the Draws", Href =url.Action("Index", "Lod", new { Area = "Site" }) },
                            new NavigationViewModel{ Title = "Activities", Href =url.Action("Index", "Activities", new { Area = "Site" }) }
                        }
                    },
                    new NavigationViewModel {
                        Title = "League",
                        Href = "#",
                        SubNavigations = {
                            new NavigationViewModel{ Title = "Members", Href = url.Action("Index", "Member", new { Area = "Manage" }) },
                            new NavigationViewModel{ Title = "Teams", Href = "manage.team.index"},
                            new NavigationViewModel{ Title = "Sponsors", Href = url.Action("Index", "Sponsor", new { Area = "Manage" })},
                            new NavigationViewModel{ Title = "Board Members", Href = "manage.boardmember.index"},
                        }
                    }
                }
            });
        }

        private async Task<SiteNavigationViewModel> DartsForDreamsNavigation(IUrlHelper url)
        {
            return await Task.FromResult(new SiteNavigationViewModel
            {
                ParentNavigations =
                {
                    new NavigationViewModel
                    {
                        Title = "Darts for Dreams 12 -  April 29th, 2017",
                        SubNavigations = {
                            new NavigationViewModel
                            {
                                Title ="Event Flyer",
                              Href = "documents/events/2016-2017/flier17.pdf"
                            },
                            new NavigationViewModel
                            {
                                Title = "GTDL Player letter",
                              Href = "documents/events/2016-2017/playerletter17.pdf"
                            },
                            new NavigationViewModel
                            {
                                Title = "Player Pledge Sheet",
                                Href = "documents/events/2016-2017/pledge17.pdf"
                            },
                            new NavigationViewModel
                            {
                                Title = "Paperwork for Sponsors",
                                Href = "documents/events/2016-2017/sponsorpackage17.pdf"
                            },
                            new NavigationViewModel
                            {
                                Title = "Current Donation List",
                                Href = "documents/events/2016-2017/donationreport.pdf"
                            },
                            new NavigationViewModel
                            {
                                Title = "Online Donation Form!!",
                                Href = "http://site.wish.org/site/TR/FriendsandFamily/Make-A-WishNewJersey?px=3100639&pg=personal&fr_id=2340"
                            },
                            new NavigationViewModel
                            {
                                Title = "What\"s a dart-a-thon",
                                Href = "documents/events/static/whatisadartathon.pdf"
                            }
                         }
                    },
                    new NavigationViewModel
                    {
                        Title = "History of Events",
                        Href = "#"
                    },
                    new NavigationViewModel
                    {
                        Title = "Return to GTDL",
                        Href = "/"
                    }
                }
            });
        }
    }
}
