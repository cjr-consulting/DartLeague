<?php

namespace TrentonDarts\MatchDomain\Models;

class MatchResult
{

    public $seasonId;
    public $seasonPart;
    public $division;
    public $date;
    public $awayTeamId;
    public $awayTeamName;
    public $homeTeamId;
    public $homeTeamName;

    private $matchId;
    private $hasScorecard = false;
    private $awayScoreOverride = 0;
    private $homeScoreOverride = 0;
    private $gameResults = [];
    private $matchRules;

    public function __construct()
    {
    }

    public function loadRules(MatchRules $rules)
    {
        $this->matchRules = $rules;
        if(empty($this->gameResults)) {
            foreach ($rules->gameRules as $gameRule) {
                $game = new GameResult($gameRule);
                array_push($this->gameResults, $game);
            }
        }
    }

    public function getRules()
    {
        return $this->rules;
    }

    public function getGames()
    {
        return $this->gameResults;
    }

    public function getHomeMatchPoints()
    {
        if(!$this->matchRules->isUsingMatchPoints)
            return 0;

        if($this->getHomeScore() > $this->getAwayScore())
            return $this->matchRules->winPoints;

        if($this->getHomeScore() >= $this->matchRules->minPointForHalfPoints)
            return $this->matchRules->halfPoints;

        return 0;
    }

    public function getAwayMatchPoints()
    {
        if(!$this->matchRules->isUsingMatchPoints)
            return 0;

        if($this->getAwayScore() > $this->getHomeScore())
            return $this->matchRules->winPoints;

        if($this->getAwayScore() >= $this->matchRules->minPointForHalfPoints)
            return $this->matchRules->halfPoints;

        return 0;
    }

    public function getSnapshot()
    {
        $snapshot = new MatchResultSnapshot();
        $snapshot->matchId = $this->matchId;
        $snapshot->awayScoreOverride = $this->awayScoreOverride;
        $snapshot->homeScoreOverride = $this->homeScoreOverride;
        $snapshot->hasScorecard = $this->hasScorecard;

        foreach($this->gameResults as $game){
            array_push($snapshot->gameResults, $game->getSnapshot());
        }

        return $snapshot;
    }

    public function loadSnapshot($snapshot)
    {
        $this->matchId = $snapshot->matchId;
        $this->hasScorecard = $snapshot->hasScorecard;
        $this->awayScoreOverride = $snapshot->awayScoreOverride;
        $this->homeScoreOverride = $snapshot->homeScoreOverride;

        $this->seasonId = $snapshot->seasonId;
        $this->seasonPart = $snapshot->seasonPart;
        $this->division = $snapshot->division;
        $this->date = $snapshot->date;
        $this->awayTeamId = $snapshot->awayTeamId;
        $this->awayTeamName = $snapshot->awayTeamName;
        $this->homeTeamId = $snapshot->homeTeamId;
        $this->homeTeamName = $snapshot->homeTeamName;

        foreach($snapshot->gameResults as $gameSnapshot)
        {
            $game = new GameResult($gameSnapshot->gameRules);
            $game->loadSnapshot($gameSnapshot);
            array_push($this->gameResults, $game);
        }
    }

    public function getMatchId()
    {
        return $this->matchId;
    }

    public function getGameById($id)
    {
        foreach($this->gameResults as $game){
            if($game->id == $id) return $game;
        }
    }

    public function getHomeScore()
    {
        if(!$this->hasScorecard)
            return $this->homeScoreOverride;

        $homeScore = 0;
        foreach($this->gameResults as $game)
            $homeScore = $homeScore + $game->getHomeScore();

        return $homeScore;
    }

    public function getAwayScore()
    {
        if(!$this->hasScorecard)
            return $this->awayScoreOverride;
        $awayScore = 0;
        foreach($this->gameResults as $game)
            $awayScore = $awayScore + $game->getAwayScore();

        return $awayScore;
    }

    public function setHasScorecard($hasScorecard)
    {
        $this->hasScorecard = $hasScorecard;
    }

    public function getHasScorecard()
    {
        return $this->hasScorecard;
    }

    public function setAwayScoreOverride($score)
    {
        if($this->hasScorecard)
        {
            throw new HasScorecardException();
        }

        $this->awayScoreOverride = $score;
    }

    public function setHomeScoreOverride($score)
    {
        if($this->hasScorecard)
        {
            throw new HasScorecardException();
        }

        $this->homeScoreOverride = $score;
    }
}

