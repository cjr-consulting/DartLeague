using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DartLeague.Web.Data.Migrations.LeagueDb
{
    public partial class BrowsableFilesupdatemimetypetocontentType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "mimeType",
                table: "browsable_files",
                newName: "contentType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "contentType",
                table: "browsable_files",
                newName: "mimeType");
        }
    }
}
