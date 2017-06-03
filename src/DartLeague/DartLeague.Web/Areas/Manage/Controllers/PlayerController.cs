using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DartLeague.Web.Areas.Manage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

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

        [Route("Manage/Player/Edit/{id}")]
        public IActionResult Edit(string id)
        {
            return View(new Player(){FirstName = $"My ID is{id}"});
        }

        public IActionResult List()
        {
            return View(new PlayersList
            {
                Players = new List<Player> {
                new Player {FirstName = "Larry", ID = "1"},
                new Player {FirstName = "Moe", ID ="2"},
                new Player {FirstName = "Curly", ID = "3"}
                }
            });
        }

        [HttpPost]
        public IActionResult Create(Player player)
        {
            return Redirect("Index");
        }

        [HttpPost]
        public IActionResult Edit(Player player)
        {
            return Redirect("Index");
        }

        public IActionResult Delete()
        {
            return Redirect("List");
        }

        
    }
}