using System;
using System.Collections.Generic;

namespace DartLeague.Repositories.WinterSeasonData
{
    public partial class WinterSeasonPlayerPayments
    {
        public int Id { get; set; }
        public string PaymentStatus { get; set; }
        public int PlayerId { get; set; }
        public int SeasonId { get; set; }
    }
}
