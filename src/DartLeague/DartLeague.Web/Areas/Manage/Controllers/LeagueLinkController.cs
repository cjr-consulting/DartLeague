using DartLeague.Domain.BrowsableFiles;
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
using EF = DartLeague.Repositories.LeagueData;

namespace DartLeague.Web.Areas.Manage.Controllers
{
    [Area("Manage")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class LeagueLinkController : Controller
    {
        private readonly EF.LeagueContext _leagueContext;
        private readonly IBrowsableFileService _browsableFileService;

        public LeagueLinkController(EF.LeagueContext leagueContext, IBrowsableFileService browsableFileService)
        {
            _leagueContext = leagueContext;
            _browsableFileService = browsableFileService;
        }

        [Route("/manage/link")]
        public async Task<IActionResult> Index()
        {
            ViewData["LeagueNavPage"] = "Links";
            var links = await GetLeagueLinks();
            return View(links);
        }

        private async Task<List<LeagueLinksListViewModel>> GetLeagueLinks()
        {
            var list = new List<LeagueLinksListViewModel>();
            foreach (var link in await _leagueContext.LeagueLinks.OrderBy(x => x.Order).ToListAsync())
            {
                var linkView = new LeagueLinksListViewModel
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

        [Route("/manage/link/create")]
        public async Task<IActionResult> Create()
        {
            var maxOrder = 0;
            if(await _leagueContext.LeagueLinks.AnyAsync())
                maxOrder = await _leagueContext.LeagueLinks.MaxAsync(x => x.Order);

            var model = new LeagueLinkViewModel();
            model.Order = maxOrder + 1;
            return View(model);
        }

        [HttpPost("/manage/link/create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeagueLinkViewModel model, List<IFormFile> linkFile)
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
                            Category = "League",
                            Stream = file.OpenReadStream()
                        };
                        linkFileId = await _browsableFileService.AddAsync(f);
                        url = Url.Action("Index", "File", new { Category = "League", FileName = f.FileName });
                    }

                    var l = new EF.LeagueLink
                    {
                        Title = model.Title,
                        LinkType = model.LinkType,
                        Url = url,
                        FileId = linkFileId,
                        Order = model.Order,
                        CreatedAt = DateTime.UtcNow
                    };

                    _leagueContext.LeagueLinks.Add(l);
                    await _leagueContext.SaveChangesAsync();

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

        [Route("manage/link/{id}/edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var leagueLink = await _leagueContext.LeagueLinks.FirstOrDefaultAsync(x => x.Id == id);
            if (leagueLink == null)
                return NotFound();

            string fileLink = "";
            if (leagueLink.LinkType == 2)
            {
                fileLink = leagueLink.Url;
            }

            var model = new LeagueLinkViewModel
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

        [HttpPost("manage/link/{id}/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LeagueLinkViewModel model, List<IFormFile> linkFile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var leagueLink = await _leagueContext.LeagueLinks.FirstOrDefaultAsync(x => x.Id == id);
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
                            Category = "League",
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

                    _leagueContext.SaveChanges();
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

        [Route("manage/link/{id}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var leagueLink = await _leagueContext.LeagueLinks.FirstOrDefaultAsync(x => x.Id == id);
            if (leagueLink != null)
            {
                _leagueContext.LeagueLinks.Remove(leagueLink);
                await _browsableFileService.DeleteAsync(leagueLink.FileId);
                await _leagueContext.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}