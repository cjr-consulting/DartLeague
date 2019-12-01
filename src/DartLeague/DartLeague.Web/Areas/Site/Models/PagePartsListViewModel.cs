using System.Collections.Generic;

namespace DartLeague.Web.Areas.Site.Models
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "DTO")]
    public class PagePartsListViewModel
    {
        public List<PagePartViewModel> PageParts { get; set; } = new List<PagePartViewModel>();       

    }
}