using Microsoft.EntityFrameworkCore;

namespace DartLeague.Repositories.SeasonData.Mappings
{
    class TeamMapping : IEntityMap<SeasonContext>
    {
        public TeamMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>()
                .ToTable("Teams", "season");
        }
    }
}
