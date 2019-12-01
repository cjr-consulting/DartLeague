using DartLeague.Domain.BrowsableFiles;
using DartLeague.Repositories.SeasonData;
using DartLeague.Web.Areas.Manage.Models;
using DartLeague.Web.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DartLeague.Web.Areas.Manage.Controllers
{
    [Area("Manage")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class SeasonLinkController : Controller
    {
        private const string SEASON_LINK_CATEGORY = "SeasonLinks";
        private readonly SeasonContext _seasonContext;
        private readonly IBrowsableFileService _browsableFileService;

        public SeasonLinkController(SeasonContext seasonContext, IBrowsableFileService browsableFileService)
        {
            _seasonContext = seasonContext;
            _browsableFileService = browsableFileService;
        }

        [Route("manage/season/{seasonId}/link")]
        public async Task<IActionResult> Index(int seasonId)
        {
            ViewData["SeasonNavPage"] = "Links";

            return View(new SeasonManagementRootViewModel<List<SeasonLinkListViewModel>>
            {
                SeasonEdit = await GetSeason(seasonId),
                Data = await GetLinks(seasonId)
            });
        }

        private async Task<SeasonEditViewModel> GetSeason(int seasonId)
        {
            return await _seasonContext.Seasons
                            .Select(x =>
                                new SeasonEditViewModel
                                {
                                    Id = x.Id,
                                    Title = x.Title,
                                    StartDate = x.StartDate,
                                    EndDate = x.EndDate
                                })
                            .FirstOrDefaultAsync(x => x.Id == seasonId);
        }

        private async Task<List<SeasonLinkListViewModel>> GetLinks(int seasonId)
        {
            return await _seasonContext.SeasonLinks
                .Where(x => x.SeasonId == seasonId)
                .OrderBy(x => x.Order)
                .Select(x => new SeasonLinkListViewModel
                    {
                        Id = x.Id,
                        Title = x.Title,
                        LinkType = x.LinkType == 1 ? "Url" : "File",
                        Url = x.Url,
                        Order = x.Order
                    })
                .ToListAsync();
        }

        [Route("manage/season/{seasonId}/link/create")]
        public async Task<IActionResult> Create(int seasonId)
        {
            var maxOrder = 0;
            if(await _seasonContext.SeasonLinks.AnyAsync(e=>e.SeasonId == seasonId))
                maxOrder = await _seasonContext.SeasonLinks
                    .Where(x => x.SeasonId == seasonId)
                    .MaxAsync(x => x.Order);

            var model = new SeasonManagementRootViewModel<SeasonLinkViewModel>
            {
                SeasonEdit = await GetSeason(seasonId),
                Data = new SeasonLinkViewModel
                {
                    Order = maxOrder + 1
                }
            };
            
            return View(model);
        }

        [HttpPost("manage/season/{seasonId}/link/create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int seasonId, [NotNull] SeasonLinkViewModel model, [NotNull] List<IFormFile> linkFile)
        {
            Contract.Requires(model != null, "SeasonLinkViewModel is null");
            Contract.Requires(linkFile != null, "linkFile is null");

            try
            {
                if (ModelState.IsValid)
                {
                    int linkFileId = 0;
                    string url = model.Url;
                    if (linkFile.Any() && model.LinkType == 2)
                    {
                        var file = linkFile[0];
                        var f = new BrowsableFile
                        {
                            FileName = $"{FileHelper.CleanString(model.Title)}{Path.GetExtension(file.FileName)}",
                            Extension = Path.GetExtension(file.FileName),
                            ContentType = file.ContentType,
                            Category = CategoryForSeason(seasonId),
                            Stream = file.OpenReadStream()
                        };
                        linkFileId = await _browsableFileService.AddAsync(f);
                        url = Url.Action("Index", "File", new { Category = CategoryForSeason(seasonId), f.FileName });
                    }

                    var l = new SeasonLink
                    {
                        SeasonId = seasonId,
                        Title = model.Title,
                        LinkType = model.LinkType,
                        Url = url,
                        FileId = linkFileId,
                        Order = model.Order,
                        CreatedAt = DateTime.UtcNow
                    };

                    _seasonContext.SeasonLinks.Add(l);
                    await _seasonContext.SaveChangesAsync();

                    return RedirectToAction("Index", "SeasonLink", new { seasonId });
                }
            }
            catch (DbUpdateException)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                                             "Try again, and if the problem persists " +
                                             "see your system administrator.");
            }

            return View(new SeasonManagementRootViewModel<SeasonLinkViewModel>
            {
                SeasonEdit = await GetSeason(seasonId),
                Data = model
            });
        }

        [Route("manage/season/{seasonId}/link/{id}/edit")]
        public async Task<IActionResult> Edit(int seasonId, int id)
        {
            var seasonLink = await _seasonContext.SeasonLinks.FirstOrDefaultAsync(x => x.Id == id);
            if (seasonLink == null)
                return NotFound();

            string fileLink = "";
            if (seasonLink.LinkType == 2)
            {
                fileLink = seasonLink.Url;
            }

            var model = new SeasonManagementRootViewModel<SeasonLinkViewModel>
            {
                SeasonEdit = await GetSeason(seasonId),
                Data = new SeasonLinkViewModel
                {
                    Id = id,
                    SeasonId = seasonId,
                    Title = seasonLink.Title,
                    LinkType = seasonLink.LinkType,
                    Url = seasonLink.Url,
                    Order = seasonLink.Order,
                    FileLink = fileLink
                }
            };

            return View(model);
        }

        [HttpPost("manage/season/{seasonId}/link/{id}/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int seasonId, int id, SeasonLinkViewModel model, List<IFormFile> linkFile)
        {
            Contract.Requires(model != null, nameof(model));
            Contract.Requires(linkFile != null, nameof(linkFile));

            try
            {
                if (ModelState.IsValid)
                {
                    var seasonLink = await _seasonContext.SeasonLinks.FirstOrDefaultAsync(x => x.Id == id);
                    if (seasonLink == null)
                        return NotFound();

                    string url = model.Url;
                    int linkFileId = seasonLink.FileId;
                    if (linkFile.Any() && model.LinkType == 2)
                    {
                        if (linkFileId > 0)
                            await _browsableFileService.DeleteAsync(linkFileId);

                        var file = linkFile[0];
                        var f = new BrowsableFile
                        {
                            FileName = $"{FileHelper.CleanString(model.Title)}{Path.GetExtension(file.FileName)}",
                            Extension = Path.GetExtension(file.FileName),
                            ContentType = file.ContentType,
                            Category = CategoryForSeason(seasonId),
                            Stream = file.OpenReadStream()
                        };
                        linkFileId = await _browsableFileService.AddAsync(f);
                        url = Url.Action("Index", "File", new { Category = CategoryForSeason(seasonId), f.FileName });
                    }

                    seasonLink.Title = model.Title;
                    seasonLink.LinkType = model.LinkType;
                    seasonLink.Url = url;
                    seasonLink.FileId = linkFileId;
                    seasonLink.Order = model.Order;
                    seasonLink.UpdatedAt = DateTime.UtcNow;

                    _seasonContext.SaveChanges();
                    return RedirectToAction("Index", "SeasonLink", new { seasonId });
                }

            }
            catch (DbUpdateException)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                                             "Try again, and if the problem persists " +
                                             "see your system administrator.");
            }
            
            return View(new SeasonManagementRootViewModel<SeasonLinkViewModel>
            {
                SeasonEdit = await GetSeason(seasonId),
                Data = model
            });
        }

        private static string CategoryForSeason(int seasonId)
        {
            return $"{SEASON_LINK_CATEGORY}-{seasonId}";
        }

        [Route("manage/season/{seasonId}/link/{id}/delete")]
        public async Task<IActionResult> Delete(int seasonId, int id)
        {
            var seasonLink = await _seasonContext.SeasonLinks.FirstOrDefaultAsync(x => x.SeasonId == seasonId && x.Id == id);
            if (seasonLink != null)
            {
                _seasonContext.SeasonLinks.Remove(seasonLink);
                await _browsableFileService.DeleteAsync(seasonLink.FileId);
                await _seasonContext.SaveChangesAsync();
            }

            return RedirectToAction("Index", "SeasonLink", new { seasonId });
        }
    }
}