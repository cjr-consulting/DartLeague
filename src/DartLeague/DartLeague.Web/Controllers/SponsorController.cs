using System.Collections.Generic;
using DartLeague.Web.Models.SponsorListViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DartLeague.Web.Controllers
{
    public class SponsorController : Controller
    {
        private readonly List<SelectListItem> _sponsorTypes = new List<SelectListItem>
        {
            new SelectListItem {Text = "League Sponsors and Partners", Value = "L"},
            new SelectListItem {Text = "Player Companies", Value = "P"},
            new SelectListItem {Text = "Charity Partners", Value = "C"},
            new SelectListItem {Text = "Team Sponsors", Value = "T"}
        };

        public IActionResult Index(string type)
        {
            ViewData["SponsorTypes"] = _sponsorTypes;
            return View(new SponsorListViewModel
            {
                SelectedSponsorType = type ?? "L",
                Sponsors = {
                    new SponsorViewModel
                    {
                        Name = "Sponsor 1",
                        Url = "url",
                        MapUrl = "mapurl",
                        Address1 = "Address 1",
                        Address2 = "Address 2",
                        City = "City",
                        State = "ST",
                        Zip = "00000",
                        FacebookUrl = "facebook",
                        Phone = "(777) 777-7777",
                        Email = "someone@somewhere.com",
                        Description = "Descriptionsssss"
                    },

                    new SponsorViewModel
                    {
                        Name = "Sponsor 1",
                        Url = "url",
                        Address1 = "Address 1",
                        Address2 = "Address 2",
                        City = "City",
                        State = "ST",
                        Zip = "00000",
                        FacebookUrl = "facebook",
                        Phone = "(777) 777-7777",
                        Email = "someone@somewhere.com",
                    }
                }
            });
        }
    }
}