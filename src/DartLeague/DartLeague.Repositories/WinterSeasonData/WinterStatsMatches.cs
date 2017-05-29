using System;
using System.Collections.Generic;

namespace DartLeague.Repositories.WinterSeasonData
{
    public partial class WinterStatsMatches
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Division { get; set; }
        public bool HasScorecard { get; set; }
        public bool HomeMatch { get; set; }
        public int MatchId { get; set; }
        public int MatchPoints { get; set; }
        public int PointsLost { get; set; }
        public int PointsWon { get; set; }
        public int SeasonId { get; set; }
        public string SeasonPart { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
    }
}
