@extends('layouts.managemaster')

@section('content')
{!! Form::model($season, ['route' => ['manage.season.update', $leagueId, $season->id], 'method' => 'put', 'class' => 'single-form']) !!}
    <h2>Edit Season</h2>
    @include('manage.season.winter.fields')
    <div>
        <button type="submit" class="btn btn-default"><i class="fa fa-floppy-o fa-lg"></i> Update</button>
        <a class="btn btn-danger" href="{{ route('manage.season.index', $leagueId)}}">Cancel</a>
    </div>
{!! Form::close() !!}
@stop
