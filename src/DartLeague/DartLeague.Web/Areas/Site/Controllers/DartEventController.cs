using DartLeague.Domain.BrowsableFiles;
using DartLeague.Web.Areas.Site.Models;
using DartLeague.Web.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EF = DartLeague.Repositories.LeagueData;

namespace DartLeague.Web.Areas.Site.Controllers
{
    [Area("Site")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class DartEventController : Controller
    {
        private readonly EF.LeagueContext _leagueContext;
        private readonly IBrowsableFileService _browsableFileService;
        private readonly ILogger<DartEventController> _logger;

        public DartEventController(
            EF.LeagueContext leagueContext, 
            IBrowsableFileService browsableFileService,
            ILogger<DartEventController> logger)
        {
            _logger = logger;
            _leagueContext = leagueContext;
            _browsableFileService = browsableFileService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new DartEventsListViewModel
            {
                DartEvents = await GetDartEvents()
            };

            return View(model);
        }

        private async Task<List<DartEventListViewModel>> GetDartEvents()
        {
            var events = await _leagueContext.DartEvents.OrderByDescending(x => x.EventDate)
                                .ToListAsync();

            return events.Select(x =>
                                new DartEventListViewModel
                                {
                                    Id = x.Id,
                                    IsTitleEvent = x.IsTitleEvent,
                                    Name = x.Name,
                                    EventDate = x.EventDate,
                                    EventType = StaticLists.DartEventTypes.First(y => y.Value == x.EventTypeId.ToString()).Text,
                                    LocationName = x.LocationName
                                }).ToList();
        }

        public IActionResult Create()
        {
            ViewBag.dartEventTypes = StaticLists.DartEventTypes;
            return View(new DartEventViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DartEventViewModel dartEvent, List<IFormFile> eventImage, List<IFormFile> posterDocument)
        {
            Contract.Requires(dartEvent != null);
            Contract.Requires(eventImage != null);
            Contract.Requires(posterDocument != null);

            try
            {
                if (ModelState.IsValid)
                {
                    int imageFileId = 0;
                    int posterFileId = 0;

                    if (eventImage.Any())
                    {
                        var file = eventImage[0];
                        imageFileId = await _browsableFileService.AddAsync(new BrowsableFile
                        {
                            FileName = $"ImageFile-{FileHelper.CleanString(dartEvent.Name)}{Path.GetExtension(file.FileName)}",
                            Extension = Path.GetExtension(file.FileName),
                            ContentType = file.ContentType,
                            Category = "DartEventImages",
                            Stream = file.OpenReadStream()
                        });
                    }

                    if (posterDocument.Any())
                    {
                        var file = posterDocument[0];
                        posterFileId = await _browsableFileService.AddAsync(new BrowsableFile
                        {
                            FileName = $"PosterDoc-{FileHelper.CleanString(dartEvent.Name)}{Path.GetExtension(file.FileName)}",
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
                        ImageFileId = imageFileId,
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

            ViewBag.dartEventTypes = StaticLists.DartEventTypes;
            return View(dartEvent);
        }

        [Route("site/dartEvent/{id}/edit")]
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

            ViewBag.dartEventTypes = StaticLists.DartEventTypes;
            return View(dartEvent);
        }

        [HttpPost("site/dartEvent/{id}/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int? id, 
            DartEventViewModel dartEvent, 
            List<IFormFile> eventImage, 
            List<IFormFile> posterDocument)
        {
            Contract.Requires(dartEvent != null);
            Contract.Requires(eventImage != null);
            Contract.Requires(posterDocument != null);

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
                        var imageFileId = await _browsableFileService.AddAsync(new BrowsableFile
                        {
                            FileName = $"ImageFile-{FileHelper.CleanString(dartEvent.Name)}{Path.GetExtension(file.FileName)}",
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
                        var posterFileId = await _browsableFileService.AddAsync(new BrowsableFile
                        {
                            FileName = $"PosterDoc-{FileHelper.CleanString(dartEvent.Name)}{Path.GetExtension(file.FileName)}",
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

            ViewBag.dartEventTypes = StaticLists.DartEventTypes;
            return View(dartEvent);
        }

        [Route("site/dartEvent/{id}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var e = await _leagueContext.DartEvents.FirstOrDefaultAsync(x => x.Id == id);
            if (e == null)
                return RedirectToAction("Index");

            _leagueContext.DartEvents.Remove(e);
            _leagueContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [Route("site/dartEvent/{id}/activate")]
        public async Task<IActionResult> Activate(int id)
        {
            var dartEvent = await _leagueContext.DartEvents.FirstOrDefaultAsync(x => x.Id == id);
            if (dartEvent == null)
                return RedirectToAction("Index");
            dartEvent.IsTitleEvent = true;

            foreach (var e in _leagueContext.DartEvents.Where(x => x.IsTitleEvent))
            {
                e.IsTitleEvent = false;
            }

            await _leagueContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Route("Site/dartEvent/{id}/Results")]
        public IActionResult Result(int id)
        {
            var dartEvent = _leagueContext.DartEvents.FirstOrDefault(x => x.Id == id);
            if (dartEvent == null) return View(new EventResultsListViewModel());
            var model = new EventResultsListViewModel
            {
                DartEventName = dartEvent.Name,
                DartEventDate = dartEvent.EventDate,
                IsTitleEvent = dartEvent.IsTitleEvent,
                DartEventId = dartEvent.Id
            };

            var results =
                from result in _leagueContext.DartEventResults
                where result.EventId == id
                select new DartEventResultViewModel
                {
                    Id = result.Id,
                    SpecificEventName = result.SpecificEventName,
                    MemberName = result.Member != null
                        ? $"{result.Member.FirstName} {result.Member.LastName}"
                        : string.Empty,
                    Finished = result.Finished
                };

            model.Results.AddRange(results);

            var membersList = from member in _leagueContext.Members
                              select new SelectListItem
                              {
                                  Text = $"{member.FirstName} {member.LastName}",
                                  Value = member.Id.ToString()
                              };

            ViewBag.Members = membersList.ToList();
            return View(model);
        }

        [HttpPost("Site/dartEvent/{id}/Results")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Result(int id, EventResultsListViewModel resultData)
        {
            Contract.Requires(resultData != null);

            try
            {
                if (ModelState.IsValid)
                {

                    var r = new EF.DartEventResult
                    {
                        EventId = id,
                        MemberId = resultData.MemberId,
                        SpecificEventName = resultData.SpecificEventName,
                        Finished = resultData.Finished,
                        OrderId = resultData.OrderId
                    };
                    _leagueContext.DartEventResults.Add(r);
                    await _leagueContext.SaveChangesAsync();
                    return RedirectToAction("Result", "DartEvent", new { id });
                }

                var membersList = from member in _leagueContext.Members
                                  select new SelectListItem
                                  {
                                      Text = $"{member.FirstName} {member.LastName}",
                                      Value = member.Id.ToString()
                                  };

                ViewBag.Members = membersList.ToList();
            }
            catch(DbUpdateException ex)
            {
                _logger.LogWarning(ex, "Failed posting DartEventResult");
                ModelState.AddModelError("", "Unable to save changes. " +
                                             "Try again, and if the problem persists " +
                                             "see your system administrato.");
            }

            return View(resultData);
        }

        [Route("site/dartEvent/{eventId}/results/{id}/delete")]
        public async Task<IActionResult> DeleteResult(int eventId, int id)
        {
            var r = await _leagueContext.DartEventResults.FirstOrDefaultAsync(x => x.Id == id);
            if (r == null) return RedirectToAction("Result", "DartEvent", eventId);
            _leagueContext.DartEventResults.Remove(r);
            await _leagueContext.SaveChangesAsync();
            return RedirectToAction("Result", "DartEvent", new { id = eventId });
        }
    }
}