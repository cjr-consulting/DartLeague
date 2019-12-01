using Microsoft.EntityFrameworkCore;

namespace DartLeague.Repositories.SeasonData.Mappings
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Ef Config")]
    internal class TeamPlayerMapping : IEntityMap<SeasonContext>
    {
        public TeamPlayerMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeamPlayer>()
                .ToTable("TeamPlayers", "season");
        }
    }
}
