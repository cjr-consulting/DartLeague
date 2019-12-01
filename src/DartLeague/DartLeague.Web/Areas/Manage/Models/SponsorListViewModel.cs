using System.Collections.Generic;

namespace DartLeague.Web.Areas.Manage.Models
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "DTO")]
    public class SponsorListViewModel
    {
        public List<SponsorViewModel> Sponsors { get; set; } = new List<SponsorViewModel>();
    }
}