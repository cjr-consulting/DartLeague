using Microsoft.EntityFrameworkCore;
using System;

namespace DartLeague.Repositories.LeagueData.Mappings
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Ef Config")]
    internal class LeagueLinkMapping : IEntityMap<LeagueContext>
    {
        public LeagueLinkMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LeagueLink>()
                .ToTable("LeagueLinks", "league");
        }
    }
}
