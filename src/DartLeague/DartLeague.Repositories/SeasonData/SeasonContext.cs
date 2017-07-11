using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DartLeague.Repositories.SeasonData
{
    public class SeasonContext : DbContext
    {
        public DbSet<Season> Seasons { get; set; }
        public DbSet<SeasonLink> SeasonLinks { get; set; }

        public SeasonContext(DbContextOptions<SeasonContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Season>(entity =>
            {
                entity.ToTable("seasons");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.StartDate)
                    .IsRequired()
                    .HasColumnName("startDate")
                    .HasColumnType("datetime(6)");

                entity.Property(e => e.EndDate)
                    .IsRequired()
                    .HasColumnName("endDate")
                    .HasColumnType("datetime(6)");

                entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .HasColumnName("createdAt")
                    .HasColumnType("datetime(6)")
                    .HasDefaultValue(null);

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("createdBy")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updatedAt")
                    .HasColumnType("datetime(6)");
                
                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updatedBy")
                    .HasColumnType("int(10) unsigned");
            });
            modelBuilder.Entity<SeasonLink>(entity =>
            {
                entity.ToTable("season_links");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.SeasonId)
                    .HasColumnName("seasonId")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.LinkType)
                    .IsRequired()
                    .HasColumnName("linkType")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.FileId)
                    .IsRequired()
                    .HasColumnName("fileId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Order)
                    .IsRequired()
                    .HasColumnName("order")
                    .HasColumnType("int(11)");

                entity.Property(x => x.CreatedAt)
                    .IsRequired()
                    .HasColumnName("created_at")
                    .HasColumnType("datetime(6)")
                    .HasDefaultValue(null);

                entity.Property(x => x.CreatedBy)
                    .IsRequired()
                    .HasColumnName("created_by")
                    .HasColumnType("int(10) unsigned");

                entity.Property(x => x.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime(6)");

                entity.Property(x => x.UpdatedBy)
                    .IsRequired()
                    .HasColumnName("updated_by")
                    .HasColumnType("int(10) unsigned");

                entity.HasOne(x => x.Season)
                    .WithMany(e => e.SeasonLinks)
                    .HasForeignKey(e => e.SeasonId);
            });
        }
    }
}
