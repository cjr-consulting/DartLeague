<?php

namespace TrentonDarts\Http\Controllers;

use TrentonDarts\SiteManagement\Models\DartEvent;
use TrentonDarts\SiteManagement\Models\PagePart;

class HomeController extends Controller
{
    public function index()
    {
        $titleEvent = DartEvent::where('isTitleEvent', true)->first();
        if($titleEvent == null) {
            $titleEvent = DartEvent::where('eventDate', '>=', date('Y-m-d'))->orderBy('eventDate', 'asc')->first();
        }

        $pageParts = PagePart::where('name', 'MainPageHeader')->get();
        $welcomeMessage = '';
        if(!$pageParts->isEmpty()) {
            $welcomeMessage = $pageParts->first()->html;
        }

        return view('welcome', ['titleEvent' => $titleEvent,
        'welcomeMessage' => $welcomeMessage]);
    }
}