using DartLeague.Domain.BrowsableFiles;
using DartLeague.Repositories.SeasonData;
using DartLeague.Web.Areas.Manage.Models;
using DartLeague.Web.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DartLeague.Web.Areas.Manage.Controllers
{
    [Area("Manage")]
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

        [Route("manage/season/{id}/link")]
        public async Task<IActionResult> Index(int id)
        {
            ViewData["SeasonNavPage"] = "Links";
            var season = await _seasonContext.Seasons
                .Select(x =>
                    new SeasonEditViewModel
                    {
                        Id = x.Id,
                        Title = x.Title,
                        StartDate = x.StartDate,
                        EndDate = x.EndDate
                    })
                .FirstOrDefaultAsync(x => x.Id == id);

            return View(new SeasonManagementRootViewModel<List<SeasonLinkListViewModel>>
            {
                SeasonEdit = season,
                Data = new List<SeasonLinkListViewModel>()
            });
        }

        private async Task<List<SeasonLinkListViewModel>> GetLinks()
        {
            var list = new List<SeasonLinkListViewModel>();
            foreach (var link in await _seasonContext.SeasonLinks.OrderBy(x => x.Order).ToListAsync())
            {
                var linkView = new SeasonLinkListViewModel
                {
                    Id = link.Id,
                    Title = link.Title,
                    LinkType = link.LinkType == 1 ? "Url" : "File",
                    Url = link.Url,
                    Order = link.Order
                };
                list.Add(linkView);
            }

            return list;
        }
        
        [Route("manage/season/{id}/link/create")]
        public async Task<IActionResult> Create()
        {
            var maxOrder = await _seasonContext.SeasonLinks.MaxAsync(x => x.Order);
            var model = new SeasonLinkViewModel();
            model.Order = maxOrder + 1;
            return View(model);
        }

        [HttpPost("manage/season/{id}/link/create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SeasonLinkViewModel model, List<IFormFile> linkFile)
        {
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
                            Category = SEASON_LINK_CATEGORY,
                            Stream = file.OpenReadStream()
                        };
                        linkFileId = await _browsableFileService.AddAsync(f);
                        url = Url.Action("Index", "File", new { Category = "League", FileName = f.FileName });
                    }

                    var l = new SeasonLink
                    {
                        Title = model.Title,
                        LinkType = model.LinkType,
                        Url = url,
                        FileId = linkFileId,
                        Order = model.Order,
                        CreatedAt = DateTime.UtcNow
                    };

                    _seasonContext.SeasonLinks.Add(l);
                    await _seasonContext.SaveChangesAsync();

                    return RedirectToAction("Index", "Home");
                }
            }
            catch (DbUpdateException)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                                             "Try again, and if the problem persists " +
                                             "see your system administrator.");
            }

            return View(model);
        }

        [Route("manage/season/{id}/link/{linkId}/edit")]
        public async Task<IActionResult> Edit(int id, int linkId)
        {
            var leagueLink = await _seasonContext.SeasonLinks.FirstOrDefaultAsync(x => x.Id == linkId);
            if (leagueLink == null)
                return NotFound();

            string fileLink = "";
            if (leagueLink.LinkType == 2)
            {
                fileLink = leagueLink.Url;
            }

            var model = new SeasonLinkViewModel
            {
                Id = id,
                Title = leagueLink.Title,
                LinkType = leagueLink.LinkType,
                Url = leagueLink.Url,
                Order = leagueLink.Order,
                FileLink = fileLink
            };

            return View(model);
        }

        [HttpPost("manage/season/{id}/link/{linkId}/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int linkId, SeasonLinkViewModel model, List<IFormFile> linkFile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var leagueLink = await _seasonContext.SeasonLinks.FirstOrDefaultAsync(x => x.Id == id);
                    if (leagueLink == null)
                        return NotFound();

                    string url = model.Url;
                    int linkFileId = leagueLink.FileId;
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
                            Category = SEASON_LINK_CATEGORY,
                            Stream = file.OpenReadStream()
                        };
                        linkFileId = await _browsableFileService.AddAsync(f);
                        url = Url.Action("Index", "File", new { Category = "League", FileName = f.FileName });
                    }

                    leagueLink.Title = model.Title;
                    leagueLink.LinkType = model.LinkType;
                    leagueLink.Url = url;
                    leagueLink.FileId = linkFileId;
                    leagueLink.Order = model.Order;
                    leagueLink.UpdatedAt = DateTime.UtcNow;

                    _seasonContext.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }

            }
            catch (DbUpdateException)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                                             "Try again, and if the problem persists " +
                                             "see your system administrator.");
            }

            return View(model);
        }

        [Route("manage/leaguelink/{id}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var leagueLink = await _seasonContext.SeasonLinks.FirstOrDefaultAsync(x => x.Id == id);
            if (leagueLink != null)
            {
                _seasonContext.SeasonLinks.Remove(leagueLink);
                await _browsableFileService.DeleteAsync(leagueLink.FileId);
                await _seasonContext.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}