using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DartLeague.Repositories.WinterSeasonData
{
    public partial class WinterSeasonContext : DbContext
    {
        public virtual DbSet<MatchTypeGameRules> MatchTypeGameRules { get; set; }
        public virtual DbSet<MatchTypes> MatchTypes { get; set; }
        public virtual DbSet<WinterGameAwards> WinterGameAwards { get; set; }
        public virtual DbSet<WinterGameResults> WinterGameResults { get; set; }
        public virtual DbSet<WinterMatchResults> WinterMatchResults { get; set; }
        public virtual DbSet<WinterSeasonMatches> WinterSeasonMatches { get; set; }
        public virtual DbSet<WinterSeasonPlayerPayments> WinterSeasonPlayerPayments { get; set; }
        public virtual DbSet<WinterSeasonTeamPayments> WinterSeasonTeamPayments { get; set; }
        public virtual DbSet<WinterSeasonTeamPlayers> WinterSeasonTeamPlayers { get; set; }
        public virtual DbSet<WinterSeasonTeams> WinterSeasonTeams { get; set; }
        public virtual DbSet<WinterSeasonWeeks> WinterSeasonWeeks { get; set; }
        public virtual DbSet<WinterSeasons> WinterSeasons { get; set; }
        public virtual DbSet<WinterStatsAwards> WinterStatsAwards { get; set; }
        public virtual DbSet<WinterStatsMatches> WinterStatsMatches { get; set; }
        public virtual DbSet<WinterStatsPlayerGames> WinterStatsPlayerGames { get; set; }
        public virtual DbSet<WinterStatsTeamGames> WinterStatsTeamGames { get; set; }

        public WinterSeasonContext(DbContextOptions<WinterSeasonContext> options)
            :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MatchTypeGameRules>(entity =>
            {
                entity.ToTable("match_type_game_rules");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.BestOfNumberOfLegs)
                    .HasColumnName("bestOfNumberOfLegs")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DoubleIn)
                    .HasColumnName("doubleIn")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.DoubleOut)
                    .HasColumnName("doubleOut")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.ForfeitIfNoPlayers)
                    .HasColumnName("forfeitIfNoPlayers")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.GamePointValue)
                    .HasColumnName("gamePointValue")
                    .HasColumnType("int(11)");

                entity.Property(e => e.GameType)
                    .IsRequired()
                    .HasColumnName("gameType")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasColumnName("groupName")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.LegPointValue)
                    .HasColumnName("legPointValue")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MatchTypeId)
                    .HasColumnName("matchTypeId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.NumberOfLegs)
                    .HasColumnName("numberOfLegs")
                    .HasColumnType("int(11)");

                entity.Property(e => e.NumberOfPlayers)
                    .HasColumnName("numberOfPlayers")
                    .HasColumnType("int(11)");

                entity.Property(e => e.OrderId)
                    .HasColumnName("orderId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.WhoStarts)
                    .IsRequired()
                    .HasColumnName("whoStarts")
                    .HasColumnType("varchar(1)");
            });
            modelBuilder.Entity<MatchTypes>(entity =>
            {
                entity.ToTable("match_types");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<WinterGameAwards>(entity =>
            {
                entity.ToTable("winter_game_awards");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.AwardType)
                    .IsRequired()
                    .HasColumnName("awardType")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.GameId)
                    .HasColumnName("gameId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PlayerId)
                    .HasColumnName("playerId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Value)
                    .HasColumnName("value")
                    .HasColumnType("int(11)");
            });
            modelBuilder.Entity<WinterGameResults>(entity =>
            {
                entity.ToTable("winter_game_results");

                entity.HasIndex(e => e.MatchId)
                    .HasName("game_results_matchid_index");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.AwayPlayers)
                    .IsRequired()
                    .HasColumnName("awayPlayers")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ForfeitedBy)
                    .IsRequired()
                    .HasColumnName("forfeitedBy")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.GameRuleId)
                    .HasColumnName("gameRuleId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.HomePlayers)
                    .IsRequired()
                    .HasColumnName("homePlayers")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Legs)
                    .IsRequired()
                    .HasColumnName("legs")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.MatchId)
                    .HasColumnName("matchId")
                    .HasColumnType("int(11)");
            });
            modelBuilder.Entity<WinterMatchResults>(entity =>
            {
                entity.ToTable("winter_match_results");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AwayScoreOverride)
                    .HasColumnName("awayScoreOverride")
                    .HasColumnType("int(11)");

                entity.Property(e => e.HasScorecard)
                    .HasColumnName("hasScorecard")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.HomeScoreOverride)
                    .HasColumnName("homeScoreOverride")
                    .HasColumnType("int(11)");
            });
            modelBuilder.Entity<WinterSeasonMatches>(entity =>
            {
                entity.ToTable("winter_season_matches");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.AwayTeamId)
                    .HasColumnName("awayTeamId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Division)
                    .IsRequired()
                    .HasColumnName("division")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.HomeTeamId)
                    .HasColumnName("homeTeamId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MatchTypeId)
                    .HasColumnName("matchTypeId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SeasonId)
                    .HasColumnName("seasonId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.WeekId)
                    .HasColumnName("weekId")
                    .HasColumnType("int(11)");
            });
            modelBuilder.Entity<WinterSeasonPlayerPayments>(entity =>
            {
                entity.ToTable("winter_season_player_payments");

                entity.HasIndex(e => e.SeasonId)
                    .HasName("season_player_payments_seasonid_index");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.PaymentStatus)
                    .IsRequired()
                    .HasColumnName("paymentStatus")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.PlayerId)
                    .HasColumnName("playerId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SeasonId)
                    .HasColumnName("seasonId")
                    .HasColumnType("int(11)");
            });
            modelBuilder.Entity<WinterSeasonTeamPayments>(entity =>
            {
                entity.ToTable("winter_season_team_payments");

                entity.HasIndex(e => e.SeasonId)
                    .HasName("season_team_payments_seasonid_index");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.PaymentStatus)
                    .IsRequired()
                    .HasColumnName("paymentStatus")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SeasonId)
                    .HasColumnName("seasonId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TeamId)
                    .HasColumnName("teamId")
                    .HasColumnType("int(11)");
            });
            modelBuilder.Entity<WinterSeasonTeamPlayers>(entity =>
            {
                entity.ToTable("winter_season_team_players");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.LeagueId)
                    .HasColumnName("leagueId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PlayerId)
                    .HasColumnName("playerId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasColumnName("role")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SeasonId)
                    .HasColumnName("seasonId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SeasonTeamId)
                    .HasColumnName("seasonTeamId")
                    .HasColumnType("int(11)");
            });
            modelBuilder.Entity<WinterSeasonTeams>(entity =>
            {
                entity.ToTable("winter_season_teams");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.PreSeasonDiv)
                    .IsRequired()
                    .HasColumnName("preSeasonDiv")
                    .HasColumnType("varchar(4)");

                entity.Property(e => e.RegularSeasonDiv)
                    .IsRequired()
                    .HasColumnName("regularSeasonDiv")
                    .HasColumnType("varchar(4)");

                entity.Property(e => e.SeasonId)
                    .HasColumnName("seasonId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TeamId)
                    .HasColumnName("teamId")
                    .HasColumnType("int(11)");
            });
            modelBuilder.Entity<WinterSeasonWeeks>(entity =>
            {
                entity.ToTable("winter_season_weeks");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.LeagueId)
                    .HasColumnName("leagueId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SeasonId)
                    .HasColumnName("seasonId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.WeekType)
                    .IsRequired()
                    .HasColumnName("weekType")
                    .HasColumnType("varchar(255)");
            });
            modelBuilder.Entity<WinterSeasons>(entity =>
            {
                entity.ToTable("winter_seasons");

                entity.HasIndex(e => e.LeagueId)
                    .HasName("seasons_leagueid_index");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.AccumulatePointsForAllParts)
                    .HasColumnName("accumulatePointsForAllParts")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.DefaultMatchTypeId)
                    .HasColumnName("defaultMatchTypeId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EndYear)
                    .HasColumnName("endYear")
                    .HasColumnType("int(11)");

                entity.Property(e => e.HalfPoints)
                    .HasColumnName("halfPoints")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsCurrent)
                    .HasColumnName("isCurrent")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.IsUsingMatchPoints)
                    .HasColumnName("isUsingMatchPoints")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.LeagueId)
                    .HasColumnName("leagueId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MinPointForHalfPoints)
                    .HasColumnName("minPointForHalfPoints")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SeasonType)
                    .IsRequired()
                    .HasColumnName("seasonType")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.StartYear)
                    .HasColumnName("startYear")
                    .HasColumnType("int(11)");

                entity.Property(e => e.WinPoints)
                    .HasColumnName("winPoints")
                    .HasColumnType("int(11)");
            });
            modelBuilder.Entity<WinterStatsAwards>(entity =>
            {
                entity.ToTable("winter_stats_awards");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.AwardId)
                    .HasColumnName("awardId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AwardType)
                    .IsRequired()
                    .HasColumnName("awardType")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Division)
                    .IsRequired()
                    .HasColumnName("division")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.GameId)
                    .HasColumnName("gameId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MatchId)
                    .HasColumnName("matchId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PlayerId)
                    .HasColumnName("playerId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PlayerName)
                    .IsRequired()
                    .HasColumnName("playerName")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SeasonId)
                    .HasColumnName("seasonId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SeasonPart)
                    .IsRequired()
                    .HasColumnName("seasonPart")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.TeamId)
                    .HasColumnName("teamId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TeamName)
                    .IsRequired()
                    .HasColumnName("teamName")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Value)
                    .HasColumnName("value")
                    .HasColumnType("int(11)");
            });
            modelBuilder.Entity<WinterStatsMatches>(entity =>
            {
                entity.ToTable("winter_stats_matches");

                entity.HasIndex(e => e.MatchId)
                    .HasName("stats_matches_matchid_index");

                entity.HasIndex(e => e.TeamId)
                    .HasName("stats_matches_teamid_index");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Division)
                    .IsRequired()
                    .HasColumnName("division")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.HasScorecard)
                    .HasColumnName("hasScorecard")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.HomeMatch)
                    .HasColumnName("homeMatch")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.MatchId)
                    .HasColumnName("matchId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MatchPoints)
                    .HasColumnName("matchPoints")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PointsLost)
                    .HasColumnName("pointsLost")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PointsWon)
                    .HasColumnName("pointsWon")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SeasonId)
                    .HasColumnName("seasonId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SeasonPart)
                    .IsRequired()
                    .HasColumnName("seasonPart")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.TeamId)
                    .HasColumnName("teamId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TeamName)
                    .IsRequired()
                    .HasColumnName("teamName")
                    .HasColumnType("varchar(255)");
            });
            modelBuilder.Entity<WinterStatsPlayerGames>(entity =>
            {
                entity.ToTable("winter_stats_player_games");

                entity.HasIndex(e => e.GameId)
                    .HasName("stats_player_games_gameid_index");

                entity.HasIndex(e => e.PlayerId)
                    .HasName("stats_player_games_playerid_index");

                entity.HasIndex(e => e.TeamId)
                    .HasName("stats_player_games_teamid_index");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Division)
                    .IsRequired()
                    .HasColumnName("division")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.GameId)
                    .HasColumnName("gameId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.GameNumber)
                    .HasColumnName("gameNumber")
                    .HasColumnType("int(11)");

                entity.Property(e => e.GameType)
                    .IsRequired()
                    .HasColumnName("gameType")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.IsForfeit)
                    .HasColumnName("isForfeit")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.IsHome)
                    .HasColumnName("isHome")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.IsWon)
                    .HasColumnName("isWon")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.MatchId)
                    .HasColumnName("matchId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.NumberOfPlayers)
                    .HasColumnName("numberOfPlayers")
                    .HasColumnType("int(11)");

                entity.Property(e => e.NumberOfPoints)
                    .HasColumnName("numberOfPoints")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PlayerId)
                    .HasColumnName("playerId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PlayerName)
                    .IsRequired()
                    .HasColumnName("playerName")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.PlayerPosition)
                    .HasColumnName("playerPosition")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SeasonId)
                    .HasColumnName("seasonId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SeasonPart)
                    .IsRequired()
                    .HasColumnName("seasonPart")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.TeamId)
                    .HasColumnName("teamId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TeamName)
                    .IsRequired()
                    .HasColumnName("teamName")
                    .HasColumnType("varchar(255)");
            });
            modelBuilder.Entity<WinterStatsTeamGames>(entity =>
            {
                entity.ToTable("winter_stats_team_games");

                entity.HasIndex(e => e.GameId)
                    .HasName("stats_team_games_gameid_index");

                entity.HasIndex(e => e.TeamId)
                    .HasName("stats_team_games_teamid_index");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Division)
                    .IsRequired()
                    .HasColumnName("division")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.GameId)
                    .HasColumnName("gameId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.GameType)
                    .IsRequired()
                    .HasColumnName("gameType")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.IsForfeitGame)
                    .HasColumnName("isForfeitGame")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.IsWon)
                    .HasColumnName("isWon")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.MatchId)
                    .HasColumnName("matchId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.NumberOfPlayers)
                    .HasColumnName("numberOfPlayers")
                    .HasColumnType("int(11)");

                entity.Property(e => e.NumberOfPoints)
                    .HasColumnName("numberOfPoints")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SeasonId)
                    .HasColumnName("seasonId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SeasonPart)
                    .IsRequired()
                    .HasColumnName("seasonPart")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.TeamId)
                    .HasColumnName("teamId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TeamName)
                    .IsRequired()
                    .HasColumnName("teamName")
                    .HasColumnType("varchar(255)");
            });
        }
    }
}