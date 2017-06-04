using System.Collections.Generic;

namespace DartLeague.Web.ViewComponents.Models.Navigation
{
    public class SiteNavigationViewModel
    {
        public List<NavigationViewModel> ParentNavigations { get; set; } = new List<NavigationViewModel>();
    }
}
