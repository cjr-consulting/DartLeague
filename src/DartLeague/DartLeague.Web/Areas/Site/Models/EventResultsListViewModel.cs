using System;
using System.Collections.Generic;
using System.Linq;

namespace DartLeague.Web.Areas.Site.Models
{
    public class EventResultsListViewModel
    {
        public int DartEventId { get; set; }
        public string DartEventName { get; set; }

        public DateTime DartEventDate { get; set; }

        public bool IsTitleEvent { get; set; }

        public string SpecificEventName { get; set; }

        public int MemberId { get; set; }

        public string Finished { get; set; }

        public int OrderId { get; set; }

        public List<DartEventResultViewModel> Results { get; } = new List<DartEventResultViewModel>();


    }
}
