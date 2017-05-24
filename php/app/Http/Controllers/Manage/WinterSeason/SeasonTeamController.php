<?php

namespace TrentonDarts\Http\Controllers\Manage\WinterSeason;

use Illuminate\Http\Request;

use TrentonDarts\Http\Requests;
use TrentonDarts\Http\Controllers\Controller;

use TrentonDarts\Http\Requests\StoreLeagueManagementRequest;

use TrentonDarts\LeagueManagement\Models\WinterSeason;
use TrentonDarts\LeagueManagement\Models\Team;
use TrentonDarts\LeagueManagement\Models\WinterSeasonTeam;

class SeasonTeamController extends Controller
{
    /**
     * Display a listing of the resource.
     *
     * @return Response
     */
    public function index($leagueId, $seasonId)
    {
        $season = WinterSeason::findOrFail($seasonId);
        $seasonTeams = WinterSeasonTeam::where('seasonId', $seasonId)->get()->sortBy(function($seasonTeam, $key){
            return $seasonTeam->team->name;
        });
        return view('manage.season.winter.team.list', ['leagueId' => $leagueId, 'season' => $season, 'seasonTeams' => $seasonTeams]);
    }

    /**
     * Show the form for creating a new resource.
     *
     * @return Response
     */
    public function create($leagueId, $seasonId)
    {
        $season = WinterSeason::findOrFail($seasonId);
        $seasonTeams = WinterSeasonTeam::where('seasonId', $seasonId)->get()->pluck('teamId');
        $teams = Team::whereNotIn('id', $seasonTeams)->orderBy('name')->get()->lists('name', 'id');
        return view('manage.season.winter.team.create', ['leagueId' => $leagueId, 'season' => $season, 'teams' => $teams]);
    }

    /**
     * Store a newly created resource in storage.
     *
     * @param  Request  $request
     * @return Response
     */
    public function store($leagueId, $seasonId, StoreLeagueManagementRequest $request)
    {
        $this->validate($request, [
            'teamId' => 'required',
            'preSeasonDiv' => 'max:4',
            'regularSeasonDiv' => 'max:4'
        ]);

        $seasonTeam = WinterSeasonTeam::create($request->input());
        $seasonTeam->seasonId = $seasonId;
        $seasonTeam->save();

        return redirect()->route('manage.seasonTeam.index', ['leagueId' => $leagueId, 'seasonId' => $seasonId]);
    }

    /**
     * Display the specified resource.
     *
     * @param  int  $id
     * @return Response
     */
    public function show($leagueId, $seasonId, $id)
    {
        $season = WinterSeason::findOrFail($seasonId);
        $seasonTeam = WinterSeasonTeam::findOrFail($id);
        $teams = Team::whereIn('id', [$seasonTeam->id])->get()->lists('name', 'id');
        return view('manage.season.winter.team.show', ['leagueId' => $leagueId, 'season' => $season, 'teams' => $teams, 'seasonTeam' => $seasonTeam]);
    }

    /**
     * Show the form for editing the specified resource.
     *
     * @param  int  $id
     * @return Response
     */
    public function edit($leagueId, $seasonId, $id)
    {
        $season = WinterSeason::findOrFail($seasonId);
        $seasonTeam = WinterSeasonTeam::findOrFail($id);
        $teams = Team::where('id', $seasonTeam->teamId)->get()->lists('name', 'id');
        return view('manage.season.winter.team.edit', ['leagueId' => $leagueId, 'season' => $season, 'teams' => $teams, 'seasonTeam' => $seasonTeam]);
    }

    /**
     * Update the specified resource in storage.
     *
     * @param  Request  $request
     * @param  int  $id
     * @return Response
     */
    public function update(StoreLeagueManagementRequest $request, $leagueId, $seasonId, $id)
    {
        $this->validate($request, [
            'teamId' => 'required',
            'preSeasonDiv' => 'max:4',
            'regularSeasonDiv' => 'max:4'
        ]);
        $seasonTeam = WinterSeasonTeam::findOrFail($id);
        $seasonTeam->update($request->input());
        $seasonTeam->save();

        return redirect()->route('manage.seasonTeam.index', ['leagueId' => $leagueId, 'seasonId' => $seasonId]);
    }

    /**
     * Remove the specified resource from storage.
     *
     * @param  int  $id
     * @return Response
     */
    public function destroy($id)
    {
        //
    }
}
