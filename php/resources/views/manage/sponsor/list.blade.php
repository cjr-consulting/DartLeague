@extends('layouts.managemaster')

@section('content')
<div class="single-form">
    <div class="panel panel-default">
        <div class="panel-heading"><a class="btn btn-success btn-sm" href="{{ route('manage.sponsor.create', ['leagueId' => $leagueId]) }}" title="Add Sponsor"><i class="fa fa-plus-square"></i></a> <strong>Sponsors</strong></div>
        <div class="table-responsive">
            <table class="table table-striped table-hover">
                <thead>
                <tr>
                    <th width="5px"></th>
                    <th>Name</th>
                    <th>Type</th>
                    <th>Contact</th>
                    <th># of Teams</th>
                    <th width="5px"></th>
                </tr>
                </thead>
                <tbody>
                @if($sponsors->isEmpty())
                    <tr class="warning"><td colspan="5">Currently No Sponsors</td></tr>
                @else
                    @foreach($sponsors as $sponsor)
                        <tr>
                            <td><a class="btn btn-link" href="{{route('manage.sponsor.edit', ['leagueId' => $leagueId, 'id' => $sponsor->id]) }}" title="Edit {{ $sponsor->name }}"><i class="fa fa-pencil-square-o fa-lg"></i></a></td>
                            <td>{{ $sponsor->name }}</td>
                            <td>{{ $sponsor->typeName() }}</td>
                            <td>{{ $sponsor->contactName }}</td>
                            <td>{{ $sponsor->teams->count() }}</td>
                            <td><a class="btn btn-link confirmation" href="{{route('manage.sponsor.delete', ['leagueId' => $leagueId, 'id' => $sponsor->id]) }}" title="Delete"><i class="fa fa-trash-o fa-lg"></i></a>
                        </tr>
                    @endforeach
                @endif
                </tbody>
            </table>
        </div>
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
