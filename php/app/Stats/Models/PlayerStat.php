<?php
/**
 * Created by PhpStorm.
 * User: johnmeade
 * Date: 9/9/15
 * Time: 2:05 PM
 */

namespace TrentonDarts\Stats\Models;

class PlayerStat
{
    public $seasonId;
    public $seasonPart;
    public $division;
    public $teamId;
    public $teamName;
    public $playerId;
    public $playerName;

    public $weeksPlayed;
    public $gamesPlayed;

    public $pointsWon;
    public $pointsLost;
    public $gamesWon;
    public $gamesLost;

    public $overallWin;
    public $singlesWin;
    public $doublesWin;
    public $eight01Win;
    public $cricketWin;
    public $oh1Win;

    public $singles301Win;
    public $singlesCricketWin;
    public $doublesCricketWin;
    public $doubles501Win;
    public $triples801Win;

    public $game301;
    public $gameCricket;
    public $game501;
    public $gameDoubleCricket;
    public $game801;

    public function __construct()
    {
        $this->pointsWon = 0;
        $this->pointsLost = 0;
        $this->overallWin = 0;
        $this->singlesWin = 0;
        $this->doublesWin = 0;
        $this->eight01Win = 0;
        $this->cricketWin = 0;
        $this->oh1Win = 0;

        $this->singles301Win = 0;
        $this->singlesCricketWin = 0;
        $this->doublesCricketWin = 0;
        $this->doubles501Win = 0;
        $this->triples801Win = 0;

        $this->game301 = new \stdClass();
        $this->game301->played = 0;
        $this->game301->won = 0;
        $this->game301->lost = 0;

        $this->gameCricket = new \stdClass();
        $this->game501 = new \stdClass();
        $this->gameDoubleCricket = new \stdClass();
        $this->game801 = new \stdClass();

    }

}