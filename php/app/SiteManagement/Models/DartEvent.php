<?php

namespace TrentonDarts\SiteManagement\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\SoftDeletes;

class DartEvent extends Model
{
    public static $eventTypes = [
        1 => 'GTDL Event',
        2 => 'Charity Dart Event',
        3 => 'Regional Event',
        4 => 'GTDL Sponsored Event',
        5 => 'GTDL All Stars',
        6 => 'GTDL Player Event',
        7 => 'Charity Event',
        8 => 'DPNY Series',
        9 => 'CDC Series',
        10 => 'DPNJ Series',
        11 => 'Qualifier'];

    use SoftDeletes;
    /**
     * The database table used by the model.
     *
     * @var string
     */
    protected $table = 'dart_events';

    /**
     * The attributes that are mass assignable.
     *
     * @var array
     */
    protected $fillable = ['id',
        'name',
        'eventContact',
        'eventContact2',
        'eventTypeId',
        'eventDate',
        'eventEndDate',
        'dartType',
        'imageFileId',
        'posterFileId',
        'posterFile',
        'url',
        'facebookUrl',
        'hostName',
        'hostUrl',
        'hostPhone',
        'locationName',
        'address1',
        'address2',
        'city',
        'state',
        'zip',
        'registrationStartTime',
        'registrationEndTime',
        'dartStart',
        'mapUrl',
        'description'];

    public function eventType()
    {
        return self::$eventTypes[$this->eventTypeId];
    }

    public function results()
    {
        return $this->hasMany('TrentonDarts\SiteManagement\Models\DartEventResult', 'eventId');
    }

    public function getEventDateAttribute($value)
    {
        if($value == null)
            return '';
        return $value;
    }

    public function getEventEndDateAttribute($value)
    {
        if($value == null)
            return '';
        return $value;
        //return date('m-d-Y', strtotime($value));
    }
}