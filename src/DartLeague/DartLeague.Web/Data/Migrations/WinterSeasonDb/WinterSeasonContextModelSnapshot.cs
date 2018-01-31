using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DartLeague.Repositories.WinterSeasonData;

namespace DartLeague.Web.Data.Migrations.WinterSeasonDb
{
    [DbContext(typeof(WinterSeasonContext))]
    partial class WinterSeasonContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("DartLeague.Repositories.WinterSeasonData.MatchTypeGameRules", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<int>("BestOfNumberOfLegs")
                        .HasColumnName("bestOfNumberOfLegs")
                        .HasColumnType("int(11)");

                    b.Property<bool>("DoubleIn")
                        .HasColumnName("doubleIn")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("DoubleOut")
                        .HasColumnName("doubleOut")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("ForfeitIfNoPlayers")
                        .HasColumnName("forfeitIfNoPlayers")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("GamePointValue")
                        .HasColumnName("gamePointValue")
                        .HasColumnType("int(11)");

                    b.Property<string>("GameType")
                        .IsRequired()
                        .HasColumnName("gameType")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasColumnName("groupName")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("LegPointValue")
                        .HasColumnName("legPointValue")
                        .HasColumnType("int(11)");

                    b.Property<int>("MatchTypeId")
                        .HasColumnName("matchTypeId")
                        .HasColumnType("int(11)");

                    b.Property<int>("NumberOfLegs")
                        .HasColumnName("numberOfLegs")
                        .HasColumnType("int(11)");

                    b.Property<int>("NumberOfPlayers")
                        .HasColumnName("numberOfPlayers")
                        .HasColumnType("int(11)");

                    b.Property<int>("OrderId")
                        .HasColumnName("orderId")
                        .HasColumnType("int(11)");

                    b.Property<string>("WhoStarts")
                        .IsRequired()
                        .HasColumnName("whoStarts")
                        .HasColumnType("varchar(1)");

                    b.HasKey("Id");

                    b.ToTable("match_type_game_rules");
                });

            modelBuilder.Entity("DartLeague.Repositories.WinterSeasonData.MatchTypes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("match_types");
                });

            modelBuilder.Entity("DartLeague.Repositories.WinterSeasonData.WinterGameAwards", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<string>("AwardType")
                        .IsRequired()
                        .HasColumnName("awardType")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("GameId")
                        .HasColumnName("gameId")
                        .HasColumnType("int(11)");

                    b.Property<int>("PlayerId")
                        .HasColumnName("playerId")
                        .HasColumnType("int(11)");

                    b.Property<int>("Value")
                        .HasColumnName("value")
                        .HasColumnType("int(11)");

                    b.HasKey("Id");

                    b.ToTable("winter_game_awards");
                });

            modelBuilder.Entity("DartLeague.Repositories.WinterSeasonData.WinterGameResults", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<string>("AwayPlayers")
                        .IsRequired()
                        .HasColumnName("awayPlayers")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ForfeitedBy")
                        .IsRequired()
                        .HasColumnName("forfeitedBy")
                        .HasColumnType("varchar(50)");

                    b.Property<int>("GameRuleId")
                        .HasColumnName("gameRuleId")
                        .HasColumnType("int(11)");

                    b.Property<string>("HomePlayers")
                        .IsRequired()
                        .HasColumnName("homePlayers")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Legs")
                        .IsRequired()
                        .HasColumnName("legs")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("MatchId")
                        .HasColumnName("matchId")
                        .HasColumnType("int(11)");

                    b.HasKey("Id");

                    b.HasIndex("MatchId")
                        .HasName("game_results_matchid_index");

                    b.ToTable("winter_game_results");
                });

            modelBuilder.Entity("DartLeague.Repositories.WinterSeasonData.WinterMatchResults", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(11)");

                    b.Property<int>("AwayScoreOverride")
                        .HasColumnName("awayScoreOverride")
                        .HasColumnType("int(11)");

                    b.Property<bool>("HasScorecard")
                        .HasColumnName("hasScorecard")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("HomeScoreOverride")
                        .HasColumnName("homeScoreOverride")
                        .HasColumnType("int(11)");

                    b.HasKey("Id");

                    b.ToTable("winter_match_results");
                });

            modelBuilder.Entity("DartLeague.Repositories.WinterSeasonData.WinterSeasonMatches", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<int>("AwayTeamId")
                        .HasColumnName("awayTeamId")
                        .HasColumnType("int(11)");

                    b.Property<string>("Division")
                        .IsRequired()
                        .HasColumnName("division")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("HomeTeamId")
                        .HasColumnName("homeTeamId")
                        .HasColumnType("int(11)");

                    b.Property<int>("MatchTypeId")
                        .HasColumnName("matchTypeId")
                        .HasColumnType("int(11)");

                    b.Property<int>("SeasonId")
                        .HasColumnName("seasonId")
                        .HasColumnType("int(11)");

                    b.Property<int>("WeekId")
                        .HasColumnName("weekId")
                        .HasColumnType("int(11)");

                    b.HasKey("Id");

                    b.ToTable("winter_season_matches");
                });

            modelBuilder.Entity("DartLeague.Repositories.WinterSeasonData.WinterSeasonPlayerPayments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<string>("PaymentStatus")
                        .IsRequired()
                        .HasColumnName("paymentStatus")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("PlayerId")
                        .HasColumnName("playerId")
                        .HasColumnType("int(11)");

                    b.Property<int>("SeasonId")
                        .HasColumnName("seasonId")
                        .HasColumnType("int(11)");

                    b.HasKey("Id");

                    b.HasIndex("SeasonId")
                        .HasName("season_player_payments_seasonid_index");

                    b.ToTable("winter_season_player_payments");
                });

            modelBuilder.Entity("DartLeague.Repositories.WinterSeasonData.WinterSeasons", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<bool>("AccumulatePointsForAllParts")
                        .HasColumnName("accumulatePointsForAllParts")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("DefaultMatchTypeId")
                        .HasColumnName("defaultMatchTypeId")
                        .HasColumnType("int(11)");

                    b.Property<int>("EndYear")
                        .HasColumnName("endYear")
                        .HasColumnType("int(11)");

                    b.Property<int>("HalfPoints")
                        .HasColumnName("halfPoints")
                        .HasColumnType("int(11)");

                    b.Property<bool>("IsCurrent")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("isCurrent")
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValueSql("0");

                    b.Property<bool>("IsUsingMatchPoints")
                        .HasColumnName("isUsingMatchPoints")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("LeagueId")
                        .HasColumnName("leagueId")
                        .HasColumnType("int(11)");

                    b.Property<int>("MinPointForHalfPoints")
                        .HasColumnName("minPointForHalfPoints")
                        .HasColumnType("int(11)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("SeasonType")
                        .IsRequired()
                        .HasColumnName("seasonType")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("StartYear")
                        .HasColumnName("startYear")
                        .HasColumnType("int(11)");

                    b.Property<int>("WinPoints")
                        .HasColumnName("winPoints")
                        .HasColumnType("int(11)");

                    b.HasKey("Id");

                    b.HasIndex("LeagueId")
                        .HasName("seasons_leagueid_index");

                    b.ToTable("winter_seasons");
                });

            modelBuilder.Entity("DartLeague.Repositories.WinterSeasonData.WinterSeasonTeamPayments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<string>("PaymentStatus")
                        .IsRequired()
                        .HasColumnName("paymentStatus")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("SeasonId")
                        .HasColumnName("seasonId")
                        .HasColumnType("int(11)");

                    b.Property<int>("TeamId")
                        .HasColumnName("teamId")
                        .HasColumnType("int(11)");

                    b.HasKey("Id");

                    b.HasIndex("SeasonId")
                        .HasName("season_team_payments_seasonid_index");

                    b.ToTable("winter_season_team_payments");
                });

            modelBuilder.Entity("DartLeague.Repositories.WinterSeasonData.WinterSeasonTeamPlayers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<int>("LeagueId")
                        .HasColumnName("leagueId")
                        .HasColumnType("int(11)");

                    b.Property<int>("PlayerId")
                        .HasColumnName("playerId")
                        .HasColumnType("int(11)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnName("role")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("SeasonId")
                        .HasColumnName("seasonId")
                        .HasColumnType("int(11)");

                    b.Property<int>("SeasonTeamId")
                        .HasColumnName("seasonTeamId")
                        .HasColumnType("int(11)");

                    b.HasKey("Id");

                    b.ToTable("winter_season_team_players");
                });

            modelBuilder.Entity("DartLeague.Repositories.WinterSeasonData.WinterSeasonTeams", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<string>("PreSeasonDiv")
                        .IsRequired()
                        .HasColumnName("preSeasonDiv")
                        .HasColumnType("varchar(4)");

                    b.Property<string>("RegularSeasonDiv")
                        .IsRequired()
                        .HasColumnName("regularSeasonDiv")
                        .HasColumnType("varchar(4)");

                    b.Property<int>("SeasonId")
                        .HasColumnName("seasonId")
                        .HasColumnType("int(11)");

                    b.Property<int>("TeamId")
                        .HasColumnName("teamId")
                        .HasColumnType("int(11)");

                    b.HasKey("Id");

                    b.ToTable("winter_season_teams");
                });

            modelBuilder.Entity("DartLeague.Repositories.WinterSeasonData.WinterSeasonWeeks", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<int>("LeagueId")
                        .HasColumnName("leagueId")
                        .HasColumnType("int(11)");

                    b.Property<int>("SeasonId")
                        .HasColumnName("seasonId")
                        .HasColumnType("int(11)");

                    b.Property<string>("WeekType")
                        .IsRequired()
                        .HasColumnName("weekType")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("winter_season_weeks");
                });

            modelBuilder.Entity("DartLeague.Repositories.WinterSeasonData.WinterStatsAwards", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<int>("AwardId")
                        .HasColumnName("awardId")
                        .HasColumnType("int(11)");

                    b.Property<string>("AwardType")
                        .IsRequired()
                        .HasColumnName("awardType")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("Date")
                        .HasColumnName("date")
                        .HasColumnType("datetime");

                    b.Property<string>("Division")
                        .IsRequired()
                        .HasColumnName("division")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("GameId")
                        .HasColumnName("gameId")
                        .HasColumnType("int(11)");

                    b.Property<int>("MatchId")
                        .HasColumnName("matchId")
                        .HasColumnType("int(11)");

                    b.Property<int>("PlayerId")
                        .HasColumnName("playerId")
                        .HasColumnType("int(11)");

                    b.Property<string>("PlayerName")
                        .IsRequired()
                        .HasColumnName("playerName")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("SeasonId")
                        .HasColumnName("seasonId")
                        .HasColumnType("int(11)");

                    b.Property<string>("SeasonPart")
                        .IsRequired()
                        .HasColumnName("seasonPart")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("TeamId")
                        .HasColumnName("teamId")
                        .HasColumnType("int(11)");

                    b.Property<string>("TeamName")
                        .IsRequired()
                        .HasColumnName("teamName")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Value")
                        .HasColumnName("value")
                        .HasColumnType("int(11)");

                    b.HasKey("Id");

                    b.ToTable("winter_stats_awards");
                });

            modelBuilder.Entity("DartLeague.Repositories.WinterSeasonData.WinterStatsMatches", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<DateTime>("Date")
                        .HasColumnName("date")
                        .HasColumnType("datetime");

                    b.Property<string>("Division")
                        .IsRequired()
                        .HasColumnName("division")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("HasScorecard")
                        .HasColumnName("hasScorecard")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("HomeMatch")
                        .HasColumnName("homeMatch")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("MatchId")
                        .HasColumnName("matchId")
                        .HasColumnType("int(11)");

                    b.Property<int>("MatchPoints")
                        .HasColumnName("matchPoints")
                        .HasColumnType("int(11)");

                    b.Property<int>("PointsLost")
                        .HasColumnName("pointsLost")
                        .HasColumnType("int(11)");

                    b.Property<int>("PointsWon")
                        .HasColumnName("pointsWon")
                        .HasColumnType("int(11)");

                    b.Property<int>("SeasonId")
                        .HasColumnName("seasonId")
                        .HasColumnType("int(11)");

                    b.Property<string>("SeasonPart")
                        .IsRequired()
                        .HasColumnName("seasonPart")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("TeamId")
                        .HasColumnName("teamId")
                        .HasColumnType("int(11)");

                    b.Property<string>("TeamName")
                        .IsRequired()
                        .HasColumnName("teamName")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("MatchId")
                        .HasName("stats_matches_matchid_index");

                    b.HasIndex("TeamId")
                        .HasName("stats_matches_teamid_index");

                    b.ToTable("winter_stats_matches");
                });

            modelBuilder.Entity("DartLeague.Repositories.WinterSeasonData.WinterStatsPlayerGames", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<DateTime>("Date")
                        .HasColumnName("date")
                        .HasColumnType("datetime");

                    b.Property<string>("Division")
                        .IsRequired()
                        .HasColumnName("division")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("GameId")
                        .HasColumnName("gameId")
                        .HasColumnType("int(11)");

                    b.Property<int>("GameNumber")
                        .HasColumnName("gameNumber")
                        .HasColumnType("int(11)");

                    b.Property<string>("GameType")
                        .IsRequired()
                        .HasColumnName("gameType")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("IsForfeit")
                        .HasColumnName("isForfeit")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsHome")
                        .HasColumnName("isHome")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsWon")
                        .HasColumnName("isWon")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("MatchId")
                        .HasColumnName("matchId")
                        .HasColumnType("int(11)");

                    b.Property<int>("NumberOfPlayers")
                        .HasColumnName("numberOfPlayers")
                        .HasColumnType("int(11)");

                    b.Property<int>("NumberOfPoints")
                        .HasColumnName("numberOfPoints")
                        .HasColumnType("int(11)");

                    b.Property<int>("PlayerId")
                        .HasColumnName("playerId")
                        .HasColumnType("int(11)");

                    b.Property<string>("PlayerName")
                        .IsRequired()
                        .HasColumnName("playerName")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("PlayerPosition")
                        .HasColumnName("playerPosition")
                        .HasColumnType("int(11)");

                    b.Property<int>("SeasonId")
                        .HasColumnName("seasonId")
                        .HasColumnType("int(11)");

                    b.Property<string>("SeasonPart")
                        .IsRequired()
                        .HasColumnName("seasonPart")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("TeamId")
                        .HasColumnName("teamId")
                        .HasColumnType("int(11)");

                    b.Property<string>("TeamName")
                        .IsRequired()
                        .HasColumnName("teamName")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("GameId")
                        .HasName("stats_player_games_gameid_index");

                    b.HasIndex("PlayerId")
                        .HasName("stats_player_games_playerid_index");

                    b.HasIndex("TeamId")
                        .HasName("stats_player_games_teamid_index");

                    b.ToTable("winter_stats_player_games");
                });

            modelBuilder.Entity("DartLeague.Repositories.WinterSeasonData.WinterStatsTeamGames", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(10) unsigned");

                    b.Property<DateTime>("Date")
                        .HasColumnName("date")
                        .HasColumnType("datetime");

                    b.Property<string>("Division")
                        .IsRequired()
                        .HasColumnName("division")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("GameId")
                        .HasColumnName("gameId")
                        .HasColumnType("int(11)");

                    b.Property<string>("GameType")
                        .IsRequired()
                        .HasColumnName("gameType")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("IsForfeitGame")
                        .HasColumnName("isForfeitGame")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsWon")
                        .HasColumnName("isWon")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("MatchId")
                        .HasColumnName("matchId")
                        .HasColumnType("int(11)");

                    b.Property<int>("NumberOfPlayers")
                        .HasColumnName("numberOfPlayers")
                        .HasColumnType("int(11)");

                    b.Property<int>("NumberOfPoints")
                        .HasColumnName("numberOfPoints")
                        .HasColumnType("int(11)");

                    b.Property<int>("SeasonId")
                        .HasColumnName("seasonId")
                        .HasColumnType("int(11)");

                    b.Property<string>("SeasonPart")
                        .IsRequired()
                        .HasColumnName("seasonPart")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("TeamId")
                        .HasColumnName("teamId")
                        .HasColumnType("int(11)");

                    b.Property<string>("TeamName")
                        .IsRequired()
                        .HasColumnName("teamName")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("GameId")
                        .HasName("stats_team_games_gameid_index");

                    b.HasIndex("TeamId")
                        .HasName("stats_team_games_teamid_index");

                    b.ToTable("winter_stats_team_games");
                });
        }
    }
}
