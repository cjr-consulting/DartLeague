using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartLeague.Repositories.LeagueData.Mappings
{
    class DartEventResultMapping : IEntityMap<LeagueContext>
    {
        public DartEventResultMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DartEventResult>()
                .ToTable("DartEventResults", "league");
        }
    }
}
