<?php

namespace TrentonDarts\Http\Controllers\Manage;

use TrentonDarts\Http\Controllers\Controller;
use TrentonDarts\Http\Requests\StoreLeagueManagementRequest;

use Illuminate\Support\Facades\Input;
use TrentonDarts\LeagueManagement\Models\Team;
use TrentonDarts\LeagueManagement\Models\Sponsor;

class TeamController extends Controller
{
    public function index($leagueId)
    {
        $teams = Team::with('sponsor')->where('leagueId', $leagueId)->orderBy('name')->get();
        return view('manage.team.list', ['leagueId' => $leagueId, 'teams' => $teams]);
    }

    public function create($leagueId, StoreLeagueManagementRequest $request)
    {
        $sponsorList = Sponsor::where('type','T')->orderBy('name')->lists('name', 'id');
        return view('manage.team.create', ['leagueId' => $leagueId, 'sponsors' => $sponsorList]);
    }

    public function store($leagueId, StoreLeagueManagementRequest $request)
    {
        $this->validate($request, [
            'name' => 'required|unique:teams|max:255'
            ]);

        $team = Team::create(Input::all());
        $team->leagueId = $leagueId;
        $team->save();

        return redirect()->route('manage.team.index', ['leagueId' => $leagueId]);
    }

    public function edit($leagueId, $id)
    {
        $team = Team::find($id);
        $sponsorList = Sponsor::where('type','T')->orderBy('name')->lists('name', 'id');
        return view('manage.team.edit', ['leagueId' => $leagueId, 'team' => $team, 'sponsors' => $sponsorList]);
    }

    public function update($leagueId, $id, StoreLeagueManagementRequest $request)
    {
        $this->validate($request, [
            'name' => 'required|unique:teams,name,'.$id.'|max:255'
            ]);
        $team = Team::findOrFail($id);
        $team->update(Input::all());
        $team->save();
        return redirect()->route('manage.team.index', ['leagueId' => $leagueId]);
    }

    public function destroy($leagueId, $id)
    {
        $team = Team::findOrFail($id);
        $team->delete();

        return redirect()->route('manage.team.index', ['leagueId' => $leagueId]);
    }
}
