using System;
using System.Collections.Generic;
using System.Linq;
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
            var today = DateTime.Now.Date;
            var members = await _leagueContext.Members.ToListAsync();

            var season = await _seasonContext.Seasons
                .Include("SeasonLinks")
                .Include("BoardMembers.Position")
                .FirstOrDefaultAsync(x => x.Title == title);
            var nextSeason = await _seasonContext.Seasons
                .Where(x => x.StartDate > season.StartDate && x.EndDate < today)
                .OrderBy(x => x.StartDate)
                .FirstOrDefaultAsync();
            var previousSeason = await _seasonContext.Seasons
                .Where(x=>x.StartDate < season.StartDate && x.EndDate < today)
                .OrderByDescending(x=>x.StartDate)
                .FirstOrDefaultAsync();

            var model = new HistoryViewModel
            {
                Title = season.Title,
                PreviousSeason = previousSeason != null
                    ? new HistorySeasonViewModel
                    {
                        Title = previousSeason.Title,
                        Url = Url.Action("Index", "History", new {title = previousSeason.Title})
                    }
                    : null,
                NextSeason = nextSeason != null
                    ? new HistorySeasonViewModel
                    {
                        Title = nextSeason.Title,
                        Url = Url.Action("Index", "History", new {title = nextSeason.Title})
                    }
                    : null,
                Documents = season.SeasonLinks
                    .OrderBy(x => x.Order)
                    .Select(x =>
                        new HistoryDocumentViewModel
                        {
                            Title = x.Title,
                            Url = x.Url
                        }).ToList(),
                BoardMembers = season.BoardMembers
                    .OrderBy(x => x.Position.Order).Select(x =>
                        new HistoryBoardMemberViewModel
                        {
                            Name = members.First(m => m.Id == x.MemberId).GetFullName(),
                            Position = x.Position.Name
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
        public List<HistoryBoardMemberViewModel> BoardMembers { get; set; } = new List<HistoryBoardMemberViewModel>();
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

    public class HistoryBoardMemberViewModel
    {
        public string Name { get; set; }
        public string Position { get; set; }
    }
}