@extends('layouts.managemaster')

@section('content')
    <h2>Dart Event</h2>
    <div class="panel panel-default">
        <div class="panel-heading"><a class="btn btn-success btn-sm" href="{{ route('manage.site.dartevent.create', ['leagueId' => $leagueId]) }}" title="Add Dart Event"><i class="fa fa-plus-square"></i></a> <strong>Dart Event</strong></div>
        <div class="table-responsive">
            <table class="table table-striped table-hover">
                <thead>
                <tr>
                    <th class="width: 84px;"></th>
                    <th>Date</th>
                    <th>Name</th>
                    <th>Type</th>
                    <th>Location</th>
                    <th width="5px"></th>
                </tr>
                </thead>
                <tbody>
                @if($dartEvents->isEmpty())
                    <tr class="warning"><td colspan="6">Currently No Dart Events</td></tr>
                @else
                    @foreach($dartEvents as $dartEvent)
                        <tr class="{{$dartEvent->isTitleEvent ? 'success' : ''}}">
                            <td>
                                <div class="btn-group">
                                    <a class="btn btn-default" href="{{route('manage.site.dartevent.edit', ['leagueId' => $leagueId, 'id' => $dartEvent->id]) }}" title="Edit {{ $dartEvent->name }}"><i class="fa fa-pencil-square-o fa-lg"></i></a>
                                    <a class="btn btn-default" href="{{route('manage.site.dartevent.result.index', ['leagueId' => $leagueId, 'dartEventId' => $dartEvent->id]) }}" title="Event Results"><i class="fa fa-star"></i></a>
                                    <a class="btn btn-default" href="{{route('manage.site.dartevent.activate', ['leagueId' => $leagueId, 'id' => $dartEvent->id]) }}" title="Change front page display of event">
                                        @if($dartEvent->isTitleEvent)
                                            <i class="fa fa-circle"></i>
                                        @else
                                            <i class="fa fa-circle-o"></i>
                                        @endif
                                    </a>
                                </div>
                            </td>
                            <td style="vertical-align: middle">{{date('m-d-Y', strtotime($dartEvent->eventDate))}}</td>
                            <td style="vertical-align: middle">{{$dartEvent->name}}</td>
                            <td style="vertical-align: middle">{{$dartEvent->eventType()}}</td>
                            <td style="vertical-align: middle">{{$dartEvent->locationName}}</td>
                            <td><a class="btn btn-link confirmation" href="{{route('manage.site.dartevent.delete', ['leagueId' => $leagueId, 'id' => $dartEvent->id]) }}" title="Delete"><i class="fa fa-trash-o fa-lg"></i></a>
                        </tr>
                    @endforeach
                @endif
                </tbody>
            </table>
        </div>
    </div>
@stop

@section('scripts')

    <script type="text/javascript">
        var elems = document.getElementsByClassName('confirmation');
        var confirmIt = function (e) {
            if (!confirm('Are you sure?')) e.preventDefault();
        };
        for (var i = 0, l = elems.length; i < l; i++) {
            elems[i].addEventListener('click', confirmIt, false);
        }
    </script>
@stop