using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace DartLeague.Web.Data.Migrations.LeagueDb
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Migration")]
    public partial class InitialSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "league");

            migrationBuilder.CreateTable(
                name: "DartEvents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address1 = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DartStart = table.Column<string>(nullable: true),
                    DartType = table.Column<string>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    EventContact = table.Column<string>(nullable: true),
                    EventContact2 = table.Column<string>(nullable: true),
                    EventDate = table.Column<DateTime>(nullable: false),
                    EventEndDate = table.Column<DateTime>(nullable: true),
                    EventTypeId = table.Column<int>(nullable: false),
                    FacebookUrl = table.Column<string>(nullable: true),
                    HostName = table.Column<string>(nullable: true),
                    HostPhone = table.Column<string>(nullable: true),
                    HostUrl = table.Column<string>(nullable: true),
                    ImageFileId = table.Column<int>(nullable: false),
                    IsTitleEvent = table.Column<bool>(nullable: false),
                    LocationName = table.Column<string>(nullable: true),
                    MapUrl = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PosterFile = table.Column<string>(nullable: true),
                    PosterFileId = table.Column<int>(nullable: false),
                    RegistrationEndTime = table.Column<string>(nullable: true),
                    RegistrationStartTime = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    Zip = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DartEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                schema: "league",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    FileId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BrowsableFiles",
                schema: "league",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Category = table.Column<string>(nullable: true),
                    ContentType = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    RelativePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrowsableFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeagueLinks",
                schema: "league",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    FileId = table.Column<int>(nullable: false),
                    LinkType = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeagueLinks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LuckOfTheDraws",
                schema: "league",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    FileId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LuckOfTheDraws", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                schema: "league",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AcceptEmail = table.Column<bool>(nullable: false),
                    AcceptText = table.Column<bool>(nullable: false),
                    Address1 = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    CellPhone = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    HomePhone = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    LeagueId = table.Column<int>(nullable: false),
                    Nickname = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    ShirtSize = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    Zip = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PageParts",
                schema: "league",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Html = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageParts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sponsors",
                schema: "league",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address1 = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Comments = table.Column<string>(nullable: true),
                    ContactName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FacebookUrl = table.Column<string>(nullable: true),
                    LeagueId = table.Column<int>(nullable: false),
                    MapUrl = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    Zip = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sponsors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DartEventResults",
                schema: "league",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EventId = table.Column<int>(nullable: false),
                    Finished = table.Column<string>(nullable: true),
                    MemberId = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: false),
                    SpecificEventName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DartEventResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DartEventResults_Members_MemberId",
                        column: x => x.MemberId,
                        principalSchema: "league",
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DartEventResults_MemberId",
                schema: "league",
                table: "DartEventResults",
                column: "MemberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DartEvents");

            migrationBuilder.DropTable(
                name: "Activities",
                schema: "league");

            migrationBuilder.DropTable(
                name: "BrowsableFiles",
                schema: "league");

            migrationBuilder.DropTable(
                name: "DartEventResults",
                schema: "league");

            migrationBuilder.DropTable(
                name: "LeagueLinks",
                schema: "league");

            migrationBuilder.DropTable(
                name: "LuckOfTheDraws",
                schema: "league");

            migrationBuilder.DropTable(
                name: "PageParts",
                schema: "league");

            migrationBuilder.DropTable(
                name: "Sponsors",
                schema: "league");

            migrationBuilder.DropTable(
                name: "Members",
                schema: "league");
        }
    }
}
