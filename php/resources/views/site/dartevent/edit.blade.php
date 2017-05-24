@extends('layouts.managemaster')

@section('content')
    {!! Form::model($dartEvent, ['route' => ['manage.site.dartevent.update', $leagueId, $dartEvent->id], 'files' => true, 'method' => 'put', 'class' => 'single-form']) !!}
    <h2>Create Dart Event</h2>
    @include('site.dartevent.fields')
    <div>
        <button type="submit" class="btn btn-default"><i class="fa fa-floppy-o fa-lg"></i> Update</button>
        <a class="btn btn-danger" href="{{ route('manage.site.dartevent.index', ['leagueId' => $leagueId])}}">Cancel</a>
    </div>
    {!! Form::close() !!}

@stop

@section('scripts')

@stop