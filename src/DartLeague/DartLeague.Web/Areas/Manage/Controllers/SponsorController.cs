using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DartLeague.Repositories.LeagueData;
using DartLeague.Web.Areas.Manage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DartLeague.Web.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SponsorController : Controller
    {
        private readonly LeagueContext _leagueContext;

        public SponsorController(LeagueContext leagueContext)
        {
            _leagueContext = leagueContext;
        }
        public IActionResult Index()
        {
            var sponsorsList = new SponsorListViewModel
            {
                Sponsors = _leagueContext.Sponsors.Select(x =>
                        new SponsorViewModel()
                        {
                            Name = x.Name,
                            Type = x.Type,
                            ContactName = x.ContactName,
                        })
                    .ToList()
            };
            return View(sponsorsList);
        }

        public IActionResult Create()
        {

            ViewBag.SponsorTypes = new List<SelectListItem>
            {
                new SelectListItem() {Text = "League Sponsors and Partners",Value = "L"},
                new SelectListItem() {Text = "Player Companies",Value= "P"},
                new SelectListItem() {Text = "Charity Partners", Value="C"},
                new SelectListItem() {Text = "Team Sponsors", Value="T"}
            };

            return View();
        }

        public IActionResult Edit()
        {
            ViewBag.SponsorTypes = new List<SelectListItem>
            {
                new SelectListItem() {Text = "League Sponsors and Partners",Value = "L"},
                new SelectListItem() {Text = "Player Companies",Value= "P"},
                new SelectListItem() {Text = "Charity Partners", Value="C"},
                new SelectListItem() {Text = "Team Sponsors", Value="T"}
            };

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SponsorViewModel sponsor)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id)
        {
            return View();
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var p = await _leagueContext.Sponsors.FirstOrDefaultAsync(x => x.Id == Id);
            if (p == null)
                return RedirectToAction("Index");

            _leagueContext.Sponsors.Remove(p);
            _leagueContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}