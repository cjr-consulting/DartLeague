using System;
using System.Collections.Generic;

namespace DartLeague.Repositories.SeasonData
{
    public class Team
    {
        public int Id { get; set; }
        public int SeasonId { get; set; }
        public string Name { get; set; }
        public int SponsorId { get; set; }
        public string Abbreviation { get; set; }
        public int BannerImageId { get; set; }
        public int LogoImageId { get; set; }
        public int TeamPictureImageId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }

        public List<TeamPlayer> Players { get; } = new List<TeamPlayer>();
        public Season Season { get; set; }
    }
}
