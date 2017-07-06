using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DartLeague.Web.Areas.Manage.Controllers;

namespace DartLeague.Web.Areas.Manage.Models
{
    public class SeasonListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; internal set; }
        public DateTime EndDate { get; internal set; }
        public SeasonStates State { get; internal set; }
    }
}
