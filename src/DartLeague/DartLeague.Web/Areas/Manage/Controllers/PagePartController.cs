using System;
using DartLeague.Web.Areas.Manage.Models;
using Microsoft.AspNetCore.Mvc;

namespace DartLeague.Web.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class PagePartController : Controller
    {
        public IActionResult Index()
        {
            var model = new PagePartsListViewModel();
            return View(model);
        }

        public IActionResult Create()
        {
            throw new NotImplementedException();
        }

        public IActionResult Edit(int id)
        {
            throw new NotImplementedException();
        }

        public IActionResult Delete()
        {
            throw new NotImplementedException();
        }
    }
}