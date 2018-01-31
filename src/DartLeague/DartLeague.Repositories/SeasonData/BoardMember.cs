using System;
using System.Collections.Generic;
using System.Text;

namespace DartLeague.Repositories.SeasonData
{
    public class BoardMember
    {
        public int Id { get; set; }
        public int SeasonId { get; set; }
        public int PositionId { get; set; }
        public int MemberId { get; set; }

        public BoardPosition Position { get; set; }

        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }

        public Season Season { get; set; }
    }
}