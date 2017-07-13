using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DartLeague.Repositories.SeasonData;
using DartLeague.Web.Areas.Manage.Models;
using Microsoft.EntityFrameworkCore;

namespace DartLeague.Web.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SeasonAchievementController : Controller
    {
        private readonly SeasonContext _seasonContext;

        public SeasonAchievementController(SeasonContext seasonContext)
        {
            _seasonContext = seasonContext;
        }

        [Route("/manage/season/{seasonId}/achievement")]
        public async Task<IActionResult> Index(int seasonId)
        {
            ViewData["SeasonNavPage"] = "Achievements";

            return View(new SeasonManagementRootViewModel<List<SeasonLinkListViewModel>>
            {
                SeasonEdit = await GetSeason(seasonId)
            });
        }

        private async Task<SeasonEditViewModel> GetSeason(int seasonId)
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
                            .FirstOrDefaultAsync(x => x.Id == seasonId);
        }
    }
}