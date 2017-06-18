using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DartLeague.Domain.BrowsableFiles;
using DartLeague.Repositories.LeagueData;
using DartLeague.Web.Areas.Manage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DartLeague.Web.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class HomeController : Controller
    {
        private LeagueContext _leagueContext;
        private IBrowsableFileService _browsableFileService;

        public HomeController(LeagueContext leagueContext, IBrowsableFileService browsableFileService)
        {
            _leagueContext = leagueContext;
            _browsableFileService = browsableFileService;
        }

        public async Task<IActionResult> Index()
        {
            var links = await GetLeagueLinks();
            return View(new HomeManagementViewModel
            {
                LeagueLinks = links
            });
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
    }
}