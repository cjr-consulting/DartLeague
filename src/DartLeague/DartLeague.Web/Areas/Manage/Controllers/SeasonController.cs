using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DartLeague.Repositories.SeasonData;
using DartLeague.Web.Areas.Manage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DartLeague.Web.Areas.Manage.Controllers
{
    public enum SeasonStates
    {
        Created = 1,
        Started = 2,
        Ended = 3
    }

    [Area("Manage")]
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
            //return View(new List<SeasonListViewModel>()
            //{
            //    new SeasonListViewModel
            //    {
            //        Id = 4,
            //        Title = "2017 - 2018",
            //        StartDate = new DateTime(2017, 9, 1),
            //        EndDate = new DateTime(2018, 8, 31),
            //    },
            //    new SeasonListViewModel
            //    {
            //        Id = 3,
            //        Title = "2016 - 2017",
            //        StartDate = new DateTime(2016, 9, 1),
            //        EndDate = new DateTime(2017, 8, 31),
            //    },
            //    new SeasonListViewModel
            //    {
            //        Id = 2,
            //        Title = "2015 - 2016",
            //        StartDate = new DateTime(2015, 9, 1),
            //        EndDate = new DateTime(2016, 8, 31),
            //    },
            //    new SeasonListViewModel
            //    {
            //        Id = 1,
            //        Title = "2014 - 2015",
            //        StartDate = new DateTime(2014, 9, 1),
            //        EndDate = new DateTime(2015, 8, 31),
            //    }
            //});
        }

        [Route("manage/season/create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("manage/season/create")]
        public async Task<IActionResult> Create(SeasonEditViewModel model)
        {
            //try
            //{
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
            //}
            //catch (DbUpdateException)
            //{
            //    //Log the error (uncomment ex variable name and write a log.
            //    ModelState.AddModelError("", "Unable to save changes. " +
            //                                 "Try again, and if the problem persists " +
            //                                 "see your system administrator.");
            //}
            return View(model);
        }

        [Route("manage/season/{id}")]
        public IActionResult Index(int id)
        {
            return RedirectToAction("Index", "SeasonLink", new { Area = "Manage" });
        }
    }
}