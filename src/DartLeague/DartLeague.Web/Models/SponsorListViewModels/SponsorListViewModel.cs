using System.Collections.Generic;

namespace DartLeague.Web.Models.SponsorListViewModels
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "DTO")]
    public class SponsorListViewModel
    {
        public string SelectedSponsorType { get; set; }

        public List<SponsorViewModel> Sponsors { get; set; } = new List<SponsorViewModel>();
    }
}