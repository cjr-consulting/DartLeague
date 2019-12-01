using Microsoft.EntityFrameworkCore;
using System;

namespace DartLeague.Repositories.SeasonData.Mappings
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Ef Config")]
    internal class BoardMemberMapping : IEntityMap<SeasonContext>
    {
        public BoardMemberMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BoardMember>()
                .ToTable("BoardMembers", "season");
        }
    }
}
