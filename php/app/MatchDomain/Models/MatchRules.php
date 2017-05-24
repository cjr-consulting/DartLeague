<?php
/**
 * Created by PhpStorm.
 * User: johnmeade
 * Date: 8/25/15
 * Time: 12:17 PM
 */
namespace TrentonDarts\MatchDomain\Models;

class MatchRules
{
    public $id;
    public $bestOfNumberOfGames;
    public $playersAreOnlyAllowedToPlayOneOfEachGameType;
    public $isUsingMatchPoints;
    public $winPoints;
    public $halfPoints;
    public $minPointForHalfPoints;
    public $gameRules = [];

    public function __construct()
    {
    }
}