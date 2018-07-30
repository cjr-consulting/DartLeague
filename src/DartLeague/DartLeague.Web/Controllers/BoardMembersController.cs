using DartLeague.Repositories.LeagueData;
using DartLeague.Repositories.SeasonData;
using DartLeague.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DartLeague.Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class BoardMembersController : Controller
    {
        private readonly LeagueContext _leagueContext;
        private readonly SeasonContext _seasonContext;

        public BoardMembersController(LeagueContext leagueContext, SeasonContext seasonContext)
        {
            _leagueContext = leagueContext;
            _seasonContext = seasonContext;
        }

        public async Task<IActionResult> Index()
        {
            var today = DateTime.Now.Date;
            var currentSeason = await _seasonContext.Seasons
                .FirstOrDefaultAsync(x => x.StartDate <= today && today <= x.EndDate);
            var members = await _leagueContext.Members.ToListAsync();
            var positions = await GetPositions();

            var boardMembers = await _seasonContext.BoardMembers
                .Where(x => x.DeletedAt.HasValue == false && x.SeasonId == currentSeason.Id)
                .ToListAsync();

            var model = boardMembers
                .Select(x =>
                    new BoardMemberListViewModel
                    {
                        Name = members.Where(m => m.Id == x.MemberId).Select(m => $"{m.FirstName} {m.LastName}")
                            .FirstOrDefault(),
                        Position = positions.First(p => p.Id == x.PositionId).Name,
                        StartSeason = _seasonContext
                            .BoardMembers
                            .Where(m => m.MemberId == x.MemberId && m.PositionId == x.PositionId)
                            .OrderBy(o => o.Season.StartDate)
                            .First().Season.Title
                    }).ToList();
            return View(model);
        }

        private async Task<List<BoardPosition>> GetPositions()
        {
            return await _seasonContext.BoardPositions.ToListAsync();
        }
    }
}