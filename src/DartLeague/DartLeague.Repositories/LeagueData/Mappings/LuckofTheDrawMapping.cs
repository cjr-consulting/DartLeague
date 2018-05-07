using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartLeague.Repositories.LeagueData.Mappings
{
    class LuckOfTheDrawMapping : IEntityMap<LeagueContext>
    {
        public LuckOfTheDrawMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LuckOfTheDraw>()
                .ToTable("LuckOfTheDraws", "league");
        }
    }
}
