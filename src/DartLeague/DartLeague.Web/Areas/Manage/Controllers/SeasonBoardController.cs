using System;
using DartLeague.Repositories.LeagueData;
using DartLeague.Repositories.SeasonData;
using DartLeague.Web.Areas.Manage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DartLeague.Web.Helpers;

namespace DartLeague.Web.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SeasonBoardController : Controller
    {
        private readonly SeasonContext _seasonContext;
        private readonly LeagueContext _leagueContext;

        public SeasonBoardController(SeasonContext seasonContext, LeagueContext leagueContext)
        {
            _seasonContext = seasonContext;
            _leagueContext = leagueContext;
        }

        [Route("manage/season/{seasonId}/board")]
        public async Task<IActionResult> Index(int seasonId)
        {
            ViewData["SeasonNavPage"] = "Board";
            var members = await _leagueContext.Members.ToListAsync();

            var model =
                new SeasonManagementRootViewModel<List<SeasonBoardListViewModel>>
                {
                    SeasonEdit = await GetSeason(seasonId),
                    Data = await _seasonContext.BoardMembers
                        .Where(x => x.SeasonId == seasonId)
                        .Select(x =>
                            new SeasonBoardListViewModel
                            {
                                Id = x.Id,
                                Name = members.First(m => m.Id == x.MemberId).FirstName + " " +
                                       members.First(m => m.Id == x.MemberId).LastName,
                                Position = x.Position.Name
                            }).ToListAsync()
                };

            return View(model);
        }

        [Route("/manage/season/{seasonId}/board/create")]
        public async Task<IActionResult> Create(int seasonId)
        {
            var model = new SeasonManagementRootViewModel<BoardMemberEditViewModel>
            {
                SeasonEdit = await GetSeason(seasonId),
                Data = new BoardMemberEditViewModel
                {
                    Positions = await GetPositions(),
                    Members = await GetMembers()
                }
            };

            return View(model);
        }

        [HttpPost("/manage/season/{seasonId}/board/create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int seasonId, BoardMemberEditViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var boardMember = new BoardMember
                    {
                        SeasonId = seasonId,
                        MemberId = model.MemberId,
                        PositionId = model.PositionId
                    };
                    _seasonContext.BoardMembers.Add(boardMember);
                    await _seasonContext.SaveChangesAsync();

                    return RedirectToAction("Index", "SeasonBoard");
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
            model.Positions = await GetPositions();
            return View(new SeasonManagementRootViewModel<BoardMemberEditViewModel>
            {
                SeasonEdit = await GetSeason(seasonId),
                Data = model
            });
        }

        [Route("/Manage/Season/{seasonId}/board/{id}/edit")]
        public async Task<IActionResult> Edit(int seasonId, int id)
        {
            var seasonBoardMember =
                await _seasonContext.BoardMembers.FirstOrDefaultAsync(x => x.SeasonId == seasonId && x.Id == id);
            var result = new SeasonManagementRootViewModel<BoardMemberEditViewModel>
            {
                SeasonEdit = await GetSeason(seasonId),
                Data = new BoardMemberEditViewModel
                {
                    Positions = await GetPositions(),
                    Members = await GetMembers(),
                    MemberId = id,
                    PositionId = seasonBoardMember.PositionId
                }
            };

            return View(result);
        }

        [HttpPost("/Manage/Season/{seasonId}/board/{id}/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int seasonId, int id, BoardMemberEditViewModel model)
        {
            return View();
        }

        private async Task<List<SelectListItem>> GetPositions()
        {
            return await _seasonContext.BoardPositions
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToListAsync();
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