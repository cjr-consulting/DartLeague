using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DartLeague.Repositories.SeasonData
{
    public class SeasonContext : DbContext
    {
        public DbSet<Season> Seasons { get; set; }

        public SeasonContext(DbContextOptions<SeasonContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Season>(entity =>
            {
                entity.ToTable("browsable_files");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.StartDate)
                    .IsRequired()
                    .HasColumnName("eventDate")
                    .HasColumnType("datetime(6)");

                entity.Property(e => e.EndDate)
                    .IsRequired()
                    .HasColumnName("eventDate")
                    .HasColumnType("datetime(6)");

                entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .HasColumnName("createdAt")
                    .HasColumnType("datetime(6)");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("createdBy")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.UpdatedAt)
                    .IsRequired()
                    .HasColumnName("updatedAt")
                    .HasColumnType("datetime(6)")
                    .HasDefaultValue(null);
                
                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updatedBy")
                    .HasColumnType("int(10) unsigned");
            });
        }
    }
}
