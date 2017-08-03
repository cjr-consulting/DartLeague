using System;
using System.Collections.Generic;
using System.Text;

namespace DartLeague.Repositories.SeasonData
{
    public class Team
    {
        public int Id { get; set; }
        public int SeasonId { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }

        public List<TeamPlayer> Players { get; set; } = new List<TeamPlayer>();
        public Season Season { get; set; }
    }

    public class TeamPlayer
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int MemberId { get; set; }
        public int RoleId { get; set; }

        public Team Team { get; set; }
    }
}
