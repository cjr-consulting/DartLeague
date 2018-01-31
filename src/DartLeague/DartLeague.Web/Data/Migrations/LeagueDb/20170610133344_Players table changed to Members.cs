using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DartLeague.Web.Data.Migrations.LeagueDb
{
    public partial class PlayerstablechangedtoMembers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "players");

            migrationBuilder.CreateTable(
                name: "members",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    acceptEmail = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    acceptText = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    address1 = table.Column<string>(type: "varchar(255)", nullable: true),
                    address2 = table.Column<string>(type: "varchar(255)", nullable: true),
                    cellPhone = table.Column<string>(type: "varchar(255)", nullable: true),
                    city = table.Column<string>(type: "varchar(255)", nullable: true),
                    email = table.Column<string>(type: "varchar(255)", nullable: true),
                    firstName = table.Column<string>(type: "varchar(255)", nullable: false),
                    homePhone = table.Column<string>(type: "varchar(255)", nullable: true),
                    lastName = table.Column<string>(type: "varchar(255)", nullable: false),
                    leagueId = table.Column<int>(type: "int(11)", nullable: false),
                    nickname = table.Column<string>(type: "varchar(255)", nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true),
                    shirtSize = table.Column<string>(type: "varchar(255)", nullable: true),
                    state = table.Column<string>(type: "varchar(255)", nullable: true),
                    userId = table.Column<int>(type: "int(11)", nullable: false),
                    zip = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_members", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "members_leagueid_index",
                table: "members",
                column: "leagueId");

            migrationBuilder.CreateIndex(
                name: "members_userid_index",
                table: "members",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "members");

            migrationBuilder.CreateTable(
                name: "players",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    acceptEmail = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    acceptText = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    address1 = table.Column<string>(type: "varchar(255)", nullable: true),
                    address2 = table.Column<string>(type: "varchar(255)", nullable: true),
                    cellPhone = table.Column<string>(type: "varchar(255)", nullable: true),
                    city = table.Column<string>(type: "varchar(255)", nullable: true),
                    email = table.Column<string>(type: "varchar(255)", nullable: true),
                    firstName = table.Column<string>(type: "varchar(255)", nullable: false),
                    homePhone = table.Column<string>(type: "varchar(255)", nullable: true),
                    lastName = table.Column<string>(type: "varchar(255)", nullable: false),
                    leagueId = table.Column<int>(type: "int(11)", nullable: false),
                    nickname = table.Column<string>(type: "varchar(255)", nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true),
                    shirtSize = table.Column<string>(type: "varchar(255)", nullable: true),
                    state = table.Column<string>(type: "varchar(255)", nullable: true),
                    userId = table.Column<int>(type: "int(11)", nullable: false),
                    zip = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_players", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "players_leagueid_index",
                table: "players",
                column: "leagueId");

            migrationBuilder.CreateIndex(
                name: "players_userid_index",
                table: "players",
                column: "userId");
        }
    }
}
