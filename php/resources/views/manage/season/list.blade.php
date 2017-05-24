@extends('layouts.managemaster')

@section('content')
<div class="single-form seasons-management">
    <h2>Seasons</h2>
    <a class="btn btn-success btn-sm" href="{{ route('manage.season.create', ['leagueId' => $leagueId]) }}" title="Add Season"><i class="fa fa-plus-square"></i> New Season</a>
    @if(isset($seasons))
        @foreach($seasons as $season)
            <div class="{{ $season->isCurrent ? 'alert alert-success' : 'well well-sm' }}">
                <div>
                    <div class="btn-group">
                    <a class="btn btn-default" href="{{route('manage.season.edit', ['leagueId' => $leagueId, 'id' => $season->id]) }}" title="Edit {{ $season->name }}"><i class="fa fa-pencil-square-o fa-lg"></i></a>
                    <a href="{{route('manage.season.show', ['leagueId' => $leagueId, 'id' => $season->id])}}" class="btn btn-default"><i class="fa fa-eye fa-lg"></i></a>
                    </div>
                    <div class="season-detail">
                        <div class="season-title">{{ $season->name }} Season</div>
                        <div>{{ $season->seasonType }}</div>
                    </div>
                </div>
            </div>
        @endforeach
    @endif
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
