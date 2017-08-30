using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DartLeague.Web.Data.Migrations.LeagueDb
{
    public partial class ChangedLuckOfTheDrawtitlecolumntoname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "title",
                table: "luck_of_the_draw",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "luck_of_the_draw",
                newName: "title");
        }
    }
}
