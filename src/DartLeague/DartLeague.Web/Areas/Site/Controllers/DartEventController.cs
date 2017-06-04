using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DartLeague.Repositories.LeagueData;
using DartLeague.Web.Areas.Site.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DartLeague.Web.Areas.Site.Controllers
{
    [Area("Site")]
    public class DartEventController : Controller
    {
        private readonly LeagueContext _leagueContext;

        private readonly List<SelectListItem> _dartEventTypes = new List<SelectListItem>
        {
            new SelectListItem {Text = "GTDL Event", Value = "1"},
            new SelectListItem {Text = "Charity Dart Event", Value = "2"},
            new SelectListItem {Text = "Regional Event", Value = "3"},
            new SelectListItem {Text = "GTDL Sponsored Event", Value = "4"},
            new SelectListItem {Text = "GTDL All Stars", Value = "5"},
            new SelectListItem {Text = "GTDL Player Event", Value = "6"},
            new SelectListItem {Text = "Charity Event", Value = "7"},
            new SelectListItem {Text = "DPNY Series", Value = "8"},
            new SelectListItem {Text = "CDC Series", Value = "9"},
            new SelectListItem {Text = "DPNJ Series", Value = "10"},
            new SelectListItem {Text = "Qualifier", Value = "11"}

        };

        public DartEventController(LeagueContext leagueContext)
        {
            _leagueContext = leagueContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            ViewBag.dartEventTypes = _dartEventTypes;
            return View();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DartEventViewModel dartEvent)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var de = new DartEvent
                    {
                        Address1 = dartEvent.Address1,
                        Address2 = dartEvent.Address2,
                        City = dartEvent.City,
                        DartStart = dartEvent.DartStart,
                        DartType = dartEvent.DartType,
                        Description = dartEvent.Description,
                        EventContact = dartEvent.EventContact,
                        EventContact2 = dartEvent.EventContact2,
                        EventDate = dartEvent.EventDate,
                        EventEndDate = dartEvent.EventEndDate,
                        EventTypeId = dartEvent.EventTypeId,
                        FacebookUrl = dartEvent.FacebookUrl,
                        HostName = dartEvent.HostName,
                        HostPhone = dartEvent.HostPhone,
                        HostUrl = dartEvent.HostUrl,
                        LocationName = dartEvent.LocationName,
                        MapUrl = dartEvent.MapUrl,
                        Name = dartEvent.Name,
                        RegistrationEndTime = dartEvent.RegistrationEndTime,
                        RegistrationStartTime = dartEvent.RegistrationStartTime,
                        State = dartEvent.State,
                        Url = dartEvent.Url,
                        Zip = dartEvent.Zip,
                    };
                    _leagueContext.DartEvents.Add(de);
                    await _leagueContext.SaveChangesAsync();
                    return Redirect("Index");
                }
            }
            catch (DbUpdateException)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                                             "Try again, and if the problem persists " +
                                             "see your system administrator.");
            }

            return View(dartEvent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, DartEventViewModel dartEvent)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var de = await _leagueContext.DartEvents.FirstOrDefaultAsync(x => x.Id == id);
                    de.Name = dartEvent.Name;

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

            return View(dartEvent);
        }
        public async Task<IActionResult> Delete(int Id)
        {
            var p = await _leagueContext.Players.FirstOrDefaultAsync(x => x.Id == Id);
            if (p == null)
                return RedirectToAction("Index");

            _leagueContext.Players.Remove(p);
            _leagueContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}