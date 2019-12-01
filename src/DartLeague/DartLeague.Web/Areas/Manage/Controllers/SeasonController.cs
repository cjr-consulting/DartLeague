using DartLeague.Repositories.SeasonData;
using DartLeague.Web.Areas.Manage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace DartLeague.Web.Areas.Manage.Controllers
{
    [Area("Manage")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class SeasonController : Controller
    {
        private readonly SeasonContext _seasonContext;

        public SeasonController(SeasonContext seasonContext)
        {
            _seasonContext = seasonContext;
        }
        
        public async Task<IActionResult> Index()
        {
            ViewData["LeagueNavPage"] = "Seasons";

            var seasons = await _seasonContext.Seasons
                .Select(x =>
                    new SeasonListViewModel
                    {
                        Id = x.Id,
                        Title = x.Title,
                        StartDate = x.StartDate,
                        EndDate = x.EndDate
                    })
                .OrderByDescending(x => x.StartDate)
                .ToListAsync();

            return View(seasons);
        }

        [Route("manage/season/create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("manage/season/create")]
        public async Task<IActionResult> Create(SeasonEditViewModel model)
        {
            Contract.Requires(model != null, nameof(model));

            try
            {
                if (ModelState.IsValid)
                {
                    var season = new Season
                    {
                        Title = model.Title,
                        StartDate = model.StartDate.Value,
                        EndDate = model.EndDate.Value,
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = 1
                    };
                    _seasonContext.Seasons.Add(season);
                    await _seasonContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateException)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                                             "Try again, and if the problem persists " +
                                             "see your system administrator.");
            }

            return View(model);
        }

        [Route("manage/season/{id}")]
        public IActionResult Index(int id)
        {
            return RedirectToAction("Index", "SeasonLink", new { Area = "Manage", seasonId = id });
        }

        [HttpPost("manage/season/{id}/edit")]
        public async Task<IActionResult> Edit(int id, SeasonEditViewModel model)
        {
            Contract.Requires(model != null, nameof(model));

            try
            {
                if (ModelState.IsValid)
                {
                    var season = await _seasonContext.Seasons.FirstOrDefaultAsync(x => x.Id == id);
                    season.Title = model.Title;
                    season.StartDate = model.StartDate.Value;
                    season.EndDate = model.EndDate.Value;
                    await _seasonContext.SaveChangesAsync();
                    return Redirect(Request.Headers["Referer"].ToString());
                }
            }
            catch (DbUpdateException)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                                             "Try again, and if the problem persists " +
                                             "see your system administrator.");
            }

            return View();
        }
    }
}