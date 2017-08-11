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
                    .HasColumnName("createdAt")
                    .HasColumnType("datetime(6)")
                    .HasDefaultValue(null);

                entity.Property(x => x.CreatedBy)
                    .IsRequired()
                    .HasColumnName("createdBy")
                    .HasColumnType("int(10) unsigned");

                entity.Property(x => x.UpdatedAt)
                    .HasColumnName("updatedAt")
                    .HasColumnType("datetime(6)");

                entity.Property(x => x.UpdatedBy)
                    .IsRequired()
                    .HasColumnName("updatedBy")
                    .HasColumnType("int(10) unsigned");

                entity.HasOne(x => x.Season)
                    .WithMany(e => e.SeasonLinks)
                    .HasForeignKey(e => e.SeasonId);
            });
            modelBuilder.Entity<BoardPosition>(entity =>
            {
                entity.ToTable("board_positions");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Order)
                    .HasColumnName("orderId")
                    .HasColumnType("int(10) unsigned");

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
            modelBuilder.Entity<BoardMember>(entity =>
            {
                entity.ToTable("board_members");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.SeasonId)
                    .HasColumnName("seasonId")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.PositionId)
                    .IsRequired()
                    .HasColumnName("positionId")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.MemberId)
                    .IsRequired()
                    .HasColumnName("memberId")
                    .HasColumnType("int(10) unsigned");
                
                entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .HasColumnName("createdAt")
                    .HasColumnType("datetime(6)")
                    .HasDefaultValue(null);

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("createdBy")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deletedAt")
                    .HasColumnType("datetime(6)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updatedAt")
                    .HasColumnType("datetime(6)");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updatedBy")
                    .HasColumnType("int(10) unsigned");

                entity.HasOne(x => x.Season)
                    .WithMany(e => e.BoardMembers)
                    .HasForeignKey(e => e.SeasonId);
            });
            modelBuilder.Entity<Team>(entity =>
            {
                entity.ToTable("teams");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.SeasonId)
                    .HasColumnName("seasonId")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.Abbreviation)
                    .IsRequired()
                    .HasColumnName("abbreviation")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.BannerImageId)
                    .HasColumnName("bannerImageId")
                    .HasColumnType("int(11) unsigned");

                entity.Property(e => e.LogoImageId)
                    .IsRequired()
                    .HasColumnName("logoImageId")
                    .HasColumnType("int(11) unsigned");

                entity.Property(e => e.TeamPictureImageId)
                    .IsRequired()
                    .HasColumnName("teamPictureImageId")
                    .HasColumnType("int(11) unsigned");
                
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

                entity.HasOne(x => x.Season)
                    .WithMany(e => e.Teams)
                    .HasForeignKey(e => e.SeasonId);
            });

            modelBuilder.Entity<TeamPlayer>(entity =>
            {

                entity.ToTable("team_players");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.TeamId)
                    .HasColumnName("teamId")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.MemberId)
                    .HasColumnName("memberId")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.RoleId)
                    .HasColumnName("roleId")
                    .HasColumnType("int(10) unsigned");

                entity.HasOne(x => x.Team)
                    .WithMany(e => e.Players)
                    .HasForeignKey(e => e.TeamId);
            });
        }
    }
}