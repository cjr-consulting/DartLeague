using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DartLeague.Web.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SeasonMessageController : Controller
    {
        [Route("manage/season/{id}/message")]
        public IActionResult Index()
        {
            ViewData["SeasonName"] = "Season 2016-2017";
            ViewData["SeasonNavPage"] = "Messages";
            return View();
        }
    }
}