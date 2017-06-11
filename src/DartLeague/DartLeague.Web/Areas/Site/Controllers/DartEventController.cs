using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DartLeague.Domain.BrowsableFiles;
using EF = DartLeague.Repositories.LeagueData;
using DartLeague.Web.Areas.Site.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace DartLeague.Web.Areas.Site.Controllers
{
    [Area("Site")]
    public class DartEventController : Controller
    {
        private readonly EF.LeagueContext _leagueContext;

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

        private IBrowsableFileService _browsableFileService;

        public DartEventController(EF.LeagueContext leagueContext, IBrowsableFileService browsableFileService)
        {
            _leagueContext = leagueContext;
            _browsableFileService = browsableFileService;
        }

        public IActionResult Index()
        {
            var events = _leagueContext.DartEvents;
            var model = new DartEventsListViewModel
            {
                DartEvents = events.Select(x =>
                new DartEventListViewModel
                {
                    Id = x.Id,
                    IsTitleEvent = x.IsTitleEvent,
                    Name = x.Name,
                    EventDate = x.EventDate,
                    EventType = _dartEventTypes.First(y => y.Value == x.EventTypeId.ToString()).Text,
                    LocationName = x.LocationName
                }).ToList()
            };
            return View(model);
        }

        public IActionResult Create()
        {
            ViewBag.dartEventTypes = _dartEventTypes;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DartEventViewModel dartEvent, List<IFormFile> eventImage, List<IFormFile> posterDocument)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int imageFileId = 0;
                    int posterFileId = 0;

                    if (eventImage.Any())
                    {
                        var file = eventImage[0];
                        imageFileId = await _browsableFileService.Add(new BrowsableFile
                        {
                            FileName = $"ImageFile-{CleanName(dartEvent.Name)}{Path.GetExtension(file.FileName)}",
                            Extension = Path.GetExtension(file.FileName),
                            ContentType = file.ContentType,
                            Category = "DartEventImages",
                            Stream = file.OpenReadStream()
                        });
                    }
                    if (posterDocument.Any())
                    {
                        var file = posterDocument[0];
                        posterFileId = await _browsableFileService.Add(new BrowsableFile
                        {
                            FileName = $"PosterDoc-{CleanName(dartEvent.Name)}{Path.GetExtension(file.FileName)}",
                            Extension = Path.GetExtension(file.FileName),
                            ContentType = file.ContentType,
                            Category = "DartEventPosters",
                            Stream = file.OpenReadStream()
                        });
                    }
                    var de = new EF.DartEvent
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
                        ImageFileId= imageFileId,
                        PosterFileId = posterFileId
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

            ViewBag.dartEventTypes = _dartEventTypes;
            return View(dartEvent);
        }

        [Route("site/dartevent/{id}/edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            var e = await _leagueContext.DartEvents.FirstOrDefaultAsync(x => x.Id == id);
            var dartEvent = new DartEventViewModel
            {
                Address1 = e.Address1,
                Address2 = e.Address2,
                City = e.City,
                DartStart = e.DartStart,
                DartType = e.DartType,
                Description = e.Description,
                EventContact = e.EventContact,
                EventContact2 = e.EventContact2,
                EventDate = e.EventDate,
                EventEndDate = e.EventEndDate,
                EventTypeId = e.EventTypeId,
                FacebookUrl = e.FacebookUrl,
                HostName = e.HostName,
                HostPhone = e.HostPhone,
                HostUrl = e.HostUrl,
                LocationName = e.LocationName,
                MapUrl = e.MapUrl,
                Name = e.Name,
                RegistrationEndTime = e.RegistrationEndTime,
                RegistrationStartTime = e.RegistrationStartTime,
                State = e.State,
                Url = e.Url,
                Zip = e.Zip,
                ImageFileId = e.ImageFileId > 0 ? NumberObfuscation.Encode(e.ImageFileId) : string.Empty,
                PosterFileId = e.PosterFileId > 0 ? NumberObfuscation.Encode(e.PosterFileId) : string.Empty,
            };

            ViewBag.dartEventTypes = _dartEventTypes;
            return View(dartEvent);
        }

        [HttpPost("site/dartevent/{id}/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, DartEventViewModel dartEvent, List<IFormFile> eventImage, List<IFormFile> posterDocument)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var de = await _leagueContext.DartEvents.FirstOrDefaultAsync(x => x.Id == id);
                    de.Name = dartEvent.Name;
                    de.Address1 = dartEvent.Address1;
                    de.Address2 = dartEvent.Address2;
                    de.City = dartEvent.City;
                    de.DartStart = dartEvent.DartStart;
                    de.DartType = dartEvent.DartType;
                    de.Description = dartEvent.Description;
                    de.EventContact = dartEvent.EventContact;
                    de.EventContact2 = dartEvent.EventContact2;
                    de.EventDate = dartEvent.EventDate;
                    de.EventEndDate = dartEvent.EventEndDate;
                    de.EventTypeId = dartEvent.EventTypeId;
                    de.FacebookUrl = dartEvent.FacebookUrl;
                    de.HostName = dartEvent.HostName;
                    de.HostPhone = dartEvent.HostPhone;
                    de.HostUrl = dartEvent.HostUrl;
                    de.LocationName = dartEvent.LocationName;
                    de.MapUrl = dartEvent.MapUrl;
                    de.Name = dartEvent.Name;
                    de.RegistrationEndTime = dartEvent.RegistrationEndTime;
                    de.RegistrationStartTime = dartEvent.RegistrationStartTime;
                    de.State = dartEvent.State;
                    de.Url = dartEvent.Url;
                    de.Zip = dartEvent.Zip;

                    if (eventImage.Any())
                    {
                        var file = eventImage[0];
                        var imageFileId = await _browsableFileService.Add(new BrowsableFile
                        {
                            FileName = $"ImageFile-{CleanName(dartEvent.Name)}{Path.GetExtension(file.FileName)}",
                            Extension = Path.GetExtension(file.FileName),
                            ContentType = file.ContentType,
                            Category = "DartEventImages",
                            Stream = file.OpenReadStream()
                        });
                        de.ImageFileId = imageFileId;
                    }

                    if (posterDocument.Any())
                    {
                        var file = posterDocument[0];
                        var posterFileId = await _browsableFileService.Add(new BrowsableFile
                        {
                            FileName = $"PosterDoc-{CleanName(dartEvent.Name)}{Path.GetExtension(file.FileName)}",
                            Extension = Path.GetExtension(file.FileName),
                            ContentType = file.ContentType,
                            Category = "DartEventPosters",
                            Stream = file.OpenReadStream()
                        });
                        de.PosterFileId = posterFileId;
                    }


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

            ViewBag.dartEventTypes = _dartEventTypes;
            return View(dartEvent);
        }

        [Route("site/dartevent/{id}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var e = await _leagueContext.DartEvents.FirstOrDefaultAsync(x => x.Id == id);
            if (e == null)
                return RedirectToAction("Index");

            _leagueContext.DartEvents.Remove(e);
            _leagueContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [Route("site/dartevent/{id}/activate")]
        public async Task<IActionResult> Activate(int id)
        {
            var dartEvent = await _leagueContext.DartEvents.FirstOrDefaultAsync(x => x.Id == id);
            if(dartEvent == null)
                return RedirectToAction("Index");
            dartEvent.IsTitleEvent = true;

            foreach (var e in _leagueContext.DartEvents.Where(x => x.IsTitleEvent))
            {
                e.IsTitleEvent = false;
            }

            await _leagueContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private string CleanName(string name)
        {
            return string.Join("", name.Split(Path.GetInvalidFileNameChars(), StringSplitOptions.RemoveEmptyEntries));
        }
    }
}