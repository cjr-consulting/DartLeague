using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DartLeague.Repositories.SeasonData
{
    public class SeasonContext : DbContext
    {
        public DbSet<Season> Seasons { get; set; }
        public DbSet<SeasonLink> SeasonLinks { get; set; }
        public DbSet<BoardMember> BoardMembers { get; set; }
        public DbSet<BoardPosition> BoardPositions { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamPlayer> TeamPlayers { get; set; }

        public SeasonContext(DbContextOptions<SeasonContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var maps = GetMappingClasses();

            foreach (var item in maps)
                Activator.CreateInstance(item, BindingFlags.Public | BindingFlags.Instance, null, new object[] { modelBuilder }, null);
        }

        private List<Type> GetMappingClasses()
        {
            return Assembly.GetExecutingAssembly().GetTypes()
               .Where(type => typeof(IEntityMap<SeasonContext>).IsAssignableFrom(type) && type.IsClass)
               .ToList();
        }
    }
}