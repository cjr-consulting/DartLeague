@extends('layouts.managemaster')

@section('content')
    <h2>Dart Event Results</h2>
    <div><b>{{ $dartEvent->name }}</b> - {{ $dartEvent->eventDate }}</div>
    <div class="table-responsive">
        {!! Form::model(new TrentonDarts\SiteManagement\Models\DartEventResult, ['route' => ['manage.site.dartevent.result.store', 'leagueId' => $leagueId, 'dartEventId' => $dartEvent->id]]) !!}
        <table class="table table-striped table-hover">
            <thead>
            <tr>
                <th class="width: 84px;"></th>
                <th>Specific Event Name</th>
                <th>Player Name</th>
                <th>Finished</th>
                <th width="5px"></th>
            </tr>
            </thead>
            <tbody>
            @if($dartEvent->results->isEmpty())
                <tr class="warning"><td colspan="5">Currently No Event Results</td></tr>
            @else
                @foreach($dartEvent->results()->orderBy('orderId')->get() as $result)
                    <tr class="{{$result->isTitleEvent ? 'success' : ''}}">
                        <td>
<!--                            <div class="btn-group">
                                <a class="btn btn-default" href="{{route('manage.site.dartevent.result.index', ['leagueId' => $leagueId, 'id' => $dartEvent->id]) }}"><i class="fa fa-arrow-up fa-lg"></i></a>
                                <a class="btn btn-default" href="{{route('manage.site.dartevent.result.index', ['leagueId' => $leagueId, 'dartEventId' => $dartEvent->id]) }}"><i class="fa fa-arrow-down fa-lg"></i></a>
                            </div>-->
                        </td>
                        <td style="vertical-align: middle">{{ $result->specificEventName}}</td>
                        <td style="vertical-align: middle">{{ $result->player->name }}</td>
                        <td style="vertical-align: middle">{{ $result->finished }}</td>
                        <td><a class="btn btn-link confirmation" href="{{route('manage.site.dartevent.result.delete', ['leagueId' => $leagueId, 'dartEventId' => $dartEvent->id, 'id' => $result->id]) }}" title="Delete"><i class="fa fa-trash-o fa-lg"></i></a>
                    </tr>
                @endforeach
            @endif<tr>
                    <td></td>
                    <td>{!! Form::text('specificEventName', null, ['class' => 'form-control']) !!}</td>
                    <td>{!! Form::select('playerId', $players, null, ['class' => 'form-control', 'placeholder' => 'Select Player']) !!}</td>
                    <td>{!! Form::text('finished', null, ['class' => 'form-control']) !!}</td>
                    <td><button type="submit" class="btn btn-default"><i class="fa fa-floppy-o fa-lg"></i> Add</button></td>
                </tr>
            </tbody>
            {!! Form::close() !!}
        </table>
        <a href="{{ route('manage.site.dartevent.index', ['leagueId' => $leagueId]) }}" class="btn btn-default">Back</a>
    </div>
@stop

@section('scripts')
    <script>
        (function(d){
            d.getElementsByName('specificEventName')[0].focus();
        })(document);
    </script>
@stop