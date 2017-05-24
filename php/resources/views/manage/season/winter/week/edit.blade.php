@extends('layouts.managemaster')

@section('content')
    {!! Form::model($seasonWeek, ['route' => ['manage.season.week.update', $leagueId, $season->id, $seasonWeek->id], 'method' => 'put', 'class' => 'single-form']) !!}

    <h2>Season {{$season->name}}</h2>
    <h2>Reschedule Week</h2>
    <div class="form-group">
        {!! Form::date('date', null, ['class' => 'form-control']) !!}
    </div>
    <div>
        <button type="submit" class="btn btn-default"><i class="fa fa-floppy-o fa-lg"></i> Update</button>
        <a class="btn btn-danger" href="{{ route('manage.season.show', ['leagueId' => $leagueId, 'seasonId' => $season->id])}}">Cancel</a>
    </div>
    {!! Form::close() !!}
@stop
