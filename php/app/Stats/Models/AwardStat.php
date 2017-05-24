<?php
/**
 * Created by PhpStorm.
 * User: johnmeade
 * Date: 9/10/15
 * Time: 2:52 PM
 */

namespace TrentonDarts\Stats\Models;

use Illuminate\Database\Eloquent\Model;

class AwardStat extends Model
{
    /**
     * The database table used by the model.
     *
     * @var string
     */
    protected $table = 'winter_stats_awards';

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
        'awardId',
        'playerId',
        'playerName',
        'awardType',
        'value'
    ];

}