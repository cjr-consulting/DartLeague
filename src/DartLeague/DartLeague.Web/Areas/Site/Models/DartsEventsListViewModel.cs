using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DartLeague.Web.Areas.Site.Models
{
    public class DartEventsListViewModel
    {
        public List<DartEventListViewModel> DartEvents { get; set; } = new List<DartEventListViewModel>();
    }
}
