@extends('layouts.managemaster')

@section('content')
    <div class="single-form">
        {!! Form::model($winterSeasonMatch, ['route' => ['manage.season.match.update', $leagueId, $season->id, $seasonWeek->id, $winterSeasonMatch->id, 'division' => $division], 'method' => 'put', 'class' => 'single-form']) !!}
        <h2>Add Match to week {{ date('m-d-Y', strtotime($seasonWeek->date)) }}</h2>
        @include('manage.season.winter.match.fields')
        <div>
            <button type="submit" class="btn btn-default"><i class="fa fa-floppy-o fa-lg"></i> Update</button>
            <a class="btn btn-danger" href="{{ route('manage.season.week.show', [ 'leagueId' => $leagueId, 'seasonId' => $season->id, 'id' => $seasonWeek->id ])}}">Cancel</a>
        </div>
        {!! Form::close() !!}
    </div>
@stop
