using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartLeague.Repositories.LeagueData.Mappings
{
    class PagePartMapping : IEntityMap<LeagueContext>
    {
        public PagePartMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PagePart>()
                .ToTable("PageParts", "league");
        }
    }
}
