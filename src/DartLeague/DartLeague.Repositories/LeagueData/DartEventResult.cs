using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DartLeague.Repositories.LeagueData
{
    public partial class DartEventResult
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string Finished { get; set; }
        public int OrderId { get; set; }
        public int MemberId { get; set; }
        public string SpecificEventName { get; set; }
        public Member Member { get; set; }
    }
}
