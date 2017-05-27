using System;
using System.Collections.Generic;

namespace DartLeague.Repositories.TrentonDartsModels
{
    public partial class WinterSeasonMatches
    {
        public int Id { get; set; }
        public int AwayTeamId { get; set; }
        public string Division { get; set; }
        public int HomeTeamId { get; set; }
        public int MatchTypeId { get; set; }
        public int SeasonId { get; set; }
        public int WeekId { get; set; }
    }
}
