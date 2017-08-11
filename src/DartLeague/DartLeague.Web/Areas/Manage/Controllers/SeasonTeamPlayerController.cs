using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DartLeague.Repositories.LeagueData;
using DartLeague.Repositories.SeasonData;
using DartLeague.Web.Areas.Manage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DartLeague.Web.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SeasonTeamPlayerController : Controller
    {
        private readonly SeasonContext _seasonContext;
        private readonly LeagueContext _leagueContext;

        public SeasonTeamPlayerController(LeagueContext leagueContext, SeasonContext seasonContext)
        {
            _leagueContext = leagueContext;
            _seasonContext = seasonContext;
        }

        [Route("/manage/season/{seasonId}/team/{teamId}/player/create")]
        public async Task<IActionResult> Create(int seasonId, int teamId)
        {
            var model = new SeasonManagementRootViewModel<SeasonTeamPlayerCreateViewModel>
            {
                SeasonEdit = await GetSeason(seasonId),
                Data = new SeasonTeamPlayerCreateViewModel
                {
                    TeamId = teamId,
                    RoleId = 3,
                    Roles = await GetRoles(),
                    Members = await GetAvailableMembers(seasonId)
                }
            };

            return View(model);
        }

        [HttpPost("/manage/season/{seasonId}/team/{teamId}/player/create")]
        public async Task<IActionResult> Create(int seasonId, int teamId, SeasonTeamPlayerCreateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var team = await _seasonContext.Teams.FirstOrDefaultAsync(x => x.Id == teamId);

                    var player = new TeamPlayer
                    {
                        MemberId = model.MemberId,
                        RoleId = model.RoleId,
                        Team = team,
                        TeamId = teamId
                    };

                    await _seasonContext.TeamPlayers.AddAsync(player);
                    await _seasonContext.SaveChangesAsync();

                    return RedirectToAction("Edit", "SeasonTeam", new {id = teamId});
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
            return View(new SeasonManagementRootViewModel<SeasonTeamPlayerCreateViewModel>
            {
                SeasonEdit = await GetSeason(seasonId),
                Data = model
            });
        }

        [Route("/manage/season/{seasonId}/team/{teamId}/player/{id}/edit")]
        public async Task<IActionResult> Edit(int seasonId, int teamId, int id)
        {
            var player = await _seasonContext.TeamPlayers.FirstAsync(x => x.Id == id);
            var member = await _leagueContext.Members.FirstAsync(x => x.Id == player.MemberId);
            var model = new SeasonTeamPlayerEditViewModel
            {
                Name = $"{member.FirstName} {member.LastName} ({member.Nickname})",
                RoleId = player.RoleId,
                Roles = await GetRoles()
            };

            return View(new SeasonManagementRootViewModel<SeasonTeamPlayerEditViewModel>
            {
                SeasonEdit = await GetSeason(seasonId),
                Data = model
            });
        }

        [HttpPost("/manage/season/{seasonId}/team/{teamId}/player/{id}/edit")]
        public async Task<IActionResult> Edit(int seasonId, int teamId, int id, SeasonTeamPlayerEditViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var player = await _seasonContext.TeamPlayers.FirstAsync(x => x.Id == id);
                    player.RoleId = model.RoleId;
                    await _seasonContext.SaveChangesAsync();

                    return RedirectToAction("Edit", "SeasonTeam", new { id = teamId });
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
            return View(new SeasonManagementRootViewModel<SeasonTeamPlayerEditViewModel>
            {
                SeasonEdit = await GetSeason(seasonId),
                Data = model
            });
        }

        [Route("/manage/season/{seasonId}/team/{teamId}/player/{id}/delete")]
        public async Task<IActionResult> Delete(int seasonId, int teamId, int id)
        {
            var player = await _seasonContext.TeamPlayers.FirstOrDefaultAsync(x => x.TeamId == teamId && x.Id == id);
            if (player != null)
            {
                _seasonContext.TeamPlayers.Remove(player);
                await _seasonContext.SaveChangesAsync();
            }

            return RedirectToAction("Edit", "SeasonTeam", new { id = teamId });
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
    }
}