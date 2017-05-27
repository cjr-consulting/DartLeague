using System;
using System.Collections.Generic;

namespace DartLeague.Repositories.TrentonDartsModels
{
    public partial class WinterStatsPlayerGames
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Division { get; set; }
        public int GameId { get; set; }
        public int GameNumber { get; set; }
        public string GameType { get; set; }
        public bool IsForfeit { get; set; }
        public bool IsHome { get; set; }
        public bool IsWon { get; set; }
        public int MatchId { get; set; }
        public int NumberOfPlayers { get; set; }
        public int NumberOfPoints { get; set; }
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public int PlayerPosition { get; set; }
        public int SeasonId { get; set; }
        public string SeasonPart { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
    }
}
