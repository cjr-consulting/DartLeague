@extends('layouts.managemaster')

@section('content')
    <div class="single-form">
        {!! Form::model(new TrentonDarts\LeagueManagement\Models\WinterSeasonTeam, ['route' => ['manage.seasonTeam.store', $leagueId, $season->id], 'class' => 'single-form']) !!}
        <h2>Add Team to {{ $season->name }} Season</h2>
        @include('manage.season.winter.team.fields')
        <div>
            <button type="submit" class="btn btn-default"><i class="fa fa-floppy-o fa-lg"></i> Add</button>
            <a class="btn btn-danger" href="{{ route('manage.seasonTeam.index', [ 'leagueId' => $leagueId, 'seasonId' => $season->id ])}}">Cancel</a>
        </div>
        {!! Form::close() !!}
    </div>
@stop
