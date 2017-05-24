<?php

namespace TrentonDarts\Http\Controllers\Manage\WinterSeason;

use Illuminate\Http\Request;

use TrentonDarts\Http\Requests;
use TrentonDarts\Http\Controllers\Controller;
use TrentonDarts\Http\Requests\StoreLeagueManagementRequest;

use TrentonDarts\LeagueManagement\Models\WinterSeasonWeek;
use TrentonDarts\LeagueManagement\Models\WinterSeason;
use TrentonDarts\LeagueManagement\Models\WinterSeasonTeam;

class SeasonWeekController extends Controller
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
    public function create($leagueId, $seasonId)
    {
        //
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
            'weekType' => 'required',
            'date' => 'required|date'
        ]);

        $seasonWeek = WinterSeasonWeek::create($request->input());
        $seasonWeek->leagueId = $leagueId;
        $seasonWeek->seasonId = $seasonId;
        $seasonWeek->save();

        return redirect()->route('manage.season.show', ['leagueId' => $leagueId, 'seasonId' => $seasonId]);

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
        $seasonWeek = WinterSeasonWeek::findOrFail($id);
        $divName = $seasonWeek->weekType == "pre" ? "preSeasonDiv" : "regularSeasonDiv";
        $divisions = WinterSeasonTeam::where('seasonId', $seasonId)->orderBy($divName)->groupBy($divName)->get()->pluck($divName);
        $d = [];
        foreach($divisions as $division){
            if($division == '') continue;
            $v = substr($division, 0, 1);
            if(!in_array($v, $d)){
                array_push($d, $v);
            }
        }

        $divisions = $d;
        $schedules = [];
        foreach($divisions as $division) {
            if($division =='') continue;
            $byeTeamNames = [];
            foreach($season->teams()->where($divName, $division)->get() as $scheduleTeam){
                array_push($byeTeamNames, $scheduleTeam->team->name);
            }
            $matches = $seasonWeek->matches()->where('division', 'like', $division.'%')->get();
            foreach($matches as $match){
                $key = array_search($match->homeTeam->name, $byeTeamNames);
                if($key!==false){
                    unset($byeTeamNames[$key]);
                }
                $key = array_search($match->awayTeam->name, $byeTeamNames);
                if($key!==false){
                    unset($byeTeamNames[$key]);
                }
            }
            array_push($schedules, new DivisionSchedule($division, $matches, $byeTeamNames));
        }
        return view('manage.season.winter.week.show', ['leagueId' => $leagueId, 'season' => $season, 'seasonWeek' => $seasonWeek, 'schedules' => $schedules]);
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
        $seasonWeek = WinterSeasonWeek::findOrFail($id);

        return view('manage.season.winter.week.edit', ['leagueId' => $leagueId, 'season' => $season, 'seasonWeek' => $seasonWeek]);
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
            'date' => 'required|date'
        ]);

        $week = WinterSeasonWeek::findOrFail($id);
        $week->date = $request->input('date');
        $week->save();
        return redirect()->route('manage.season.show', ['leagueId' => $leagueId, 'seasonId' => $seasonId]);
    }

    /**
     * Remove the specified resource from storage.
     *
     * @param  int  $id
     * @return Response
     */
    public function destroy($leagueId, $seasonId, $id)
    {
        $seasonWeek = WinterSeasonWeek::findOrFail($id);
        $seasonWeek->delete();
        return redirect()->route('manage.season.show', ['leagueId' => $leagueId, 'seasonId' => $seasonId]);
    }
}

class DivisionSchedule
{
    public $name = '';
    public $byeTeamNames = [];
    public $matches;

    public function __construct($name, $matches, $byeTeamNames)
    {
        $this->name = $name;
        $this->byeTeamNames = $byeTeamNames;
        $this->matches = $matches;
    }
}