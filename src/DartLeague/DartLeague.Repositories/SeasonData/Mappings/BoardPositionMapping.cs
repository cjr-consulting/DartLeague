using Microsoft.EntityFrameworkCore;

namespace DartLeague.Repositories.SeasonData.Mappings
{
    class BoardPositionMapping : IEntityMap<SeasonContext>
    {
        public BoardPositionMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BoardPosition>()
                .ToTable("BoardPositions", "season");
        }
    }
}
