using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DartLeague.Repositories.LeagueData
{
    public partial class LeagueContext : DbContext
    {
        public virtual DbSet<BrowsableFiles> BrowsableFiles { get; set; }
        public virtual DbSet<BoardMembers> BoardMembers { get; set; }
        public virtual DbSet<DartEventResults> DartEventResults { get; set; }
        public virtual DbSet<DartEvents> DartEvents { get; set; }
        public virtual DbSet<PageParts> PageParts { get; set; }
        public virtual DbSet<Players> Players { get; set; }
        public virtual DbSet<Sponsors> Sponsors { get; set; }
        public virtual DbSet<Teams> Teams { get; set; }

        public LeagueContext(DbContextOptions<LeagueContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BrowsableFiles>(entity =>
            {
                entity.ToTable("browsable_files");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasColumnName("category")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasColumnName("fileName")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.MimeType)
                    .IsRequired()
                    .HasColumnName("mimeType")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.RelativePath)
                    .IsRequired()
                    .HasColumnName("relativePath")
                    .HasColumnType("varchar(255)");
            });
            modelBuilder.Entity<BoardMembers>(entity =>
            {
                entity.ToTable("board_members");

                entity.HasIndex(e => e.LeagueId)
                    .HasName("board_members_leagueid_index");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.EndSeasonId)
                    .HasColumnName("endSeasonId")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.EndingSeason)
                    .HasColumnName("endingSeason")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.LeagueId)
                    .HasColumnName("leagueId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Position)
                    .IsRequired()
                    .HasColumnName("position")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.StartSeasonId)
                    .HasColumnName("startSeasonId")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.StartingSeason)
                    .IsRequired()
                    .HasColumnName("startingSeason")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasColumnType("int(11)");

                entity.Property(x => x.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime(6)");

                entity.Property(x => x.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("datetime(6)")
                    .HasDefaultValue(null);

                entity.Property(x => x.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime")
                    .HasDefaultValue(null);
            });
            modelBuilder.Entity<DartEventResults>(entity =>
            {
                entity.ToTable("dart_event_results");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.EventId)
                    .HasColumnName("eventId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Finished)
                    .IsRequired()
                    .HasColumnName("finished")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.OrderId)
                    .HasColumnName("orderId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PlayerId)
                    .HasColumnName("playerId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SpecificEventName)
                    .IsRequired()
                    .HasColumnName("specificEventName")
                    .HasColumnType("varchar(255)");
            });
            modelBuilder.Entity<DartEvents>(entity =>
            {
                entity.ToTable("dart_events");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasColumnName("address1")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Address2)
                    .IsRequired()
                    .HasColumnName("address2")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.DartStart)
                    .IsRequired()
                    .HasColumnName("dartStart")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.DartType)
                    .IsRequired()
                    .HasColumnName("dartType")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.EventContact)
                    .IsRequired()
                    .HasColumnName("eventContact")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.EventContact2)
                    .IsRequired()
                    .HasColumnName("eventContact2")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.EventTypeId)
                    .HasColumnName("eventTypeId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FacebookUrl)
                    .IsRequired()
                    .HasColumnName("facebookUrl")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.HostName)
                    .IsRequired()
                    .HasColumnName("hostName")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.HostPhone)
                    .IsRequired()
                    .HasColumnName("hostPhone")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.HostUrl)
                    .IsRequired()
                    .HasColumnName("hostUrl")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ImageFileId)
                    .HasColumnName("imageFileId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsTitleEvent)
                    .HasColumnName("isTitleEvent")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.LocationName)
                    .IsRequired()
                    .HasColumnName("locationName")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.MapUrl)
                    .IsRequired()
                    .HasColumnName("mapUrl")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.PosterFile)
                    .IsRequired()
                    .HasColumnName("posterFile")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.PosterFileId)
                    .HasColumnName("posterFileId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RegistrationEndTime)
                    .IsRequired()
                    .HasColumnName("registrationEndTime")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.RegistrationStartTime)
                    .IsRequired()
                    .HasColumnName("registrationStartTime")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnName("state")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnName("url")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Zip)
                    .IsRequired()
                    .HasColumnName("zip")
                    .HasColumnType("varchar(255)");
            });
            modelBuilder.Entity<PageParts>(entity =>
            {
                entity.ToTable("page_parts");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("text");

                entity.Property(e => e.Html)
                    .IsRequired()
                    .HasColumnName("html")
                    .HasColumnType("text");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("text");
            });
            modelBuilder.Entity<Players>(entity =>
            {
                entity.ToTable("players");

                entity.HasIndex(e => e.LeagueId)
                    .HasName("players_leagueid_index");

                entity.HasIndex(e => e.UserId)
                    .HasName("players_userid_index");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.AcceptEmail)
                    .HasColumnName("acceptEmail")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.AcceptText)
                    .HasColumnName("acceptText")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasColumnName("address1")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Address2)
                    .IsRequired()
                    .HasColumnName("address2")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.CellPhone)
                    .IsRequired()
                    .HasColumnName("cellPhone")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstName")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.HomePhone)
                    .IsRequired()
                    .HasColumnName("homePhone")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("lastName")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.LeagueId)
                    .HasColumnName("leagueId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Nickname)
                    .IsRequired()
                    .HasColumnName("nickname")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Notes)
                    .IsRequired()
                    .HasColumnName("notes")
                    .HasColumnType("text");

                entity.Property(e => e.ShirtSize)
                    .IsRequired()
                    .HasColumnName("shirtSize")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnName("state")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Zip)
                    .IsRequired()
                    .HasColumnName("zip")
                    .HasColumnType("varchar(255)");
            });
            modelBuilder.Entity<Sponsors>(entity =>
            {
                entity.ToTable("sponsors");

                entity.HasIndex(e => e.LeagueId)
                    .HasName("sponsors_leagueid_index");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasColumnName("address1")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Address2)
                    .IsRequired()
                    .HasColumnName("address2")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Comments)
                    .IsRequired()
                    .HasColumnName("comments")
                    .HasColumnType("text");

                entity.Property(e => e.ContactName)
                    .IsRequired()
                    .HasColumnName("contactName")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("text");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.FacebookUrl)
                    .IsRequired()
                    .HasColumnName("facebookUrl")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.LeagueId)
                    .HasColumnName("leagueId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MapUrl)
                    .IsRequired()
                    .HasColumnName("mapUrl")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnName("state")
                    .HasColumnType("varchar(2)");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnName("url")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Zip)
                    .IsRequired()
                    .HasColumnName("zip")
                    .HasColumnType("varchar(10)");
            });
            modelBuilder.Entity<Teams>(entity =>
            {
                entity.ToTable("teams");

                entity.HasIndex(e => e.LeagueId)
                    .HasName("teams_leagueid_index");

                entity.HasIndex(e => e.SponsorId)
                    .HasName("teams_sponsorid_index");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.LeagueId)
                    .HasColumnName("leagueId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Notes)
                    .IsRequired()
                    .HasColumnName("notes")
                    .HasColumnType("text");

                entity.Property(e => e.SponsorId)
                    .HasColumnName("sponsorId")
                    .HasColumnType("int(10) unsigned");
            });
        }
    }
}