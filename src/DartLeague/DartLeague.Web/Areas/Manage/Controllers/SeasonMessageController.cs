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
    [ApiExplorerSettings(IgnoreApi = true)]
    public class SeasonMessageController : Controller
    {
        private readonly SeasonContext _seasonContext;

        public SeasonMessageController(SeasonContext seasonContext)
        {
            _seasonContext = seasonContext;
        }

        [Route("manage/season/{seasonId}/message")]
        public async Task<IActionResult> Index(int seasonId)
        {
            ViewData["SeasonNavPage"] = "Messages";
            var season = await _seasonContext.Seasons
                .Select(x =>
                    new SeasonEditViewModel
                    {
                        Id = x.Id,
                        Title = x.Title,
                        StartDate = x.StartDate,
                        EndDate = x.EndDate
                    })
                .FirstOrDefaultAsync(x => x.Id == seasonId);

            return View(
                new SeasonManagementRootViewModel<List<SeasonBoardListViewModel>>
                {
                    SeasonEdit = season,
                    Data = new List<SeasonBoardListViewModel>()
                });
        }
    }
}