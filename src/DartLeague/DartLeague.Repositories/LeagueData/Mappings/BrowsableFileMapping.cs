using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartLeague.Repositories.LeagueData.Mappings
{
    class BrowsableFileMapping : IEntityMap<LeagueContext>
    {
        public BrowsableFileMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BrowsableFile>()
                .ToTable("BrowsableFiles", "league");
        }
    }
}
