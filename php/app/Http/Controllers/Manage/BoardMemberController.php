<?php

namespace TrentonDarts\Http\Controllers\Manage;

use Illuminate\Http\Request;
use Illuminate\Support\Facades\Input;

use TrentonDarts\Http\Requests;
use TrentonDarts\Http\Requests\StoreLeagueManagementRequest;
use TrentonDarts\Http\Controllers\Controller;

use TrentonDarts\LeagueManagement\Models\BoardMember;
use TrentonDarts\LeagueManagement\Models\WinterSeason;
use TrentonDarts\User;

class BoardMemberController extends Controller
{
    /**
     * Display a listing of the resource.
     *
     * @return Response
     */
    public function index($leagueId)
    {
        $boardMembers = BoardMember::where('leagueId', $leagueId)->orderBy('name')->get();
        return view('manage.boardmember.list', ['leagueId'=> $leagueId, 'boardMembers' => $boardMembers]);
    }

    /**
     * Show the form for creating a new resource.
     *
     * @return Response
     */
    public function create($leagueId)
    {
        $users = User::orderBy('name')->lists('name', 'id');
        $seasons = WinterSeason::orderBy('startYear')->lists('name', 'id');

        return view('manage.boardmember.create', ['leagueId' => $leagueId, 'users' => $users, 'seasons' => $seasons]);
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
            'name' => 'required|unique:board_members|max:255'
            ]);

        $boardMember = BoardMember::create(Input::all());
        $boardMember->leagueId = $leagueId;
        if($boardMember->userId == 0) $boardMember->userId = null;
        $boardMember->save();

        return redirect()->route('manage.boardmember.index', ['leagueId' => $leagueId]);
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
        $boardMember = BoardMember::findOrFail($id);
        $users = User::orderBy('name')->lists('name', 'id');
        $seasons = WinterSeason::orderBy('startYear')->lists('name', 'id');

        return view('manage.boardmember.edit', ['leagueId' => $leagueId, 'boardMember' => $boardMember, 'users' => $users, 'seasons' => $seasons]);
    }

    /**
     * Update the specified resource in storage.
     *
     * @param  Request  $request
     * @param  int  $id
     * @return Response
     */
    public function update(StoreLeagueManagementRequest $request, $leagueId, $id)
    {
        $this->validate($request, [
            'name' => 'required|unique:board_members,name,'.$id.'|max:255'
            ]);
        $boardMember = BoardMember::findOrFail($id);
        $boardMember->update(Input::all());
        if($boardMember->userId == 0) $boardMember->userId = null;
        $boardMember->save();

        return redirect()->route('manage.boardmember.index', ['leagueId' => $leagueId]);
    }

    /**
     * Remove the specified resource from storage.
     *
     * @param  int  $id
     * @return Response
     */
    public function destroy($leagueId, $id)
    {
        $boardMember = BoardMember::findOrFail($id);
        $boardMember->delete();

        return redirect()->route('manage.boardmember.index', ['leagueId' => $leagueId]);
    }
}
