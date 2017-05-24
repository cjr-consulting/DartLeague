@extends('layouts.managemaster')

@section('content')
    <div class="single-form">
        {!! Form::model($teamPlayer, ['route' => ['manage.seasonTeam.player.update', $leagueId, $season->id, $seasonTeam->id, $teamPlayer->id], 'method' => 'put', 'class' => 'single-form']) !!}
        <h3>Edit Player on Team {{ $seasonTeam->team->name }} for {{ $season->name }} Season</h3>
        @include('manage.season.winter.teamplayer.fields')
        <div>
            <button type="submit" class="btn btn-default"><i class="fa fa-floppy-o fa-lg"></i> Update</button>
            <a class="btn btn-danger" href="{{ route('manage.seasonTeam.show', [ 'leagueId' => $leagueId, 'seasonId' => $season->id , 'teamId' => $seasonTeam->id])}}">Cancel</a>
        </div>
        {!! Form::close() !!}
    </div>
@stop
