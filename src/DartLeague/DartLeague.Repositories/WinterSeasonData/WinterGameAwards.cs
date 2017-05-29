using System;
using System.Collections.Generic;

namespace DartLeague.Repositories.WinterSeasonData
{
    public partial class WinterGameAwards
    {
        public int Id { get; set; }
        public string AwardType { get; set; }
        public int GameId { get; set; }
        public int PlayerId { get; set; }
        public int Value { get; set; }
    }
}
