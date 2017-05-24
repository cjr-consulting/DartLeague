<?php

namespace TrentonDarts\LeagueManagement\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\SoftDeletes;

class BoardMember extends Model
{

    use SoftDeletes;
    /**
     * The database table used by the model.
     *
     * @var string
     */
    protected $table = 'board_members';

    /**
     * The attributes that are mass assignable.
     *
     * @var array
     */
    protected $fillable = ['leagueId', 'userId', 'name', 'position', 'startingSeason', 'endingSeason', 'startSeasonId', 'endSeasonId'];

    public function user()
    {
        return $this->hasOne('TrentonDarts\User', 'userId');
    }
}
