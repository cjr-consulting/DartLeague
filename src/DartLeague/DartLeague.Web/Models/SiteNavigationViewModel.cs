using System.Collections.Generic;

namespace DartLeague.Web.Models
{
    public class SiteNavigationViewModel
    {
        public List<NavigationViewModel> ParentNavigations { get; set; } = new List<NavigationViewModel>();
    }

    public class NavigationViewModel
    {
        public string Title { get; set; }
        public string Href { get; set; } = "#";
        public bool IsHeader { get; set; }
        public bool IsSeperator { get; set; }
        public List<NavigationViewModel> SubNavigations { get; set; } = new List<NavigationViewModel>();
    }
}
