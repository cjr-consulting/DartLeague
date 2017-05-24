<?php

namespace TrentonDarts\Http\Controllers;

use Illuminate\Database\Eloquent\Collection;
use TrentonDarts\SiteManagement\Models\DartEvent;

class EventsController extends Controller
{
    public function allResults()
    {
        $events = DartEvent::where('eventDate', '<=', date('Y-m-d'))->orderBy('eventDate', 'desc')->get();
        $eventResults = [];
        foreach($events as $event)
        {
            foreach($event->results as $result)
            {
                array_push($eventResults, [
                    'eventName' => $event->name,
                    'eventDate' => $event->eventDate,
                    'specificEvent' => $result->specificEventName,
                    'name' => $result->player->name,
                    'place' => $result->finished
                ]);
            }
        }

        return view('event.results', ['eventResults' => new Collection($eventResults)]);
    }
}