using Microsoft.EntityFrameworkCore;
using System;

namespace DartLeague.Repositories.LeagueData.Mappings
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Ef Config")]
    internal class MemberMapping : IEntityMap<LeagueContext>
    {
        public MemberMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>()
                .ToTable("Members", "league");
        }
    }
}
