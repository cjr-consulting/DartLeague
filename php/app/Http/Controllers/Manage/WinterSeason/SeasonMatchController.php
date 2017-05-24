<?php

namespace TrentonDarts\Http\Controllers\Manage\WinterSeason;

use Illuminate\Http\Request;

use TrentonDarts\Http\Requests;
use TrentonDarts\Http\Controllers\Controller;
use TrentonDarts\Http\Requests\StoreLeagueManagementRequest;

use TrentonDarts\LeagueManagement\Models\WinterSeasonMatch;
use TrentonDarts\LeagueManagement\Models\WinterSeasonWeek;
use TrentonDarts\LeagueManagement\Models\WinterSeason;
use TrentonDarts\LeagueManagement\Models\WinterSeasonTeam;
use TrentonDarts\LeagueManagement\Models\Team;
use TrentonDarts\LeagueManagement\Models\MatchType;

class SeasonMatchController extends Controller
{
    /**
     * Display a listing of the resource.
     *
     * @return Response
     */
    public function index()
    {
        //
    }

    /**
     * Show the form for creating a new resource.
     *
     * @return Response
     */
    public function create($leagueId, $seasonId, $weekId, StoreLeagueManagementRequest $request)
    {
        $season = WinterSeason::findOrFail($seasonId);
        $seasonWeek = WinterSeasonWeek::findOrFail($weekId);
        $seasonMatch = new WinterSeasonMatch();
        $seasonMatch->matchTypeId = $season->defaultMatchTypeId;
        $seasonPart = 'regularSeasonDiv';
        $division = $request->input('division');
        if($seasonWeek->weekType == 'pre')
        {
            $seasonPart = 'preSeasonDiv';
        }
        $seasonTeams = WinterSeasonTeam::where('seasonId', $seasonId)->Where($seasonPart, 'like', substr($division, 1, 1).'%')->get()->pluck('teamId')->toArray();

        foreach($seasonWeek->matches as $match){
            $key = array_search($match->homeTeamId, $seasonTeams);
            if($key!==false){
                unset($seasonTeams[$key]);
            }
            $key = array_search($match->awayTeamId, $seasonTeams);
            if($key!==false){
                unset($seasonTeams[$key]);
            }
        }
        $teams = Team::whereIn('id', $seasonTeams)->orderBy('name')->get()->lists('name', 'id');
        $matchTypes = MatchType::orderBy('name')->get()->lists('name', 'id');
        return view('manage.season.winter.match.create',
            array('leagueId' => $leagueId,
                'season' => $season,
                'winterSeasonMatch' => $seasonMatch,
                'seasonWeek' => $seasonWeek,
                'matchTypes' => $matchTypes,
                'teams' => $teams,
                'division' => $division));
    }

    /**
     * Store a newly created resource in storage.
     *
     * @param  Request  $request
     * @return Response
     */
    public function store($leagueId, $seasonId, $weekId, StoreLeagueManagementRequest $request)
    {
        $this->validate($request, [
            'homeTeamId' => 'required',
            'awayTeamId' => 'required',
            'matchTypeId' => 'required'
        ]);

        $division = $request->input('division');

        $seasonMatch = WinterSeasonMatch::create($request->input());
        $seasonMatch->weekId = $weekId;
        $seasonMatch->seasonId = $seasonId;
        $seasonMatch->division = $division;
        $seasonMatch->save();

        return redirect()->route('manage.season.week.show', ['leagueId' => $leagueId, 'seasonId' => $seasonId, 'id' => $weekId]);
    }

    /**
     * Display the specified resource.
     *
     * @param  int  $id
     * @return Response
     */
    public function show($leagueId, $seasonId, $weekId, $id, StoreLeagueManagementRequest $request)
    {

    }

    /**
     * Show the form for editing the specified resource.
     *
     * @param  int  $id
     * @return Response
     */
    public function edit($leagueId, $seasonId, $weekId, $id, StoreLeagueManagementRequest $request)
    {
        $season = WinterSeason::findOrFail($seasonId);
        $seasonWeek = WinterSeasonWeek::findOrFail($weekId);
        $seasonMatch = WinterSeasonMatch::findOrFail($id);
        $seasonPart = 'regularSeasonDiv';
        $division = $request->input('division');
        if ($seasonWeek->weekType == 'pre') {
            $seasonPart = 'preSeasonDiv';
        }
        $seasonTeams = WinterSeasonTeam::where('seasonId', $seasonId)->Where($seasonPart, 'like', substr($division, 1, 1) . '%')->get()->pluck('teamId')->toArray();

        foreach ($seasonWeek->matches as $match) {
            if($match->id == $id) continue;
            $key = array_search($match->homeTeamId, $seasonTeams);
            if ($key !== false) {
                unset($seasonTeams[$key]);
            }
            $key = array_search($match->awayTeamId, $seasonTeams);
            if ($key !== false) {
                unset($seasonTeams[$key]);
            }
        }

        $teams = Team::whereIn('id', $seasonTeams)->orderBy('name')->get()->lists('name', 'id');
        $matchTypes = MatchType::orderBy('name')->get()->lists('name', 'id');

        return view('manage.season.winter.match.edit',
            ['leagueId' => $leagueId,
                'season' => $season,
                'winterSeasonMatch' => $seasonMatch,
                'seasonWeek' => $seasonWeek,
                'matchTypes' => $matchTypes,
                'teams' => $teams,
                'division' => $division]);
    }

    /**
     * Update the specified resource in storage.
     *
     * @param  Request  $request
     * @param  int  $id
     * @return Response
     */
    public function update($leagueId, $seasonId, $weekId, $id, StoreLeagueManagementRequest $request)
    {
        $this->validate($request, [
            'homeTeamId' => 'required',
            'awayTeamId' => 'required',
            'matchTypeId' => 'required'
        ]);

        $division = $request->input('division');

        $seasonMatch = WinterSeasonMatch::findOrFail($id);
        $seasonMatch->update($request->input());
        $seasonMatch->weekId = $weekId;
        $seasonMatch->seasonId = $seasonId;
        $seasonMatch->division = $division;
        $seasonMatch->save();

        return redirect()->route('manage.season.week.show', ['leagueId' => $leagueId, 'seasonId' => $seasonId, 'id' => $weekId]);
    }

    /**
     * Remove the specified resource from storage.
     *
     * @param  int  $id
     * @return Response
     */
    public function destroy($leagueId, $seasonId, $weekId, $id)
    {
        $seasonMatch = WinterSeasonMatch::findOrFail($id);
        $seasonMatch->delete();

        return redirect()->route('manage.season.week.show', ['leagueId' => $leagueId, 'seasonId' => $seasonId, 'id' => $weekId]);
    }
}
