using Microsoft.EntityFrameworkCore;

namespace DartLeague.Repositories.SeasonData.Mappings
{
    class SeasonMapping : IEntityMap<SeasonContext>
    {
        public SeasonMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Season>()
                .ToTable("Seasons", "season");
        }
    }
}
