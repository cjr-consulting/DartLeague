using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartLeague.Repositories.LeagueData.Mappings
{
    class SponsorMapping : IEntityMap<LeagueContext>
    {
        public SponsorMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sponsor>()
                .ToTable("Sponsors", "league");
        }
    }
}
