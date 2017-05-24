<?php

namespace TrentonDarts\Http\Controllers\Manage\WinterSeason;

use Illuminate\Http\Request;

use TrentonDarts\Http\Requests;
use TrentonDarts\Http\Controllers\Controller;
use TrentonDarts\Http\Requests\StoreLeagueManagementRequest;

use TrentonDarts\LeagueManagement\Models\WinterSeason;
use TrentonDarts\LeagueManagement\Models\WinterSeasonTeam;
use TrentonDarts\LeagueManagement\Models\Player;
use TrentonDarts\LeagueManagement\Models\WinterSeasonTeamPlayer;

class SeasonTeamPlayerController extends Controller
{
    private $roles = ['Player' => 'Player', 'Captain' => 'Captain', 'Co-Captain' => 'Co-Captain'];
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
    public function create($leagueId, $seasonId, $teamId)
    {
        $season = WinterSeason::findOrFail($seasonId);
        $seasonTeam = WinterSeasonTeam::findOrFail($teamId);
        $seasonPlayers = WinterSeasonTeamPlayer::where('seasonId', $seasonId)->get()->pluck('playerId');
        $players = Player::whereNotIn('id', $seasonPlayers)->orderBy('firstName')->orderBy('lastName')->get()->lists('name', 'id');
        return view('manage.season.winter.teamplayer.create', [
            'leagueId' => $leagueId,
            'season' => $season,
            'seasonTeam' => $seasonTeam,
            'players' => $players,
            'roles' => $this->roles]);
    }

    /**
     * Store a newly created resource in storage.
     *
     * @param  Request  $request
     * @return Response
     */
    public function store($leagueId, $seasonId, $teamId, StoreLeagueManagementRequest $request)
    {
        $this->validate($request, [
            'playerId' => 'required',
            'role' => 'required|max:255'
        ]);

        $seasonTeamPlayer = WinterSeasonTeamPlayer::create($request->input());
        $seasonTeamPlayer->leagueId = $leagueId;
        $seasonTeamPlayer->seasonId = $seasonId;
        $seasonTeamPlayer->seasonTeamId = $teamId;
        $seasonTeamPlayer->save();

        return redirect()->route('manage.seasonTeam.show', ['leagueId' => $leagueId, 'seasonId' => $seasonId, 'teamId'=> $teamId]);
    }

    /**
     * Display the specified resource.
     *
     * @param  int  $id
     * @return Response
     */
    public function show($id)
    {
        //
    }

    /**
     * Show the form for editing the specified resource.
     *
     * @param  int  $id
     * @return Response
     */
    public function edit($leagueId, $seasonId, $teamId, $id)
    {
        $season = WinterSeason::findOrFail($seasonId);
        $seasonTeam = WinterSeasonTeam::findOrFail($teamId);
        $teamPlayer = WinterSeasonTeamPlayer::findOrFail($id);
        $players = Player::whereIn('id', [$teamPlayer->playerId])->orderBy('firstName')->orderBy('lastName')->get()->lists('name', 'id');
        return view('manage.season.winter.teamplayer.edit', [
            'leagueId' => $leagueId,
            'season' => $season,
            'seasonTeam' => $seasonTeam,
            'teamPlayer' => $teamPlayer,
            'players' => $players,
            'roles' => $this->roles]);

    }

    /**
     * Update the specified resource in storage.
     *
     * @param  Request  $request
     * @param  int  $id
     * @return Response
     */
    public function update($leagueId, $seasonId, $teamId, StoreLeagueManagementRequest $request, $id)
    {
        $this->validate($request, [
            'playerId' => 'required',
            'role' => 'required|max:255'
        ]);

        $seasonTeamPlayer = WinterSeasonTeamPlayer::findOrFail($id);
        $seasonTeamPlayer->update($request->input());
        $seasonTeamPlayer->save();

        return redirect()->route('manage.seasonTeam.show', ['leagueId' => $leagueId, 'seasonId' => $seasonId, 'teamId' => $teamId]);

    }

    /**
     * Remove the specified resource from storage.
     *
     * @param  int  $id
     * @return Response
     */
    public function destroy($leagueId, $seasonId, $teamId, $id)
    {
        $seasonTeamPlayer = WinterSeasonTeamPlayer::findOrFail($id);
        $seasonTeamPlayer->delete();
        return redirect()->route('manage.seasonTeam.show', ['leagueId' => $leagueId, 'seasonId' => $seasonId, 'teamId' => $teamId]);
    }
}