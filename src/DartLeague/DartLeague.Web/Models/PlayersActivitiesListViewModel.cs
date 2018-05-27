using System.Collections.Generic;

namespace DartLeague.Web.Models
{
    public class PlayersActivitiesListViewModel
    {
        public List<PlayerActivityModel> PlayerActivityResults { get; protected set; } = new List<PlayerActivityModel>();
    }
}