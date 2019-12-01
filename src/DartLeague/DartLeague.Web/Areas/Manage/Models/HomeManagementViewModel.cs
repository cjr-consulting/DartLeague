using System;
using System.Collections.Generic;
using System.Linq;

namespace DartLeague.Web.Areas.Manage.Models
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "DTO")]
    public class HomeManagementViewModel
    {
        public List<LeagueLinksListViewModel> LeagueLinks { get; set; } = new List<LeagueLinksListViewModel>();
        public List<SeasonListViewModel> Seasons { get; set; } = new List<SeasonListViewModel>();
    }
}
