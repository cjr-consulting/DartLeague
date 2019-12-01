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
    [ApiExplorerSettings(IgnoreApi = true)]
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
            var currentSeason = await GetSeason(seasonId);
            var previousSeason = await _seasonContext.Seasons.Include("BoardMembers").Where(x => x.StartDate < currentSeason.StartDate)
                .OrderByDescending(x => x.StartDate)
                .FirstOrDefaultAsync();

            var indexModel = new SeasonBoardIndexViewModel
            {
                BoardMembers = await GetBoardMembers(seasonId, members)
            };
            indexModel.IsCopyAvailable = !indexModel.BoardMembers.Any() &&
                previousSeason != null && previousSeason.BoardMembers.Any();

            var model =
                new SeasonManagementRootViewModel<SeasonBoardIndexViewModel>
                {
                    SeasonEdit = currentSeason,
                    Data = indexModel
                };

            return View(model);
        }

        private async Task<List<SeasonBoardListViewModel>> GetBoardMembers(int seasonId, List<Member> members)
        {
            var dbBoardMembers = await _seasonContext.BoardMembers.Include("Position")
                                    .Where(x => x.SeasonId == seasonId)
                                    .OrderBy(x => x.Position.Order)
                                    .ToListAsync();
            return dbBoardMembers.Select(x =>
                                        new SeasonBoardListViewModel
                                        {
                                            Id = x.Id,
                                            Name = members.First(m => m.Id == x.MemberId).FirstName + " " +
                                                   members.First(m => m.Id == x.MemberId).LastName,
                                            Position = x.Position.Name
                                        })
                                        .ToList();
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
                        PositionId = model.PositionId,
                        CreatedAt = DateTime.UtcNow
                    };
                    _seasonContext.BoardMembers.Add(boardMember);
                    await _seasonContext.SaveChangesAsync();

                    return RedirectToAction("Index", "SeasonBoard", new { seasonId });
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
                    MemberId = seasonBoardMember.MemberId,
                    PositionId = seasonBoardMember.PositionId
                }
            };

            return View(result);
        }

        [HttpPost("/Manage/Season/{seasonId}/board/{id}/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int seasonId, int id, BoardMemberEditViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var boardMember =
                        await _seasonContext.BoardMembers
                            .FirstOrDefaultAsync(x => x.SeasonId == seasonId && x.Id == id);

                    boardMember.MemberId = model.MemberId;
                    boardMember.PositionId = model.PositionId;
                    boardMember.UpdatedAt = DateTime.UtcNow;
                    
                    await _seasonContext.SaveChangesAsync();
                    return RedirectToAction("Index", "SeasonBoard", new { seasonId });
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

            var result = new SeasonManagementRootViewModel<BoardMemberEditViewModel>
            {
                SeasonEdit = await GetSeason(seasonId),
                Data = model
            };
            return View(result);
        }

        [Route("/Manage/Season/{seasonId}/board/{id}/delete")]
        public async Task<IActionResult> Delete(int seasonId, int id)
        {
            var boardmember =
                await _seasonContext.BoardMembers.FirstOrDefaultAsync(x => x.SeasonId == seasonId && x.Id == id);

            if (boardmember != null)
            {
                _seasonContext.BoardMembers.Remove(boardmember);
                await _seasonContext.SaveChangesAsync();
            }

            return RedirectToAction("Index", "SeasonBoard", new { seasonId });
        }

        [Route("/Manage/Season/{seasonId}/board/copy")]
        public async Task<IActionResult> Copy(int seasonId)
        {
            if (await _seasonContext.BoardMembers.AnyAsync(x => x.SeasonId == seasonId))
            {
                return RedirectToAction("Index", "SeasonBoard", new { seasonId });
            }

            var currentSeason = await _seasonContext.Seasons.FirstOrDefaultAsync(x => x.Id == seasonId);
            var previousSeason = await _seasonContext.Seasons.Include("BoardMembers").Where(x => x.StartDate < currentSeason.StartDate)
                .OrderByDescending(x => x.StartDate)
                .FirstOrDefaultAsync();
            await _seasonContext.BoardMembers.AddRangeAsync(
                previousSeason.BoardMembers.Select(x =>
                    new BoardMember
                    {
                        SeasonId = seasonId,
                        MemberId = x.MemberId,
                        PositionId = x.PositionId,
                        CreatedAt = DateTime.UtcNow
                    }));
            await _seasonContext.SaveChangesAsync();

            return RedirectToAction("Index", "SeasonBoard", new { seasonId });
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