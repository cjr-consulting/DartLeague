using System;
using System.Collections.Generic;
using System.Linq;
using EF = DartLeague.Repositories.LeagueData;
using System.Threading.Tasks;
using DartLeague.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DartLeague.Web.Controllers
{
    public class DartEventsController : Controller
    {
        private readonly EF.LeagueContext _leagueContext;

        public DartEventsController(EF.LeagueContext leagueContext)
        {
            _leagueContext = leagueContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LeaguePlayerEventActivity()
        {
            var model = new PlayersActivitiesListViewModel();
            foreach (var dartEvent in await _leagueContext.DartEvents.ToListAsync())
            {
                var eventResults = await _leagueContext.DartEventResults.Where(x => x.EventId == dartEvent.Id).ToListAsync();
                foreach (var eventResult in eventResults)
                {
                    var member = await _leagueContext.Members.Where(x => x.Id == eventResult.MemberId).FirstOrDefaultAsync();
                    var memberName = member == null ? "" : $"{member.FirstName} {member.LastName}";
                    model.PlayerActivityResults.Add(new PlayerActivityModel
                    {
                        DartEventName = dartEvent.Name,
                        Id = eventResult.Id,
                        SpecificEventName = eventResult.SpecificEventName,
                        MemberName = memberName,
                        Finished = eventResult.Finished
                    });
                }
            }
            return View(model);
        }
    }
}