<?php
/**
 * Created by PhpStorm.
 * User: johnmeade
 * Date: 9/9/15
 * Time: 2:31 PM
 */

namespace TrentonDarts\Stats\Models;


use Illuminate\Database\Eloquent\Model;

class PlayerGameStat extends Model
{
    /**
     * The database table used by the model.
     *
     * @var string
     */
    protected $table = 'winter_stats_player_games';

    /**
     * The attributes that are mass assignable.
     *
     * @var array
     */
    protected $fillable = [
        'seasonId',
        'seasonPart',
        'date',
        'matchId',
        'division',
        'gameId',
        'teamId',
        'teamName',
        'playerId',
        'playerName',
        'playerPosition',
        'gameType',
        'numberOfPlayers',
        'numberOfPoints',
        'isWon'
    ];

}