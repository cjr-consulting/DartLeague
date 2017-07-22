using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DartLeague.Web.Data.Migrations.SeasonDb
{
    public partial class AddBoardMemberstables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "updated_by",
                table: "season_links",
                newName: "updatedBy");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "season_links",
                newName: "updatedAt");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "season_links",
                newName: "createdBy");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "season_links",
                newName: "createdAt");

            migrationBuilder.CreateTable(
                name: "board_positions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    orderId = table.Column<int>(type: "int(10) unsigned", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    createdBy = table.Column<int>(type: "int(10) unsigned", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updatedBy = table.Column<int>(type: "int(10) unsigned", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_board_positions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "board_members",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    seasonId = table.Column<int>(type: "int(10) unsigned", nullable: false),
                    memberId = table.Column<int>(type: "int(10) unsigned", nullable: false),
                    positionId = table.Column<int>(type: "int(10) unsigned", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    createdBy = table.Column<int>(type: "int(10) unsigned", nullable: false),
                    deletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updatedBy = table.Column<int>(type: "int(10) unsigned", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_board_members", x => x.id);
                    table.ForeignKey(
                        name: "FK_board_members_board_positions_positionId",
                        column: x => x.positionId,
                        principalTable: "board_positions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_board_members_seasons_seasonId",
                        column: x => x.seasonId,
                        principalTable: "seasons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_board_members_positionId",
                table: "board_members",
                column: "positionId");

            migrationBuilder.CreateIndex(
                name: "IX_board_members_seasonId",
                table: "board_members",
                column: "seasonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "updatedBy",
                table: "season_links",
                newName: "updated_by");

            migrationBuilder.RenameColumn(
                name: "updatedAt",
                table: "season_links",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "createdBy",
                table: "season_links",
                newName: "created_by");

            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "season_links",
                newName: "created_at");

            migrationBuilder.DropTable(
                name: "board_members");

            migrationBuilder.DropTable(
                name: "board_positions");
        }
    }
}
