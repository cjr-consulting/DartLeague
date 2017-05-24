@extends('layouts.managemaster')

@section('content')
    <div class="single-form">
        <h2>{{ $season->name }} Season</h2>
        <h3>Week {{ date('m-d-Y', strtotime($seasonWeek->date)) }} of {{$seasonWeek->weekType}} season</h3>

        @if( sizeof($schedules) == 0)
        <div class="alert alert-warning">Currently no divisions</div>
        @endif
        @foreach($schedules as $schedule)
            <div class="panel panel-default">
                <div class="panel-heading"><strong>Division {{ $schedule->name }}</strong>  <a class="btn btn-success btn-sm" href="{{ route('manage.season.match.create', ['leagueId' => $leagueId, 'seasonId' => $season->id, 'weekId' => $seasonWeek->id, 'division' => $schedule->name]) }}" title="Add match"><i class="fa fa-plus-square"></i> Match</a></div>
                <table class="table table-striped table-hover">
                    <thead>
                    <tr>
                        <th></th>
                        <th>Away</th>
                        <th>Home</th>
                        <th>Match Type</th>
                        <th width="5px"></th>
                    </tr>
                    </thead>
                    <tbody>
                    @if($schedule->matches->isEmpty())
                        <tr class="warning"><td colspan="5">Currently No matches</td></tr>
                    @else
                        @foreach($schedule->matches as $match)
                            <tr>
                                <td><a class="btn btn-default" href="{{route('manage.season.match.edit', ['leagueId' => $leagueId, 'seasonId' => $season->id, 'weekId' => $seasonWeek->id, 'id' => $match->id, 'division' => $schedule->name]) }}" title="Record Results"><i class="fa fa-pencil"></i></a></td>
                                <td>{{ $match->awayTeam->name }}</td>
                                <td>@ {{ $match->homeTeam->name }}</td>
                                <td>{{ $match->matchType->name }}</td>
                                <td><a class="btn btn-link confirmation" href="{{route('manage.season.match.delete', ['leagueId' => $leagueId, 'seasonId' => $season->id, 'weekId' => $seasonWeek->id, 'id' => $match->id]) }}" title="Delete"><i class="fa fa-trash-o fa-lg"></i></a></td>
                            </tr>
                        @endforeach
                    @endif
                    </tbody>
                </table>
                @if(sizeof($schedule->byeTeamNames) > 0)
                <div class="panel-footer">
                    <strong>Bye:</strong> {{ join(', ', $schedule->byeTeamNames) }}
                </div>
                @endif
            </div>
        @endforeach
        <a class="btn btn-link" href="{{route('manage.season.show', ['leagueId' => $leagueId, 'id' => $season->id]) }}" title="Back">back to season</a>
    </div>


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
