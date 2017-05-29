using System;
using System.Collections.Generic;

namespace DartLeague.Repositories.WinterSeasonData
{
    public partial class Seasons
    {
        public int Id { get; set; }
        public bool IsCurrent { get; set; }
        public int LeagueId { get; set; }
        public string LinkName { get; set; }
        public string Name { get; set; }
    }
}
