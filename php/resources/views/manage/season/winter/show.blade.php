@extends('layouts.managemaster')

@section('content')
<div class="season single-form">
    <h2>{{ $season->name }} Season</h2>
    <div style="margin-top: 5px; margin-bottom: 5px;">
        <a class="btn btn-default btn-half" href="{{route('manage.season.players.export', ['leagueId' => $leagueId, 'seasonId' => $season->id]) }}">Export Players</a>
        <a class="btn btn-default btn-half" href="{{route('manage.seasonTeam.index', ['leagueId' => $leagueId, 'id' => $season->id]) }}" title="Edit {{ $season->name }} Teams">Teams <span class="badge">{{ $season->teams->count() }}</span></a>
        <a class="btn btn-default btn-half" href="{{route('manage.seasonTeamPayments', ['leagueId' => $leagueId, 'id' => $season->id]) }}" title="Edit {{ $season->name }} Payments">Payments <span class="badge">{{ $paymentStillOutStanding }}</span></a>
        <a class="btn btn-default btn-half" href="{{route('manage.season.resetstats', ['leagueId' => $leagueId, 'id' => $season->id])}}" title="Reset Stats">Reset Stats</a>
    </div>
    <div class="panel panel-default">
        <div class="panel-heading"><h3>Pre Season Schedule</h3>
            {!! Form::model(new TrentonDarts\LeagueManagement\Models\WinterSeasonWeek, ['route' => ['manage.season.week.store', $leagueId, $season->id], 'class' => 'form-inline']) !!}
            <div class="form-group">
                {!! Form::hidden('weekType', 'pre') !!}
                {!! Form::date('date', null, ['class' => 'form-control']) !!}
                <button type="submit" class="btn btn-info"><i class="fa fa-plus-square-o"></i> Add Week</button>
            </div>
            {!! Form::close() !!}
        </div>
        <table class="table table-striped table-hover">
            <thead>
            <tr>
                <th width="5px"></th>
                <th>Date</th>
                <th style="text-align: center;">Scheduled</th>
                <th width="5px"></th>
            </tr>
            </thead>
            <tbody>
            @if($season->preSeasonWeeks->isEmpty())
                <tr class="warning"><td colspan="4">Currently No Weeks</td></tr>
            @else
                @foreach($season->preSeasonWeeks as $week)
                    <tr>
                        <td><a class="btn btn-link" href="{{route('manage.season.week.edit', ['leagueId' => $leagueId, 'seasonId' => $season->id, 'id' => $week->id]) }}" title="Edit {{ $week->date }}"><i class="fa fa-pencil-square-o fa-lg"></i></a></td>
                        <td><a class="btn btn-link" href="{{route('manage.season.week.show', ['leagueId' => $leagueId, 'seasonId' => $season->id, 'id' => $week->id]) }}" title="Edit {{ $week->date }}">{{ date('m-d-Y', strtotime($week->date))  }}</a></td>
                        <td style="text-align: center;">{{ $week->matches()->count() }}</td>
                        <td><a class="btn btn-link confirmation" href="{{route('manage.season.week.delete', ['leagueId' => $leagueId, 'seasonId' => $season->id, 'id' => $week->id]) }}" title="Delete"><i class="fa fa-trash-o fa-lg"></i></a></td>
                    </tr>
                @endforeach
            @endif
            </tbody>
        </table>
    </div>
    <div class="panel panel-default">
        <div class="panel-heading"><h3>Regular Season Schedule</h3>
            {!! Form::model(new TrentonDarts\LeagueManagement\Models\WinterSeasonWeek, ['route' => ['manage.season.week.store', $leagueId, $season->id], 'class' => 'form-inline']) !!}
            <div class="form-group">
                {!! Form::hidden('weekType', 'regular') !!}
                {!! Form::date('date', null, ['class' => 'form-control']) !!}
                <button type="submit" class="btn btn-info"><i class="fa fa-plus-square-o"></i> Add Week</button>
            </div>
            {!! Form::close() !!}
        </div>
        <table class="table table-striped table-hover">
            <thead>
            <tr>
                <th width="5px"></th>
                <th>Date</th>
                <th style="text-align: center;">Scheduled</th>
                <th width="5px"></th>
            </tr>
            </thead>
            <tbody>
            @if($season->regularSeasonWeeks->isEmpty())
                <tr class="warning"><td colspan="4">Currently No Weeks</td></tr>
            @else
                @foreach($season->regularSeasonWeeks as $week)
                    <tr>
                        <td><a class="btn btn-link" href="{{route('manage.season.week.edit', ['leagueId' => $leagueId, 'seasonId' => $season->id, 'id' => $week->id]) }}" title="Edit {{ $week->date }}"><i class="fa fa-pencil-square-o fa-lg"></i></a></td>
                        <td><a class="btn btn-link" href="{{route('manage.season.week.show', ['leagueId' => $leagueId, 'seasonId' => $season->id, 'id' => $week->id]) }}" title="Edit {{ $week->date }}">{{ date('m-d-Y', strtotime($week->date))  }}</a></td>
                        <td style="text-align: center;">{{ $week->matches()->count() }}</td>
                        <td><a class="btn btn-link confirmation" href="{{route('manage.season.week.delete', ['leagueId' => $leagueId, 'seasonId' => $season->id, 'id' => $week->id]) }}" title="Delete"><i class="fa fa-trash-o fa-lg"></i></a></td>
                    </tr>
                @endforeach
            @endif
            </tbody>
        </table>
    </div>
    <a class="btn btn-link" href="{{route('manage.season.index', ['leagueId' => $leagueId]) }}" title="Back">back to seasons</a>
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
