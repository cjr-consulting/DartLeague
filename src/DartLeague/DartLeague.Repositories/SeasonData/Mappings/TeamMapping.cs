using Microsoft.EntityFrameworkCore;

namespace DartLeague.Repositories.SeasonData.Mappings
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Ef Config")]
    internal class TeamMapping : IEntityMap<SeasonContext>
    {
        public TeamMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>()
                .ToTable("Teams", "season");
        }
    }
}
