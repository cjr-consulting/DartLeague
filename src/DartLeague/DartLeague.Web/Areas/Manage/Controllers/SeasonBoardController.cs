using DartLeague.Repositories.LeagueData;
using DartLeague.Repositories.SeasonData;
using DartLeague.Web.Areas.Manage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        [Route("manage/season/{id}/board")]
        public async Task<IActionResult> Index(int id)
        {
            ViewData["SeasonNavPage"] = "Board";
            
            return View(
                new SeasonManagementRootViewModel<List<SeasonBoardListViewModel>>
                {
                    SeasonEdit = await GetSeason(id),
                    Data = new List<SeasonBoardListViewModel>()
                });
        }
        
        [Route("/manage/season/{seasonId}/board/create")]
        public async Task<IActionResult> Create(int seasonId)
        {
            var result = new SeasonManagementRootViewModel<BoardMemberEditViewModel>
            {
                SeasonEdit = await GetSeason(seasonId),
                Data = new BoardMemberEditViewModel
                {
                    Positions = await GetPositions(),
                    Members = await GetMembers()
                }
            };

            return View(result);
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