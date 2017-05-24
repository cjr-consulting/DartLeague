<div class="important-message">
    @if($titleEvent->imageFileId != null && $titleEvent->imageFileId > 0)
        @if($titleEvent->posterFileId != null && $titleEvent->posterFileId > 0)
            <a href="{{ route('file.get', ['id' => \TrentonDarts\LeagueManagement\Services\BrowsableFileService::getHashedFileId($titleEvent->posterFileId)])  }}" target="_blank">
                <img class="important-banner"  src="{{route('file.get', ['id' => \TrentonDarts\LeagueManagement\Services\BrowsableFileService::getHashedFileId($titleEvent->imageFileId)])}}"/>
            </a>
        @elseif($titleEvent->url != null)
            <a href="{{ $titleEvent->url }}" target="_blank">
                <img class="important-banner"  src="{{route('file.get', ['id' => \TrentonDarts\LeagueManagement\Services\BrowsableFileService::getHashedFileId($titleEvent->imageFileId)])}}"/>
            </a>
        @else
            <img class="important-banner"  src="{{route('file.get', ['id' => \TrentonDarts\LeagueManagement\Services\BrowsableFileService::getHashedFileId($titleEvent->imageFileId)])}}"/>
        @endif
        @if($titleEvent->description != null && $titleEvent->description != '')
            <div class="clearfix"></div>
            <a style="cursor: pointer" data-toggle="collapse" data-target="#titleevent{{$titleEvent->id}}">Details</a>
        @endif
        @if($titleEvent->description != null && $titleEvent->description != '')
            <div class="clearfix"></div>
            <div class="event-description">
                <div id="titleevent{{$titleEvent->id}}" class="collapse">
                    <div class="well">{!! $titleEvent->description !!}</div>
                </div>
            </div>
        @endif
    @else
        <div class="dart-event clearfix">
            <div class="event-date pull-left">
                <div class="event-date-dayofweek">{{date('D', strtotime($titleEvent->eventDate)) }}</div>
                <div class="event-date-day">{{date('d', strtotime($titleEvent->eventDate)) }}</div>
                <div class="event-date-month">{{date('M', strtotime($titleEvent->eventDate)) }}</div>
                <div class="event-date-year">{{date('Y', strtotime($titleEvent->eventDate)) }}</div>
            </div>
            <div class="event-detail pull-left">
                <div class="event-type pull-left">{{$titleEvent->eventType()}}</div>
                <div class="event-dart-type pull-right">
                    {{ $titleEvent->dartType }}
                </div>

                <div class="clearfix"></div>
                <div class="event-name pull-left">
                    @if($titleEvent->url != null)
                        <a href="{{$titleEvent->url}}" target="_blank">{{$titleEvent->name}}</a>
                    @else
                        {{$titleEvent->name}}
                    @endif
                    @if($titleEvent->posterFileId != null && $titleEvent->posterFileId > 0)
                        <a href="{{ route('file.get', ['id' => \TrentonDarts\LeagueManagement\Services\BrowsableFileService::getHashedFileId($titleEvent->posterFileId)])  }}" target="_blank"><i class="fa fa-file"></i></a>
                    @endif
                    @if($titleEvent->facebookUrl != null)
                        <a href="{{$titleEvent->facebookUrl}}"><i class="fa fa-facebook-official"></i></a>
                    @endif
                </div>
                @if($titleEvent->hostName != null)
                    <div class="pull-left">
                        <small class="hidden-xs">hosted by </small>
                        @if($titleEvent->hostUrl != null)
                            <a href="{{$titleEvent->hostUrl}}">{{$titleEvent->hostName}}</a>
                        @else
                            {{$titleEvent->hostName}}
                        @endif
                    </div>
                @endif
                <div class="clearfix"></div>
                <div class="event-location">
                    <div class="event-location-name pull-left">
                        <b>
                            @if($titleEvent->mapUrl != null)
                                <a href="{{$titleEvent->mapUrl}}" target="_blank"><i class="fa fa-map-o"></i> {{$titleEvent->locationName}}</a>
                            @else
                                {{$titleEvent->locationName}}
                            @endif
                        </b>
                    </div>
                    @if($titleEvent->address1 != '')
                        <div class="visible-xs">
                            <div class="clearfix"></div>
                            <div class="event-address">
                                {{ $titleEvent->address1 }}<br/>
                                @if($titleEvent->address2 != '')
                                    {{ $titleEvent->address2 }}<br/>
                                @endif
                                {{ $titleEvent->city }}, {{ $titleEvent->state }}  {{ $titleEvent->zip }}
                            </div>
                        </div>
                        <div class="hidden-xs pull-left">
                            <div class="event-address">
                                {{ $titleEvent->address1 }} {{ $titleEvent->address2 }} {{ $titleEvent->city }}, {{ $titleEvent->state }}  {{ $titleEvent->zip }}
                            </div>
                        </div>
                        <div class="clearfix"></div>
                    @endif
                </div>
                @if($titleEvent->registrationStartTime != null)
                    <div class="event-registration pull-left"><strong>Reg:</strong> {{$titleEvent->registrationStartTime}} to {{$titleEvent->registrationEndTime}}</div>
                @endif
                @if($titleEvent->dartStart != '')
                    <div class="event-dart-start pull-left"><strong>Start:</strong> {{$titleEvent->dartStart}}</div>
                @endif
                @if($titleEvent->description != null && $titleEvent->description != '')
                    <div class="clearfix"></div>
                    <a style="cursor: pointer" data-toggle="collapse" data-target="#titleevent{{$titleEvent->id}}">Details</a>
                @endif
            </div>
            @if($titleEvent->description != null && $titleEvent->description != '')
                <div class="clearfix"></div>
                <div class="event-description">
                    <div id="titleevent{{$titleEvent->id}}" class="collapse">
                        <div class="well">{!! $titleEvent->description !!}</div>
                    </div>
                </div>
            @endif
            <div class="clearfix"></div>
        </div>
    @endif
</div>
