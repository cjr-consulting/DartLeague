using DartLeague.Web.Areas.Site.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using DartLeague.Repositories.LeagueData;

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
        [Route("EventResults/{id}")]
        public IActionResult Index(int id)
        {
            var dartEvent = _leagueContext.DartEvents.FirstOrDefault(x => x.Id == id);
            var model = new EventResultsListViewModel()
            {
                DartEventName = dartEvent.Name,
                DartEventDate = dartEvent.EventDate,
                IsTitleEvent = dartEvent.IsTitleEvent
            };
            //model.Results = _leagueContext.DartEventResults.Where(x=>x.EventId == id).Select(x => new DartEventResultViewModel
           
            model.Results.Add(new DartEventResultViewModel { Id = 1, MemberName = "test", Finished = 3, IsTitleEvent = false, SpecificEventName = "test Event name" });
            ViewBag.Members = new List<SelectListItem> {
                new SelectListItem { Text = "Larry", Value = "0" },
                new SelectListItem { Text = "Moe", Value = "1" },
                new SelectListItem { Text = "Curly", Value = "2" } };
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(EventResultsListViewModel resultData)
        {
            return View();
        }
    }
}