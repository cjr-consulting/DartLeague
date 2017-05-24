<?php

namespace TrentonDarts\SiteManagement\Models;

use Illuminate\Database\Eloquent\Model;

class DartEventResult extends Model
{
    protected $table = 'dart_event_results';

    /**
     * The attributes that are mass assignable.
     *
     * @var array
     */
    protected $fillable = ['id',
        'eventId',
        'specificEventName',
        'playerId',
        'finished',
        'orderId'];

    public function dartEvent()
    {
        return $this->belongsTo('TrentonDarts\SiteManagement\Models\DartEvent', 'eventId');
    }

    public function player()
    {
        return $this->belongsTo('TrentonDarts\LeagueManagement\Models\Player', 'playerId');
    }
}