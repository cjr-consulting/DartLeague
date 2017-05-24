<?php

namespace TrentonDarts\Stats\Models;

use Illuminate\Database\Eloquent\Model;

class TeamGameStat extends Model
{
    /**
     * The database table used by the model.
     *
     * @var string
     */
    protected $table = 'winter_stats_team_games';

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
        'gameType',
        'numberOfPlayers',
        'numberOfPoints',
        'isWon',
        'isForfeitGame'
    ];

}
