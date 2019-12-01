using Microsoft.EntityFrameworkCore;

namespace DartLeague.Repositories.SeasonData.Mappings
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Ef Config")]
    internal class BoardPositionMapping : IEntityMap<SeasonContext>
    {
        public BoardPositionMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BoardPosition>()
                .ToTable("BoardPositions", "season");
        }
    }
}
