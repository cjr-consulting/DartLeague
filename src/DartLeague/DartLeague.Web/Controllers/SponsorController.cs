using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DartLeague.Repositories.LeagueData;
using DartLeague.Web.Models.SponsorListViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DartLeague.Web.Controllers
{
    public class SponsorController : Controller
    {
        private readonly LeagueContext _leagueContext;

        private readonly List<SelectListItem> _sponsorTypes = new List<SelectListItem>
        {
            new SelectListItem {Text = "League Sponsors and Partners", Value = "L"},
            new SelectListItem {Text = "Player Companies", Value = "P"},
            new SelectListItem {Text = "Charity Partners", Value = "C"},
            new SelectListItem {Text = "Team Sponsors", Value = "T"}
        };

        public SponsorController(LeagueContext leagueContext)
        {
            _leagueContext = leagueContext;
        }

        public async Task<IActionResult> Index(string type)
        {
            ViewData["SponsorTypes"] = _sponsorTypes;
            var model = new SponsorListViewModel
            {
                SelectedSponsorType = type ?? "L"
            };

            model.Sponsors = await _leagueContext.Sponsors
                .Where(x => x.Type == model.SelectedSponsorType)
                .Select(x =>
                    new SponsorViewModel
                    {
                        Name = x.Name,
                        Url = x.Url,
                        MapUrl = x.MapUrl,
                        Address1 = x.Address1,
                        Address2 = x.Address2,
                        City = x.City,
                        State = x.State,
                        Zip = x.Zip,
                        FacebookUrl = x.FacebookUrl,
                        Phone = x.Phone,
                        Email = x.Email,
                        Description = x.Description
                    }).ToListAsync();

            return View(model);
        }
    }
}