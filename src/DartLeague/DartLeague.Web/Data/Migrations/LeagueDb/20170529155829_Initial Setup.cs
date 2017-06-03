using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DartLeague.Web.Data.Migrations.LeagueDb
{
    public partial class InitialSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "board_members",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    endSeasonId = table.Column<long>(type: "bigint(20)", nullable: true),
                    endingSeason = table.Column<string>(type: "varchar(255)", nullable: true),
                    leagueId = table.Column<int>(type: "int(11)", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    position = table.Column<string>(type: "varchar(255)", nullable: false),
                    startSeasonId = table.Column<long>(type: "bigint(20)", nullable: true),
                    startingSeason = table.Column<string>(type: "varchar(255)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    userId = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_board_members", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "browsable_files",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    category = table.Column<string>(type: "varchar(255)", nullable: false),
                    fileName = table.Column<string>(type: "varchar(255)", nullable: false),
                    mimeType = table.Column<string>(type: "varchar(255)", nullable: false),
                    relativePath = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_browsable_files", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "dart_event_results",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    eventId = table.Column<int>(type: "int(11)", nullable: false),
                    finished = table.Column<string>(type: "varchar(255)", nullable: false),
                    orderId = table.Column<int>(type: "int(11)", nullable: false),
                    playerId = table.Column<int>(type: "int(11)", nullable: false),
                    specificEventName = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dart_event_results", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "dart_events",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    address1 = table.Column<string>(type: "varchar(255)", nullable: false),
                    address2 = table.Column<string>(type: "varchar(255)", nullable: false),
                    city = table.Column<string>(type: "varchar(255)", nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    dartStart = table.Column<string>(type: "varchar(255)", nullable: false),
                    dartType = table.Column<string>(type: "varchar(255)", nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    description = table.Column<string>(nullable: false),
                    eventContact = table.Column<string>(type: "varchar(255)", nullable: false),
                    eventContact2 = table.Column<string>(type: "varchar(255)", nullable: false),
                    eventTypeId = table.Column<int>(type: "int(11)", nullable: false),
                    facebookUrl = table.Column<string>(type: "varchar(255)", nullable: false),
                    hostName = table.Column<string>(type: "varchar(255)", nullable: false),
                    hostPhone = table.Column<string>(type: "varchar(255)", nullable: false),
                    hostUrl = table.Column<string>(type: "varchar(255)", nullable: false),
                    imageFileId = table.Column<int>(type: "int(11)", nullable: false),
                    isTitleEvent = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    locationName = table.Column<string>(type: "varchar(255)", nullable: false),
                    mapUrl = table.Column<string>(type: "varchar(255)", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    posterFile = table.Column<string>(type: "varchar(255)", nullable: false),
                    posterFileId = table.Column<int>(type: "int(11)", nullable: false),
                    registrationEndTime = table.Column<string>(type: "varchar(255)", nullable: false),
                    registrationStartTime = table.Column<string>(type: "varchar(255)", nullable: false),
                    state = table.Column<string>(type: "varchar(255)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    url = table.Column<string>(type: "varchar(255)", nullable: false),
                    zip = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dart_events", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "page_parts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    description = table.Column<string>(type: "text", nullable: false),
                    html = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_page_parts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "players",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    acceptEmail = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    acceptText = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    address1 = table.Column<string>(type: "varchar(255)", nullable: false),
                    address2 = table.Column<string>(type: "varchar(255)", nullable: false),
                    cellPhone = table.Column<string>(type: "varchar(255)", nullable: false),
                    city = table.Column<string>(type: "varchar(255)", nullable: false),
                    email = table.Column<string>(type: "varchar(255)", nullable: false),
                    firstName = table.Column<string>(type: "varchar(255)", nullable: false),
                    homePhone = table.Column<string>(type: "varchar(255)", nullable: false),
                    lastName = table.Column<string>(type: "varchar(255)", nullable: false),
                    leagueId = table.Column<int>(type: "int(11)", nullable: false),
                    nickname = table.Column<string>(type: "varchar(255)", nullable: false),
                    notes = table.Column<string>(type: "text", nullable: false),
                    shirtSize = table.Column<string>(type: "varchar(255)", nullable: false),
                    state = table.Column<string>(type: "varchar(255)", nullable: false),
                    userId = table.Column<int>(type: "int(11)", nullable: false),
                    zip = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_players", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sponsors",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    address1 = table.Column<string>(type: "varchar(255)", nullable: false),
                    address2 = table.Column<string>(type: "varchar(255)", nullable: false),
                    city = table.Column<string>(type: "varchar(255)", nullable: false),
                    comments = table.Column<string>(type: "text", nullable: false),
                    contactName = table.Column<string>(type: "varchar(255)", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "varchar(255)", nullable: false),
                    facebookUrl = table.Column<string>(type: "varchar(255)", nullable: false),
                    leagueId = table.Column<int>(type: "int(11)", nullable: false),
                    mapUrl = table.Column<string>(type: "varchar(255)", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    phone = table.Column<string>(type: "varchar(25)", nullable: false),
                    state = table.Column<string>(type: "varchar(2)", nullable: false),
                    type = table.Column<string>(type: "varchar(1)", nullable: false),
                    url = table.Column<string>(type: "varchar(255)", nullable: false),
                    zip = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sponsors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "teams",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    leagueId = table.Column<int>(type: "int(11)", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    notes = table.Column<string>(type: "text", nullable: false),
                    sponsorId = table.Column<int>(type: "int(10) unsigned", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teams", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "board_members_leagueid_index",
                table: "board_members",
                column: "leagueId");

            migrationBuilder.CreateIndex(
                name: "players_leagueid_index",
                table: "players",
                column: "leagueId");

            migrationBuilder.CreateIndex(
                name: "players_userid_index",
                table: "players",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "sponsors_leagueid_index",
                table: "sponsors",
                column: "leagueId");

            migrationBuilder.CreateIndex(
                name: "teams_leagueid_index",
                table: "teams",
                column: "leagueId");

            migrationBuilder.CreateIndex(
                name: "teams_sponsorid_index",
                table: "teams",
                column: "sponsorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "board_members");

            migrationBuilder.DropTable(
                name: "browsable_files");

            migrationBuilder.DropTable(
                name: "dart_event_results");

            migrationBuilder.DropTable(
                name: "dart_events");

            migrationBuilder.DropTable(
                name: "page_parts");

            migrationBuilder.DropTable(
                name: "players");

            migrationBuilder.DropTable(
                name: "sponsors");

            migrationBuilder.DropTable(
                name: "teams");
        }
    }
}
