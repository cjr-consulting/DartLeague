using Microsoft.EntityFrameworkCore;
using System;

namespace DartLeague.Repositories.LeagueData.Mappings
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Ef Config")]
    internal class ActivityMapping : IEntityMap<LeagueContext>
    {
        public ActivityMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>()
                .ToTable("Activities", "league");
        }
    }
}
