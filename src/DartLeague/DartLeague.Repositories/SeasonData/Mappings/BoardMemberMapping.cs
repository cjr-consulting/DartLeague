using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartLeague.Repositories.SeasonData.Mappings
{
    class BoardMemberMapping : IEntityMap<SeasonContext>
    {
        public BoardMemberMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BoardMember>()
                .ToTable("BoardMembers", "season");
        }
    }
}
