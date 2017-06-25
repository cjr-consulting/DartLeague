using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DartLeague.Web.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SeasonController : Controller
    {
        [Route("manage/season/{id}")]
        public IActionResult Index(int id)
        {
            ViewData["SeasonNavPage"] = "Links";
            return View();
        }

        [Route("manage/season/{id}/board")]
        public IActionResult Board(int id)
        {
            ViewData["SeasonNavPage"] = "Competitions";
            return View();
        }

        [Route("manage/season/{id}/links")]
        public IActionResult Links(int id)
        {
            ViewData["SeasonNavPage"] = "Links";
            return View();
        }

        [Route("manage/season/{id}/messages")]
        public IActionResult Messages(int id)
        {
            ViewData["SeasonNavPage"] = "Messages";
            return View();
        }

        [Route("manage/season/{id}/competitions")]
        public IActionResult Competitions(int id)
        {
            ViewData["SeasonNavPage"] = "Competitions";
            return View();
        }
    }
}