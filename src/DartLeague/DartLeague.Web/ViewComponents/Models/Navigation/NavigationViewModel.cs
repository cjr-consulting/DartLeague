using System.Collections.Generic;

namespace DartLeague.Web.ViewComponents.Models.Navigation
{
    public class NavigationViewModel
    {
        public string Title { get; set; }
        public string Href { get; set; } = "#";
        public bool IsHeader { get; set; }
        public bool IsSeperator { get; set; }
        public List<NavigationViewModel> SubNavigations { get; set; } = new List<NavigationViewModel>();
    }
}
