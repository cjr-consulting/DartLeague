using System.Collections.Generic;

namespace DartLeague.Web.ViewComponents.Models.Lod
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "DTO")]
    public class LodEventsListModel
    {
        public List<LodModel> LodEvents { get; set; } = new List<LodModel>();
    }
}