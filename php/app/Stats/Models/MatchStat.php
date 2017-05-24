<?php

namespace TrentonDarts\Stats\Models;

use Illuminate\Database\Eloquent\Model;

class MatchStat extends Model
{
    /**
     * The database table used by the model.
     *
     * @var string
     */
    protected $table = 'winter_stats_matches';

    /**
     * The attributes that are mass assignable.
     *
     * @var array
     */
    protected $fillable = [
        'seasonId',
        'seasonPart',
        'matchId',
        'division',
        'date',
        'teamId',
        'teamName',
        'pointsWon',
        'pointsLost',
        'matchPoints'
    ];

}
