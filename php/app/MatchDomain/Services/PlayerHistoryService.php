<?php
/**
 * Created by PhpStorm.
 * User: johnmeade
 * Date: 9/29/15
 * Time: 2:20 PM
 */

namespace TrentonDarts\MatchDomain\Services;

use Illuminate\Database\Eloquent\Collection;
use TrentonDarts\MatchDomain\Models\GamePlayer;
use TrentonDarts\Stats\Models\PlayerGameStat;

class PlayerHistoryService
{
    public function getPlayerGameHistory($seasonId, $playerId)
    {
        $gameHistories = [];
        $gameIds = PlayerGameStat::where('seasonId', $seasonId)
            ->where('playerId', $playerId)
            ->where('isForfeit', 0)
            ->orderBy('date')
            ->orderBy('gameNumber')->lists('gameId');
        $games = PlayerGameStat::whereIn('gameId', $gameIds)->get();

        foreach($gameIds as $gameId)
        {
            $playerGame = $games->where('gameId', $gameId)->where('playerId', (int)$playerId)->first();
            $gameHistory = new PlayerGameHistory();
            $gameHistory->matchId = $playerGame->matchId;
            $gameHistory->date = $playerGame->date;
            $gameHistory->gameType = $this->getHistoryGameType($playerGame);
            $gameHistory->gameOrder = $playerGame->gameNumber;
            $gameHistory->seasonPart = $playerGame->seasonPart;
            $gameHistory->isHomeGame = $playerGame->isHome;
            $gameHistory->isWon = $playerGame->isWon == 1;

            foreach($games->where('gameId', $gameId)->where('teamId', $playerGame->teamId)->filter(function($g)use($playerGame){return $g->playerId != $playerGame->playerId; })->sortBy('playerPosition') as $game)
            {
                $gamePlayer = new GamePlayer();
                $gamePlayer->id = $game->playerId;
                $gamePlayer->name = $game->playerName;
                array_push($gameHistory->teamPlayers, $gamePlayer);
            }
            foreach($games->where('gameId', $gameId)->filter(function($g)use($playerGame){return $g->teamId != $playerGame->teamId;})->sortBy('playerPosition') as $game)
            {
                $gameHistory->opponentTeamName = $game->teamName;
                $gameHistory->opponentTeamId = $game->teamId;
                $gamePlayer = new GamePlayer();
                $gamePlayer->id = $game->playerId;
                $gamePlayer->name = $game->playerName;
                array_push($gameHistory->opponents, $gamePlayer);
            }
            array_push($gameHistories, $gameHistory);
        }

        return new Collection($gameHistories);
    }

    private function getHistoryGameType($playerGame)
    {
        $name = '';
        if($playerGame->numberOfPlayers == 2)
            $name = $name.'D-';
        elseif($playerGame->numberOfPlayers == 3)
            $name = $name.'T-';

        if($playerGame->gameType == 'cricket' || $playerGame->gameType == 'doublecricket')
            $name = $name.'Cricket';
        else
            $name = $name.$playerGame->gameType;

        return $name;
    }
}

class PlayerGameHistory
{
    public $matchId;
    public $date;
    public $gameType;
    public $gameOrder;
    public $seasonPart;
    public $isHomeGame;
    public $teamPlayers = [];
    public $opponents = [];
    public $opponentTeamName;
    public $opponentTeamId;
    public $isWon;

}