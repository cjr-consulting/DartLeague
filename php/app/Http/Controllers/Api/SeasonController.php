<?php

namespace TrentonDarts\Http\Controllers\Api;

use Illuminate\Http\Request;

use TrentonDarts\Http\Requests;
use TrentonDarts\Http\Controllers\Controller;

use TrentonDarts\LeagueManagement\Models\WinterSeason;

class SeasonController extends Controller
{
    /**
     * Display a listing of the resource.
     *
     * @return Response
     */
    public function index()
    {
        $seasons = WinterSeason::where('leagueId', 1)->orderBy('startYear', 'desc')->get();
        return response()->json($seasons);
    }

    /**
     * Show the form for creating a new resource.
     *
     * @return Response
     */
    public function create()
    {
        //
    }

    /**
     * Store a newly created resource in storage.
     *
     * @param  Request  $request
     * @return Response
     */
    public function store(Request $request)
    {
        $this->validate($request, [
            'name' => 'required|unique:winter_seasons|max:255'
        ]);

        $season = WinterSeason::create($request->input());
        $season->leagueId = 1;
        $season->save();

        return response("", 201);
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
    public function edit($id)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     *
     * @param  Request  $request
     * @param  int  $id
     * @return Response
     */
    public function update(Request $request, $id)
    {
        //
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
