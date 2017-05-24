<?php

namespace TrentonDarts\Http\Controllers\Manage;

use TrentonDarts\Http\Controllers\Controller;
use TrentonDarts\Http\Requests\StoreLeagueManagementRequest;
use TrentonDarts\LeagueManagement\Models\Player;
use TrentonDarts\SiteManagement\Models\DartEvent;
use TrentonDarts\SiteManagement\Models\DartEventResult;

class DartEventResultsController extends Controller
{
    public function index($leagueId, $dartEventId)
    {
        $event = DartEvent::findOrFail($dartEventId);
        $players = Player::orderBy('firstName')->orderBy('lastName')->get()->lists('name', 'id');
        return view('site.dartevent.result.list', [
            'leagueId' => $leagueId,
            'dartEvent' => $event,
            'players' => $players]);
    }

    public function create($leagueId, $dartEventId)
    {
    }

    public function store($leagueId, $dartEventId, StoreLeagueManagementRequest $request)
    {
        $event = DartEvent::findOrFail($dartEventId);
        $count = $event->results()->max('orderId');
        $eventResult = DartEventResult::create($request->input());
        $eventResult->eventId = $dartEventId;
        $eventResult->orderId = $count + 1;
        $eventResult->save();
        return redirect()->route('manage.site.dartevent.result.index', ['leagueId' => $leagueId, 'dartEventId' => $dartEventId]);
    }

    public function show($leagueId, $id)
    {
    }

    public function edit($leagueId, $dartEventId, $id)
    {
    }

    public function update(StoreLeagueManagementRequest $request, $leagueId, $dartEventId, $id)
    {
    }

    public function destroy($leagueId, $dartEventId, $id)
    {
        $eventResult = DartEventResult::FindOrFail($id);
        $eventResult->delete();

        $event = DartEvent::findOrFail($dartEventId);
        $orderId = 1;
        foreach($event->results()->orderBy('orderId')->get() as $result)
        {
            $result->orderId = $orderId;
            $result->save();
            $orderId++;
        }

        return redirect()->route('manage.site.dartevent.result.index',
            ['leagueId' => $leagueId,
                'dartEventId' => $dartEventId]);
    }
}