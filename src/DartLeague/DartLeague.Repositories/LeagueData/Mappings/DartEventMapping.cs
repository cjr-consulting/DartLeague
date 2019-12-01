using Microsoft.EntityFrameworkCore;
using System;

namespace DartLeague.Repositories.LeagueData.Mappings
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Ef Config")]
    internal class DartEventMapping : IEntityMap<LeagueContext>
    {
        public DartEventMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DartEvent>()
                .ToTable("DartEvents", "league");
        }
    }
}
