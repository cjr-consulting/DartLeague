using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DartLeague.Repositories.LeagueData
{
    public partial class LeagueContext : DbContext
    {
        public virtual DbSet<BrowsableFile> BrowsableFiles { get; set; }
        public virtual DbSet<BoardMember> BoardMembers { get; set; }
        public virtual DbSet<DartEventResult> DartEventResults { get; set; }
        public virtual DbSet<DartEvent> DartEvents { get; set; }
        public virtual DbSet<PagePart> PageParts { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Sponsor> Sponsors { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<LeagueLink> LeagueLinks { get; set; }

        public LeagueContext(DbContextOptions<LeagueContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BrowsableFile>(entity =>
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

                entity.Property(e => e.ContentType)
                    .IsRequired()
                    .HasColumnName("contentType")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.RelativePath)
                    .IsRequired()
                    .HasColumnName("relativePath")
                    .HasColumnType("varchar(255)");
            });
            modelBuilder.Entity<BoardMember>(entity =>
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
            modelBuilder.Entity<DartEventResult>(entity =>
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

                entity.Property(e => e.MemberId)
                    .HasColumnName("memberId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SpecificEventName)
                    .IsRequired()
                    .HasColumnName("specificEventName")
                    .HasColumnType("varchar(255)");
            });
            modelBuilder.Entity<DartEvent>(entity =>
            {
                entity.ToTable("dart_events");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Address1)
                    .HasColumnName("address1")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Address2)
                    .HasColumnName("address2")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.DartStart)
                    .HasColumnName("dartStart")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.DartType)
                    .IsRequired()
                    .HasColumnName("dartType")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.EventDate)
                    .IsRequired()
                    .HasColumnName("eventDate")
                    .HasColumnType("datetime(6)");

                entity.Property(e => e.EventEndDate)
                    .HasColumnName("eventEndDate")
                    .HasColumnType("datetime(6)");

                entity.Property(e => e.Description)
                    .HasColumnName("description");

                entity.Property(e => e.EventContact)
                    .IsRequired()
                    .HasColumnName("eventContact")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.EventContact2)
                    .HasColumnName("eventContact2")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.EventTypeId)
                    .IsRequired()
                    .HasColumnName("eventTypeId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FacebookUrl)
                    .HasColumnName("facebookUrl")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.HostName)
                    .HasColumnName("hostName")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.HostPhone)
                    .HasColumnName("hostPhone")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.HostUrl)
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
                    .HasColumnName("mapUrl")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.PosterFile)
                    .HasColumnName("posterFile")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.PosterFileId)
                    .HasColumnName("posterFileId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RegistrationEndTime)
                    .HasColumnName("registrationEndTime")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.RegistrationStartTime)
                    .HasColumnName("registrationStartTime")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Zip)
                    .HasColumnName("zip")
                    .HasColumnType("varchar(255)");
            });
            modelBuilder.Entity<PagePart>(entity =>
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
            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("members");

                entity.HasIndex(e => e.LeagueId)
                    .HasName("members_leagueid_index");

                entity.HasIndex(e => e.UserId)
                    .HasName("members_userid_index");

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
                    .HasColumnName("address1")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Address2)
                    .HasColumnName("address2")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.CellPhone)
                    .HasColumnName("cellPhone")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstName")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.HomePhone)
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
                    .HasColumnName("nickname")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("text");

                entity.Property(e => e.ShirtSize)
                    .HasColumnName("shirtSize")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Zip)
                    .HasColumnName("zip")
                    .HasColumnType("varchar(255)");
            });
            modelBuilder.Entity<Sponsor>(entity =>
            {
                entity.ToTable("sponsors");

                entity.HasIndex(e => e.LeagueId)
                    .HasName("sponsors_leagueid_index");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Address1)
                    .HasColumnName("address1")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Address2)
                    .HasColumnName("address2")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Comments)
                    .HasColumnName("comments")
                    .HasColumnType("text");

                entity.Property(e => e.ContactName)
                    .IsRequired()
                    .HasColumnName("contactName")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("text");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.FacebookUrl)
                    .HasColumnName("facebookUrl")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.LeagueId)
                    .HasColumnName("leagueId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MapUrl)
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
                    .HasColumnName("state")
                    .HasColumnType("varchar(2)");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Zip)
                    .HasColumnName("zip")
                    .HasColumnType("varchar(10)");
            });
            modelBuilder.Entity<Team>(entity =>
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
            modelBuilder.Entity<LeagueLink>(entity =>
            {
                entity.ToTable("league_links");
                
                entity.Property(e => e.Id)
                    .HasColumnName("id")
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

                entity.Property(x => x.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime(6)");
            });
        }
    }
}