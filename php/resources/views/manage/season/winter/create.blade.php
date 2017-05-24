@extends('layouts.managemaster')

@section('content')
{!! Form::model(new TrentonDarts\LeagueManagement\Models\WinterSeason, ['route' => ['manage.season.store', $leagueId], 'class' => 'single-form']) !!}
    <h2>Add Season</h2>
    @include('manage.season.winter.fields')
    <div>
        <button type="submit" class="btn btn-default"><i class="fa fa-floppy-o fa-lg"></i> Add</button>
        <a class="btn btn-danger" href="{{ route('manage.season.index', $leagueId)}}">Cancel</a>
    </div>
{!! Form::close() !!}
@stop
