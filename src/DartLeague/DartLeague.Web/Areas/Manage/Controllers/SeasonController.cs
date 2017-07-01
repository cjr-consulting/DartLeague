using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DartLeague.Web.Areas.Manage.Models;
using Microsoft.AspNetCore.Mvc;

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

        public  IActionResult Index()
        {
            ViewData["LeagueNavPage"] = "Seasons";
            return View(new List<SeasonListViewModel>()
            {
                new SeasonListViewModel
                {
                    Id = 3,
                    Name = "2016 - 2017",
                    StartDate = new DateTime(2016, 9, 1),
                    EndDate = new DateTime(2017, 8, 31),
                    State = SeasonStates.Created,
                },
                new SeasonListViewModel
                {
                    Id = 2,
                    Name = "2015 - 2016",
                    StartDate = new DateTime(2015, 9, 1),
                    EndDate = new DateTime(2016, 8, 31),
                    State = SeasonStates.Started,
                },
                new SeasonListViewModel
                {
                    Id = 1,
                    Name = "2014 - 2015",
                    StartDate = new DateTime(2014, 9, 1),
                    EndDate = new DateTime(2015, 8, 31),
                    State = SeasonStates.Ended,
                }
            });
        }

        [Route("manage/season/create")]
        public IActionResult Create()
        {
            return View();
        }

        [Route("manage/season/{id}")]
        public IActionResult Index(int id)
        {
            return RedirectToAction("Index", "SeasonLink", new { Area = "Manage" });
        }
    }
}