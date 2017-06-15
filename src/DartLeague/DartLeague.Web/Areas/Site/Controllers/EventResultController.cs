using DartLeague.Web.Areas.Site.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DartLeague.Repositories.LeagueData;
using Newtonsoft.Json;

namespace DartLeague.Web.Areas.Site.Controllers
{
    [Area("Site")]
    public class EventResultController : Controller
    {
        private readonly LeagueContext _leagueContext;

        public EventResultController(LeagueContext leagueContext)
        {
            _leagueContext = leagueContext;
        }

        [Route("Site/EventResults/{id}")]
        public IActionResult Index(int id)
        {
            var dartEvent = _leagueContext.DartEvents.FirstOrDefault(x => x.Id == id);
            if (dartEvent == null) return View(new EventResultsListViewModel());
            var model = new EventResultsListViewModel
            {
                DartEventName = dartEvent.Name,
                DartEventDate = dartEvent.EventDate,
                IsTitleEvent = dartEvent.IsTitleEvent
            };
            var results =
                from result in _leagueContext.DartEventResults
                join member in _leagueContext.Members on result.MemberId equals member.Id
                where result.EventId == id
                select new DartEventResultViewModel
                {
                    Id = result.Id,
                    SpecificEventName = result.SpecificEventName,
                    MemberName = $"{member.FirstName} {member.LastName}"

                };
            model.Results = results.ToList();

            var membersList = from member in _leagueContext.Members
                select new SelectListItem
                {
                    Text = $"{member.FirstName} {member.LastName}",
                    Value = member.Id.ToString()
                };

            ViewBag.Members = membersList.ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(EventResultsListViewModel resultData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var r = new DartEventResult
                    {
                        EventId = resultData.DartEventId,
                        MemberId = resultData.MemberId,
                        SpecificEventName = resultData.SpecificEventName,
                        Finished = resultData.Finished,
                        OrderId = resultData.OrderId
                    };
                    _leagueContext.DartEventResults.Add(r);
                    await _leagueContext.SaveChangesAsync();
                }
            }
            catch
            {

                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                                             "Try again, and if the problem persists " +
                                             "see your system administrato.");
            }
            return View(resultData);
        }
    }
}