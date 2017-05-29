using System;
using System.Collections.Generic;

namespace DartLeague.Repositories.WinterSeasonData
{
    public partial class WinterSeasonTeams
    {
        public int Id { get; set; }
        public string PreSeasonDiv { get; set; }
        public string RegularSeasonDiv { get; set; }
        public int SeasonId { get; set; }
        public int TeamId { get; set; }
    }
}
