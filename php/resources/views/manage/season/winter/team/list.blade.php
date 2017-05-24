@extends('layouts.managemaster')

@section('content')
    <div class="single-form seasons-management">
        <h2>Teams for Season {{ $season->name }}</h2>
        <div class="panel panel-default">
            <div class="panel-heading"><a class="btn btn-success btn-sm" href="{{ route('manage.seasonTeam.create', ['leagueId' => $leagueId, 'seasonId' => $season->id]) }}" title="Add Team"><i class="fa fa-plus-square"></i></a> <strong>Team</strong></div>
            <div class="table-responsive">
            <table class="table table-striped table-hover">
                <thead>
                <tr>
                    <th width="100px"></th>
                    <th>Team</th>
                    <th style="text-align: center">Pre</th>
                    <th style="text-align: center">Regular</th>
                    <th width="5px"></th>
                </tr>
                </thead>
                <tbody>
                @if($seasonTeams->isEmpty())
                    <tr class="warning"><td colspan="6">Currently No Teams</td></tr>
                @else
                    @foreach($seasonTeams as $seasonTeam)
                        <tr>
                            <td>
                                <div class="btn-group" role="group">
                                    <a class="btn btn-default" role="button" href="{{route('manage.seasonTeam.edit', ['leagueId' => $leagueId, 'seasonId' => $season->id, 'id' => $seasonTeam->id]) }}" title="Edit {{ $seasonTeam->team->name }}"><i class="fa fa-pencil-square-o fa-lg"></i></a>
                                    <a class="btn btn-default" role="button" href="{{route('manage.seasonTeam.show', ['leagueId' => $leagueId, 'seasonId' => $season->id, 'id' => $seasonTeam->id])}}"><i class="fa fa-eye"></i></a>
                                </div>
                            </td>
                            <td><a href="{{route('manage.seasonTeam.show', ['leagueId' => $leagueId, 'seasonId' => $season->id, 'id' => $seasonTeam->id])}}">{{ $seasonTeam->team->name }}</a></td>
                            <td style="text-align: center">{{ $seasonTeam->preSeasonDiv }}</td>
                            <td style="text-align: center">{{ $seasonTeam->regularSeasonDiv }}</td>
                            <td><a class="btn btn-link confirmation" href="{{route('manage.seasonTeam.delete', ['leagueId' => $leagueId, 'seasonId' => $season->id, 'id' => $seasonTeam->id]) }}" title="Delete Team"><i class="fa fa-trash-o fa-lg"></i></a></td>
                        </tr>
                    @endforeach
                @endif
                </tbody>
            </table>
            </div>
        </div>
        <a class="btn btn-link" href="{{route('manage.season.show', ['leagueId' => $leagueId, 'id' => $season->id]) }}" title="Back">back to season</a>
    </div>
@stop
