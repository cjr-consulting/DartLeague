<?php

namespace TrentonDarts\Http\Controllers\Manage;

use Illuminate\Http\Request;

use TrentonDarts\Http\Requests;
use TrentonDarts\Http\Controllers\Controller;

use TrentonDarts\Http\Requests\StoreLeagueManagementRequest;

use TrentonDarts\User;
use TrentonDarts\LeagueManagement\Models\Player;

class PlayerController extends Controller
{
    /**
     * Display a listing of the resource.
     *
     * @return Response
     */
    public function index($leagueId)
    {
        $players = Player::where('leagueId', $leagueId)->orderBy('firstName')->get();
        return view('manage.player.list', ['leagueId' => $leagueId, 'players' => $players]);
    }

    /**
     * Show the form for creating a new resource.
     *
     * @return Response
     */
    public function create($leagueId)
    {
        $users = User::orderBy('name')->lists('name', 'id');
        return view('manage.player.create', ['leagueId' => $leagueId, 'users' => $users]);
    }

    /**
     * Store a newly created resource in storage.
     *
     * @param  Request  $request
     * @return Response
     */
    public function store($leagueId, StoreLeagueManagementRequest $request)
    {
        $this->validate($request, [
            'email' => 'email|unique:players|max:255'
        ]);

        $player = Player::create($request->input());
        $player->leagueId = $leagueId;
        $player->save();

        return redirect()->route('manage.player.index', ['leagueId' => $leagueId]);
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
    public function edit($leagueId, $id)
    {
        $player = Player::find($id);
        $users = User::orderBy('name')->lists('name', 'id');
        return view('manage.player.edit', ['leagueId' => $leagueId, 'player' => $player, 'users' => $users]);
    }

    /**
     * Update the specified resource in storage.
     *
     * @param  Request  $request
     * @param  int  $id
     * @return Response
     */
    public function update($leagueId, $id, StoreLeagueManagementRequest $request)
    {
        $this->validate($request, [
            'email' => 'email|unique:players,email,'.$id.'|max:255'
        ]);
        $player = Player::findOrFail($id);
        $player->update($request->input());
        $player->save();
        return redirect()->route('manage.player.index', ['leagueId' => $leagueId]);
    }

    /**
     * Remove the specified resource from storage.
     *
     * @param  int  $id
     * @return Response
     */
    public function destroy($leagueId, $id)
    {
        $player = Player::findOrFail($id);
        $player->delete();

        return redirect()->route('manage.player.index', ['leagueId' => $leagueId]);
    }
}
