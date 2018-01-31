using System;
using System.Collections.Generic;

namespace DartLeague.Repositories.WinterSeasonData
{
    public partial class WinterSeasonTeamPlayers
    {
        public int Id { get; set; }
        public int LeagueId { get; set; }
        public int PlayerId { get; set; }
        public string Role { get; set; }
        public int SeasonId { get; set; }
        public int SeasonTeamId { get; set; }
    }
}
