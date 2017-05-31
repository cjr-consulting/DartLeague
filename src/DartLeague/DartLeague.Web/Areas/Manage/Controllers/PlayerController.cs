using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DartLeague.Web.Areas.Manage.Models;
using Microsoft.AspNetCore.Mvc;

namespace DartLeague.Web.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class PlayerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            var model = new Player();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(Player player)
        {
            return Redirect("Index");
        }
    }
}