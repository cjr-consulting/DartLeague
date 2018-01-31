using System;
using System.Collections.Generic;
using System.Text;

namespace DartLeague.Repositories.LeagueData
{
    public class LeagueLink
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int LinkType { get; set; }
        public string Url { get; set; }
        public int FileId { get; set; }
        public int Order { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
