<?php

namespace TrentonDarts\LeagueManagement\Models;

use Illuminate\Database\Eloquent\Model;
use TrentonDarts\LeagueManagement\Models\WinterSeason;

class WinterSeasonTeam extends Model
{
    /**
     * The database table used by the model.
     *
     * @var string
     */
    protected $table = 'winter_season_teams';

    /**
     * The attributes that are mass assignable.
     *
     * @var array
     */
    protected $fillable = ['leagueId', 'seasonId', 'teamId', 'preSeasonDiv', 'regularSeasonDiv'];

    public function season()
    {
        return $this->belongsTo('TrentonDarts\LeagueManagement\Models\WinterSeason', 'seasonId');
    }

    public function team()
    {
        return $this->belongsTo('TrentonDarts\LeagueManagement\Models\Team','teamId');
    }

    public function teamPlayers()
    {
        return $this->hasMany('TrentonDarts\LeagueManagement\Models\WinterSeasonTeamPlayer', 'seasonTeamId');
    }
}
