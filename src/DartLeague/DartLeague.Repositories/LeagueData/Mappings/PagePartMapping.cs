using Microsoft.EntityFrameworkCore;
using System;

namespace DartLeague.Repositories.LeagueData.Mappings
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Ef Config")]
    internal class PagePartMapping : IEntityMap<LeagueContext>
    {
        public PagePartMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PagePart>()
                .ToTable("PageParts", "league");
        }
    }
}
