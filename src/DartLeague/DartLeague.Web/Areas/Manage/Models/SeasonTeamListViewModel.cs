using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DartLeague.Web.Areas.Manage.Models
{
    public class SeasonTeamListViewModel
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public int NumberOfPlayers { get; set; }
    }
}
