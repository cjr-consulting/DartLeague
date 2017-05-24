@extends('layouts.managemaster')

@section('content')
    <div class="single-form">
        {!! Form::model(new TrentonDarts\LeagueManagement\Models\WinterSeasonTeamPlayer, ['route' => ['manage.seasonTeam.player.store', $leagueId, $season->id, $seasonTeam->id], 'class' => 'single-form']) !!}
        <h3>Add Player to Team {{ $seasonTeam->team->name }} for {{ $season->name }} Season</h3>
        @include('manage.season.winter.teamplayer.fields')
        <div>
            <button type="submit" class="btn btn-default"><i class="fa fa-floppy-o fa-lg"></i> Add</button>
            <a class="btn btn-danger" href="{{ route('manage.seasonTeam.show', [ 'leagueId' => $leagueId, 'seasonId' => $season->id , 'teamId' => $seasonTeam->id])}}">Cancel</a>
        </div>
        {!! Form::close() !!}
    </div>
@stop
