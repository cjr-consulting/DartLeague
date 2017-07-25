using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DartLeague.Web.Data.Migrations.LeagueDb
{
    public partial class renameofLuckOfTheDrawtabletoreflectothertablenamesconvention : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "LuckofTheDraws",
                newName: "luck_of_the_draw");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "luck_of_the_draw",
                newName: "LuckofTheDraws");
        }
    }
}
