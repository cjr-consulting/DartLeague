using DartLeague.Repositories.LeagueData;
using DartLeague.Web.Areas.Manage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DartLeague.Web.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SponsorController : Controller
    {
        private readonly LeagueContext _leagueContext;

        private readonly List<SelectListItem> _sponsorTypes = new List<SelectListItem>
        {
            new SelectListItem {Text = "League Sponsors and Partners", Value = "L"},
            new SelectListItem {Text = "Player Companies", Value = "P"},
            new SelectListItem {Text = "Charity Partners", Value = "C"},
            new SelectListItem {Text = "Team Sponsors", Value = "T"}
        };

        public SponsorController(LeagueContext leagueContext)
        {
            _leagueContext = leagueContext;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["LeagueNavPage"] = "Sponsors";
            var sponsorsList = new SponsorListViewModel
            {
                Sponsors = await _leagueContext.Sponsors.Select(x =>
                        new SponsorViewModel
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Type = x.Type,
                            ContactName = x.ContactName
                        })
                    .ToListAsync()
            };
            return View(sponsorsList);
        }

        public IActionResult Create()
        {
            ViewBag.SponsorTypes = _sponsorTypes;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SponsorViewModel sponsor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newSponsor = new Sponsor
                    {
                        Name = sponsor.Name,
                        ContactName = sponsor.ContactName,
                        Type = sponsor.Type,
                        Phone = sponsor.Phone,
                        Address1 = sponsor.Address1,
                        Address2 = sponsor.Address2,
                        City = sponsor.City,
                        State = sponsor.State,
                        Zip = sponsor.Zip,
                        Url = sponsor.Url,
                        FacebookUrl = sponsor.FacebookUrl,
                        Email = sponsor.Email,
                        MapUrl = sponsor.MapUrl,
                        Description = sponsor.Description,
                        Comments = sponsor.Comments
                    };
                    _leagueContext.Sponsors.Add(newSponsor);
                    await _leagueContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }

            catch (DbUpdateException)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                                             "Try again, and if the problem persists " +
                                             "see your system administrator.");
            }

            ViewBag.SponsorTypes = _sponsorTypes;
            return View(sponsor);
        }

        [Route("manage/sponsor/{id}/edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.SponsorTypes = _sponsorTypes;
            var s = await _leagueContext.Sponsors.FirstOrDefaultAsync(x => x.Id == id);
            if (s == null) return NotFound();
            var sponsor = new SponsorViewModel
            {
                Id = s.Id,
                Name = s.Name,
                ContactName = s.ContactName,
                Type = s.Type,
                Phone = s.Phone,
                Address1 = s.Address1,
                Address2 = s.Address2,
                City = s.City,
                State = s.State,
                Zip = s.Zip,
                Url = s.Url,
                FacebookUrl = s.FacebookUrl,
                Email = s.Email,
                MapUrl = s.MapUrl,
                Description = s.Description,
                Comments = s.Comments
            };
            return View(sponsor);
        }

        [HttpPost("manage/sponsor/{id}/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, SponsorViewModel sponsor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var s = await _leagueContext.Sponsors.FirstOrDefaultAsync(x => x.Id == id);
                    s.Name = sponsor.Name;
                    s.ContactName = sponsor.ContactName;
                    s.Type = sponsor.Type;
                    s.Phone = sponsor.Phone;
                    s.Address1 = sponsor.Address1;
                    s.Address2 = sponsor.Address2;
                    s.City = sponsor.City;
                    s.State = sponsor.State;
                    s.Zip = sponsor.Zip;
                    s.Url = sponsor.Url;
                    s.FacebookUrl = sponsor.FacebookUrl;
                    s.Email = sponsor.Email;
                    s.MapUrl = sponsor.MapUrl;
                    s.Description = sponsor.Description;
                    s.Comments = sponsor.Comments;

                    await _leagueContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateException)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                                             "Try again, and if the problem persists " +
                                             "see your system administrator.");
            }

            ViewBag.SponsorTypes = _sponsorTypes;
            return View(sponsor);
        }

        [Route("manage/sponsor/{id}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var p = await _leagueContext.Sponsors.FirstOrDefaultAsync(x => x.Id == id);
            if (p == null)
                return RedirectToAction("Index");

            _leagueContext.Sponsors.Remove(p);
            _leagueContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}