@extends('layouts.managemaster')

@section('content')
    {!! Form::model($sponsor, ['route' => ['manage.sponsor.update', $sponsor->leagueId, $sponsor->id], 'method' => 'put', 'class' => 'single-form']) !!}
    <h2>Edit Sponsor</h2>
    @include('manage.sponsor.fields')
    <div>
        <button type="submit" class="btn btn-default"><i class="fa fa-floppy-o fa-lg"></i> Update</button>
        <a class="btn btn-danger" href="{{ route('manage.sponsor.index', $sponsor->leagueId)}}">Cancel</a>
    </div>
    {!! Form::close() !!}
@stop
