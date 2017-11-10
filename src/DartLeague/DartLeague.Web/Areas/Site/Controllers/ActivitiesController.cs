using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DartLeague.Domain.BrowsableFiles;
using DartLeague.Web.Areas.Site.Models;
using DartLeague.Web.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EF = DartLeague.Repositories.LeagueData;

namespace DartLeague.Web.Areas.Site.Controllers
{
    [Area("Site")]
    public class ActivitiesController : Controller
    {
        private readonly IBrowsableFileService _browsableFileService;
        private readonly EF.LeagueContext _leagueContext;

        public ActivitiesController(EF.LeagueContext leagueContext, IBrowsableFileService browseableFileService)
        {
            _leagueContext = leagueContext;
            _browsableFileService = browseableFileService;
        }

        public IActionResult Index()
        {
            var model = new ActivitiesListViewModel();
            model.Activities = _leagueContext.Activities
                .Select(x => new ActivityViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ActivityDate = x.Date,
                    FileId = x.FileId > 0 ? NumberObfuscation.Encode(x.FileId) : string.Empty,
                    Active = x.Active
                }).ToList();
            return View(model);
        }

        public IActionResult Create()
        {
            var model = new ActivityViewModel() {ActivityDate = DateTime.Now};
            return View(model);
        }

        [Route("site/activities/{id}/edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            var activity = await _leagueContext.Activities.FirstOrDefaultAsync(x => x.Id == id);
            var activityModel = new ActivityViewModel()
            {
                Id = activity.Id,
                Name = activity.Name,
                ActivityDate = activity.Date,
                FileId = activity.FileId > 0 ? NumberObfuscation.Encode(activity.FileId) : string.Empty
            };
            return View(activityModel);
        }

        [HttpPost("site/activities/{id}/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, ActivityViewModel activityModel, List<IFormFile> activityImage)
        {
            try
            {
                var activity = await _leagueContext.Activities.FirstOrDefaultAsync(x => x.Id == id);
                activity.Name = activityModel.Name;
                activity.Date = activityModel.ActivityDate;
                activity.UpdatedAt = DateTime.UtcNow;
                if (activityImage.Any())
                {
                    if (activity.FileId > 0)
                        await _browsableFileService.DeleteAsync(activity.FileId);

                    activity.FileId = await CreateBrowseableFile(activityModel, activityImage[0]);
                }

                await _leagueContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            catch (DbUpdateException)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                                             "Try again, and if the problem persists " +
                                             "see your system administrator.");
            }
            return View(activityModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ActivityViewModel activityModel, List<IFormFile> activityImage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!_leagueContext.Activities.Any(x => string.Equals(x.Name.Trim(), activityModel.Name.Trim(),
                        StringComparison.CurrentCultureIgnoreCase)))
                    {
                        var activity = new EF.Activity()
                        {
                            Name = activityModel.Name,
                            Date = activityModel.ActivityDate,
                            Active = true,
                            CreatedAt = DateTime.UtcNow
                        };
                        if (activityImage.Any())
                        {
                            if (activity.FileId > 0)
                                await _browsableFileService.DeleteAsync(activity.FileId);
                            activity.FileId = await CreateBrowseableFile(activityModel, activityImage[0]);
                        }
                        _leagueContext.Activities.Add(activity);
                        await _leagueContext.SaveChangesAsync();
                        return Redirect("Index");
                    }
                    ModelState.AddModelError("",
                        "An activity already exists with this name.  Choose another name, or edit the existing LOD event.");
                }
            }
            catch (DbUpdateException)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                                             "Try again, and if the problem persists " +
                                             "see your system administrator.");
            }
            return View(activityModel);
        }

        [Route("site/activities/{id}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var activity = await _leagueContext.Activities.FirstOrDefaultAsync(x => x.Id == id);
                _leagueContext.Remove(activity);
                await _leagueContext.SaveChangesAsync();
                await _browsableFileService.DeleteAsync(activity.FileId);
            }
            catch (DbUpdateException)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                                             "Try again, and if the problem persists " +
                                             "see your system administrator.");
            }

            return RedirectToAction("Index");
        }

        [Route("site/activities/{id}/activate")]
        public async Task<IActionResult> Activate(int id)
        {
            try
            {
                var activity = await _leagueContext.Activities.FirstOrDefaultAsync(x => x.Id == id);
                activity.Active = activity.Active == false;
                activity.UpdatedAt = DateTime.UtcNow;
                await _leagueContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (DbUpdateException)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                                             "Try again, and if the problem persists " +
                                             "see your system administrator.");
            }
            return RedirectToAction("Index");
        }
        private async Task<int> CreateBrowseableFile(ActivityViewModel activity, IFormFile activityImage)
        {
            int imageFileId;
            var file = activityImage;
            imageFileId = await _browsableFileService.AddAsync(new BrowsableFile
            {
                FileName =
                    $"ImageFile-{FileHelper.CleanString(activity.Name)}{Path.GetExtension(file.FileName)}",
                Extension = Path.GetExtension(file.FileName),
                ContentType = file.ContentType,
                Category = "LodImages",
                Stream = file.OpenReadStream()
            });

            return imageFileId;
        }

    }
}