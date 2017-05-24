<?php
namespace TrentonDarts\MatchDomain\Models;

class GameRules
{
    public $id;
    public $gameType;
    public $doubleIn;
    public $doubleOut;
    public $orderId;
    public $bestOfNumberOfLegs;
    public $numberOfLegs;
    public $whoStarts;
    public $numberOfPlayers;
    public $gamePointValue;
    public $legPointValue;
    public $forfeitIfNoPlayers;
    public $groupName;
}