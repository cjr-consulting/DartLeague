<?php

namespace TrentonDarts\LeagueManagement\Models;

use Illuminate\Database\Eloquent\Model;

class WinterSeasonTeamPlayer extends Model
{
    /**
     * The database table used by the model.
     *
     * @var string
     */
    protected $table = 'winter_season_team_players';

    /**
     * The attributes that are mass assignable.
     *
     * @var array
     */
    protected $fillable = ['leagueId', 'seasonId', 'seasonTeamId', 'playerId', 'role'];

    public function season(){
        return $this->belongsTo('TrentonDarts\LeagueManagement\Models\WinterSeason', 'seasonId');
    }

    public function seasonTeam(){
        return $this->belongsTo('TrentonDarts\LeagueManagement\Models\WinterSeasonTeam','seasonTeamId');
    }

    public function player(){
        return $this->belongsTo('TrentonDarts\LeagueManagement\Models\Player', 'playerId');
    }
}
