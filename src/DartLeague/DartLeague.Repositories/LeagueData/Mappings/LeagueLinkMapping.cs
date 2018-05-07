using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartLeague.Repositories.LeagueData.Mappings
{
    class LeagueLinkMapping : IEntityMap<LeagueContext>
    {
        public LeagueLinkMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LeagueLink>()
                .ToTable("LeagueLinks", "league");
        }
    }
}
