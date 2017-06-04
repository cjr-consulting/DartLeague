using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DartLeague.Web.ViewComponents.Models.EventList;

namespace DartLeague.Web.ViewComponents
{
    public class EventListViewComponent : ViewComponent
    {
        public EventListViewComponent()
        {

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new EventListViewModel
            {
                Events =
                {
                    new EventViewModel
                    {
                        Id = 1,
                        Name = "My Event",
                        EventDate = new DateTime(2017, 7, 1),
                        DartType = "Steel",
                        EventType = "Charity",
                        RegistrationStartTime = "7:00 PM",
                        RegistrationEndTime = "7:30 PM",
                        DartStart = "8:00 PM",
                        HostName = "Host Name",
                        HostUrl = "hosturl",
                        LocationName = "Location Name",
                        MapUrl = "mapUrl",
                        Address1 = "Address 1",
                        Address2 = "Address 2",
                        City = "City",
                        State = "ST",
                        Zip = "00000"
                    },
                    new EventViewModel
                    {
                        Id = 2,
                        Name = "My Event 2",
                        EventDate = new DateTime(2017, 7, 1),
                        DartType = "Steel",
                        EventType = "GTDL Event",
                        DartStart = "8:00 PM",
                        FacebookUrl = "facebook",
                        HostName = "Host Name",
                        LocationName = "Location Name",
                        MapUrl = "mapUrl",
                        Address1 = "Address 1",
                        Address2 = "Address 2",
                        City = "City",
                        State = "ST",
                        Zip = "00000"
                    },
                    new EventViewModel
                    {
                        Id = 3,
                        Name = "My Event 3",
                        EventDate = new DateTime(2017, 7, 1),
                        DartType = "Steel",
                        EventType = "Charity",
                        RegistrationStartTime = "7:00 PM",
                        RegistrationEndTime = "8:30 PM",
                        DartStart = "8:00 PM",
                        PosterFileId = 1,
                        HostName = "Host Name",
                        HostUrl = "hosturl",
                        LocationName = "Location Name",
                        MapUrl = "mapUrl",
                        Address1 = "Address 1",
                        Address2 = "Address 2",
                        City = "City",
                        State = "ST",
                        Zip = "00000",
                        Description = "My Sample Description"
                    }
                }
            };
            return View(model);
        }
    }
}
