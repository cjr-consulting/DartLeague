<?php

namespace TrentonDarts\LeagueManagement\Models;

use Illuminate\Database\Eloquent\Model;

class WinterSeasonMatch extends Model
{
    /**
     * The database table used by the model.
     *
     * @var string
     */
    protected $table = 'winter_season_matches';

    /**
     * The attributes that are mass assignable.
     *
     * @var array
     */
    protected $fillable = ['leagueId', 'seasonId', 'weekId', 'matchTypeId', 'homeTeamId', 'awayTeamId'];

    public function season()
    {
        return $this->belongsTo('TrentonDarts\LeagueManagement\Models\WinterSeason', 'seasonId');
    }

    public function week()
    {
        return $this->belongsTo('TrentonDarts\LeagueManagement\Models\WinterSeasonWeek', 'weekId');
    }

    public function homeTeam()
    {
        return $this->belongsTo('TrentonDarts\LeagueManagement\Models\Team', 'homeTeamId');
    }

    public function awayTeam()
    {
        return $this->belongsTo('TrentonDarts\LeagueManagement\Models\Team', 'awayTeamId');
    }

    public function matchType()
    {
        return $this->belongsTo('TrentonDarts\LeagueManagement\Models\MatchType', 'matchTypeId');
    }
}
