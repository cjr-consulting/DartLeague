using System.Collections.Generic;

namespace DartLeague.Repositories.LeagueData
{
    public partial class BrowsableFile
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string RelativePath { get; set; }
    }
}
