using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DartLeague.Repositories.LeagueData;
using DartLeague.Web.ViewComponents.Models.Activity;
using Microsoft.AspNetCore.Mvc;

namespace DartLeague.Web.ViewComponents
{
    public class ActivitiesListViewComponent : ViewComponent
    {
        private readonly LeagueContext _leagueContext;

        public ActivitiesListViewComponent(LeagueContext leagueContext)
        {
            _leagueContext = leagueContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var activities = await GetActivities();
            var model = new ActivitiesListViewModel
            {
                Activities = activities
            };
            return View(model);
        }

        private async Task<List<ActivityModel>> GetActivities()
        {
           return new List<ActivityModel>();
        }
    }

    public class ActivitiesListViewModel
    {
        public List<ActivityModel> Activities { get; set; }
    }
}
