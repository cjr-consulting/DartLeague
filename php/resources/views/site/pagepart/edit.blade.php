@extends('layouts.managemaster')

@section('content')
    {!! Form::model($pagePart, ['route' => ['manage.site.pagepart.update', $leagueId, $pagePart->id], 'method' => 'put', 'class' => 'single-form']) !!}
    <h2>Create Dart Event</h2>
    @include('site.pagepart.fields')
    <div>
        <button type="submit" class="btn btn-default"><i class="fa fa-floppy-o fa-lg"></i> Update</button>
        <a class="btn btn-danger" href="{{ route('manage.site.pagepart.index', ['leagueId' => $leagueId])}}">Cancel</a>
    </div>
    {!! Form::close() !!}

@stop