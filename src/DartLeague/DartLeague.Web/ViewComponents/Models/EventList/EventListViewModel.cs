using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DartLeague.Web.ViewComponents.Models.EventList
{
    public class EventListViewModel
    {
        public List<EventViewModel> Events { get; set; } = new List<EventViewModel>();
    }
}
