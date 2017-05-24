<?php

namespace TrentonDarts\Stats;

use Illuminate\Database\Eloquent\Collection;
use Illuminate\Support\Facades\DB;
use TrentonDarts\LeagueManagement\Models\WinterSeason;
use TrentonDarts\LeagueManagement\Models\WinterSeasonMatch;
use TrentonDarts\MatchDomain\Models\MatchResult;
use TrentonDarts\Stats\Models\TeamGameStat;
use TrentonDarts\Stats\Models\TeamStat;

class TeamGameRepository
{
    public function updateTeamGameStats(MatchResult $result)
    {
        if(!$result->getHasScorecard())
            return;

        foreach($result->getGames() as $game)
        {
            $teamGameAwayStat = TeamGameStat::where('gameId', $game->id)->where('teamId', $result->awayTeamId )->first();
            if(!$teamGameAwayStat)
                $teamGameAwayStat = new TeamGameStat();

            $teamGameAwayStat->seasonId = $result->seasonId;
            $teamGameAwayStat->seasonPart = $result->seasonPart;
            $teamGameAwayStat->matchId = $result->getMatchId();
            $teamGameAwayStat->division = $result->division;
            $teamGameAwayStat->date = $result->date;
            $teamGameAwayStat->gameId = $game->id;
            $teamGameAwayStat->teamId = $result->awayTeamId;
            $teamGameAwayStat->teamName = $result->awayTeamName;
            $teamGameAwayStat->gameType = $game->gameRules->gameType;
            $teamGameAwayStat->numberOfPlayers = $game->gameRules->numberOfPlayers;
            $teamGameAwayStat->numberOfPoints = $game->getAwayScore() == 0 ? $game->getHomeScore() : $game->getAwayScore();
            $teamGameAwayStat->isWon = $game->getAwayScore() > $game->getHomeScore();
            $teamGameAwayStat->isForfeitGame = $game->forfeitedBy != '';
            $teamGameAwayStat->save();

            $teamGameHomeStat = TeamGameStat::where('gameId', $game->id)->where('teamId', $result->homeTeamId )->first();
            if(!$teamGameHomeStat)
                $teamGameHomeStat = new TeamGameStat();

            $teamGameHomeStat->seasonId = $result->seasonId;
            $teamGameHomeStat->seasonPart = $result->seasonPart;
            $teamGameHomeStat->matchId = $result->getMatchId();
            $teamGameHomeStat->division = $result->division;
            $teamGameHomeStat->date = $result->date;
            $teamGameHomeStat->gameId = $game->id;
            $teamGameHomeStat->teamId = $result->homeTeamId;
            $teamGameHomeStat->teamName = $result->homeTeamName;
            $teamGameHomeStat->gameType = $game->gameRules->gameType;
            $teamGameHomeStat->numberOfPlayers = $game->gameRules->numberOfPlayers;
            $teamGameHomeStat->numberOfPoints = $game->getHomeScore() == 0 ? $game->getAwayScore() : $game->getHomeScore();
            $teamGameHomeStat->isWon = $game->getAwayScore() < $game->getHomeScore();
            $teamGameHomeStat->isForfeitGame = $game->forfeitedBy != '';
            $teamGameHomeStat->save();
        }
    }

    public function getTeamStatsForSeasonPart_old($id, $seasonPart, $division)
    {
        $query = DB::table('winter_season_teams')
            ->leftJoin('teams', 'winter_season_teams.teamId','=','teams.id')
            ->leftJoin('winter_stats_team_games', 'teams.id', '=', 'winter_stats_team_games.teamId')
            ->select(DB::raw('teams.id as teamId, teams.name as teamName, winter_season_teams.preSeasonDiv, winter_season_teams.regularSeasonDiv,
            SUM(CASE WHEN isWon = 1 THEN numberOfPoints ELSE 0 END) as pointsWon,
            SUM(CASE WHEN isWon = 0 THEN numberOfPoints ELSE 0 END) as pointsLost,
            SUM(CASE WHEN winter_stats_team_games.teamId is not null AND isForfeitGame = 0 THEN 1 ELSE 0 END) as gamesPlayed,
            SUM(CASE WHEN isWon = 1 AND isForfeitGame = 0 THEN 1 ELSE 0 END) as gamesWon,
            SUM(CASE WHEN isWon = 0 AND isForfeitGame = 0 THEN 1 ELSE 0 END) as gamesLost,
            SUM(CASE WHEN gameType = \'cricket\' AND isForfeitGame = 0 THEN 1 ELSE 0 END) as gameCricketPlayed,
            SUM(CASE WHEN gameType = \'301\' AND isForfeitGame = 0 THEN 1 ELSE 0 END) as game301Played,
            SUM(CASE WHEN gameType = \'501\' AND isForfeitGame = 0 THEN 1 ELSE 0 END) as game501Played,
            SUM(CASE WHEN gameTYpe = \'doublecricket\' AND isForfeitGame = 0 THEN 1 ELSE 0 END) as gameDoubleCricketPlayed,
            SUM(CASE WHEN gameType = \'801\' AND isForfeitGame = 0 THEN 1 ELSE 0 END) as game801Played,
            SUM(CASE WHEN gameType = \'cricket\' AND isWon = 1 AND isForfeitGame = 0 THEN 1 ELSE 0 END) as gameCricketWon,
            SUM(CASE WHEN gameType = \'301\' AND isWon = 1 AND isForfeitGame = 0 THEN 1 ELSE 0 END) as game301Won,
            SUM(CASE WHEN gameType = \'501\' AND isWon = 1 AND isForfeitGame = 0 THEN 1 ELSE 0 END) as game501Won,
            SUM(CASE WHEN gameTYpe = \'doublecricket\' AND isWon = 1 AND isForfeitGame = 0 THEN 1 ELSE 0 END) as gameDoubleCricketWon,
            SUM(CASE WHEN gameType = \'801\' AND isWon = 1 THEN 1 AND isForfeitGame = 0 ELSE 0 END) as game801Won,
            SUM(CASE WHEN gameType = \'cricket\' AND isWon = 0 AND isForfeitGame = 0 THEN 1 ELSE 0 END) as gameCricketLost,
            SUM(CASE WHEN gameType = \'301\' AND isWon = 0 AND isForfeitGame = 0 THEN 1 ELSE 0 END) as game301Lost,
            SUM(CASE WHEN gameType = \'501\' AND isWon = 0 AND isForfeitGame = 0 THEN 1 ELSE 0 END) as game501Lost,
            SUM(CASE WHEN gameTYpe = \'doublecricket\' AND isWon = 0 AND isForfeitGame = 0 THEN 1 ELSE 0 END) as gameDoubleCricketLost,
            SUM(CASE WHEN gameType = \'801\' AND isWon = 0 AND isForfeitGame = 0 THEN 1 ELSE 0 END) as game801Lost'))
            ->where('winter_season_teams.seasonId', $id)->where(function($query)use($id){
                $query->whereNull('winter_stats_team_games.seasonId')
                    ->orWhere('winter_stats_team_games.seasonId', $id);
            });

        if(isset($division) && $division != null)
            $query->where(function($query)use($division){
                $query->where('winter_season_teams.preSeasonDiv', $division)
                    ->orWhere('winter_season_teams.regularSeasonDiv', $division);
            });

        if($seasonPart != 'whole') {
            $query->where('seasonPart', $seasonPart);
        }

        $query->groupBy('teamId')
            ->groupBy('teamName');

        $query->orderBy('teamName');

        $teamGameStats = $query->get();

        $matchQuery = DB::table('winter_stats_matches')
            ->select(DB::raw('teamId, SUM(pointsWon) as pointsWon, SUM(pointsLost) as pointsLost'))
            ->where('seasonId', $id);
        if($seasonPart != 'whole')
            $matchQuery->where('seasonPart', $seasonPart);

        $matchStats = new Collection($matchQuery->groupBy('teamId')->get());

        $stats = [];
        foreach($teamGameStats as $stat)
        {
            $newStat = new TeamStat();
            $newStat->seasonId = $id;
            $newStat->seasonPart = $seasonPart;
            $newStat->preSeasonDiv = $stat->preSeasonDiv;
            $newStat->regularSeasonDiv = $stat->regularSeasonDiv;
            $newStat->teamId = $stat->teamId;
            $newStat->teamName = $stat->teamName;
            $matchStat = $matchStats->where('teamId', $stat->teamId)->first();
            if($matchStat != null) {
                $newStat->pointsWon = $matchStat->pointsWon;
                $newStat->pointsLost = $matchStat->pointsLost;
            } else {
                $newStat->pointsWon = $stat->pointsWon;
                $newStat->pointsLost = $stat->pointsLost;
            }
            $newStat->gamesWon = $stat->gamesWon;
            $newStat->gamesLost = $stat->gamesLost;
            $newStat->gamesPlayed = $stat->gamesPlayed;

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

    public function getTeamStatsForSeasonPart($id, $seasonPart, $division)
    {
        $query = DB::table('winter_season_teams')
            ->leftJoin('teams', 'winter_season_teams.teamId','=','teams.id')
            ->leftJoin('winter_stats_team_games', 'teams.id', '=', 'winter_stats_team_games.teamId')
            ->select(DB::raw('teams.id as teamId, teams.name as teamName, winter_season_teams.preSeasonDiv, winter_season_teams.regularSeasonDiv,
            SUM(CASE WHEN isWon = 1 THEN numberOfPoints ELSE 0 END) as pointsWon,
            SUM(CASE WHEN isWon = 0 THEN numberOfPoints ELSE 0 END) as pointsLost,
            SUM(CASE WHEN winter_stats_team_games.teamId is not null AND isForfeitGame = 0 THEN 1 ELSE 0 END) as gamesPlayed,
            SUM(CASE WHEN isWon = 1 AND isForfeitGame = 0 THEN 1 ELSE 0 END) as gamesWon,
            SUM(CASE WHEN isWon = 0 AND isForfeitGame = 0 THEN 1 ELSE 0 END) as gamesLost,
            SUM(CASE WHEN gameType = \'cricket\' AND isForfeitGame = 0 THEN 1 ELSE 0 END) as gameCricketPlayed,
            SUM(CASE WHEN gameType like \'%01\' AND numberOfPlayers = 1 AND isForfeitGame = 0 THEN 1 ELSE 0 END) as game301Played,
            SUM(CASE WHEN gameType = \'double501\' AND isForfeitGame = 0 THEN 1 ELSE 0 END) as game501Played,
            SUM(CASE WHEN gameTYpe = \'doublecricket\' AND isForfeitGame = 0 THEN 1 ELSE 0 END) as gameDoubleCricketPlayed,
            SUM(CASE WHEN gameType like \'%01\' AND numberOfPlayers = 3 AND isForfeitGame = 0 THEN 1 ELSE 0 END) as game801Played,
            SUM(CASE WHEN gameType = \'cricket\' AND isWon = 1 AND isForfeitGame = 0 THEN 1 ELSE 0 END) as gameCricketWon,
            SUM(CASE WHEN gameType like \'%01\' AND numberOfPlayers = 1 AND isWon = 1 AND isForfeitGame = 0 THEN 1 ELSE 0 END) as game301Won,
            SUM(CASE WHEN gameType = \'double501\' AND isWon = 1 AND isForfeitGame = 0 THEN 1 ELSE 0 END) as game501Won,
            SUM(CASE WHEN gameTYpe = \'doublecricket\' AND isWon = 1 AND isForfeitGame = 0 THEN 1 ELSE 0 END) as gameDoubleCricketWon,
            SUM(CASE WHEN gameType like \'%01\' AND numberOfPlayers = 3 AND isWon = 1 THEN 1 AND isForfeitGame = 0 ELSE 0 END) as game801Won,
            SUM(CASE WHEN gameType = \'cricket\' AND isWon = 0 AND isForfeitGame = 0 THEN 1 ELSE 0 END) as gameCricketLost,
            SUM(CASE WHEN gameType like \'%01\' AND numberOfPlayers = 1 AND isWon = 0 AND isForfeitGame = 0 THEN 1 ELSE 0 END) as game301Lost,
            SUM(CASE WHEN gameType = \'double501\' AND isWon = 0 AND isForfeitGame = 0 THEN 1 ELSE 0 END) as game501Lost,
            SUM(CASE WHEN gameTYpe = \'doublecricket\' AND isWon = 0 AND isForfeitGame = 0 THEN 1 ELSE 0 END) as gameDoubleCricketLost,
            SUM(CASE WHEN gameType like \'%01\' AND numberOfPlayers = 3 AND isWon = 0 AND isForfeitGame = 0 THEN 1 ELSE 0 END) as game801Lost'))
            ->where('winter_season_teams.seasonId', $id)->where(function($query)use($id){
                $query->whereNull('winter_stats_team_games.seasonId')
                    ->orWhere('winter_stats_team_games.seasonId', $id);
            });

        if(isset($division) && $division != null)
            $query->where(function($query)use($division){
                $query->where('winter_season_teams.preSeasonDiv', $division)
                    ->orWhere('winter_season_teams.regularSeasonDiv', $division);
            });

        if($seasonPart != 'whole') {
            $query->where('seasonPart', $seasonPart);
        }

        $query->groupBy('teamId')
            ->groupBy('teamName');

        $query->orderBy('teamName');

        $teamGameStats = $query->get();

        $matchQuery = DB::table('winter_stats_matches')
            ->select(DB::raw('teamId, SUM(pointsWon) as pointsWon, SUM(pointsLost) as pointsLost'))
            ->where('seasonId', $id);
        if($seasonPart != 'whole')
            $matchQuery->where('seasonPart', $seasonPart);

        $matchStats = new Collection($matchQuery->groupBy('teamId')->get());

        $stats = [];
        foreach($teamGameStats as $stat)
        {
            $newStat = new TeamStat();
            $newStat->seasonId = $id;
            $newStat->seasonPart = $seasonPart;
            $newStat->preSeasonDiv = $stat->preSeasonDiv;
            $newStat->regularSeasonDiv = $stat->regularSeasonDiv;
            $newStat->teamId = $stat->teamId;
            $newStat->teamName = $stat->teamName;
            $matchStat = $matchStats->where('teamId', $stat->teamId)->first();
            if($matchStat != null) {
                $newStat->pointsWon = $matchStat->pointsWon;
                $newStat->pointsLost = $matchStat->pointsLost;
            } else {
                $newStat->pointsWon = $stat->pointsWon;
                $newStat->pointsLost = $stat->pointsLost;
            }
            $newStat->gamesWon = $stat->gamesWon;
            $newStat->gamesLost = $stat->gamesLost;
            $newStat->gamesPlayed = $stat->gamesPlayed;

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
}