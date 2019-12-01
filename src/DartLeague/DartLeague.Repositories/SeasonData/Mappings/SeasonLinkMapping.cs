using Microsoft.EntityFrameworkCore;

namespace DartLeague.Repositories.SeasonData.Mappings
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Ef Config")]
    internal class SeasonLinkMapping : IEntityMap<SeasonContext>
    {
        public SeasonLinkMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SeasonLink>()
                .ToTable("SeasonLinks", "season");
        }
    }
}
