@extends('layouts.managemaster')

@section('content')
    {!! Form::model(new TrentonDarts\LeagueManagement\Models\Player, ['route' => ['manage.player.store', $leagueId], 'class' => 'single-form']) !!}
    <h2>Add Player</h2>
    @include('manage.player.fields')
    <div>
        <button type="submit" class="btn btn-default"><i class="fa fa-floppy-o fa-lg"></i> Add</button>
        <a class="btn btn-danger" href="{{ route('manage.player.index', $leagueId)}}">Cancel</a>
    </div>
    {!! Form::close() !!}
@stop
@section('scripts')
    <script>
        (function(d){
            d.getElementsByName('firstName')[0].focus();
        })(document);
    </script>
@stop