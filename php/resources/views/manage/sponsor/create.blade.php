@extends('layouts.managemaster')

@section('content')
    {!! Form::model(new TrentonDarts\LeagueManagement\Models\Sponsor, ['route' => ['manage.sponsor.store', $leagueId], 'class' => 'single-form']) !!}
    <h2>Add Sponsor</h2>
    @include('manage.sponsor.fields')
    <div>
        <button type="submit" class="btn btn-default"><i class="fa fa-floppy-o fa-lg"></i> Add</button>
        <a class="btn btn-danger" href="{{ route('manage.sponsor.index', $leagueId)}}">Cancel</a>
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
