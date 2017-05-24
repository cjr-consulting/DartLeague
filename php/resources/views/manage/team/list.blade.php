@extends('layouts.managemaster')

@section('content')
<div class="single-form">
    <div class="panel panel-default">
        <div class="panel-heading"><a class="btn btn-success btn-sm" href="{{ route('manage.team.create', ['leagueId' => $leagueId]) }}" title="Add Team"><i class="fa fa-plus-square"></i></a> <strong>Teams</strong></div>
        <table class="table table-striped table-hover">
            <thead>
            <tr>
                <th width="5px"></th>
                <th>Team Name</th>
                <th>Sponsor Name</th>
                <th width="5px"></th>
            </tr>
            </thead>
            <tbody>
            @if($teams->isEmpty())
                <tr class="warning"><td colspan="4">Currently No Teams</td></tr>
            @else
                @foreach($teams as $team)
                    <tr>
                        <td><a class="btn btn-link" href="{{route('manage.team.edit', ['leagueId' => $leagueId, 'id' => $team->id]) }}" title="Edit {{ $team->name }}"><i class="fa fa-pencil-square-o fa-lg"></i></a></td>
                        <td>{{ $team->name }}</td>
                        <td>{{ isset($team->sponsor) ? $team->sponsor->name : 'None' }}</td>
                        <td><a class="btn btn-link confirmation" href="{{route('manage.team.delete', ['leagueId' => $leagueId, 'id' => $team->id]) }}" title="Delete"><i class="fa fa-trash-o fa-lg"></i></a></td>
                    </tr>
                @endforeach
            @endif
            </tbody>
        </table>
    </div>
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