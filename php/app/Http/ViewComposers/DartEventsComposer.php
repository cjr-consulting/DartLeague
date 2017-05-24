<?php

namespace TrentonDarts\Http\ViewComposers;

use Illuminate\Contracts\View\View;
use TrentonDarts\SiteManagement\Models\DartEvent;

class DartEventsComposer
{
    public function compose(View $view)
    {
        $events = DartEvent::where('eventDate', '>=', date('Y-m-d'))->orderBy('eventDate', 'asc')->get();
        return $view->with(['events' => $events]);
    }
}