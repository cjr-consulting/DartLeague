using DartLeague.Repositories.SeasonData;
using DartLeague.Web.Areas.Manage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DartLeague.Web.Areas.Manage.Controllers
{
    [Area("Manage")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class SeasonCompetitionController : Controller
    {
        private readonly SeasonContext _seasonContext;

        public SeasonCompetitionController(SeasonContext seasonContext)
        {
            _seasonContext = seasonContext;
        }

        [Route("manage/season/{seasonId}/competition")]
        public async Task<IActionResult> Index(int seasonId)
        {
            ViewData["SeasonNavPage"] = "Competitions";
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