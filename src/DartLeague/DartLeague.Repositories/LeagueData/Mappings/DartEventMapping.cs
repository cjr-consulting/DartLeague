using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartLeague.Repositories.LeagueData.Mappings
{
    class DartEventMapping : IEntityMap<LeagueContext>
    {
        public DartEventMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DartEvent>()
                .ToTable("DartEvents", "league");
        }
    }
}
