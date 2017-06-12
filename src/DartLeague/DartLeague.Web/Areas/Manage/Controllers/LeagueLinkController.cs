using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DartLeague.Web.Areas.Manage.Models;
using Microsoft.AspNetCore.Mvc;

namespace DartLeague.Web.Areas.Manage.Controllers
{
    [Area("manage")]
    public class LeagueLinkController : Controller
    {
        public IActionResult Create()
        {
            return View(new LeagueLinkViewModel());
        }

        [HttpPost]
        public IActionResult Create(LeagueLinkViewModel leagueLink)
        {
            return RedirectToAction("Index", "Home");
        }

        [Route("manage/leaguelink/{id}/edit")]
        public IActionResult Edit(int id)
        {
            return View(new LeagueLinkViewModel());
        }

        [HttpPost("manage/leaguelink/{id}/edit")]
        public IActionResult Edit(int id, LeagueLinkViewModel leagueLink)
        {
            return View();
        }
    }
}