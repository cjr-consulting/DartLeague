using System;
using System.Collections.Generic;
using System.Linq;

namespace DartLeague.Web.Areas.Site.Models
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "DTO")]
    public class DartEventsListViewModel
    {
        public List<DartEventListViewModel> DartEvents { get; set; } = new List<DartEventListViewModel>();
    }
}
