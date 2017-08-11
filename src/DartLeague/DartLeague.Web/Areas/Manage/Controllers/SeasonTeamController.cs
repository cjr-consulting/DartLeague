using System;
using DartLeague.Repositories.SeasonData;
using DartLeague.Web.Areas.Manage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DartLeague.Domain.BrowsableFiles;
using DartLeague.Repositories.LeagueData;
using DartLeague.Web.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using BrowsableFile = DartLeague.Domain.BrowsableFiles.BrowsableFile;

namespace DartLeague.Web.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SeasonTeamController : Controller
    {
        private readonly SeasonContext _seasonContext;
        private readonly IBrowsableFileService _browsableFileService;
        private readonly LeagueContext _leagueContext;

        public SeasonTeamController(LeagueContext leagueContext, SeasonContext seasonContext,
            IBrowsableFileService browsableFileService)
        {
            _leagueContext = leagueContext;
            _seasonContext = seasonContext;
            _browsableFileService = browsableFileService;
        }

        [Route("manage/season/{seasonId}/team")]
        public async Task<IActionResult> Index(int seasonId)
        {
            ViewData["SeasonNavPage"] = "Teams";

            return View(
                new SeasonManagementRootViewModel<List<SeasonTeamListViewModel>>
                {
                    SeasonEdit = await GetSeason(seasonId),
                    Data = await _seasonContext.Teams.Include("Players")
                        .Where(x => x.SeasonId == seasonId)
                        .OrderBy(x => x.Name)
                        .Select(x =>
                            new SeasonTeamListViewModel
                            {
                                TeamId = x.Id,
                                Name = x.Name,
                                Abbreviation = x.Abbreviation,
                                NumberOfPlayers = x.Players.Count()
                            }).ToListAsync()
                });
        }

        [Route("/manage/season/{seasonId}/team/create")]
        public async Task<IActionResult> Create(int seasonId)
        {
            var model = new SeasonManagementRootViewModel<SeasonTeamCreateViewModel>
            {
                SeasonEdit = await GetSeason(seasonId),
                Data = new SeasonTeamCreateViewModel
                {
                    Roles = await GetRoles(),
                    Members = await GetAvailableMembers(seasonId),
                    Sponsors = await GetSponsors()
                }
            };

            return View(model);
        }

        [HttpPost("/manage/season/{seasonId}/team/create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            int seasonId,
            SeasonTeamCreateViewModel model,
            List<IFormFile> bannerFile,
            List<IFormFile> logoFile,
            List<IFormFile> teamFile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var team = new Team
                    {
                        SeasonId = seasonId,
                        Name = model.Name,
                        Abbreviation = model.Abbreviation,
                        SponsorId = model.SponsorId,
                        CreatedAt = DateTime.UtcNow
                    };

                    team.Players.Add(new TeamPlayer
                    {
                        Team = team,
                        MemberId = model.CaptainMemberId,
                        RoleId = 1
                    });

                    if (bannerFile.Any())
                    {
                        var file = bannerFile[0];
                        var f = new BrowsableFile
                        {
                            FileName = $"{FileHelper.CleanString(team.Name)}-banner{Path.GetExtension(file.FileName)}",
                            Extension = Path.GetExtension(file.FileName),
                            ContentType = file.ContentType,
                            Category = await FileCategoryName(seasonId, team.Name),
                            Stream = file.OpenReadStream()
                        };
                        team.BannerImageId = await _browsableFileService.AddAsync(f);
                    }

                    if (logoFile.Any())
                    {
                        var file = logoFile[0];
                        var f = new BrowsableFile
                        {
                            FileName = $"{FileHelper.CleanString(team.Name)}-logo{Path.GetExtension(file.FileName)}",
                            Extension = Path.GetExtension(file.FileName),
                            ContentType = file.ContentType,
                            Category = await FileCategoryName(seasonId, team.Name),
                            Stream = file.OpenReadStream()
                        };
                        team.LogoImageId = await _browsableFileService.AddAsync(f);
                    }

                    if (teamFile.Any())
                    {
                        var file = teamFile[0];
                        var f = new BrowsableFile
                        {
                            FileName =
                                $"{FileHelper.CleanString(team.Name)}-teamPicture{Path.GetExtension(file.FileName)}",
                            Extension = Path.GetExtension(file.FileName),
                            ContentType = file.ContentType,
                            Category = await FileCategoryName(seasonId, team.Name),
                            Stream = file.OpenReadStream()
                        };
                        team.TeamPictureImageId = await _browsableFileService.AddAsync(f);
                    }

                    await _seasonContext.Teams.AddAsync(team);
                    await _seasonContext.SaveChangesAsync();

                    return RedirectToAction("Index", "SeasonTeam");
                }
            }
            catch (DbUpdateException)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                                             "Try again, and if the problem persists " +
                                             "see your system administrator.");
            }

            model.Members = await GetAvailableMembers(seasonId);
            model.Roles = await GetRoles();
            model.Sponsors = await GetSponsors();
            return View(new SeasonManagementRootViewModel<SeasonTeamCreateViewModel>
            {
                SeasonEdit = await GetSeason(seasonId),
                Data = model
            });
        }

        [Route("/manage/season/{seasonId}/team/{id}/edit")]
        public async Task<IActionResult> Edit(int seasonId, int id)
        {
            var team = await GetTeamEditViewModel(id);
            team.Roles = await GetRoles();
            team.Members = await GetAvailableMembers(seasonId);
            team.Sponsors = await GetSponsors();

            var model = new SeasonManagementRootViewModel<SeasonTeamEditViewModel>
            {
                SeasonEdit = await GetSeason(seasonId),
                Data = team
            };

            return View(model);
        }

        [HttpPost("/manage/season/{seasonId}/team/{id}/edit")]
        public async Task<IActionResult> Edit(int seasonId, int id, SeasonTeamEditViewModel model,
            List<IFormFile> bannerFile,
            List<IFormFile> logoFile,
            List<IFormFile> teamFile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var team = await _seasonContext.Teams.FirstAsync(x => x.Id == id);
                    team.Name = model.Name;
                    team.Abbreviation = model.Abbreviation;
                    team.SponsorId = model.SponsorId;
                    team.UpdatedAt = DateTime.UtcNow;

                    if (bannerFile.Any())
                    {
                        var file = bannerFile[0];
                        var f = new BrowsableFile
                        {
                            FileName = $"{FileHelper.CleanString(team.Name)}-banner{Path.GetExtension(file.FileName)}",
                            Extension = Path.GetExtension(file.FileName),
                            ContentType = file.ContentType,
                            Category = await FileCategoryName(seasonId, team.Name),
                            Stream = file.OpenReadStream()
                        };

                        if (team.BannerImageId > 0)
                            await _browsableFileService.DeleteAsync(team.BannerImageId);

                        team.BannerImageId = await _browsableFileService.AddAsync(f);
                    }

                    if (logoFile.Any())
                    {
                        var file = logoFile[0];
                        var f = new BrowsableFile
                        {
                            FileName = $"{FileHelper.CleanString(team.Name)}-logo{Path.GetExtension(file.FileName)}",
                            Extension = Path.GetExtension(file.FileName),
                            ContentType = file.ContentType,
                            Category = await FileCategoryName(seasonId, team.Name),
                            Stream = file.OpenReadStream()
                        };

                        if (team.LogoImageId > 0)
                            await _browsableFileService.DeleteAsync(team.LogoImageId);

                        team.LogoImageId = await _browsableFileService.AddAsync(f);
                    }

                    if (teamFile.Any())
                    {
                        var file = teamFile[0];
                        var f = new BrowsableFile
                        {
                            FileName =
                                $"{FileHelper.CleanString(team.Name)}-teamPicture{Path.GetExtension(file.FileName)}",
                            Extension = Path.GetExtension(file.FileName),
                            ContentType = file.ContentType,
                            Category = await FileCategoryName(seasonId, team.Name),
                            Stream = file.OpenReadStream()
                        };

                        if (team.TeamPictureImageId > 0)
                            await _browsableFileService.DeleteAsync(team.TeamPictureImageId);

                        team.TeamPictureImageId = await _browsableFileService.AddAsync(f);
                    }

                    await _seasonContext.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateException)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                                             "Try again, and if the problem persists " +
                                             "see your system administrator.");
            }

            model.Roles = await GetRoles();
            model.Members = await GetAvailableMembers(seasonId);
            model.Sponsors = await GetSponsors();
            return View(new SeasonManagementRootViewModel<SeasonTeamEditViewModel>
            {
                SeasonEdit = await GetSeason(seasonId),
                Data = model
            });
        }

        [Route("/manage/season/{seasonId}/team/{id}/delete")]
        public async Task<IActionResult> Delete(int seasonId, int id)
        {
            var team = await _seasonContext.Teams.Include("Players")
                .FirstAsync(x => x.SeasonId == seasonId && x.Id == id);
            if (team != null)
            {
                foreach (var player in team.Players)
                    _seasonContext.TeamPlayers.Remove(player);

                _seasonContext.Teams.Remove(team);

                await _browsableFileService.DeleteAsync(team.LogoImageId);
                await _browsableFileService.DeleteAsync(team.BannerImageId);
                await _browsableFileService.DeleteAsync(team.TeamPictureImageId);
                await _seasonContext.SaveChangesAsync();
            }

            return RedirectToAction("Index", "SeasonTeam");
        }

        [Route("manage/season/{seasonId}/team/copyteams")]
        public async Task<IActionResult> CopyTeamsFromPreviousSeason(int seasonId, bool withPlayers)
        {
            var season = await _seasonContext.Seasons.Include("Teams").FirstAsync(x => x.Id == seasonId);
            if (season.Teams.Any())
                return RedirectToAction("Index", "SeasonTeam");

            var prevSeason = await _seasonContext.Seasons.Include("Teams.Players")
                .Where(x => x.StartDate < season.StartDate)
                .OrderByDescending(x => x.StartDate)
                .FirstAsync();
            foreach (var team in prevSeason.Teams)
            {
                var newTeam = new Team
                {
                    Name = team.Name,
                    Abbreviation = team.Abbreviation,
                    SponsorId = team.SponsorId,
                    CreatedAt = DateTime.UtcNow,
                    SeasonId = seasonId,
                };

                if (team.BannerImageId > 0)
                {
                    var banner = await _browsableFileService.GetAsync(team.BannerImageId);
                    banner.Category = await FileCategoryName(seasonId, team.Name);
                    newTeam.BannerImageId = await _browsableFileService.AddAsync(banner);
                }

                if (team.LogoImageId > 0)
                {
                    var logo = await _browsableFileService.GetAsync(team.LogoImageId);
                    logo.Category = await FileCategoryName(seasonId, team.Name);
                    newTeam.LogoImageId = await _browsableFileService.AddAsync(logo);
                }

                if (withPlayers)
                {
                    newTeam.Players.AddRange(team.Players.Select(x => new TeamPlayer
                    {
                        MemberId = x.MemberId,
                        RoleId = x.RoleId,
                        Team = newTeam
                    }));
                }

                await _seasonContext.Teams.AddAsync(newTeam);
            }

            await _seasonContext.SaveChangesAsync();
            return RedirectToAction("Index", "SeasonTeam");
        }

        private async Task<SeasonTeamEditViewModel> GetTeamEditViewModel(int teamId)
        {
            var team = await _seasonContext.Teams.Include("Players").FirstAsync(x => x.Id == teamId);
            var model = new SeasonTeamEditViewModel
            {
                Id = team.Id,
                Name = team.Name,
                SponsorId = team.SponsorId,
                Abbreviation = team.Abbreviation,
                BannerUrl = team.BannerImageId > 0
                    ? Url.Action("Index", "File", new {Area = "", Id = NumberObfuscation.Encode(team.BannerImageId)})
                    : string.Empty,
                LogoFileUrl = team.LogoImageId > 0
                    ? Url.Action("Index", "File", new {Area = "", Id = NumberObfuscation.Encode(team.LogoImageId)})
                    : string.Empty,
                TeamFileUrl = team.TeamPictureImageId > 0
                    ? Url.Action("Index", "File",
                        new {Area = "", Id = NumberObfuscation.Encode(team.TeamPictureImageId)})
                    : string.Empty
            };

            var members = await _leagueContext.Members.ToListAsync();
            var roles = await GetRoles();
            foreach (var player in team.Players)
            {
                var member = members.First(x => x.Id == player.MemberId);
                model.Players.Add(new SeasonTeamPlayerListViewModel
                {
                    Id = player.Id,
                    Name = $"{member.FirstName} {member.LastName}",
                    Email = member.Email,
                    Nickname = member.Nickname,
                    Role = roles.First(x => x.Value == player.RoleId.ToString()).Text
                });
            }

            return model;
        }

        private async Task<string> FileCategoryName(int seasonId, string teamName)
        {
            var season = await _seasonContext.Seasons.FirstAsync(x => x.Id == seasonId);
            return $"Season-{season.Title}-{teamName}";
        }

        private async Task<List<SelectListItem>> GetRoles()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Captain",
                    Value = "1"
                },
                new SelectListItem
                {
                    Text = "Co-Captain",
                    Value = "2"
                },
                new SelectListItem
                {
                    Text = "Player",
                    Value = "3"
                }
            };
        }

        private async Task<List<SelectListItem>> GetAvailableMembers(int seasonId)
        {
            var players = await _seasonContext.Teams
                .Include("Players")
                .Where(x => x.SeasonId == seasonId)
                .SelectMany(x => x.Players)
                .ToListAsync();

            return await _leagueContext.Members
                .Where(x => players.Any(p => p.MemberId == x.Id) == false)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = $"{x.FirstName} {x.LastName}"
                }).ToListAsync();
        }

        private async Task<SeasonEditViewModel> GetSeason(int id)
        {
            return await _seasonContext.Seasons
                .Select(x =>
                    new SeasonEditViewModel
                    {
                        Id = x.Id,
                        Title = x.Title,
                        StartDate = x.StartDate,
                        EndDate = x.EndDate
                    })
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        private async Task<List<SelectListItem>> GetSponsors()
        {
            return await _leagueContext.Sponsors
                .Where(x => x.Type == "T")
                .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToListAsync();
        }
    }
}