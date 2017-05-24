<?php

namespace TrentonDarts\LeagueManagement\Models;

use Illuminate\Database\Eloquent\Model;

class WinterSeasonTeamPayment extends Model
{
    /**
     * The database table used by the model.
     *
     * @var string
     */
    protected $table = 'winter_season_team_payments';

    /**
     * The attributes that are mass assignable.
     *
     * @var array
     */
    protected $fillable = ['seasonId', 'teamId', 'paymentType'];
}