using System;
using System.Collections.Generic;

namespace DartLeague.Repositories.TrentonDartsModels
{
    public partial class WinterSeasonTeamPayments
    {
        public int Id { get; set; }
        public string PaymentStatus { get; set; }
        public int SeasonId { get; set; }
        public int TeamId { get; set; }
    }
}
