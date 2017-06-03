using System;
using System.Collections.Generic;

namespace DartLeague.Repositories.LeagueData
{
    public partial class BoardMembers
    {
        public int Id { get; set; }
        public long? EndSeasonId { get; set; }
        public string EndingSeason { get; set; }
        public int LeagueId { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public long? StartSeasonId { get; set; }
        public string StartingSeason { get; set; }
        public int? UserId { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
