using Microsoft.EntityFrameworkCore;

namespace DartLeague.Repositories.SeasonData.Mappings
{
    class SeasonLinkMapping : IEntityMap<SeasonContext>
    {
        public SeasonLinkMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SeasonLink>()
                .ToTable("SeasonLinks", "season");
        }
    }
}
