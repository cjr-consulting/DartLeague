using System;
using System.Collections.Generic;

namespace DartLeague.Repositories.TrentonDartsModels
{
    public partial class WinterSeasons
    {
        public int Id { get; set; }
        public bool AccumulatePointsForAllParts { get; set; }
        public int DefaultMatchTypeId { get; set; }
        public int EndYear { get; set; }
        public int HalfPoints { get; set; }
        public bool IsCurrent { get; set; }
        public bool IsUsingMatchPoints { get; set; }
        public int LeagueId { get; set; }
        public int MinPointForHalfPoints { get; set; }
        public string Name { get; set; }
        public string SeasonType { get; set; }
        public int StartYear { get; set; }
        public int WinPoints { get; set; }
    }
}
