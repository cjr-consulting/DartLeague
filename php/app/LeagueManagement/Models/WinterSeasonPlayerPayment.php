<?php

namespace TrentonDarts\LeagueManagement\Models;

use Illuminate\Database\Eloquent\Model;

class WinterSeasonPlayerPayment extends Model
{
    /**
     * The database table used by the model.
     *
     * @var string
     */
    protected $table = 'winter_season_player_payments';

    /**
     * The attributes that are mass assignable.
     *
     * @var array
     */
    protected $fillable = ['seasonId', 'playerId', 'paymentStatus'];

}