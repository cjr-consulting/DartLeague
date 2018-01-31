using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DartLeague.Repositories.SeasonData;

namespace DartLeague.Web.Data.Migrations.SeasonDb
{
    [DbContext(typeof(SeasonContext))]
    partial class SeasonContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("DartLeague.Repositories.SeasonData.BoardMember", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("createdAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatedBy")
                        .HasColumnName("createdBy")
                        .HasColumnType("int(10) unsigned");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnName("deletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("DeletedBy");

                    b.Property<int>("MemberId")
                        .HasColumnName("memberId")
                        .HasColumnType("int(10) unsigned");

                    b.Property<int>("PositionId")
                        .HasColumnName("positionId")
                        .HasColumnType("int(10) unsigned");

                    b.Property<int>("SeasonId")
                        .HasColumnName("seasonId")
                        .HasColumnType("int(10) unsigned");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnName("updatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnName("updatedBy")
                        .HasColumnType("int(10) unsigned");

                    b.HasKey("Id");

                    b.HasIndex("PositionId");

                    b.HasIndex("SeasonId");

                    b.ToTable("board_members");
                });

            modelBuilder.Entity("DartLeague.Repositories.SeasonData.BoardPosition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("createdAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatedBy")
                        .HasColumnName("createdBy")
                        .HasColumnType("int(10) unsigned");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Order")
                        .HasColumnName("orderId")
                        .HasColumnType("int(10) unsigned");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnName("updatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnName("updatedBy")
                        .HasColumnType("int(10) unsigned");

                    b.HasKey("Id");

                    b.ToTable("board_positions");
                });

            modelBuilder.Entity("DartLeague.Repositories.SeasonData.Season", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("createdAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatedBy")
                        .HasColumnName("createdBy")
                        .HasColumnType("int(10) unsigned");

                    b.Property<DateTime>("EndDate")
                        .HasColumnName("endDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnName("startDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnName("title")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnName("updatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnName("updatedBy")
                        .HasColumnType("int(10) unsigned");

                    b.HasKey("Id");

                    b.ToTable("seasons");
                });

            modelBuilder.Entity("DartLeague.Repositories.SeasonData.SeasonLink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("createdAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatedBy")
                        .HasColumnName("createdBy")
                        .HasColumnType("int(10) unsigned");

                    b.Property<int>("FileId")
                        .HasColumnName("fileId")
                        .HasColumnType("int(11)");

                    b.Property<int>("LinkType")
                        .HasColumnName("linkType")
                        .HasColumnType("int(11)");

                    b.Property<int>("Order")
                        .HasColumnName("order")
                        .HasColumnType("int(11)");

                    b.Property<int>("SeasonId")
                        .HasColumnName("seasonId")
                        .HasColumnType("int(10) unsigned");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnName("title")
                        .HasColumnType("varchar(500)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnName("updatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UpdatedBy")
                        .HasColumnName("updatedBy")
                        .HasColumnType("int(10) unsigned");

                    b.Property<string>("Url")
                        .HasColumnName("url")
                        .HasColumnType("varchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("SeasonId");

                    b.ToTable("season_links");
                });

            modelBuilder.Entity("DartLeague.Repositories.SeasonData.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasColumnName("abbreviation")
                        .HasColumnType("varchar(10)");

                    b.Property<int>("BannerImageId")
                        .HasColumnName("bannerImageId")
                        .HasColumnType("int(11) unsigned");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("createdAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatedBy")
                        .HasColumnName("createdBy")
                        .HasColumnType("int(10) unsigned");

                    b.Property<int>("LogoImageId")
                        .HasColumnName("logoImageId")
                        .HasColumnType("int(11) unsigned");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(500)");

                    b.Property<int>("SeasonId")
                        .HasColumnName("seasonId")
                        .HasColumnType("int(10) unsigned");

                    b.Property<int>("SponsorId")
                        .HasColumnName("sponsorId")
                        .HasColumnType("int(10) unsigned");

                    b.Property<int>("TeamPictureImageId")
                        .HasColumnName("teamPictureImageId")
                        .HasColumnType("int(11) unsigned");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnName("updatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnName("updatedBy")
                        .HasColumnType("int(10) unsigned");

                    b.HasKey("Id");

                    b.HasIndex("SeasonId");

                    b.ToTable("teams");
                });

            modelBuilder.Entity("DartLeague.Repositories.SeasonData.TeamPlayer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<int>("MemberId")
                        .HasColumnName("memberId")
                        .HasColumnType("int(10) unsigned");

                    b.Property<int>("RoleId")
                        .HasColumnName("roleId")
                        .HasColumnType("int(10) unsigned");

                    b.Property<int>("TeamId")
                        .HasColumnName("teamId")
                        .HasColumnType("int(10) unsigned");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("team_players");
                });

            modelBuilder.Entity("DartLeague.Repositories.SeasonData.BoardMember", b =>
                {
                    b.HasOne("DartLeague.Repositories.SeasonData.BoardPosition", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DartLeague.Repositories.SeasonData.Season", "Season")
                        .WithMany("BoardMembers")
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DartLeague.Repositories.SeasonData.SeasonLink", b =>
                {
                    b.HasOne("DartLeague.Repositories.SeasonData.Season", "Season")
                        .WithMany("SeasonLinks")
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DartLeague.Repositories.SeasonData.Team", b =>
                {
                    b.HasOne("DartLeague.Repositories.SeasonData.Season", "Season")
                        .WithMany("Teams")
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DartLeague.Repositories.SeasonData.TeamPlayer", b =>
                {
                    b.HasOne("DartLeague.Repositories.SeasonData.Team", "Team")
                        .WithMany("Players")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
