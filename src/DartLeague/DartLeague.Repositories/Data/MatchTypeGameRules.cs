using System;
using System.Collections.Generic;

namespace DartLeague.Repositories.TrentonDartsModels
{
    public partial class MatchTypeGameRules
    {
        public int Id { get; set; }
        public int BestOfNumberOfLegs { get; set; }
        public bool DoubleIn { get; set; }
        public bool DoubleOut { get; set; }
        public bool ForfeitIfNoPlayers { get; set; }
        public int GamePointValue { get; set; }
        public string GameType { get; set; }
        public string GroupName { get; set; }
        public int LegPointValue { get; set; }
        public int MatchTypeId { get; set; }
        public int NumberOfLegs { get; set; }
        public int NumberOfPlayers { get; set; }
        public int OrderId { get; set; }
        public string WhoStarts { get; set; }
    }
}
