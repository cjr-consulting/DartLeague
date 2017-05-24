@extends('layouts.master')

@section('content')
    <div class="team-list-page">
        <h2 style="text-align: center;">{{ $season->name }} GTDL Teams</h2>
        <div style="text-align: center; font-size: 1.75em;"><a href="https://maps.google.com/maps/ms?msid=213093344398242645697.0004d758955d29604ecbf&msa=0&ll=40.212047,-74.718361&spn=0.090191,0.168743">GTDL Sponsor Map - Find us here</a></div>
        <div style="text-align: center;"><a href="https://mapsengine.google.com/map/edit?mid=zLyNyjI06bP0.kk7uxNW5mwRA">NJ Dart League Map - Looking for somewhere else to play?</a></div>
        <div class="container-fluid">
            <table class="table table-striped table-hover table-align-middle">
                <thead>
                <tr>
                    <th>Team</th>
                    <th>Sponsor</th>
                    <th></th>
                    <th>Address</th>
                    <th>Captains</th>
                    <th style="text-align: center">Pre</th>
                    <th style="text-align: center">Regular</th>
                </tr>
                </thead>
                <tbody>
                @if($seasonTeams->isEmpty())
                    <tr class="warning"><td colspan="6">Currently No Teams</td></tr>
                @else
                    @foreach($seasonTeams->sortBy(function($seasonTeam, $key){return $seasonTeam->team->name;}) as $seasonTeam)
                        <tr>
                            <td><a href="{{route('team.show', ['id' => $seasonTeam->team->id])}}"><strong>{{ $seasonTeam->team->name }}</strong></a></td>
                            <td>
                                @if($seasonTeam->team->sponsor['url'] != null)
                                    <a href="{{$seasonTeam->team->sponsor['url']}}" target="_blank"><strong>{{ $seasonTeam->team->sponsor['name'] }}</strong></a>
                                @else
                                    <strong>{{ $seasonTeam->team->sponsor['name'] }}</strong>
                                @endif
                                @if($seasonTeam->team->sponsor['phone'] != null)
                                    <div style="font-size: .8em;"><strong>Ph: </strong>{{$seasonTeam->team->sponsor['phone']}}</div>
                                @endif
                            </td>
                            <td>
                                @if($seasonTeam->team->sponsor['facebookUrl'] != null )
                                    <a href="{{$seasonTeam->team->sponsor['facebookUrl']}}" target="_blank"><i class="fa fa-facebook-official fa-2x"></i></a>
                                @endif
                            </td>
                            <td>
                                @if($seasonTeam->team->sponsor['address1'] != '')
                                    <a href="{{$seasonTeam->team->sponsor['mapUrl']}}" target="_blank">
                                        {{ $seasonTeam->team->sponsor['address1'] }}<br/>
                                        @if($seasonTeam->team->sponsor['address2'] != '')
                                        {{ $seasonTeam->team->sponsor['address2'] }}<br/>
                                        @endif
                                        {{ $seasonTeam->team->sponsor['city'] }}, {{ $seasonTeam->team->sponsor['state'] }}
                                        {{ $seasonTeam->team->sponsor['zip'] }}
                                    </a>
                                @endif
                            </td>
                            <td>
                                @foreach($seasonTeam->captains as $captain)
                                    {{$captain->player->name}}<br/>
                                @endforeach
                            </td>
                            <td style="text-align: center">{{ $seasonTeam->preSeasonDiv }}</td>
                            <td style="text-align: center">{{ $seasonTeam->regularSeasonDiv }}</td>
                        </tr>
                    @endforeach
                @endif
                </tbody>
            </table>
        </div>
    </div>
@stop

@section('scripts')

@stop