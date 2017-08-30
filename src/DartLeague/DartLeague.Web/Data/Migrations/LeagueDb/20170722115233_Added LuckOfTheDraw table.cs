using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DartLeague.Web.Data.Migrations.LeagueDb
{
    public partial class AddedLuckOfTheDrawtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LuckofTheDraws",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    eventDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    fileId = table.Column<int>(type: "int(11)", nullable: false),
                    title = table.Column<string>(type: "varchar(500)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LuckofTheDraws", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LuckofTheDraws");
        }
    }
}
