@extends('layouts.managemaster')

@section('content')
    {!! Form::model($team, ['route' => ['manage.team.update', $team->leagueId, $team->id], 'method' => 'put', 'class' => 'single-form']) !!}
    <h2>Edit Team</h2>
    @include('manage.team.fields')
    <div>
        <button type="submit" class="btn btn-default"><i class="fa fa-floppy-o fa-lg"></i> Update</button>
        <a class="btn btn-danger" href="{{ route('manage.team.index', $team->leagueId)}}">Cancel</a>
    </div>
    {!! Form::close() !!}
@stop
