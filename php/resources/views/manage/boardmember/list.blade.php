@extends('layouts.managemaster')

@section('content')
    <div class="single-form">
    <div class="panel panel-default">
        <div class="panel-heading"><a class="btn btn-success btn-sm" href="{{ route('manage.boardmember.create', ['leagueId' => $leagueId]) }}" title="Add Board Memeber"><i class="fa fa-plus-square"></i></a> <strong>Board Members</strong></div>
        <table class="table table-striped table-hover">
            <thead>
            <tr>
                <th width="5px"></th>
                <th>Name</th>
                <th>Position</th>
                <th width="5px"></th>
            </tr>
            </thead>
            <tbody>
            @if($boardMembers->isEmpty())
                <tr class="warning"><td colspan="4">Currently No Board Members</td></tr>
            @else
                @foreach($boardMembers as $boardMember)
                    <tr>
                        <td><a class="btn btn-link" href="{{route('manage.boardmember.edit', ['leagueId' => $leagueId, 'id' => $boardMember->id]) }}" title="Edit {{ $boardMember->name }}"><i class="fa fa-pencil-square-o fa-lg"></i></a></td>
                        <td>{{ $boardMember->name }}</td>
                        <td>{{ isset($boardMember->position) ? $boardMember->position : 'None' }}</td>
                        <td><a class="btn btn-link confirmation" href="{{route('manage.boardmember.delete', ['leagueId' => $leagueId, 'id' => $boardMember->id]) }}" title="Delete"><i class="fa fa-trash-o fa-lg"></i></a></td>
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
