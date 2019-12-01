using Microsoft.EntityFrameworkCore;
using System;

namespace DartLeague.Repositories.LeagueData.Mappings
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Ef Config")]
    internal class LuckOfTheDrawMapping : IEntityMap<LeagueContext>
    {
        public LuckOfTheDrawMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LuckOfTheDraw>()
                .ToTable("LuckOfTheDraws", "league");
        }
    }
}
