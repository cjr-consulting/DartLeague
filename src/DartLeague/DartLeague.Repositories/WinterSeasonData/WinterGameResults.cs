using System;
using System.Collections.Generic;

namespace DartLeague.Repositories.WinterSeasonData
{
    public partial class WinterGameResults
    {
        public int Id { get; set; }
        public string AwayPlayers { get; set; }
        public string ForfeitedBy { get; set; }
        public int GameRuleId { get; set; }
        public string HomePlayers { get; set; }
        public string Legs { get; set; }
        public int MatchId { get; set; }
    }
}
