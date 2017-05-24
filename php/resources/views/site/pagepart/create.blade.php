@extends('layouts.managemaster')

@section('content')
    {!! Form::model(new TrentonDarts\SiteManagement\Models\PagePart, ['route' => ['manage.site.pagepart.store', $leagueId], 'class' => 'single-form']) !!}
    <h2>Create Page Part</h2>
    @include('site.pagepart.fields')
    <div>
        <button type="submit" class="btn btn-default"><i class="fa fa-floppy-o fa-lg"></i> Add</button>
        <a class="btn btn-danger" href="{{ route('manage.site.pagepart.index', $leagueId)}}">Cancel</a>
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