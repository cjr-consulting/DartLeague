using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DartLeague.Web.Data.Migrations.LeagueDb
{
    public partial class OndartEventResultchangedplayerIdtoMemberId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "playerId",
                table: "dart_event_results");

            migrationBuilder.AddColumn<int>(
                name: "memberId",
                table: "dart_event_results",
                type: "int(10) unsigned",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_dart_event_results_memberId",
                table: "dart_event_results",
                column: "memberId");

            migrationBuilder.AddForeignKey(
                name: "FK_dart_event_results_members_memberId",
                table: "dart_event_results",
                column: "memberId",
                principalTable: "members",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dart_event_results_members_memberId",
                table: "dart_event_results");

            migrationBuilder.DropIndex(
                name: "IX_dart_event_results_memberId",
                table: "dart_event_results");

            migrationBuilder.DropColumn(
                name: "memberId",
                table: "dart_event_results");

            migrationBuilder.AddColumn<int>(
                name: "playerId",
                table: "dart_event_results",
                type: "int(11)",
                nullable: false,
                defaultValue: 0);
        }
    }
}
