<?php

namespace TrentonDarts\LeagueManagement\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\SoftDeletes;

class WinterSeason extends Model
{

    use SoftDeletes;
    /**
     * The database table used by the model.
     *
     * @var string
     */
    protected $table = 'winter_seasons';

    /**
     * The attributes that are mass assignable.
     *
     * @var array
     */
    protected $fillable = [
        'leagueId',
        'name',
        'startYear',
        'endYear',
        'isCurrent',
        'seasonType',
        'defaultMatchTypeId',
        'isUsingMatchPoints',
        'winPoints',
        'halfPoints',
        'minPointForHalfPoints',
        'accumulatePointsForAllParts'];

    public function teams()
    {
        return $this->hasMany('TrentonDarts\LeagueManagement\Models\WinterSeasonTeam', 'seasonId');
    }

    public function weeks() {
        return $this->hasMany('TrentonDarts\LeagueManagement\Models\WinterSeasonWeek', 'seasonId');
    }

    public function preSeasonWeeks(){
        return $this->hasMany('TrentonDarts\LeagueManagement\Models\WinterSeasonWeek', 'seasonId')->where('weekType', 'pre')->orderBy('date');
    }

    public function regularSeasonWeeks(){
        return $this->hasMany('TrentonDarts\LeagueManagement\Models\WinterSeasonWeek', 'seasonId')->where('weekType', 'regular')->orderBy('date');
    }
}
