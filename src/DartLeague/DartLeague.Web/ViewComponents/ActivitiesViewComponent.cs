using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DartLeague.Repositories.LeagueData;
using DartLeague.Web.ViewComponents.Models.Activities;
using Microsoft.AspNetCore.Mvc;

namespace DartLeague.Web.ViewComponents
{
    public class ActivitiesViewComponent : ViewComponent
    {
        private readonly LeagueContext _leagueContext;

        public ActivitiesViewComponent(LeagueContext leagueContext)
        {
            _leagueContext = leagueContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var activities = await GetActivities();
            var model = new ActivityListModel
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
