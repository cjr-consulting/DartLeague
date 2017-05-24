<?php

namespace TrentonDarts\LeagueManagement\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\SoftDeletes;

class Player extends Model
{
    use SoftDeletes;
    /**
     * The database table used by the model.
     *
     * @var string
     */
    protected $table = 'players';

    /**
     * The attributes that are mass assignable.
     *
     * @var array
     */
    protected $fillable = ['leagueId',
        'userId',
        'firstName',
        'lastName',
        'nickname',
        'email',
        'homePhone',
        'cellPhone',
        'shirtSize',
        'address1',
        'address2',
        'city',
        'state',
        'zip',
        'acceptText',
        'acceptEmail'];

    public function getNameAttribute(){
        return $this->attributes["firstName"]." ".$this->attributes["lastName"];
    }
}
