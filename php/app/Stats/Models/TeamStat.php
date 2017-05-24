<?php
/**
 * Created by PhpStorm.
 * User: johnmeade
 * Date: 9/7/15
 * Time: 11:36 PM
 */

namespace TrentonDarts\Stats\Models;


class TeamStat
{
    public $seasonId;
    public $seasonPart;
    public $division;
    public $teamId;
    public $teamName;
    public $pointsWon;
    public $pointsLost;
    public $gamesWon;
    public $gamesLost;
    public $gamesPlayed;

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
        $this->gamesPlayed = 0;
        $this->pointsWon = 0;
        $this->pointsLost = 0;
        $this->overallWin = 0;
        $this->singlesWin = 0;
        $this->doublesWin = 0;
        $this->eight01Win = 0;
        $this->cricketWin = 0;
        $this->oh1Win = 0;

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