using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DartLeague.Web
{
    public static class HtmlLeagueHelper
    {
        public static SiteNavigation NavBuilder(IUrlHelper url, string name = "Default")
        {
            switch (name)
            {
                case "DartsForDreams":
                    return DartsForDreamsNavigation(url);
                case "Manage":
                    return ManagementNavigation(url);
                default:
                    return DefaultNavigation(url);
            }
        }

        private static SiteNavigation DefaultNavigation(IUrlHelper url)
        {
            return new SiteNavigation
            {
                ParentNavigations = {
                    new Navigation
                    {
                        Title = "Current",
                        SubNavigations =
                        {
                            new Navigation
                            {
                                Title = "Weekly Standings",
                                Href = url.Action("Index", "Home", null)
                            },
                            new Navigation
                            {
                                Title = "Full Schedule"
                            },
                            new Navigation
                            {
                                Title = "Stats"
                            },
                            new Navigation
                            {
                                Title = "Leaderboards"
                            },
                            new Navigation
                            {
                                Title = "Awards"
                            },
                            new Navigation
                            {
                                Title = "Teams"
                            }
                        }
                    },
                    new Navigation
                    {
                        Title="Activities and Events",
                        SubNavigations =
                        {
                            new Navigation
                            {
                                Title = "Memorial Tournament Bracket"
                            },
                            new Navigation
                            {
                                Title = "Memorial Tournament Info"
                            },
                            new Navigation
                            {
                                Title = "Mr. Trenton Bracket"
                            },
                            new Navigation
                            {
                                Title = "Mr. Trenton Rules"
                            },
                            new Navigation
                            {
                                Title = "All Star Qualifying Info"
                            },
                            new Navigation
                            {
                                Title = "Winter Singles League"
                            },
                            new Navigation
                            {
                                Title = "Summer Singles Weekly"
                            },
                            new Navigation
                            {
                                Title = "Summer Singles Schedule"
                            },
                            new Navigation
                            {
                                Title = "NJ State Cricket Championship"
                            },
                            new Navigation
                            {
                                Title = "Darts for Dreams Info"
                            },
                            new Navigation
                            {
                                Title = "GTDL Player Results at Events"
                            }
                        }
                    },
                    new Navigation
                    {
                        Title="League",
                        SubNavigations =
                        {
                            new Navigation
                            {
                                Title="Where we Play"
                            },
                            new Navigation
                            {
                                Title="Sponsors and Partners"
                            },
                            new Navigation
                            {
                                Title = "Darts in the Region"
                            },
                            new Navigation
                            {
                                Title = "Board Members"
                            },
                            new Navigation
                            {
                                Title="Contact"
                            }
                        }
                    },
                    new Navigation
                    {
                        Title="History",
                        SubNavigations =
                        {
                            new Navigation
                            {
                                Title="2015 - 2016"
                            },
                            new Navigation
                            {
                                Title = "2014 - 2015"
                            },
                            new Navigation
                            {
                                Title = "2013 - 2014"
                            },
                            new Navigation
                            {
                                Title = "2012 - 2013"
                            },
                            new Navigation
                            {
                                Title = "2011 - 2012"
                            },
                            new Navigation
                            {
                                Title = "2010 - 2011"
                            },
                            new Navigation
                            {
                                Title = "2009 - 2010"
                            }
                        }
                    },
                    new Navigation
                    {
                        Title="Other",
                        SubNavigations =
                        {
                            new Navigation
                            {
                                Title = "League Rules"
                            },
                            new Navigation
                            {
                                Title = "Sponsor Paperwork"
                            },
                            new Navigation
                            {
                                Title = "Player Registration"
                            },
                            new Navigation
                            {
                                Title = "Roster Sheet"
                            },
                            new Navigation
                            {
                                Title = "Scoresheet"
                            },
                            new Navigation
                            {
                                Title = "Chalker Guidelines"
                            },
                            new Navigation
                            {
                                Title = "01 Strategy"
                            },
                            new Navigation
                            {
                                Title = "Cricket Strategy"
                            }
                        }
                    }
                }
            };
        }

        private static SiteNavigation ManagementNavigation(IUrlHelper url)
        {
            return new SiteNavigation
            {
                ParentNavigations =
                {
                    new Navigation
                    {
                        Title = "Home",
                        Href = "manage/1"
                    },
                    new Navigation
                    {
                        Title = "Site",
                        Href = "#",
                        SubNavigations = {
                            new Navigation{ Title = "Dart Events", Href = "manage.site.dartevent.index"},
                            new Navigation{ Title = "Page Content", Href = "manage.site.pagepart.index"}
                        }
                    },
                    new Navigation {
                        Title = "League",
                        Href = "#",
                        SubNavigations = {
                            new Navigation{ Title = "Players", Href = url.Action("Index", "Player", new {Area = "Manage" }) },
                            new Navigation{ Title = "Teams", Href = "manage.team.index"},
                            new Navigation{ Title = "Sponsors", Href = "manage.sponsor.index"},
                            new Navigation{ Title = "Board Members", Href = "manage.boardmember.index"},
                        }
                    },
                    new Navigation
                    {
                        Title = "Seasons",
                        Href = "manage.season.index"
                    }
                }
            };
        }

        private static SiteNavigation DartsForDreamsNavigation(IUrlHelper url)
        {
            return new SiteNavigation
            {
                ParentNavigations =
                {
                    new Navigation
                    {
                        Title = "Darts for Dreams 12 -  April 29th, 2017",
                        SubNavigations = {
                            new Navigation
                            {
                                Title ="Event Flyer",
                              Href = "documents/events/2016-2017/flier17.pdf"
                            },
                            new Navigation
                            {
                                Title = "GTDL Player letter",
                              Href = "documents/events/2016-2017/playerletter17.pdf"
                            },
                            new Navigation
                            {
                                Title = "Player Pledge Sheet",
                                Href = "documents/events/2016-2017/pledge17.pdf"
                            },
                            new Navigation
                            {
                                Title = "Paperwork for Sponsors",
                                Href = "documents/events/2016-2017/sponsorpackage17.pdf"
                            },
                            new Navigation
                            {
                                Title = "Current Donation List",
                                Href = "documents/events/2016-2017/donationreport.pdf"
                            },
                            new Navigation
                            {
                                Title = "Online Donation Form!!",
                                Href = "http://site.wish.org/site/TR/FriendsandFamily/Make-A-WishNewJersey?px=3100639&pg=personal&fr_id=2340"
                            },
                            new Navigation
                            {
                                Title = "What\"s a dart-a-thon",
                                Href = "documents/events/static/whatisadartathon.pdf"
                            }
                         }
                    },
                    new Navigation
                    {
                        Title = "History of Events",
                        Href = "#"
                    },
                    new Navigation
                    {
                        Title = "Return to GTDL",
                        Href = "/"
                    }
                }
            };
        }
    }

    public class SiteNavigation
    {
        public List<Navigation> ParentNavigations { get; set; } = new List<Navigation>();
    }

    public class Navigation
    {
        public string Title { get; set; }
        public string Href { get; set; } = "#";
        public bool IsHeader { get; set; }
        public bool IsSeperator { get; set; }
        public List<Navigation> SubNavigations { get; set; } = new List<Navigation>();
    }
}