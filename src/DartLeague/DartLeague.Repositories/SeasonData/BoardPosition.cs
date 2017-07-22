using System;

namespace DartLeague.Repositories.SeasonData
{
    public class BoardPosition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }

        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
    }
}