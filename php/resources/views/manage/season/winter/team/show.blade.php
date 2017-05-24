@extends('layouts.managemaster')

@section('content')
    <div>
        <h2>Players for {{ $seasonTeam->team->name }} - {{ $season->name }} Season</h2>

        <div class="panel panel-default">
            <div class="panel-heading"><a class="btn btn-success btn-sm" href="{{ route('manage.seasonTeam.player.create', ['leagueId' => $leagueId, 'seasonId' => $season->id, 'teamId' => $seasonTeam->id]) }}" title="Add Player"><i class="fa fa-plus-square"></i></a> <strong>Players</strong></div>
            <table class="table table-striped table-hover">
                <thead>
                <tr>
                    <th width="5px"></th>
                    <th>Name</th>
                    <th>Email</th>
                    <th>Cell</th>
                    <th>Size</th>
                    <th>Role</th>
                    <th width="5px"></th>
                </tr>
                </thead>
                <tbody>
                @if($seasonTeam->teamPlayers->isEmpty())
                    <tr class="warning"><td colspan="5">Currently No Players</td></tr>
                @else
                    @foreach($seasonTeam->teamPlayers->sortBy(function($tplayer){return sprintf('%-12s%s', $tplayer->role, $tplayer->player->name);}) as $teamPlayer)
                        <tr>
                            <td><a class="btn btn-link" href="{{route('manage.seasonTeam.player.edit', ['leagueId' => $leagueId, 'seasonId' => $season->id, 'teamId' => $seasonTeam->id, 'id' => $teamPlayer->id]) }}" title="Edit {{ $teamPlayer->player->email }}"><i class="fa fa-pencil-square-o fa-lg"></i></a></td>
                            <td>{{ $teamPlayer->player->firstName }} {{ $teamPlayer->player->lastName }}</td>
                            <td>{{ isset($teamPlayer->player->email) ? $teamPlayer->player->email : 'None' }}</td>
                            <td>{{ $teamPlayer->player->cellPhone }}</td>
                            <td>{{ $teamPlayer->player->shirtSize }}</td>
                            <td>{{ $teamPlayer->role }}</td>
                            <td><a class="btn btn-link confirmation" href="{{route('manage.seasonTeam.player.delete', ['leagueId' => $leagueId,'seasonId' =>$season->id, 'teamid' => $seasonTeam->id , 'id' => $teamPlayer->id]) }}" title="Delete"><i class="fa fa-trash-o fa-lg"></i></a></td>
                        </tr>
                    @endforeach
                @endif
                </tbody>
            </table>
        </div>
        <a class="btn btn-link" href="{{route('manage.seasonTeam.index', ['leagueId' => $leagueId, 'seasonId' => $season->id]) }}" title="Back">back to teams</a>
    </div>
@stop
