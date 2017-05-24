<?php

namespace TrentonDarts\Http\Controllers\Manage;

use TrentonDarts\Http\Controllers\Controller;
use TrentonDarts\Http\Requests\StoreLeagueManagementRequest;

use Illuminate\Support\Facades\Input;
use TrentonDarts\LeagueManagement\Models\Team;
use TrentonDarts\LeagueManagement\Models\Sponsor;

class SponsorController extends Controller
{
    public function index($leagueId)
    {
        $sponsors = Sponsor::with('teams')->where('leagueId', $leagueId)->orderBy('type')->orderBy('name')->get();
        return view('manage.sponsor.list', ['leagueId' => $leagueId, 'sponsors' => $sponsors]);
    }

    public function create($leagueId)
    {
        return view('manage.sponsor.create', ['leagueId' => $leagueId, 'sponsorTypes' => Sponsor::$sponsorTypes]);
    }

    public function store($leagueId, StoreLeagueManagementRequest $request)
    {
        $this->validate($request, [
            'name' => 'required|unique:sponsors|max:255',
            'type' => 'required|max:1'
            ]);

        $sponsor = Sponsor::create(Input::all());
        $sponsor->leagueId = $leagueId;
        $sponsor->save();

        return redirect()->route('manage.sponsor.index', ['leagueId' => $leagueId]);
    }

    public function edit($leagueId, $id)
    {
        $sponsor = Sponsor::find($id);

        return view('manage.sponsor.edit', [
            'leagueId' => $leagueId,
            'sponsor' => $sponsor,
            'sponsorTypes' => Sponsor::$sponsorTypes]);
    }

    public function update($leagueId, $id, StoreLeagueManagementRequest $request)
    {
        $this->validate($request, [
            'name' => 'required|unique:sponsors,name,'.$id.'|max:255',
            'type' => 'required|max:1'
            ]);

        $sponsor = Sponsor::findOrFail($id);
        $sponsor->update(Input::all());
        $sponsor->save();

        return redirect()->route('manage.sponsor.index', ['leagueId' => $leagueId]);
    }

    public function destroy($leagueId, $id)
    {
        $sponsor = Sponsor::findOrFail($id);
        $sponsor->delete();

        return redirect()->route('manage.sponsor.index', ['leagueId' => $leagueId]);
    }
}
