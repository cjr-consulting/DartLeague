using System.Collections.Generic;

namespace DartLeague.Web.ViewComponents.Models.Navigation
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "DTO")]
    public class NavigationViewModel
    {
        public string Title { get; set; }
        public string Href { get; set; } = "#";
        public bool IsHeader { get; set; }
        public bool IsSeparator { get; set; }
        public List<NavigationViewModel> SubNavigations { get; set; } = new List<NavigationViewModel>();
    }
}
