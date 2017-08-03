using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DartLeague.Web.Data.Migrations.SeasonDb
{
    public partial class Addteamsandteam_players : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "teams",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    seasonId = table.Column<int>(type: "int(10) unsigned", nullable: false),
                    name = table.Column<string>(type: "varchar(500)", nullable: false),
                    abbreviation = table.Column<string>(type: "varchar(10)", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    createdBy = table.Column<int>(type: "int(10) unsigned", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updatedBy = table.Column<int>(type: "int(10) unsigned", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teams", x => x.id);
                    table.ForeignKey(
                        name: "FK_teams_seasons_seasonId",
                        column: x => x.seasonId,
                        principalTable: "seasons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "team_players",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    teamId = table.Column<int>(type: "int(10) unsigned", nullable: false),
                    memberId = table.Column<int>(type: "int(10) unsigned", nullable: false),
                    roleId = table.Column<int>(type: "int(10) unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_team_players", x => x.id);
                    table.ForeignKey(
                        name: "FK_team_players_teams_teamId",
                        column: x => x.teamId,
                        principalTable: "teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_teams_seasonId",
                table: "teams",
                column: "seasonId");

            migrationBuilder.CreateIndex(
                name: "IX_team_players_teamId",
                table: "team_players",
                column: "teamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "team_players");

            migrationBuilder.DropTable(
                name: "teams");
        }
    }
}
