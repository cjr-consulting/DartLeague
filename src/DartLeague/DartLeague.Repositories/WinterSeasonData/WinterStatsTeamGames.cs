using System;
using System.Collections.Generic;

namespace DartLeague.Repositories.WinterSeasonData
{
    public partial class WinterStatsTeamGames
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Division { get; set; }
        public int GameId { get; set; }
        public string GameType { get; set; }
        public bool IsForfeitGame { get; set; }
        public bool IsWon { get; set; }
        public int MatchId { get; set; }
        public int NumberOfPlayers { get; set; }
        public int NumberOfPoints { get; set; }
        public int SeasonId { get; set; }
        public string SeasonPart { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
    }
}
