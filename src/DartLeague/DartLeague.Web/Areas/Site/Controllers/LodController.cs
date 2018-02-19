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
    public class LodController : Controller
    {
        private readonly IBrowsableFileService _browsableFileService;
        private readonly EF.LeagueContext _leagueContext;

        public LodController(EF.LeagueContext leagueContext, IBrowsableFileService browsableFileService)
        {
            _leagueContext = leagueContext;
            _browsableFileService = browsableFileService;
        }

        public IActionResult Index()
        {
            var model = new LodListViewModel();
            model.LuckOfTheDraws = _leagueContext.LuckOfTheDraws
                .Select(x => new LodViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    EventDate = x.Date,
                    FileId = x.FileId > 0 ? NumberObfuscation.Encode(x.FileId) : string.Empty,
                    Active = x.Active
                })
                .ToList();
            return View(model);
        }

        public IActionResult Create()
        {
            var model = new LodViewModel {EventDate = DateTime.Now.Date};
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LodViewModel lodEvent, List<IFormFile> lodImage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!_leagueContext.LuckOfTheDraws.Any(x => string.Equals(x.Name.Trim(), lodEvent.Name.Trim(),
                        StringComparison.CurrentCultureIgnoreCase)))
                    {
                        var lod = new EF.LuckOfTheDraw
                        {
                            Name = lodEvent.Name,
                            Date = lodEvent.EventDate,
                            Active = true,
                            CreatedAt = DateTime.UtcNow
                        };
                        if (lodImage.Any())
                        {
                            if (lod.FileId > 0)
                                await _browsableFileService.DeleteAsync(lod.FileId);
                            lod.FileId = await CreateBrowseableFile(lodEvent, lodImage[0]);
                        }
                        _leagueContext.LuckOfTheDraws.Add(lod);
                        await _leagueContext.SaveChangesAsync();
                        return Redirect("Index");
                    }
                    ModelState.AddModelError("",
                        "A LOD event already exists with this name.  Choose another name, or edit the existing LOD event.");
                }
            }
            catch (DbUpdateException)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                                             "Try again, and if the problem persists " +
                                             "see your system administrator.");
            }
            return View(lodEvent);
        }


        [Route("site/lod/{id}/edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            var lod = await _leagueContext.LuckOfTheDraws.FirstOrDefaultAsync(x => x.Id == id);
            var lodModel = new LodViewModel
            {
                Id = lod.Id,
                Name = lod.Name,
                EventDate = lod.Date,
                FileId = lod.FileId > 0 ? NumberObfuscation.Encode(lod.FileId) : string.Empty
            };
            return View(lodModel);
        }

        [HttpPost("site/lod/{id}/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, LodViewModel lodEvent, List<IFormFile> lodImage)
        {
            try
            {
                var lod = await _leagueContext.LuckOfTheDraws.FirstOrDefaultAsync(x => x.Id == id);
                lod.Name = lodEvent.Name;
                lod.Date = lodEvent.EventDate;
                lod.UpdatedAt = DateTime.UtcNow;
                if (lodImage.Any())
                {
                    if (lod.FileId > 0)
                        await _browsableFileService.DeleteAsync(lod.FileId);

                    lod.FileId = await CreateBrowseableFile(lodEvent, lodImage[0]);
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
            return View(lodEvent);
        }

        [Route("site/lod/{id}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var lod = await _leagueContext.LuckOfTheDraws.FirstOrDefaultAsync(x => x.Id == id);
                _leagueContext.Remove(lod);
                await _leagueContext.SaveChangesAsync();
                await _browsableFileService.DeleteAsync(lod.FileId);
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

        [Route("site/lod/{id}/activate")]
        public async Task<IActionResult> Activate(int id)
        {
            try
            {
                var lod = await _leagueContext.LuckOfTheDraws.FirstOrDefaultAsync(x => x.Id == id);
                lod.Active = lod.Active == false;
                lod.UpdatedAt = DateTime.UtcNow;
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

        private async Task<int> CreateBrowseableFile(LodViewModel lodEvent, IFormFile lodImage)
        {
            int imageFileId;
            var file = lodImage;
            imageFileId = await _browsableFileService.AddAsync(new BrowsableFile
            {
                FileName =
                    $"ImageFile-{FileHelper.CleanString(lodEvent.Name)}{Path.GetExtension(file.FileName)}",
                Extension = Path.GetExtension(file.FileName),
                ContentType = file.ContentType,
                Category = "LodImages",
                Stream = file.OpenReadStream()
            });

            return imageFileId;
        }
    }
}