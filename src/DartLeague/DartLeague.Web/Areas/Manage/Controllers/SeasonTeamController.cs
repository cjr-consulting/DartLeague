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

        public SeasonTeamController(LeagueContext leagueContext, SeasonContext seasonContext, IBrowsableFileService browsableFileService)
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
                    Members = await GetMembers()
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
                            FileName = $"{FileHelper.CleanString(team.Name)}-teamPicture{Path.GetExtension(file.FileName)}",
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

            model.Members = await GetMembers();
            model.Roles = await GetRoles();
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
            team.Members = await GetMembers();

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
                            FileName = $"{FileHelper.CleanString(team.Name)}-teamPicture{Path.GetExtension(file.FileName)}",
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
            model.Members = await GetMembers();
            return View(new SeasonManagementRootViewModel<SeasonTeamEditViewModel>
            {
                SeasonEdit = await GetSeason(seasonId),
                Data = model
            });
        }

        private async Task<SeasonTeamEditViewModel> GetTeamEditViewModel(int teamId)
        {
            var team = await _seasonContext.Teams.Include("Players").FirstAsync(x => x.Id == teamId);
            var model = new SeasonTeamEditViewModel
            {
                Id = team.Id,
                Name = team.Name,
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

        private async Task<List<SelectListItem>> GetMembers()
        {
            return await _leagueContext.Members
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
    }
}