using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using DartLeague.Repositories.LeagueData;
using DartLeague.Repositories.SeasonData;
using DartLeague.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DartLeague.Web.Controllers
{
    public class HistoryController : Controller
    {
        private readonly LeagueContext _leagueContext;
        private readonly SeasonContext _seasonContext;

        public HistoryController(LeagueContext leagueContext, SeasonContext seasonContext)
        {
            _leagueContext = leagueContext;
            _seasonContext = seasonContext;
        }

        [Route("/history/{title}")]
        public async Task<IActionResult> Index(string title)
        {
            var season = await _seasonContext.Seasons
                .Include("SeasonLinks")
                .FirstOrDefaultAsync(x => x.Title == title);

            var model = new HistoryViewModel
            {
                Title = season.Title,
                Documents = season.SeasonLinks.OrderBy(x => x.Order).Select(x => new HistoryDocumentViewModel
                {
                    Title = x.Title,
                    Url = x.Url
                }).ToList()
            };
            return View(model);
        }
    }

    public class HistoryViewModel
    {
        public HistorySeasonViewModel PreviousSeason { get; set; }
        public HistorySeasonViewModel NextSeason { get; set; }

        public string Title { get; set; }
        public List<HistoryDocumentViewModel> Documents { get; set; } = new List<HistoryDocumentViewModel>();
    }

    public class HistorySeasonViewModel
    {
        public string Title { get; set; }
        public string Url { get; set; }
    }

    public class HistoryDocumentViewModel
    {
        public string Title { get; set; }
        public string Url { get; set; }
    }
}