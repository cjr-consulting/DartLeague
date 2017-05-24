@extends('layouts.managemaster')

@section('content')

    <h2>Page Part Contents</h2>
    <div class="panel panel-default">
        <div class="panel-heading"><a class="btn btn-success btn-sm" href="{{ route('manage.site.pagepart.create', ['leagueId' => $leagueId]) }}" title="Add Dart Event"><i class="fa fa-plus-square"></i></a> <strong>Page Part</strong></div>
        <div class="table-responsive">
            <table class="table table-striped table-hover">
                <thead>
                <tr>
                    <th class="width: 84px;"></th>
                    <th>Name</th>
                    <th>Description</th>
                    <th>Edited</th>
                    <th width="5px"></th>
                </tr>
                </thead>
                <tbody>
                @if($pageParts->isEmpty())
                    <tr class="warning"><td colspan="6">Currently No Page Parts</td></tr>
                @else
                    @foreach($pageParts as $pagePart)
                        <tr>
                            <td>
                                <div class="btn-group">
                                    <a class="btn btn-default" href="{{route('manage.site.pagepart.edit', ['leagueId' => $leagueId, 'id' => $pagePart->id]) }}" title="Edit {{ $pagePart->name }}"><i class="fa fa-pencil-square-o fa-lg"></i></a></a>
                                </div>
                            </td>
                            <td style="vertical-align: middle">{{$pagePart->name}}</td>
                            <td style="vertical-align: middle">{{$pagePart->description}}</td>
                            <td style="">{{$pagePart->updated_at}}</td>
                            <td><a class="btn btn-link confirmation" href="{{route('manage.site.pagepart.delete', ['leagueId' => $leagueId, 'id' => $pagePart->id]) }}" title="Delete"><i class="fa fa-trash-o fa-lg"></i></a>
                        </tr>
                    @endforeach
                @endif
                </tbody>
            </table>
        </div>
    </div>
@stop