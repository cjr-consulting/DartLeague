using System.Collections.Generic;

namespace DartLeague.Web.Models.SponsorListViewModels
{
    public class SponsorListViewModel
    {
        public string SelectedSponsorType { get; set; }
        public List<SponsorViewModel> Sponsors { get; set; } = new List<SponsorViewModel>();
    }
}