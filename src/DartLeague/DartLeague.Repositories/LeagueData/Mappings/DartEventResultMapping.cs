using Microsoft.EntityFrameworkCore;
using System;

namespace DartLeague.Repositories.LeagueData.Mappings
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Ef Config")]
    internal class DartEventResultMapping : IEntityMap<LeagueContext>
    {
        public DartEventResultMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DartEventResult>()
                .ToTable("DartEventResults", "league");
        }
    }
}
