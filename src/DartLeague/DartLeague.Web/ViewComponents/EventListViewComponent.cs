using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DartLeague.Web.ViewComponents.Models.EventList;
using DartLeague.Repositories.LeagueData;
using Microsoft.EntityFrameworkCore;

namespace DartLeague.Web.ViewComponents
{
    public class EventListViewComponent : ViewComponent
    {
        private LeagueContext _leagueContext;

        public EventListViewComponent(LeagueContext leagueContext)
        {
            _leagueContext = leagueContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var events = await GetEvents();
            var model = new EventListViewModel
            {
                Events = events
            };
            return View(model);
        }

        private async Task<List<EventViewModel>> GetEvents()
        {
            return await _leagueContext.DartEvents.Select(e => new EventViewModel
            {
                Id = e.Id,
                Name = e.Name,
                EventDate = e.EventDate,
                DartType = e.DartType,
                EventType = e.EventTypeId.ToString(),
                RegistrationStartTime = e.RegistrationStartTime,
                RegistrationEndTime = e.RegistrationEndTime,
                DartStart = e.DartStart,
                PosterFileId = e.PosterFileId,
                HostName = e.HostName,
                HostUrl = e.HostUrl,
                LocationName = e.LocationName,
                MapUrl = e.MapUrl,
                Address1 = e.Address1,
                Address2 = e.Address2,
                City = e.City,
                State = e.State,
                Zip = e.Zip,
                Description = e.Description,
                FacebookUrl = e.FacebookUrl,
                Url = e.Url,
            }).ToListAsync();
        }
    }
}