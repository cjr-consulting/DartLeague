<div class="panel panel-default">
    <div class="panel-heading"><h3 class="panel-title">Area Events <small><a href="{{ route('event.results') }}">GTDL Player Results</a></small></h3></div>
    @if($events->isEmpty())
        <div class="panel-body">Currently No Events</div>
    @else
        <div class="dart-events">
        @foreach($events as $event)
            <div class="dart-event clearfix">
                <div class="event-date pull-left">
                    <div class="event-date-dayofweek">{{date('D', strtotime($event->eventDate)) }}</div>
                    <div class="event-date-day">{{date('d', strtotime($event->eventDate)) }}</div>
                    <div class="event-date-month">{{date('M', strtotime($event->eventDate)) }}</div>
                    <div class="event-date-year">{{date('Y', strtotime($event->eventDate)) }}</div>
                </div>
                <div class="event-detail pull-left">
                    <div class="event-type pull-left">{{$event->eventType()}}</div>
                    <div class="event-dart-type pull-right">
                        {{ $event->dartType }}
                    </div>

                    <div class="clearfix"></div>
                    <div class="event-name pull-left">
                        @if($event->url != null)
                            <a href="{{$event->url}}" target="_blank">{{$event->name}}</a>
                        @else
                            {{$event->name}}
                        @endif
                            @if($event->posterFileId != null && $event->posterFileId > 0)
                                <a href="{{ route('file.get', ['id' => \TrentonDarts\LeagueManagement\Services\BrowsableFileService::getHashedFileId($event->posterFileId)])  }}" target="_blank"><i class="fa fa-file"></i></a>
                            @endif
                            @if($event->facebookUrl != null)
                                <a href="{{$event->facebookUrl}}"><i class="fa fa-facebook-official"></i></a>
                            @endif
                    </div>
                    @if($event->hostName != null)
                        <div class="pull-left">
                            <small class="hidden-xs">hosted by </small>
                            @if($event->hostUrl != null)
                                <a href="{{$event->hostUrl}}">{{$event->hostName}}</a>
                            @else
                                {{$event->hostName}}
                            @endif
                        </div>
                    @endif
                    <div class="clearfix"></div>
                    <div class="event-location">
                        <div class="event-location-name pull-left">
                            <b>
                            @if($event->mapUrl != null)
                                <a href="{{$event->mapUrl}}" target="_blank"><i class="fa fa-map-o"></i> {{$event->locationName}}</a>
                            @else
                                {{$event->locationName}}
                            @endif
                            </b>
                        </div>
                    @if($event->address1 != '')
                        <div class="visible-xs">
                            <div class="clearfix"></div>
                            <div class="event-address">
                                {{ $event->address1 }}<br/>
                                @if($event->address2 != '')
                                    {{ $event->address2 }}<br/>
                                @endif
                                {{ $event->city }}, {{ $event->state }}  {{ $event->zip }}
                            </div>
                        </div>
                        <div class="hidden-xs pull-left">
                            <div class="event-address">
                                {{ $event->address1 }} {{ $event->address2 }} {{ $event->city }}, {{ $event->state }}  {{ $event->zip }}
                            </div>
                        </div>
                        <div class="clearfix"></div>
                    @endif
                    </div>
                    @if($event->registrationStartTime != null)
                        <div class="event-registration pull-left"><strong>Reg:</strong> {{$event->registrationStartTime}} to {{$event->registrationEndTime}}</div>
                    @endif
                    @if($event->dartStart != '')
                        <div class="event-dart-start pull-left"><strong>Start:</strong> {{$event->dartStart}}</div>
                    @endif
                    @if($event->description != null && $event->description != '')
                        <div class="clearfix"></div>
                        <a style="cursor: pointer" data-toggle="collapse" data-target="#event{{$event->id}}">Details</a>
                    @endif
                    <div class="clearfix"></div>
                </div>
                <div class="clearfix"></div>
                @if($event->description != null && $event->description != '')
                    <div class="event-description">
                        <div id="event{{$event->id}}" class="collapse">
                            <div class="well well-sm">{!! $event->description !!}</div>
                        </div>
                    </div>
                @endif
            </div>
        @endforeach
        </div>
    @endif
</div>