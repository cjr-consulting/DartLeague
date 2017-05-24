<?php

namespace TrentonDarts\LeagueManagement\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\SoftDeletes;

class Sponsor extends Model
{
    public static $sponsorTypes = [
        'L' => 'League Sponsors and Partners',
        'P' => 'Player Companies',
        'C' => 'Charity Partners',
        'T' => 'Team Sponsors'];

    use SoftDeletes;
    /**
     * The database table used by the model.
     *
     * @var string
     */
    protected $table = 'sponsors';

    /**
     * The attributes that are mass assignable.
     *
     * @var array
     */
    protected $fillable = [
        'leagueId',
        'name',
        'type',
        'contactName',
        'address1',
        'address2',
        'city',
        'state',
        'zip',
        'phone',
        'url',
        'facebookUrl',
        'email',
        'mapUrl',
        'description',
        'comments'];

    public function teams()
    {
        return $this->hasMany('TrentonDarts\LeagueManagement\Models\Team', 'sponsorId');
    }

    public function typeName()
    {
        if(!array_key_exists($this->type, self::$sponsorTypes))
           return 'Unknown';

        return self::$sponsorTypes[$this->type];
    }
}
