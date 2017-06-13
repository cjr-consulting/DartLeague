using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DartLeague.Web.Areas.Site.Models
{
    public class EventResultsListViewModel
    {
        public string DartEventName { get; set; }

        public DateTime DartEventDate { get; set; }

        public bool IsTitleEvent { get; set; }

        public string SpecificEventName { get; set; }

        public int MemberId { get; set; }

        public int Finished { get; set; }

        public List<DartEventResultViewModel> Results { get; set; } = new List<DartEventResultViewModel>();


    }
}
