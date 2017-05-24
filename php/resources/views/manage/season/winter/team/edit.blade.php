@extends('layouts.managemaster')

@section('content')
    <div class="single-form">
        {!! Form::model($seasonTeam, ['route' => ['manage.seasonTeam.update', $leagueId, $season->id, $seasonTeam->id], 'method' => 'put', 'class' => 'single-form']) !!}
        <h2>Edit {{ $seasonTeam->team->name }} Team - {{ $season->name }} Season</h2>
        @include('manage.season.winter.team.fields')
        <div>
            <button type="submit" class="btn btn-default"><i class="fa fa-floppy-o fa-lg"></i> Update</button>
            <a class="btn btn-danger" href="{{ route('manage.seasonTeam.index', [ 'leagueId' => $leagueId, 'seasonId' => $season->id ])}}">Cancel</a>
        </div>
        {!! Form::close() !!}
    </div>
@stop
