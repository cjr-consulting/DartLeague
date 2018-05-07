using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DartLeague.Repositories.LeagueData
{
    public partial class LeagueContext : DbContext
    {
        public virtual DbSet<BrowsableFile> BrowsableFiles { get; set; }
        public virtual DbSet<DartEventResult> DartEventResults { get; set; }
        public virtual DbSet<DartEvent> DartEvents { get; set; }
        public virtual DbSet<PagePart> PageParts { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Sponsor> Sponsors { get; set; }
        public virtual DbSet<LeagueLink> LeagueLinks { get; set; }
        public virtual DbSet<LuckOfTheDraw> LuckOfTheDraws { get; set; }
        public virtual DbSet<Activity> Activities { get; set; }

        public LeagueContext(DbContextOptions<LeagueContext> options)
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
               .Where(type => typeof(IEntityMap<LeagueContext>).IsAssignableFrom(type) && type.IsClass)
               .ToList();
        }
    }
}