﻿using Microsoft.EntityFrameworkCore;

namespace DartLeague.Repositories.SeasonData.Mappings
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Ef Config")]
    internal class SeasonMapping : IEntityMap<SeasonContext>
    {
        public SeasonMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Season>()
                .ToTable("Seasons", "season");
        }
    }
}
