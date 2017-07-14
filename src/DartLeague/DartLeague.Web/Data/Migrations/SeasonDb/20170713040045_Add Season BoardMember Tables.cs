using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DartLeague.Web.Data.Migrations.SeasonDb
{
    public partial class AddSeasonBoardMemberTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "board_positions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    createdBy = table.Column<int>(type: "int(10) unsigned", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
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
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    createdBy = table.Column<int>(type: "int(10) unsigned", nullable: false),
                    deletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    memberId = table.Column<int>(type: "int(10) unsigned", nullable: false),
                    positionId = table.Column<int>(type: "int(10) unsigned", nullable: false),
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
                });

            migrationBuilder.CreateIndex(
                name: "IX_board_members_positionId",
                table: "board_members",
                column: "positionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "board_members");

            migrationBuilder.DropTable(
                name: "board_positions");
        }
    }
}
