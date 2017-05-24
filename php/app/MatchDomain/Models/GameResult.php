<?php

namespace TrentonDarts\MatchDomain\Models;

class GameResult
{
    public $id;
    public $homePlayers = [];
    public $awayPlayers = [];
    public $legs = [];
    public $forfeitedBy;
    public $gameRules;
    public $awards = [];

    public function __construct(GameRules $gameRules)
    {
        $this->gameRules = $gameRules;
    }

    public function getHomePlayers()
    {
        return $this->homePlayers;
    }

    public function getAwayPlayers()
    {
        return $this->awayPlayers;
    }

    public function setHomePlayer($player, $position)
    {
        if (isset($position))
            $this->homePlayers[$position] = $player;
        else {
            if ($this->hasToManyPlayersOnTeam($this->homePlayers)) {
                throw new TooManyPlayersException();
            }

            array_push($this->homePlayers, $player);
        }
    }

    public function setAwayPlayer($player, $position)
    {
        if (isset($position))
            $this->awayPlayers[$position] = $player;
        else {
            if ($this->hasToManyPlayersOnTeam($this->awayPlayers)) {
                throw new TooManyPlayersException();
            }

            array_push($this->awayPlayers, $player);
        }
    }

    public function getSnapshot()
    {
        $snapshot = new GameResultSnapshot();
        $snapshot->id = $this->id;
        $snapshot->awayPlayers = $this->awayPlayers;
        $snapshot->homePlayers = $this->homePlayers;
        $snapshot->legs = $this->legs;
        $snapshot->gameRules = $this->gameRules;
        $snapshot->forfeitedBy = $this->forfeitedBy;
        $snapshot->awards = $this->awards;
        return $snapshot;
    }

    public function loadSnapshot($snapshot)
    {
        $this->id = $snapshot->id;
        $this->awayPlayers = $snapshot->awayPlayers;
        $this->homePlayers = $snapshot->homePlayers;
        $this->legs = $snapshot->legs;
        $this->gameRules = $snapshot->gameRules;
        $this->forfeitedBy = $snapshot->forfeitedBy;
        $this->awards = $snapshot->awards;
    }

    public function removeAwayPlayerAtPosition($index)
    {
        unset($this->awayPlayers[$index]);
    }

    public function removeHomePlayerAtPosition($index)
    {
        unset($this->homePlayers[$index]);
    }

    public function removeHomePlayer($player)
    {
        $key = array_search($player, $this->homePlayers);
        if ($key !== false) {
            unset($this->homePlayers[$key]);
        }
    }

    public function removeAwayPlayer($player)
    {
        $key = array_search($player, $this->awayPlayers);
        if ($key !== false) {
            unset($this->awayPlayers[$key]);
        }
    }

    public function addLeg($legNumber, $winner)
    {
        if($this->hasTooManyLegs())
        {
            throw new TooManyLegsException();
        }
        $this->legs[$legNumber] = $winner;
    }

    public function setLegs($legs)
    {
        $this->legs = $legs;
    }

    public function hasToManyPlayersOnTeam($teamPlayers)
    {
        return sizeof($teamPlayers) >= $this->gameRules->numberOfPlayers;
    }

    private function hasTooManyLegs()
    {
        return sizeof($this->legs) >= $this->gameRules->numberOfLegs;
    }

    public function getHomeScore()
    {
        $score = 0;
        if($this->gameRules->gamePointValue > 0) {
            if($this->didHomeTeamWin()) {
                $score = $this->gameRules->gamePointValue;
            }
        }

        return $score;
    }

    public function getAwayScore()
    {
        $score = 0;
        if($this->gameRules->gamePointValue > 0) {
            if($this->didAwayTeamWin()) {
                $score = $this->gameRules->gamePointValue;
            }
        }

        return $score;
    }

    private function didHomeTeamWin()
    {
        $homeLegsWon = $this->getLegsWonByTeamType("home");
        $awayLegsWon = $this->getLegsWonByTeamType("away");

        if($homeLegsWon > $awayLegsWon)
            return true;
        return false;
    }

    private function didAwayTeamWin()
    {
        $homeLegsWon = $this->getLegsWonByTeamType("home");
        $awayLegsWon = $this->getLegsWonByTeamType("away");

        if($homeLegsWon < $awayLegsWon)
            return true;
        return false;
    }

    private function getLegsWonByTeamType($teamType)
    {
        $homeLegsWon = 0;
        foreach ($this->legs as $leg) {
            if ($leg == $teamType) $homeLegsWon++;
        }
        return $homeLegsWon;
    }
}