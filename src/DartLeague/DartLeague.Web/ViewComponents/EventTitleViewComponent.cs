using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DartLeague.Repositories.LeagueData;
using DartLeague.Web.Helpers;
using DartLeague.Web.ViewComponents.Models.EventList;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DartLeague.Web.ViewComponents
{
    public class EventTitleViewComponent : ViewComponent
    {
        private LeagueContext _leagueContext;

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
                .Select(e => new EventViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    EventDate = e.EventDate,
                    DartType = e.DartType,
                    EventType = StaticLists.DartEventTypes.FirstOrDefault(x => x.Value == e.EventTypeId.ToString()).Text,
                    RegistrationStartTime = e.RegistrationStartTime,
                    RegistrationEndTime = e.RegistrationEndTime,
                    DartStart = e.DartStart,
                    ImageFileId = e.ImageFileId > 0 ? NumberObfuscation.Encode(e.ImageFileId) : string.Empty,
                    PosterFileId = e.PosterFileId > 0 ? NumberObfuscation.Encode(e.PosterFileId) : string.Empty,
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
                }).FirstOrDefaultAsync();

            if (titleEvent == null)
                titleEvent = await _leagueContext.DartEvents
                    .Where(e => e.EventDate > DateTime.Now.Date)
                    .OrderBy(e => e.EventDate)
                    .Select(e => new EventViewModel
                    {
                        Id = e.Id,
                        Name = e.Name,
                        EventDate = e.EventDate,
                        DartType = e.DartType,
                        EventType = StaticLists.DartEventTypes.FirstOrDefault(x => x.Value == e.EventTypeId.ToString()).Text,
                        RegistrationStartTime = e.RegistrationStartTime,
                        RegistrationEndTime = e.RegistrationEndTime,
                        DartStart = e.DartStart,
                        ImageFileId = e.ImageFileId > 0 ? NumberObfuscation.Encode(e.ImageFileId) : string.Empty,
                        PosterFileId = e.PosterFileId > 0 ? NumberObfuscation.Encode(e.PosterFileId) : string.Empty,
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
                    }).FirstOrDefaultAsync();

            return titleEvent;
        }
    }
}
