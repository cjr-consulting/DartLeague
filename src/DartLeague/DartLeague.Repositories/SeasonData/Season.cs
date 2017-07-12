using System;
using System.Collections.Generic;
using System.Text;

namespace DartLeague.Repositories.SeasonData
{
    public class Season
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }

        public List<SeasonLink> SeasonLinks { get; set; }
    }
}
