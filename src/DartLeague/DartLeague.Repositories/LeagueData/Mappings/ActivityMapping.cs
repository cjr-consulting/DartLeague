using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartLeague.Repositories.LeagueData.Mappings
{
    class ActivityMapping : IEntityMap<LeagueContext>
    {
        public ActivityMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>()
                .ToTable("Activities", "league");
        }
    }
}
