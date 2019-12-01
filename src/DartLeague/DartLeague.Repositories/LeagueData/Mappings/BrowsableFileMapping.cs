using Microsoft.EntityFrameworkCore;
using System;

namespace DartLeague.Repositories.LeagueData.Mappings
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Ef Config")]
    internal class BrowsableFileMapping : IEntityMap<LeagueContext>
    {
        public BrowsableFileMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BrowsableFile>()
                .ToTable("BrowsableFiles", "league");
        }
    }
}
