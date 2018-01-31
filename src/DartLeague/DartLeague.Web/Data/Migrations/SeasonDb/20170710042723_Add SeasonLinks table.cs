using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DartLeague.Web.Data.Migrations.SeasonDb
{
    public partial class AddSeasonLinkstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "season_links",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    seasonId = table.Column<int>(type: "int(10) unsigned", nullable: false),
                    title = table.Column<string>(type: "varchar(500)", nullable: false),
                    linkType = table.Column<int>(type: "int(11)", nullable: false),
                    fileId = table.Column<int>(type: "int(11)", nullable: false),
                    url = table.Column<string>(type: "varchar(500)", nullable: true),
                    order = table.Column<int>(type: "int(11)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    created_by = table.Column<int>(type: "int(10) unsigned", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_by = table.Column<int>(type: "int(10) unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_season_links", x => x.id);
                    table.ForeignKey(
                        name: "FK_season_links_seasons_seasonId",
                        column: x => x.seasonId,
                        principalTable: "seasons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_season_links_seasonId",
                table: "season_links",
                column: "seasonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "season_links");
        }
    }
}
