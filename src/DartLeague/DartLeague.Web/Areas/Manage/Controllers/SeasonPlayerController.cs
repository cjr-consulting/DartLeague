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
    public class SeasonPlayerController : Controller
    {
        private readonly SeasonContext _seasonContext;

        public SeasonPlayerController(SeasonContext seasonContext)
        {
            _seasonContext = seasonContext;
        }

        [Route("manage/season/{seasonId}/player")]
        public async Task<IActionResult> Index(int seasonId)
        {
            ViewData["SeasonNavPage"] = "Players";
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
                new SeasonManagementRootViewModel<List<SeasonLinkViewModel>>
                {
                    SeasonEdit = season,
                    Data = new List<SeasonLinkViewModel>()
                });
        }
    }
}