using System.Collections.Generic;

namespace DartLeague.Repositories.LeagueData
{
    public partial class BrowsableFile
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public string RelativePath { get; set; }
    }
}
