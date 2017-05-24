<?php

namespace TrentonDarts\LeagueManagement\Models;

use Illuminate\Database\Eloquent\Model;

class WinterSeasonWeek extends Model
{
    /**
     * The database table used by the model.
     *
     * @var string
     */
    protected $table = 'winter_season_weeks';

    /**
     * The attributes that are mass assignable.
     *
     * @var array
     */
    protected $fillable = ['leagueId', 'seasonId', 'weekType', 'date'];

    public function season()
    {
        return $this->belongsTo('TrentonDarts\LeagueManagement\Models\WinterSeason', 'seasonId');
    }

    public function matches(){
        return $this->hasMany('TrentonDarts\LeagueManagement\Models\WinterSeasonMatch', 'weekId');
    }
}
