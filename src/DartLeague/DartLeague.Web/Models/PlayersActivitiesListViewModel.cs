using System.Collections.Generic;

namespace DartLeague.Web.Models
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "DTO")]
    public class PlayersActivitiesListViewModel
    {
        public List<PlayerActivityModel> PlayerActivityResults { get; protected set; } = new List<PlayerActivityModel>();
    }
}