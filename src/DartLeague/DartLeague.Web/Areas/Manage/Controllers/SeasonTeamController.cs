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
    public class SeasonTeamController : Controller
    {
        private readonly SeasonContext _seasonContext;

        public SeasonTeamController(SeasonContext seasonContext)
        {
            _seasonContext = seasonContext;
        }

        [Route("manage/season/{id}/team")]
        public async Task<IActionResult> Index(int id)
        {
            ViewData["SeasonNavPage"] = "Teams";
            var season = await _seasonContext.Seasons
                .Select(x =>
                    new SeasonEditViewModel
                    {
                        Id = x.Id,
                        Title = x.Title,
                        StartDate = x.StartDate,
                        EndDate = x.EndDate
                    })
                .FirstOrDefaultAsync(x => x.Id == id);

            return View(
                new SeasonManagementRootViewModel<List<SeasonLinkViewModel>>
                {
                    SeasonEdit = season,
                    Data = new List<SeasonLinkViewModel>()
                });
        }
    }
}