using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DartLeague.Repositories.SeasonData;

namespace DartLeague.Web.Data.Migrations.SeasonDb
{
    [DbContext(typeof(SeasonContext))]
    [Migration("20170710042723_Add SeasonLinks table")]
    partial class AddSeasonLinkstable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

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
                        .HasColumnName("created_at")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatedBy")
                        .HasColumnName("created_by")
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
                        .HasColumnName("updated_at")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UpdatedBy")
                        .HasColumnName("updated_by")
                        .HasColumnType("int(10) unsigned");

                    b.Property<string>("Url")
                        .HasColumnName("url")
                        .HasColumnType("varchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("SeasonId");

                    b.ToTable("season_links");
                });

            modelBuilder.Entity("DartLeague.Repositories.SeasonData.SeasonLink", b =>
                {
                    b.HasOne("DartLeague.Repositories.SeasonData.Season")
                        .WithMany("SeasonLinks")
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
