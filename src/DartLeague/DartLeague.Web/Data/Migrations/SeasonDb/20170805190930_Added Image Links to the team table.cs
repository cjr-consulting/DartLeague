using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DartLeague.Web.Data.Migrations.SeasonDb
{
    public partial class AddedImageLinkstotheteamtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "bannerImageId",
                table: "teams",
                type: "int(11) unsigned",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "logoImageId",
                table: "teams",
                type: "int(11) unsigned",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "teamPictureImageId",
                table: "teams",
                type: "int(11) unsigned",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bannerImageId",
                table: "teams");

            migrationBuilder.DropColumn(
                name: "logoImageId",
                table: "teams");

            migrationBuilder.DropColumn(
                name: "teamPictureImageId",
                table: "teams");
        }
    }
}
