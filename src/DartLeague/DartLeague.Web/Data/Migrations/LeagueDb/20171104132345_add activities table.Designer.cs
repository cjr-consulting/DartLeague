using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DartLeague.Repositories.LeagueData;

namespace DartLeague.Web.Data.Migrations.LeagueDb
{
    [DbContext(typeof(LeagueContext))]
    [Migration("20171104132345_add activities table")]
    partial class addactivitiestable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("DartLeague.Repositories.LeagueData.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<bool>("Active")
                        .HasColumnName("active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("created_at")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Date")
                        .HasColumnName("eventDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("FileId")
                        .HasColumnName("fileId")
                        .HasColumnType("int(11)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(500)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnName("updated_at")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("activities");
                });

            modelBuilder.Entity("DartLeague.Repositories.LeagueData.BrowsableFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnName("category")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnName("contentType")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnName("fileName")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RelativePath")
                        .IsRequired()
                        .HasColumnName("relativePath")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("browsable_files");
                });

            modelBuilder.Entity("DartLeague.Repositories.LeagueData.DartEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<string>("Address1")
                        .HasColumnName("address1")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Address2")
                        .HasColumnName("address2")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("City")
                        .HasColumnName("city")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("DartStart")
                        .HasColumnName("dartStart")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("DartType")
                        .IsRequired()
                        .HasColumnName("dartType")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<string>("Description")
                        .HasColumnName("description");

                    b.Property<string>("EventContact")
                        .IsRequired()
                        .HasColumnName("eventContact")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("EventContact2")
                        .HasColumnName("eventContact2")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("EventDate")
                        .HasColumnName("eventDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("EventEndDate")
                        .HasColumnName("eventEndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("EventTypeId")
                        .HasColumnName("eventTypeId")
                        .HasColumnType("int(11)");

                    b.Property<string>("FacebookUrl")
                        .HasColumnName("facebookUrl")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("HostName")
                        .HasColumnName("hostName")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("HostPhone")
                        .HasColumnName("hostPhone")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("HostUrl")
                        .HasColumnName("hostUrl")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("ImageFileId")
                        .HasColumnName("imageFileId")
                        .HasColumnType("int(11)");

                    b.Property<bool>("IsTitleEvent")
                        .HasColumnName("isTitleEvent")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LocationName")
                        .IsRequired()
                        .HasColumnName("locationName")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("MapUrl")
                        .HasColumnName("mapUrl")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("PosterFile")
                        .HasColumnName("posterFile")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("PosterFileId")
                        .HasColumnName("posterFileId")
                        .HasColumnType("int(11)");

                    b.Property<string>("RegistrationEndTime")
                        .HasColumnName("registrationEndTime")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RegistrationStartTime")
                        .HasColumnName("registrationStartTime")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("State")
                        .HasColumnName("state")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("UpdatedAt");

                    b.Property<string>("Url")
                        .HasColumnName("url")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Zip")
                        .HasColumnName("zip")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("dart_events");
                });

            modelBuilder.Entity("DartLeague.Repositories.LeagueData.DartEventResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<int>("EventId")
                        .HasColumnName("eventId")
                        .HasColumnType("int(11)");

                    b.Property<string>("Finished")
                        .IsRequired()
                        .HasColumnName("finished")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("MemberId")
                        .HasColumnName("memberId")
                        .HasColumnType("int(10) unsigned");

                    b.Property<int>("OrderId")
                        .HasColumnName("orderId")
                        .HasColumnType("int(11)");

                    b.Property<string>("SpecificEventName")
                        .IsRequired()
                        .HasColumnName("specificEventName")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.ToTable("dart_event_results");
                });

            modelBuilder.Entity("DartLeague.Repositories.LeagueData.LeagueLink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("created_at")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("FileId")
                        .HasColumnName("fileId")
                        .HasColumnType("int(11)");

                    b.Property<int>("LinkType")
                        .HasColumnName("linkType")
                        .HasColumnType("int(11)");

                    b.Property<int>("Order")
                        .HasColumnName("order")
                        .HasColumnType("int(11)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnName("title")
                        .HasColumnType("varchar(500)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnName("updated_at")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Url")
                        .HasColumnName("url")
                        .HasColumnType("varchar(500)");

                    b.HasKey("Id");

                    b.ToTable("league_links");
                });

            modelBuilder.Entity("DartLeague.Repositories.LeagueData.LuckofTheDraw", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<bool>("Active")
                        .HasColumnName("active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("created_at")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Date")
                        .HasColumnName("eventDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("FileId")
                        .HasColumnName("fileId")
                        .HasColumnType("int(11)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(500)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnName("updated_at")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("luck_of_the_draw");
                });

            modelBuilder.Entity("DartLeague.Repositories.LeagueData.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<bool>("AcceptEmail")
                        .HasColumnName("acceptEmail")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("AcceptText")
                        .HasColumnName("acceptText")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Address1")
                        .HasColumnName("address1")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Address2")
                        .HasColumnName("address2")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CellPhone")
                        .HasColumnName("cellPhone")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("City")
                        .HasColumnName("city")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Email")
                        .HasColumnName("email")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnName("firstName")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("HomePhone")
                        .HasColumnName("homePhone")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnName("lastName")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("LeagueId")
                        .HasColumnName("leagueId")
                        .HasColumnType("int(11)");

                    b.Property<string>("Nickname")
                        .HasColumnName("nickname")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Notes")
                        .HasColumnName("notes")
                        .HasColumnType("text");

                    b.Property<string>("ShirtSize")
                        .HasColumnName("shirtSize")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("State")
                        .HasColumnName("state")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("UserId")
                        .HasColumnName("userId")
                        .HasColumnType("int(11)");

                    b.Property<string>("Zip")
                        .HasColumnName("zip")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("LeagueId")
                        .HasName("members_leagueid_index");

                    b.HasIndex("UserId")
                        .HasName("members_userid_index");

                    b.ToTable("members");
                });

            modelBuilder.Entity("DartLeague.Repositories.LeagueData.PagePart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description")
                        .HasColumnType("text");

                    b.Property<string>("Html")
                        .IsRequired()
                        .HasColumnName("html")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnName("updated_at")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("page_parts");
                });

            modelBuilder.Entity("DartLeague.Repositories.LeagueData.Sponsor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<string>("Address1")
                        .HasColumnName("address1")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Address2")
                        .HasColumnName("address2")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("City")
                        .HasColumnName("city")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Comments")
                        .HasColumnName("comments")
                        .HasColumnType("text");

                    b.Property<string>("ContactName")
                        .IsRequired()
                        .HasColumnName("contactName")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnName("email")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("FacebookUrl")
                        .HasColumnName("facebookUrl")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("LeagueId")
                        .HasColumnName("leagueId")
                        .HasColumnType("int(11)");

                    b.Property<string>("MapUrl")
                        .HasColumnName("mapUrl")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnName("phone")
                        .HasColumnType("varchar(25)");

                    b.Property<string>("State")
                        .HasColumnName("state")
                        .HasColumnType("varchar(2)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnName("type")
                        .HasColumnType("varchar(1)");

                    b.Property<string>("Url")
                        .HasColumnName("url")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Zip")
                        .HasColumnName("zip")
                        .HasColumnType("varchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("LeagueId")
                        .HasName("sponsors_leagueid_index");

                    b.ToTable("sponsors");
                });

            modelBuilder.Entity("DartLeague.Repositories.LeagueData.DartEventResult", b =>
                {
                    b.HasOne("DartLeague.Repositories.LeagueData.Member", "Member")
                        .WithMany()
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
