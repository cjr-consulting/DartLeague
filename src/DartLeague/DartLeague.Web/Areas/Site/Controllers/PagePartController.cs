using System;
using System.Linq;
using System.Threading.Tasks;
using DartLeague.Repositories.LeagueData;
using DartLeague.Web.Areas.Site.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DartLeague.Web.Areas.Site.Controllers
{
    [Area("Site")]
    public class PagePartController : Controller
    {
        private readonly LeagueContext _leagueContext;

        public PagePartController(LeagueContext leagueContext)
        {
            _leagueContext = leagueContext;
        }

        public IActionResult Index()
        {
            var pagePartsList = new PagePartsListViewModel();
            pagePartsList.PageParts = _leagueContext.PageParts.Select(x => new PagePartViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Html = x.Html,
                UpdatedAt = x.UpdatedAt
            }).ToList();
            return View(pagePartsList);
        }

        public IActionResult Create()
        {
            var model = new PagePartViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PagePartViewModel pagePart)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newPagePart = new PagePart
                    {
                        Name = pagePart.Name,
                        Description = pagePart.Description,
                        Html = pagePart.Html,
                        UpdatedAt = DateTime.Now
                    };
                    _leagueContext.PageParts.Add(newPagePart);
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
            return View(pagePart);
        }

        [Route("site/pagepart/{id}/edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var pagePart = await _leagueContext.PageParts.FirstOrDefaultAsync(x => x.Id == id);
            if (pagePart == null)
                return NotFound();
            var pagePartModel = new PagePartViewModel
            {
                Id = id,
                Name = pagePart.Name,
                Description = pagePart.Description,
                Html = pagePart.Html,
                UpdatedAt = pagePart.UpdatedAt
            };
            return View(pagePartModel);
        }

        [HttpPost("site/pagepart/{id}/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PagePartViewModel pagePart)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingPagePart = await _leagueContext.PageParts.FirstOrDefaultAsync(x => x.Id == id);
                    existingPagePart.Name = pagePart.Name;
                    existingPagePart.Description = pagePart.Description;
                    existingPagePart.Html = pagePart.Html;
                    existingPagePart.UpdatedAt = DateTime.Now;
                };
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
            return View(pagePart);
        }
        [Route("site/pagepart/{id}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var pagePart = await _leagueContext.PageParts.FirstOrDefaultAsync(x => x.Id == id);
            if (pagePart == null) return RedirectToAction("Index");

            _leagueContext.PageParts.Remove(pagePart);
            _leagueContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}