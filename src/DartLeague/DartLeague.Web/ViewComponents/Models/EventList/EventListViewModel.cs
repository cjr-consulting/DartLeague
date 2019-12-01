using System.Collections.Generic;
using System.Linq;

namespace DartLeague.Web.ViewComponents.Models.EventList
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "DTO")]
    public class EventListViewModel
    {
        public List<EventViewModel> Events { get; set; } = new List<EventViewModel>();
    }
}
