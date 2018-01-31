using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DartLeague.Web.Data.Migrations.WinterSeasonDb
{
    public partial class InitialSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "match_type_game_rules",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    bestOfNumberOfLegs = table.Column<int>(type: "int(11)", nullable: false),
                    doubleIn = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    doubleOut = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    forfeitIfNoPlayers = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    gamePointValue = table.Column<int>(type: "int(11)", nullable: false),
                    gameType = table.Column<string>(type: "varchar(255)", nullable: false),
                    groupName = table.Column<string>(type: "varchar(255)", nullable: false),
                    legPointValue = table.Column<int>(type: "int(11)", nullable: false),
                    matchTypeId = table.Column<int>(type: "int(11)", nullable: false),
                    numberOfLegs = table.Column<int>(type: "int(11)", nullable: false),
                    numberOfPlayers = table.Column<int>(type: "int(11)", nullable: false),
                    orderId = table.Column<int>(type: "int(11)", nullable: false),
                    whoStarts = table.Column<string>(type: "varchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_match_type_game_rules", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "match_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    name = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_match_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "seasons",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    isCurrent = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    leagueId = table.Column<int>(type: "int(11)", nullable: false),
                    linkName = table.Column<string>(type: "varchar(255)", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_seasons", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "winter_game_awards",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    awardType = table.Column<string>(type: "varchar(255)", nullable: false),
                    gameId = table.Column<int>(type: "int(11)", nullable: false),
                    playerId = table.Column<int>(type: "int(11)", nullable: false),
                    value = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_winter_game_awards", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "winter_game_results",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    awayPlayers = table.Column<string>(type: "varchar(255)", nullable: false),
                    forfeitedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    gameRuleId = table.Column<int>(type: "int(11)", nullable: false),
                    homePlayers = table.Column<string>(type: "varchar(255)", nullable: false),
                    legs = table.Column<string>(type: "varchar(255)", nullable: false),
                    matchId = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_winter_game_results", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "winter_match_results",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    awayScoreOverride = table.Column<int>(type: "int(11)", nullable: false),
                    hasScorecard = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    homeScoreOverride = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_winter_match_results", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "winter_season_matches",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    awayTeamId = table.Column<int>(type: "int(11)", nullable: false),
                    division = table.Column<string>(type: "varchar(255)", nullable: false),
                    homeTeamId = table.Column<int>(type: "int(11)", nullable: false),
                    matchTypeId = table.Column<int>(type: "int(11)", nullable: false),
                    seasonId = table.Column<int>(type: "int(11)", nullable: false),
                    weekId = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_winter_season_matches", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "winter_season_player_payments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    paymentStatus = table.Column<string>(type: "varchar(255)", nullable: false),
                    playerId = table.Column<int>(type: "int(11)", nullable: false),
                    seasonId = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_winter_season_player_payments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "winter_seasons",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    accumulatePointsForAllParts = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    defaultMatchTypeId = table.Column<int>(type: "int(11)", nullable: false),
                    endYear = table.Column<int>(type: "int(11)", nullable: false),
                    halfPoints = table.Column<int>(type: "int(11)", nullable: false),
                    isCurrent = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    isUsingMatchPoints = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    leagueId = table.Column<int>(type: "int(11)", nullable: false),
                    minPointForHalfPoints = table.Column<int>(type: "int(11)", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    seasonType = table.Column<string>(type: "varchar(255)", nullable: false),
                    startYear = table.Column<int>(type: "int(11)", nullable: false),
                    winPoints = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_winter_seasons", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "winter_season_team_payments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    paymentStatus = table.Column<string>(type: "varchar(255)", nullable: false),
                    seasonId = table.Column<int>(type: "int(11)", nullable: false),
                    teamId = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_winter_season_team_payments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "winter_season_team_players",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    leagueId = table.Column<int>(type: "int(11)", nullable: false),
                    playerId = table.Column<int>(type: "int(11)", nullable: false),
                    role = table.Column<string>(type: "varchar(255)", nullable: false),
                    seasonId = table.Column<int>(type: "int(11)", nullable: false),
                    seasonTeamId = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_winter_season_team_players", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "winter_season_teams",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    preSeasonDiv = table.Column<string>(type: "varchar(4)", nullable: false),
                    regularSeasonDiv = table.Column<string>(type: "varchar(4)", nullable: false),
                    seasonId = table.Column<int>(type: "int(11)", nullable: false),
                    teamId = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_winter_season_teams", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "winter_season_weeks",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    leagueId = table.Column<int>(type: "int(11)", nullable: false),
                    seasonId = table.Column<int>(type: "int(11)", nullable: false),
                    weekType = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_winter_season_weeks", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "winter_stats_awards",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    awardId = table.Column<int>(type: "int(11)", nullable: false),
                    awardType = table.Column<string>(type: "varchar(255)", nullable: false),
                    date = table.Column<DateTime>(type: "datetime", nullable: false),
                    division = table.Column<string>(type: "varchar(255)", nullable: false),
                    gameId = table.Column<int>(type: "int(11)", nullable: false),
                    matchId = table.Column<int>(type: "int(11)", nullable: false),
                    playerId = table.Column<int>(type: "int(11)", nullable: false),
                    playerName = table.Column<string>(type: "varchar(255)", nullable: false),
                    seasonId = table.Column<int>(type: "int(11)", nullable: false),
                    seasonPart = table.Column<string>(type: "varchar(255)", nullable: false),
                    teamId = table.Column<int>(type: "int(11)", nullable: false),
                    teamName = table.Column<string>(type: "varchar(255)", nullable: false),
                    value = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_winter_stats_awards", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "winter_stats_matches",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    date = table.Column<DateTime>(type: "datetime", nullable: false),
                    division = table.Column<string>(type: "varchar(255)", nullable: false),
                    hasScorecard = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    homeMatch = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    matchId = table.Column<int>(type: "int(11)", nullable: false),
                    matchPoints = table.Column<int>(type: "int(11)", nullable: false),
                    pointsLost = table.Column<int>(type: "int(11)", nullable: false),
                    pointsWon = table.Column<int>(type: "int(11)", nullable: false),
                    seasonId = table.Column<int>(type: "int(11)", nullable: false),
                    seasonPart = table.Column<string>(type: "varchar(255)", nullable: false),
                    teamId = table.Column<int>(type: "int(11)", nullable: false),
                    teamName = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_winter_stats_matches", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "winter_stats_player_games",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    date = table.Column<DateTime>(type: "datetime", nullable: false),
                    division = table.Column<string>(type: "varchar(255)", nullable: false),
                    gameId = table.Column<int>(type: "int(11)", nullable: false),
                    gameNumber = table.Column<int>(type: "int(11)", nullable: false),
                    gameType = table.Column<string>(type: "varchar(255)", nullable: false),
                    isForfeit = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    isHome = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    isWon = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    matchId = table.Column<int>(type: "int(11)", nullable: false),
                    numberOfPlayers = table.Column<int>(type: "int(11)", nullable: false),
                    numberOfPoints = table.Column<int>(type: "int(11)", nullable: false),
                    playerId = table.Column<int>(type: "int(11)", nullable: false),
                    playerName = table.Column<string>(type: "varchar(255)", nullable: false),
                    playerPosition = table.Column<int>(type: "int(11)", nullable: false),
                    seasonId = table.Column<int>(type: "int(11)", nullable: false),
                    seasonPart = table.Column<string>(type: "varchar(255)", nullable: false),
                    teamId = table.Column<int>(type: "int(11)", nullable: false),
                    teamName = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_winter_stats_player_games", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "winter_stats_team_games",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    date = table.Column<DateTime>(type: "datetime", nullable: false),
                    division = table.Column<string>(type: "varchar(255)", nullable: false),
                    gameId = table.Column<int>(type: "int(11)", nullable: false),
                    gameType = table.Column<string>(type: "varchar(255)", nullable: false),
                    isForfeitGame = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    isWon = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    matchId = table.Column<int>(type: "int(11)", nullable: false),
                    numberOfPlayers = table.Column<int>(type: "int(11)", nullable: false),
                    numberOfPoints = table.Column<int>(type: "int(11)", nullable: false),
                    seasonId = table.Column<int>(type: "int(11)", nullable: false),
                    seasonPart = table.Column<string>(type: "varchar(255)", nullable: false),
                    teamId = table.Column<int>(type: "int(11)", nullable: false),
                    teamName = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_winter_stats_team_games", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "game_results_matchid_index",
                table: "winter_game_results",
                column: "matchId");

            migrationBuilder.CreateIndex(
                name: "season_player_payments_seasonid_index",
                table: "winter_season_player_payments",
                column: "seasonId");

            migrationBuilder.CreateIndex(
                name: "seasons_leagueid_index",
                table: "winter_seasons",
                column: "leagueId");

            migrationBuilder.CreateIndex(
                name: "season_team_payments_seasonid_index",
                table: "winter_season_team_payments",
                column: "seasonId");

            migrationBuilder.CreateIndex(
                name: "stats_matches_matchid_index",
                table: "winter_stats_matches",
                column: "matchId");

            migrationBuilder.CreateIndex(
                name: "stats_matches_teamid_index",
                table: "winter_stats_matches",
                column: "teamId");

            migrationBuilder.CreateIndex(
                name: "stats_player_games_gameid_index",
                table: "winter_stats_player_games",
                column: "gameId");

            migrationBuilder.CreateIndex(
                name: "stats_player_games_playerid_index",
                table: "winter_stats_player_games",
                column: "playerId");

            migrationBuilder.CreateIndex(
                name: "stats_player_games_teamid_index",
                table: "winter_stats_player_games",
                column: "teamId");

            migrationBuilder.CreateIndex(
                name: "stats_team_games_gameid_index",
                table: "winter_stats_team_games",
                column: "gameId");

            migrationBuilder.CreateIndex(
                name: "stats_team_games_teamid_index",
                table: "winter_stats_team_games",
                column: "teamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "match_type_game_rules");

            migrationBuilder.DropTable(
                name: "match_types");

            migrationBuilder.DropTable(
                name: "seasons");

            migrationBuilder.DropTable(
                name: "winter_game_awards");

            migrationBuilder.DropTable(
                name: "winter_game_results");

            migrationBuilder.DropTable(
                name: "winter_match_results");

            migrationBuilder.DropTable(
                name: "winter_season_matches");

            migrationBuilder.DropTable(
                name: "winter_season_player_payments");

            migrationBuilder.DropTable(
                name: "winter_seasons");

            migrationBuilder.DropTable(
                name: "winter_season_team_payments");

            migrationBuilder.DropTable(
                name: "winter_season_team_players");

            migrationBuilder.DropTable(
                name: "winter_season_teams");

            migrationBuilder.DropTable(
                name: "winter_season_weeks");

            migrationBuilder.DropTable(
                name: "winter_stats_awards");

            migrationBuilder.DropTable(
                name: "winter_stats_matches");

            migrationBuilder.DropTable(
                name: "winter_stats_player_games");

            migrationBuilder.DropTable(
                name: "winter_stats_team_games");
        }
    }
}
