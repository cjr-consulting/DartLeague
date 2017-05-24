<?php

namespace TrentonDarts\Http\Controllers\Manage;

use TrentonDarts\Http\Controllers\Controller;
use TrentonDarts\Http\Requests\StoreLeagueManagementRequest;
use TrentonDarts\LeagueManagement\Services\BrowsableFileService;
use TrentonDarts\SiteManagement\Models\DartEvent;

class DartEventsController extends Controller
{
    public function index($leagueId)
    {
        $events = DartEvent::orderBy('eventDate', 'desc')->get();
        return view('site.dartevent.list', [
            'leagueId' => $leagueId,
            'dartEvents' => $events]);
    }

    public function create($leagueId)
    {
        return view('site.dartevent.create', [
            'leagueId' => $leagueId,
            'eventTypes' => DartEvent::$eventTypes
        ]);
    }

    public function store($leagueId, StoreLeagueManagementRequest $request)
    {
        $browsableFileService = new BrowsableFileService();
        $this->validate($request, [
            'name' => 'required|unique:dart_events|max:255',
            'eventDate' => 'required',
            'eventTypeId' => 'required',
            'dartType' => 'required',
            'locationName' => 'required'
        ]);

        $dartEvent = DartEvent::create($request->input());
        $dartEvent->save();

        if($request->hasFile('eventImage') && $request->file('eventImage')->isValid()) {
            $uploadFile = $request->file('eventImage');
            $dartEvent->imageFileId = $browsableFileService->saveBrowsableFile(
                'dartevents',
                file_get_contents($uploadFile->getRealPath()),
                $uploadFile->getMimeType(),
                $this->slug($dartEvent->name).'-img',
                $uploadFile->guessExtension());
        }

        if($request->hasFile('posterDocument') && $request->file('posterDocument')->isValid()) {
            $uploadFile = $request->file('posterDocument');
            $dartEvent->posterFileId = $browsableFileService->saveBrowsableFile(
                'dartevents',
                file_get_contents($uploadFile->getRealPath()),
                $uploadFile->getMimeType(),
                $this->slug($dartEvent->name).'-doc',
                $uploadFile->guessExtension());
        }

        $dartEvent->save();
        return redirect()->route('manage.site.dartevent.index', ['leagueId' => $leagueId]);
    }

    public function show($leagueId, $id)
    {
    }

    public function edit($leagueId, $id)
    {
        $dartEvent = DartEvent::findOrFail($id);

        return view('site.dartevent.edit',[
            'leagueId' => $leagueId,
            'dartEvent' => $dartEvent,
            'eventTypes' => DartEvent::$eventTypes
        ]);
    }

    public function update(StoreLeagueManagementRequest $request, $leagueId, $id)
    {
        $browsableFileService = new BrowsableFileService();

        $this->validate($request, [
            'name' => 'required|unique:dart_events,name,'.$id.'|max:255',
            'eventDate' => 'required',
            'eventTypeId' => 'required',
            'dartType' => 'required',
            'locationName' => 'required'
        ]);
        $dartEvent = DartEvent::findOrFail($id);
        $dartEvent->update($request->input());

        if($request->hasFile('eventImage') && $request->file('eventImage')->isValid()) {
            $uploadFile = $request->file('eventImage');
            $dartEvent->imageFileId = $browsableFileService->saveBrowsableFile(
                'dartevents',
                file_get_contents($uploadFile->getRealPath()),
                $uploadFile->getMimeType(),
                $this->slug($dartEvent->name).'-img',
                $uploadFile->guessExtension());
        }

        if($request->hasFile('posterDocument') && $request->file('posterDocument')->isValid()) {
            $uploadFile = $request->file('posterDocument');
            $dartEvent->posterFileId = $browsableFileService->saveBrowsableFile(
                'dartevents',
                file_get_contents($uploadFile->getRealPath()),
                $uploadFile->getMimeType(),
                $this->slug($dartEvent->name).'-doc',
                $uploadFile->guessExtension());
        }

        $dartEvent->save();

        return redirect()->route('manage.site.dartevent.index', ['leagueId' => $leagueId]);
    }

    public function destroy($leagueId, $id)
    {
        $dartEvent = DartEvent::findOrFail($id);
        $dartEvent->delete();

        return redirect()->route('manage.site.dartevent.index', ['leagueId' => $leagueId]);
    }

    public function activate($leagueId, $id, StoreLeagueManagementRequest $request)
    {
        $isTitleEvent = false;
        $titleEvents = DartEvent::where('isTitleEvent', true)->get();

        foreach($titleEvents as $titleEvent) {
            if($titleEvent->id == (int) $id)
                $isTitleEvent = true;
            $titleEvent->isTitleEvent = false;
            $titleEvent->save();
        }

        if(!$isTitleEvent) {
            $titleEvent = DartEvent::findorFail($id);
            $titleEvent->isTitleEvent = true;
            $titleEvent->save();
        }

        return redirect()->route('manage.site.dartevent.index', ['leagueId' => $leagueId]);
    }

    private function slug($z)
    {
        $z = strtolower($z);
        $z = preg_replace('/[^a-z0-9 -]+/', '', $z);
        $z = str_replace(' ', '-', $z);
        return trim($z, '-');
    }
}