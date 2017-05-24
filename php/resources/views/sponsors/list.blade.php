@extends('layouts.master')

@section('content')
    <div class="page-header"><h2>GTDL Sponsors</h2></div>
        <ul class="nav nav-pills nav-justified">
            @foreach($sponsorTypes as $key => $type)
                <li role="presentation" class="{{$selectedType == $key ? 'active' : ''}}">
                    <a href="{{route('sponsor.list', ['type' => $key])}}">{{$type}}</a></li>
            @endforeach
        </ul>
    <table class="table table-striped table-hover">
        <thead>
        <tr>
            <th>Name</th>
            <th>Address</th>
            <th>Contact</th>
            <th></th>
            <th style="width: 30%;">Description</th>
        </tr>
        </thead>
        <tbody>
        @if($sponsors->isEmpty())
            <tr class="warning"><td colspan="5">Currently No Sponsors</td></tr>
        @else
            @foreach($sponsors as $sponsor)
                <tr>
                    <td>
                        @if($sponsor->url != null)
                            <a href="{{ $sponsor->url}}" target="_blank"><strong>{{ $sponsor->name }}</strong></a>
                        @else
                            <strong>{{ $sponsor->name }}</strong>
                        @endif
                    </td>
                    <td>
                        @if($sponsor->address1 != '')
                            @if($sponsor->mapUrl != null)
                            <a href="{{$sponsor->mapUrl}}" target="_blank">
                                {{ $sponsor->address1 }}<br/>
                                @if($sponsor->address2 != '')
                                    {{ $sponsor->address2 }}<br/>
                                @endif
                                {{ $sponsor->city }}, {{ $sponsor->state }}
                                {{ $sponsor->zip }}
                            </a>
                            @else
                                {{ $sponsor->address1 }}<br/>
                                @if($sponsor->address2 != '')
                                    {{ $sponsor->address2 }}<br/>
                                @endif
                                {{ $sponsor->city }}, {{ $sponsor->state }}
                                {{ $sponsor->zip }}
                            @endif
                        @endif
                    </td>
                    <td>
                        <div>{{ $sponsor->phone }}</div>
                        <div>{{ $sponsor->email }}</div>
                    </td>
                    <td>
                        @if($sponsor->facebookUrl != null )
                            <a href="{{$sponsor->facebookUrl}}" target="_blank"><i class="fa fa-facebook-official fa-2x"></i></a>
                        @endif
                    </td>
                    <td>
                        @if($sponsor->teams()->count() > 0)
                            @foreach($sponsor->teams as $team)
                                <div>{{ $team->name }}</div>
                            @endforeach
                            <div></div>
                        @endif
                        <div>{{ $sponsor->description }}</div>
                    </td>
                </tr>
            @endforeach
        @endif
        </tbody>
    </table>
@stop