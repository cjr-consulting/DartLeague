@extends('layouts.managemaster')

@section('content')
    {!! Form::model($player, ['route' => ['manage.player.update', $leagueId, $player->id], 'method' => 'put', 'class' => 'single-form']) !!}
    <h2>Edit Player</h2>
    @include('manage.player.fields')
    <div>
        <button type="submit" class="btn btn-default"><i class="fa fa-floppy-o fa-lg"></i> Update</button>
        <a class="btn btn-danger" href="{{ route('manage.player.index', $leagueId)}}">Cancel</a>
    </div>
    {!! Form::close() !!}
@stop
