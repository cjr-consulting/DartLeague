using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DartLeague.Repositories.LeagueData;
using DartLeague.Web.ViewComponents.Models.Activities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var model = new ActivityListModel();
            model.Activities.AddRange(activities);
            return View(model);
        }

        private async Task<List<ActivityModel>> GetActivities()
        {
            return await _leagueContext.Activities
                .Where(x => x.Active)
                .Select(x => new ActivityModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImageFileId = x.FileId > 0 ? NumberObfuscation.Encode(x.FileId) : string.Empty
                }).ToListAsync();
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "DTO")]
    public class ActivitiesListViewModel
    {
        public List<ActivityModel> Activities { get; set; }
    }
}
