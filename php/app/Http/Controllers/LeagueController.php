<?php

namespace TrentonDarts\Http\Controllers;

use Illuminate\Http\Request;
use TrentonDarts\Http\Requests;
use TrentonDarts\LeagueManagement\Models\Sponsor;

class LeagueController extends Controller
{
    public function sponsors(Request $request)
    {
        $sponsorTypes = Sponsor::$sponsorTypes;
        $selectedType = $request->query('type', 'L');
        $sponsors = Sponsor::where('type', $selectedType)->get();

        return view('sponsors.list', [
            'sponsors' => $sponsors,
            'sponsorTypes' => $sponsorTypes,
            'selectedType' => $selectedType]);
    }
}