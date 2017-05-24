<?php
/**
 * Created by PhpStorm.
 * User: johnmeade
 * Date: 9/9/15
 * Time: 2:40 PM
 */

namespace TrentonDarts\Stats;


use Illuminate\Database\Eloquent\Collection;
use Illuminate\Support\Facades\DB;
use TrentonDarts\LeagueManagement\Models\WinterSeason;
use TrentonDarts\LeagueManagement\Models\WinterSeasonMatch;
use TrentonDarts\MatchDomain\Models\MatchResult;
use TrentonDarts\Stats\Models\PlayerGameStat;
use TrentonDarts\Stats\Models\PlayerStat;

class PlayerGameRepository
{
    private static function removePlayerStatsNoLongerPartOfGame($game)
    {
        $playerStats = PlayerGameStat::where('gameId', $game->id)->get();
        $gamePlayers = array_merge($game->awayPlayers, $game->homePlayers);
        foreach($playerStats as $playerStat)
        {
            $found = false;
            foreach($gamePlayers as $player)
            {
                if($player != null && $playerStat->playerId == $player->id) {
                    $found = true;
                    continue;
                }
            }

            if(!$found)
                $playerStat->delete();
        }
    }

    public function updatePlayerGameStats(MatchResult $result)
    {
        foreach ($result->getGames() as $game) {
            self::removePlayerStatsNoLongerPartOfGame($game);
            $playerPosition = 1;
            foreach($game->awayPlayers as $player) {
                if($player == null)
                    continue;
                $playerGameAwayStat = PlayerGameStat::where('playerId', $player->id)->where('gameId', $game->id)->where('teamId', $result->awayTeamId)->first();

                if (!$playerGameAwayStat)
                    $playerGameAwayStat = new PlayerGameStat();

                $playerGameAwayStat->seasonId = $result->seasonId;
                $playerGameAwayStat->seasonPart = $result->seasonPart;
                $playerGameAwayStat->matchId = $result->getMatchId();
                $playerGameAwayStat->division = $result->division;
                $playerGameAwayStat->date = $result->date;
                $playerGameAwayStat->gameId = $game->id;
                $playerGameAwayStat->teamId = $result->awayTeamId;
                $playerGameAwayStat->teamName = $result->awayTeamName;
                $playerGameAwayStat->playerId = $player->id;
                $playerGameAwayStat->playerName = $player->name;
                $playerGameAwayStat->playerPosition = $playerPosition;
                $playerGameAwayStat->gameType = $game->gameRules->gameType;
                $playerGameAwayStat->numberOfPlayers = $game->gameRules->numberOfPlayers;
                $playerGameAwayStat->numberOfPoints = $game->getAwayScore() == 0 ? $game->getHomeScore() : $game->getAwayScore();
                $playerGameAwayStat->isWon = $game->getAwayScore() > $game->getHomeScore();
                $playerGameAwayStat->isForfeit = $game->forfeitedBy == 'home';
                $playerGameAwayStat->isHome = false;
                $playerGameAwayStat->gameNumber = $game->gameRules->orderId;
                $playerGameAwayStat->save();
                $playerPosition = $playerPosition + 1;
            }

            $playerPosition = 1;
            foreach($game->homePlayers as $player) {
                if($player == null)
                    continue;
                $playerGameHomeStat = PlayerGameStat::where('playerId', $player->id)->where('gameId', $game->id)->where('teamId', $result->homeTeamId)->first();

                if (!$playerGameHomeStat)
                    $playerGameHomeStat = new PlayerGameStat();

                $playerGameHomeStat->seasonId = $result->seasonId;
                $playerGameHomeStat->seasonPart = $result->seasonPart;
                $playerGameHomeStat->matchId = $result->getMatchId();
                $playerGameHomeStat->division = $result->division;
                $playerGameHomeStat->date = $result->date;
                $playerGameHomeStat->gameId = $game->id;
                $playerGameHomeStat->teamId = $result->homeTeamId;
                $playerGameHomeStat->teamName = $result->homeTeamName;
                $playerGameHomeStat->playerId = $player->id;
                $playerGameHomeStat->playerName = $player->name;
                $playerGameHomeStat->playerPosition = $playerPosition;
                $playerGameHomeStat->gameType = $game->gameRules->gameType;
                $playerGameHomeStat->numberOfPlayers = $game->gameRules->numberOfPlayers;
                $playerGameHomeStat->numberOfPoints = $game->getHomeScore() == 0 ? $game->getAwayScore() : $game->getHomeScore();
                $playerGameHomeStat->isWon = $game->getAwayScore() < $game->getHomeScore();
                $playerGameHomeStat->isForfeit =  $game->forfeitedBy == 'away';
                $playerGameHomeStat->isHome = true;
                $playerGameHomeStat->gameNumber = $game->gameRules->orderId;
                $playerGameHomeStat->save();
                $playerPosition = $playerPosition + 1;
            }
        }
    }

    public function getPlayerStatsForSeasonPart_old($id, $seasonPart, $division)
    {
        $query = DB::table('winter_season_team_players')
            ->leftJoin('players', 'winter_season_team_players.playerId','=','players.id')
            ->leftJoin('winter_season_teams', 'winter_season_team_players.seasonTeamId', '=' ,'winter_season_teams.id')
            ->leftJoin('teams', 'winter_season_teams.teamId', '=', 'teams.id')
            ->leftJoin('winter_stats_player_games', 'players.id', '=', 'winter_stats_player_games.playerId')
            ->select(DB::raw('teams.id as teamId, teams.name as teamName, players.Id as playerId, CONCAT(players.firstName, \' \', players.lastName) as playerName,
            SUM(CASE WHEN winter_stats_player_games.playerId is not null AND isForfeit = 0 THEN 1 ELSE 0 END) as gamesPlayed,
            SUM(CASE WHEN winter_stats_player_games.playerId is not null AND isForfeit = 0 AND isWon = 1 THEN numberOfPoints ELSE 0 END) as pointsWon,
            SUM(CASE WHEN winter_stats_player_games.playerId is not null AND isWon = 0 THEN numberOfPoints ELSE 0 END) as pointsLost,
            SUM(CASE WHEN winter_stats_player_games.playerId is not null AND isForfeit = 0 AND isWon = 1 THEN 1 ELSE 0 END) as gamesWon,
            SUM(CASE WHEN winter_stats_player_games.playerId is not null AND isWon = 0 THEN 1 ELSE 0 END) as gamesLost,
            SUM(CASE WHEN winter_stats_player_games.playerId is not null AND gameType = \'cricket\' AND isForfeit = 0 THEN 1 ELSE 0 END) as gameCricketPlayed,
            SUM(CASE WHEN winter_stats_player_games.playerId is not null AND gameType = \'301\' AND isForfeit = 0 THEN 1 ELSE 0 END) as game301Played,
            SUM(CASE WHEN winter_stats_player_games.playerId is not null AND gameType = \'501\' AND isForfeit = 0 THEN 1 ELSE 0 END) as game501Played,
            SUM(CASE WHEN winter_stats_player_games.playerId is not null AND gameTYpe = \'doublecricket\' AND isForfeit = 0 THEN 1 ELSE 0 END) as gameDoubleCricketPlayed,
            SUM(CASE WHEN winter_stats_player_games.playerId is not null AND gameType = \'801\' AND isForfeit = 0 THEN 1 ELSE 0 END) as game801Played,
            SUM(CASE WHEN winter_stats_player_games.playerId is not null AND gameType = \'cricket\' AND isForfeit = 0 AND isWon = 1 THEN 1 ELSE 0 END) as gameCricketWon,
            SUM(CASE WHEN winter_stats_player_games.playerId is not null AND gameType = \'301\' AND isForfeit = 0 AND isWon = 1 THEN 1 ELSE 0 END) as game301Won,
            SUM(CASE WHEN winter_stats_player_games.playerId is not null AND gameType = \'501\' AND isForfeit = 0 AND isWon = 1 THEN 1 ELSE 0 END) as game501Won,
            SUM(CASE WHEN winter_stats_player_games.playerId is not null AND gameTYpe = \'doublecricket\' AND isForfeit = 0 AND isWon = 1 THEN 1 ELSE 0 END) as gameDoubleCricketWon,
            SUM(CASE WHEN winter_stats_player_games.playerId is not null AND gameType = \'801\' AND isForfeit = 0 AND isWon = 1 THEN 1 ELSE 0 END) as game801Won,
            SUM(CASE WHEN winter_stats_player_games.playerId is not null AND gameType = \'cricket\' AND isForfeit = 0 AND isWon = 0 THEN 1 ELSE 0 END) as gameCricketLost,
            SUM(CASE WHEN winter_stats_player_games.playerId is not null AND gameType = \'301\' AND isForfeit = 0 AND isWon = 0 THEN 1 ELSE 0 END) as game301Lost,
            SUM(CASE WHEN winter_stats_player_games.playerId is not null AND gameType = \'501\' AND isForfeit = 0 AND isWon = 0 THEN 1 ELSE 0 END) as game501Lost,
            SUM(CASE WHEN winter_stats_player_games.playerId is not null AND gameTYpe = \'doublecricket\' AND isForfeit = 0 AND isWon = 0 THEN 1 ELSE 0 END) as gameDoubleCricketLost,
            SUM(CASE WHEN winter_stats_player_games.playerId is not null AND gameType = \'801\' AND isForfeit = 0 AND isWon = 0 THEN 1 ELSE 0 END) as game801Lost'))
            ->where('winter_season_teams.seasonId', $id)
            ->where(function($query)use($id){
                $query->whereNull('winter_stats_player_games.seasonId')
                    ->orWhere('winter_stats_player_games.seasonId', $id);
            });

        if(isset($division) && $division != null)
            $query->where(function($query)use($division){
                $query->where('winter_season_teams.preSeasonDiv', $division)
                    ->orWhere('winter_season_teams.regularSeasonDiv', $division);
            });


        if($seasonPart != 'whole') {
            $query->where(function($query)use($seasonPart){
                $query->whereNull('winter_stats_player_games.seasonPart')
                    ->orWhere('winter_stats_player_games.seasonPart', $seasonPart);
            });
        }

        $query->groupBy('teamId')
            ->groupBy('teams.name')
            ->groupBy('playerId')
            ->groupBy('players.firstName')
            ->groupBy('players.lastName');

        $query->orderBy('teamName');

        $teamGameStats = $query->get();
        $stats = [];

        foreach ($teamGameStats as $stat)
        {
            $newStat = new PlayerStat();
            $newStat->seasonId = $id;
            $newStat->seasonPart = $seasonPart;
            $newStat->teamId = $stat->teamId;
            $newStat->teamName = $stat->teamName;
            $newStat->playerId = $stat->playerId;
            $newStat->playerName = $stat->playerName;

            $newStat->gamesPlayed = $stat->gamesPlayed;
            $newStat->weeksPlayed = PlayerGameStat::where('seasonId', $id)->where('playerId', $stat->playerId)->groupBy('date')->get()->count();

            $newStat->pointsWon = $stat->pointsWon;
            $newStat->pointsLost = $stat->pointsLost;
            $newStat->gamesWon = $stat->gamesWon;
            $newStat->gamesLost = $stat->gamesLost;

            $newStat->overallWin = 0;
            if($stat->gamesWon + $stat->gamesLost != 0)
                $newStat->overallWin = $stat->gamesWon / ($stat->gamesWon + $stat->gamesLost);

            $newStat->singlesWin = 0;
            if(($stat->game301Played + $stat->gameCricketPlayed) != 0)
                $newStat->singlesWin = ($stat->game301Won + $stat->gameCricketWon) / ($stat->game301Played + $stat->gameCricketPlayed);

            $newStat->doublesWin = 0;
            if(($stat->game501Played + $stat->gameDoubleCricketPlayed) != 0)
                $newStat->doublesWin = ($stat->game501Won + $stat->gameDoubleCricketWon) / ($stat->game501Played + $stat->gameDoubleCricketPlayed);

            $newStat->game801->eight01Win = 0;
            if($stat->game801Played != 0)
                $newStat->eight01Win = ($stat->game801Won / $stat->game801Played);

            $newStat->cricketWin = 0;
            if(($stat->gameCricketPlayed + $stat->gameDoubleCricketPlayed) != 0)
                $newStat->cricketWin = ($stat->gameCricketWon + $stat->gameDoubleCricketWon) / ($stat->gameCricketPlayed + $stat->gameDoubleCricketPlayed);

            $newStat->oh1Win = 0;
            if(($stat->game301Played + $stat->game501Played + $stat->game801Played) != 0)
                $newStat->oh1Win = ($stat->game301Won + $stat->game501Won + $stat->game801Won) / ($stat->game301Played + $stat->game501Played + $stat->game801Played);

            if($stat->game301Played != 0)
                $newStat->singles301Win = $stat->game301Won / $stat->game301Played;

            if($stat->gameCricketPlayed != 0)
                $newStat->singlesCricketWin = $stat->gameCricketWon / $stat->gameCricketPlayed;

            if($stat->gameDoubleCricketPlayed != 0)
                $newStat->doublesCricketWin = $stat->gameDoubleCricketWon / $stat->gameDoubleCricketPlayed;

            if($stat->game501Played != 0)
                $newStat->doubles501Win = $stat->game501Won / $stat->game501Played;

            if($stat->game801Played != 0)
                $newStat->triples801Win = $stat->game801Won / $stat->game801Played;

            $newStat->game301->played = $stat->game301Played;
            $newStat->game301->won = $stat->game301Won;
            $newStat->game301->lost = $stat->game301Lost;

            $newStat->gameCricket->played = $stat->gameCricketPlayed;
            $newStat->gameCricket->won = $stat->gameCricketWon;
            $newStat->gameCricket->lost = $stat->gameCricketLost;

            $newStat->game501->played = $stat->game501Played;
            $newStat->game501->won = $stat->game501Won;
            $newStat->game501->lost = $stat->game501Lost;

            $newStat->gameDoubleCricket->played = $stat->gameDoubleCricketPlayed;
            $newStat->gameDoubleCricket->won = $stat->gameDoubleCricketWon;
            $newStat->gameDoubleCricket->lost = $stat->gameDoubleCricketLost;

            $newStat->game801->played = $stat->game801Played;
            $newStat->game801->won = $stat->game801Won;
            $newStat->game801->lost = $stat->game801Lost;

            array_push($stats, $newStat);
        }

        return new Collection($stats);
    }

    public function getPlayerStatsForSeasonPart($id, $seasonPart, $division)
    {
        $query = DB::table('winter_season_team_players')
            ->leftJoin('players', 'winter_season_team_players.playerId','=','players.id')
            ->leftJoin('winter_season_teams', 'winter_season_team_players.seasonTeamId', '=' ,'winter_season_teams.id')
            ->leftJoin('teams', 'winter_season_teams.teamId', '=', 'teams.id')
            ->leftJoin('winter_stats_player_games', 'players.id', '=', 'winter_stats_player_games.playerId')
            ->select(DB::raw('teams.id as teamId, teams.name as teamName, players.Id as playerId, CONCAT(players.firstName, \' \', players.lastName) as playerName,
            SUM(CASE WHEN winter_stats_player_games.playerId is not null AND isForfeit = 0 THEN 1 ELSE 0 END) as gamesPlayed,
            SUM(CASE WHEN winter_stats_player_games.playerId is not null AND isForfeit = 0 AND isWon = 1 THEN numberOfPoints ELSE 0 END) as pointsWon,
            SUM(CASE WHEN winter_stats_player_games.playerId is not null AND isWon = 0 THEN numberOfPoints ELSE 0 END) as pointsLost,
            SUM(CASE WHEN winter_stats_player_games.playerId is not null AND isForfeit = 0 AND isWon = 1 THEN 1 ELSE 0 END) as gamesWon,
            SUM(CASE WHEN winter_stats_player_games.playerId is not null AND isWon = 0 THEN 1 ELSE 0 END) as gamesLost,
            SUM(CASE WHEN winter_stats_player_games.playerId is not null AND gameType = \'cricket\' AND isForfeit = 0 THEN 1 ELSE 0 END) as gameCricketPlayed,
            SUM(CASE WHEN winter_stats_player_games.playerId is not null AND gameType like \'%01\' AND numberOfPlayers = 1 AND isForfeit = 0 THEN 1 ELSE 0 END) as gameSingle01Played,
            SUM(CASE WHEN winter_stats_player_games.playerId is not null AND gameType = \'double501\' AND isForfeit = 0 THEN 1 ELSE 0 END) as gameDouble501Played,
            SUM(CASE WHEN winter_stats_player_games.playerId is not null AND gameTYpe = \'doublecricket\' AND isForfeit = 0 THEN 1 ELSE 0 END) as gameDoubleCricketPlayed,
            SUM(CASE WHEN winter_stats_player_games.playerId is not null AND gameType like \'%01\' AND numberOfPlayers = 3 AND isForfeit = 0 THEN 1 ELSE 0 END) as gameTriple01Played,
            SUM(CASE WHEN winter_stats_player_games.playerId is not null AND gameType = \'cricket\' AND isForfeit = 0 AND isWon = 1 THEN 1 ELSE 0 END) as gameCricketWon,
            SUM(CASE WHEN winter_stats_player_games.playerId is not null AND gameType like \'%01\' AND numberOfPlayers = 1 AND isForfeit = 0 AND isWon = 1 THEN 1 ELSE 0 END) as gameSingle01Won,
            SUM(CASE WHEN winter_stats_player_games.playerId is not null AND gameType = \'double501\' AND isForfeit = 0 AND isWon = 1 THEN 1 ELSE 0 END) as gameDouble501Won,
            SUM(CASE WHEN winter_stats_player_games.playerId is not null AND gameTYpe = \'doublecricket\' AND isForfeit = 0 AND isWon = 1 THEN 1 ELSE 0 END) as gameDoubleCricketWon,
            SUM(CASE WHEN winter_stats_player_games.playerId is not null AND gameType like \'%01\' AND numberOfPlayers = 3 AND isForfeit = 0 AND isWon = 1 THEN 1 ELSE 0 END) as gameTriple01Won,
            SUM(CASE WHEN winter_stats_player_games.playerId is not null AND gameType = \'cricket\' AND isForfeit = 0 AND isWon = 0 THEN 1 ELSE 0 END) as gameCricketLost,
            SUM(CASE WHEN winter_stats_player_games.playerId is not null AND gameType like \'%01\' AND numberOfPlayers = 1 AND isForfeit = 0 AND isWon = 0 THEN 1 ELSE 0 END) as gameSingle01Lost,
            SUM(CASE WHEN winter_stats_player_games.playerId is not null AND gameType = \'double501\' AND isForfeit = 0 AND isWon = 0 THEN 1 ELSE 0 END) as gameDouble501Lost,
            SUM(CASE WHEN winter_stats_player_games.playerId is not null AND gameTYpe = \'doublecricket\' AND isForfeit = 0 AND isWon = 0 THEN 1 ELSE 0 END) as gameDoubleCricketLost,
            SUM(CASE WHEN winter_stats_player_games.playerId is not null AND gameType like \'%01\' AND numberOfPlayers = 3 AND isForfeit = 0 AND isWon = 0 THEN 1 ELSE 0 END) as gameTriple01Lost'))
            ->where('winter_season_teams.seasonId', $id)
            ->where(function($query)use($id){
                $query->whereNull('winter_stats_player_games.seasonId')
                    ->orWhere('winter_stats_player_games.seasonId', $id);
            });

        if(isset($division) && $division != null)
            $query->where(function($query)use($division){
                $query->where('winter_season_teams.preSeasonDiv', $division)
                    ->orWhere('winter_season_teams.regularSeasonDiv', $division);
            });


        if($seasonPart != 'whole') {
            $query->where(function($query)use($seasonPart){
                $query->whereNull('winter_stats_player_games.seasonPart')
                    ->orWhere('winter_stats_player_games.seasonPart', $seasonPart);
            });
        }

        $query->groupBy('teamId')
            ->groupBy('teams.name')
            ->groupBy('playerId')
            ->groupBy('players.firstName')
            ->groupBy('players.lastName');

        $query->orderBy('teamName');

        $teamGameStats = $query->get();
        $stats = [];

        foreach ($teamGameStats as $stat)
        {
            $newStat = new PlayerStat();
            $newStat->seasonId = $id;
            $newStat->seasonPart = $seasonPart;
            $newStat->teamId = $stat->teamId;
            $newStat->teamName = $stat->teamName;
            $newStat->playerId = $stat->playerId;
            $newStat->playerName = $stat->playerName;

            $newStat->gamesPlayed = $stat->gamesPlayed;
            $newStat->weeksPlayed = PlayerGameStat::where('seasonId', $id)->where('playerId', $stat->playerId)->groupBy('date')->get()->count();

            $newStat->pointsWon = $stat->pointsWon;
            $newStat->pointsLost = $stat->pointsLost;
            $newStat->gamesWon = $stat->gamesWon;
            $newStat->gamesLost = $stat->gamesLost;

            $newStat->overallWin = 0;
            if($stat->gamesWon + $stat->gamesLost != 0)
                $newStat->overallWin = $stat->gamesWon / ($stat->gamesWon + $stat->gamesLost);

            $newStat->singlesWin = 0;
            if(($stat->gameSingle01Played + $stat->gameCricketPlayed) != 0)
                $newStat->singlesWin = ($stat->gameSingle01Won + $stat->gameCricketWon) / ($stat->gameSingle01Played + $stat->gameCricketPlayed);

            $newStat->doublesWin = 0;
            if(($stat->gameDouble501Played + $stat->gameDoubleCricketPlayed) != 0)
                $newStat->doublesWin = ($stat->gameDouble501Won + $stat->gameDoubleCricketWon) / ($stat->gameDouble501Played + $stat->gameDoubleCricketPlayed);

            $newStat->game801->eight01Win = 0;
            if($stat->gameTriple01Played != 0)
                $newStat->eight01Win = ($stat->gameTriple01Won / $stat->gameTriple01Played);

            $newStat->cricketWin = 0;
            if(($stat->gameCricketPlayed + $stat->gameDoubleCricketPlayed) != 0)
                $newStat->cricketWin = ($stat->gameCricketWon + $stat->gameDoubleCricketWon) / ($stat->gameCricketPlayed + $stat->gameDoubleCricketPlayed);

            $newStat->oh1Win = 0;
            if(($stat->gameSingle01Played + $stat->gameDouble501Played + $stat->gameTriple01Played) != 0)
                $newStat->oh1Win = ($stat->gameSingle01Won + $stat->gameDouble501Won + $stat->gameTriple01Won) / ($stat->gameSingle01Played + $stat->gameDouble501Played + $stat->gameTriple01Played);

            if($stat->gameSingle01Played != 0)
                $newStat->singles301Win = $stat->gameSingle01Won / $stat->gameSingle01Played;

            if($stat->gameCricketPlayed != 0)
                $newStat->singlesCricketWin = $stat->gameCricketWon / $stat->gameCricketPlayed;

            if($stat->gameDoubleCricketPlayed != 0)
                $newStat->doublesCricketWin = $stat->gameDoubleCricketWon / $stat->gameDoubleCricketPlayed;

            if($stat->gameDouble501Played != 0)
                $newStat->doubles501Win = $stat->gameDouble501Won / $stat->gameDouble501Played;

            if($stat->gameTriple01Played != 0)
                $newStat->triples801Win = $stat->gameTriple01Won / $stat->gameTriple01Played;

            $newStat->game301->played = $stat->gameSingle01Played;
            $newStat->game301->won = $stat->gameSingle01Won;
            $newStat->game301->lost = $stat->gameSingle01Lost;

            $newStat->gameCricket->played = $stat->gameCricketPlayed;
            $newStat->gameCricket->won = $stat->gameCricketWon;
            $newStat->gameCricket->lost = $stat->gameCricketLost;

            $newStat->game501->played = $stat->gameDouble501Played;
            $newStat->game501->won = $stat->gameDouble501Won;
            $newStat->game501->lost = $stat->gameDouble501Lost;

            $newStat->gameDoubleCricket->played = $stat->gameDoubleCricketPlayed;
            $newStat->gameDoubleCricket->won = $stat->gameDoubleCricketWon;
            $newStat->gameDoubleCricket->lost = $stat->gameDoubleCricketLost;

            $newStat->game801->played = $stat->gameTriple01Played;
            $newStat->game801->won = $stat->gameTriple01Won;
            $newStat->game801->lost = $stat->gameTriple01Lost;

            array_push($stats, $newStat);
        }

        return new Collection($stats);
    }
}