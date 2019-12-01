using DartLeague.Repositories.LeagueData;
using DartLeague.Web.Helpers;
using DartLeague.Web.ViewComponents.Models.EventList;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DartLeague.Web.ViewComponents
{
    public class EventTitleViewComponent : ViewComponent
    {
        readonly LeagueContext _leagueContext;

        public EventTitleViewComponent(LeagueContext leagueContext)
        {
            _leagueContext = leagueContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var titleEvent = await GetTitleEvent();
            return View(titleEvent);
        }

        private async Task<EventViewModel> GetTitleEvent()
        {
            var titleEvent = await _leagueContext.DartEvents
                .Where(e => e.IsTitleEvent && e.EventDate >= DateTime.Now.Date)
                .FirstOrDefaultAsync();

            if (titleEvent == null)
                titleEvent = await _leagueContext.DartEvents
                    .Where(e => e.EventDate > DateTime.Now.Date)
                    .OrderBy(e => e.EventDate)
                    .FirstOrDefaultAsync();

            if (titleEvent == null)
                return null;

            return Map(titleEvent);
        }

        private static EventViewModel Map(DartEvent dartEvent)
        {
            return new EventViewModel
            {
                Id = dartEvent.Id,
                Name = dartEvent.Name,
                EventDate = dartEvent.EventDate,
                DartType = dartEvent.DartType,
                EventType = StaticLists.DartEventTypes.FirstOrDefault(x => x.Value == dartEvent.EventTypeId.ToString()).Text,
                RegistrationStartTime = dartEvent.RegistrationStartTime,
                RegistrationEndTime = dartEvent.RegistrationEndTime,
                DartStart = dartEvent.DartStart,
                ImageFileId = dartEvent.ImageFileId > 0 ? NumberObfuscation.Encode(dartEvent.ImageFileId) : string.Empty,
                PosterFileId = dartEvent.PosterFileId > 0 ? NumberObfuscation.Encode(dartEvent.PosterFileId) : string.Empty,
                HostName = dartEvent.HostName,
                HostUrl = dartEvent.HostUrl,
                LocationName = dartEvent.LocationName,
                MapUrl = dartEvent.MapUrl,
                Address1 = dartEvent.Address1,
                Address2 = dartEvent.Address2,
                City = dartEvent.City,
                State = dartEvent.State,
                Zip = dartEvent.Zip,
                Description = dartEvent.Description,
                FacebookUrl = dartEvent.FacebookUrl,
                Url = dartEvent.Url,
            };
        }
    }
}
