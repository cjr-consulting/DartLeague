<?php
namespace TrentonDarts\MatchDomain\Models;

class MatchResultSnapshot
{
    public $matchId;
    public $seasonId;
    public $seasonPart;
    public $division;
    public $date;
    public $awayTeamId;
    public $awayTeamName;
    public $homeTeamId;
    public $homeTeamName;

    public $hasScorecard = false;
    public $awayScoreOverride = 0;
    public $homeScoreOverride = 0;
    public $gameResults = [];
    public $rules;
}