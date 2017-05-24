<?php

namespace TrentonDarts\Http\Controllers\Manage;

use TrentonDarts\Http\Controllers\Controller;

use TrentonDarts\Http\Requests\StoreLeagueManagementRequest;

class LeagueController extends Controller
{
    public function getLeagueDetail($leagueId, StoreLeagueManagementRequest $request)
    {
        return view('manage.leaguedetail', ['leagueId' => $leagueId]);
    }
}
