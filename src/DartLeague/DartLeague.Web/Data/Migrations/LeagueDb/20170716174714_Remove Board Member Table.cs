using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DartLeague.Web.Data.Migrations.LeagueDb
{
    public partial class RemoveBoardMemberTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "board_members");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "board_members_leagueid_index",
                table: "board_members",
                column: "leagueId");
        }
    }
}
