using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DartLeague.Web.Areas.Manage.Models
{
    public class HomeManagementViewModel
    {
        public List<LeagueLinksListViewModel> LeagueLinks { get; set; } = new List<LeagueLinksListViewModel>();
    }
}
