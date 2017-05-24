<?php

namespace TrentonDarts\Stats\Models;


class MatchResults
{
    private $matchResults;

    public function __construct($matchResults)
    {
        $this->matchResults = $matchResults;
    }

    public function getMatchResultByMatchId($matchId)
    {
        if(isset($this->matchResults[$matchId]))
            return $this->matchResults[$matchId];

        foreach($this->matchResults as $result)
        {
            if($result->matchId == $matchId)
                return $result;
        }

        $result = new MatchResult();
        $result->matchId = 0;
        $result->awayTeamId = 0;
        $result->awayScore = 0;
        $result->homeTeamId = 0;
        $result->homeScore = 0;
        return $result;
    }
}