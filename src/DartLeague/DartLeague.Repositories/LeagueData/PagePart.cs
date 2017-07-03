using System;
using System.Collections.Generic;

namespace DartLeague.Repositories.LeagueData
{
    public partial class PagePart
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Html { get; set; }
        public string Name { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
