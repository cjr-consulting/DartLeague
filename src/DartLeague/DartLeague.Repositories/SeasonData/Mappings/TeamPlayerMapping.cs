using Microsoft.EntityFrameworkCore;

namespace DartLeague.Repositories.SeasonData.Mappings
{
    class TeamPlayerMapping : IEntityMap<SeasonContext>
    {
        public TeamPlayerMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeamPlayer>()
                .ToTable("TeamPlayers", "season");
        }
    }
}
