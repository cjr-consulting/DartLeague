using System;
using System.Collections.Generic;

namespace DartLeague.Repositories.LeagueData
{
    public partial class Sponsors
    {
        public int Id { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Comments { get; set; }
        public string ContactName { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string FacebookUrl { get; set; }
        public int LeagueId { get; set; }
        public string MapUrl { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string State { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public string Zip { get; set; }
    }
}
