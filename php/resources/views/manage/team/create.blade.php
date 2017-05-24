@extends('layouts.managemaster')

@section('content')
    {!! Form::model(new TrentonDarts\LeagueManagement\Models\Team, ['route' => ['manage.team.store', $leagueId], 'class' => 'single-form']) !!}
    <h2>Add Team</h2>
    @include('manage.team.fields')
    <div>
        <button type="submit" class="btn btn-default"><i class="fa fa-floppy-o fa-lg"></i> Add</button>
        <a class="btn btn-danger" href="{{ route('manage.team.index', $leagueId)}}">Cancel</a>
    </div>
    {!! Form::close() !!}
@stop
