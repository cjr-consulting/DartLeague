using System;
using System.Collections.Generic;

namespace DartLeague.Repositories.TrentonDartsModels
{
    public partial class Jobs
    {
        public ulong Id { get; set; }
        public byte Attempts { get; set; }
        public int AvailableAt { get; set; }
        public int CreatedAt { get; set; }
        public string Payload { get; set; }
        public string Queue { get; set; }
        public byte Reserved { get; set; }
        public int? ReservedAt { get; set; }
    }
}
