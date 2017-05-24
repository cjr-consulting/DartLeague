@extends('layouts.managemaster')

@section('content')
    <div class="single-form">
        <div class="panel panel-default">
            <div class="panel-heading"><a class="btn btn-success btn-sm" href="{{ route('manage.player.create', ['leagueId' => $leagueId]) }}" title="Add Player"><i class="fa fa-plus-square"></i></a> <strong>Players</strong></div>
            <table class="table table-condensed table-striped table-hover">
                <thead>
                <tr>
                    <th class="hidden-sm hidden-xs" width="5px"></th>
                    <th>Name</th>
                    <th>Email</th>
                    <th>Cell Phone</th>
                    <th class="hidden-xs">Shirt</th>
                    <th class="hidden-sm hidden-xs" width="5px"></th>
                </tr>
                </thead>
                <tbody>
                @if($players->isEmpty())
                    <tr class="warning"><td colspan="4">Currently No Players</td></tr>
                @else
                    @foreach($players as $player)
                        <tr>
                            <td class="hidden-sm hidden-xs"><a class="btn btn-link" href="{{route('manage.player.edit', ['leagueId' => $leagueId, 'id' => $player->id]) }}" title="Edit {{ $player->email }}"><i class="fa fa-pencil-square-o fa-lg"></i></a></td>
                            <td><a class="btn btn-link" href="{{route('manage.player.edit', ['leagueId' => $leagueId, 'id' => $player->id]) }}" title="Edit {{ $player->email }}">{{ $player->name }}</a></td>
                            <td>{{ isset($player->email) ? $player->email : 'None' }}</td>
                            <td>{{ $player->cellPhone }}</td>
                            <td class="hidden-xs">{{ $player->shirtSize }}</td>
                            <td class="hidden-sm hidden-xs"><a class="btn btn-link confirmation" href="{{route('manage.player.delete', ['leagueId' => $leagueId, 'id' => $player->id]) }}" title="Delete"><i class="fa fa-trash-o fa-lg"></i></a></td>
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

