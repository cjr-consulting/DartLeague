<?php
namespace TrentonDarts\MatchDomain\Models;

class GameResultSnapshot
{
    public $id;
    public $homePlayers = [];
    public $awayPlayers = [];
    public $legs = [];
    public $awards = [];
    public $forfeitedBy;
    public $gameRules;
}