@extends('layouts.managemaster')

@section('content')
    {!! Form::model(new TrentonDarts\SiteManagement\Models\DartEvent, ['route' => ['manage.site.dartevent.store', $leagueId], 'files' => true, 'class' => 'single-form']) !!}
    <h2>Create Dart Event</h2>
    @include('site.dartevent.fields')
    <div>
        <button type="submit" class="btn btn-default"><i class="fa fa-floppy-o fa-lg"></i> Add</button>
        <a class="btn btn-danger" href="{{ route('manage.site.dartevent.index', $leagueId)}}">Cancel</a>
    </div>
    {!! Form::close() !!}
@stop

@section('scripts')
    <script>
        (function(d){
            d.getElementsByName('name')[0].focus();
        })(document);
    </script>
@stop