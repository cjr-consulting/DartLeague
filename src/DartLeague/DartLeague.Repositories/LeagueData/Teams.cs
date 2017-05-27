using System;
using System.Collections.Generic;

namespace DartLeague.Repositories.LeagueData
{
    public partial class Teams
    {
        public int Id { get; set; }
        public int LeagueId { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public int? SponsorId { get; set; }
    }
}
