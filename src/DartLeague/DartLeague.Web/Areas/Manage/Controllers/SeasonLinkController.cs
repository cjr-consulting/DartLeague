using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DartLeague.Web.Areas.Manage.Models;
using Microsoft.AspNetCore.Mvc;

namespace DartLeague.Web.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SeasonLinkController : Controller
    {
        [Route("manage/season/{id}/link")]
        public IActionResult Index(int id)
        {
            ViewData["SeasonName"] = "Season 2016-2017";
            ViewData["SeasonNavPage"] = "Links";
            return View(new List<SeasonLinkViewModel>());
        }
    }
}