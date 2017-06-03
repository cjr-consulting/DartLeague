using System;
using System.Collections.Generic;

namespace DartLeague.Repositories.WinterSeasonData
{
    public partial class WinterMatchResults
    {
        public int Id { get; set; }
        public int AwayScoreOverride { get; set; }
        public bool HasScorecard { get; set; }
        public int HomeScoreOverride { get; set; }
    }
}
