@extends('layouts.managemaster')

@section('content')
<div class="single-form">
    <h2>League Management</h2>

    <div style="margin-top: 5px; margin-bottom: 5px;">
        <a class="btn btn-info btn-block" href="{{ route('manage.season.index', ['leagueId' => $leagueId]) }}" title="Manage Seasons"><i class="fa fa-calendar fa-2"></i> Seasons</a>
    </div>
    <div style="margin-top: 5px; margin-bottom: 5px;">
        <a class="btn btn-warning btn-block" href="{{ route('manage.player.index', ['leagueId' => $leagueId]) }}" title="Players"><i class="fa fa-user fa-2"></i> Players</a>
    </div>
    <div style="margin-top: 5px; margin-bottom: 5px;">
        <a class="btn btn-default btn-block" href="{{ route('manage.team.index', ['leagueId' => $leagueId]) }}" title="Teams"><i class="fa fa-users fa-2"></i> Teams</a>
    </div>
    <div style="margin-top: 5px; margin-bottom: 5px;">
        <a class="btn btn-danger btn-block" href="{{ route('manage.sponsor.index', ['leagueId' => $leagueId]) }}" title="Sponsors"><i class="fa fa-meh-o fa-2"></i> Sponsors</a>
    </div>
    <div style="margin-top: 5px; margin-bottom: 5px;">
        <a class="btn btn-success btn-block" href="{{ route('manage.boardmember.index', ['leagueId' => $leagueId]) }}" title="Boardmembers"><i class="fa fa-star fa-2"></i> Board Members</a>
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
