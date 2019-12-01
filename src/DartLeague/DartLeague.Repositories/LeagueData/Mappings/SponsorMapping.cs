using Microsoft.EntityFrameworkCore;
using System;

namespace DartLeague.Repositories.LeagueData.Mappings
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Ef Config")]
    internal class SponsorMapping : IEntityMap<LeagueContext>
    {
        public SponsorMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sponsor>()
                .ToTable("Sponsors", "league");
        }
    }
}
