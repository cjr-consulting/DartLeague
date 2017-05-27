using System;
using System.Collections.Generic;

namespace DartLeague.Repositories.TrentonDartsModels
{
    public partial class WinterSeasonWeeks
    {
        public int Id { get; set; }
        public int LeagueId { get; set; }
        public int SeasonId { get; set; }
        public string WeekType { get; set; }
    }
}
