@extends('layouts.master')

@section('content')

    <h1 style="text-align:center;">Greater Trenton Dart League <br> Player Activity in Events</h1>

    <table class="table table-bordered table-condensed" style="width: 80%; margin-right: auto; margin-left: auto;">
        <thead>
        <tr valign="center">
            <th scope="col">Event Name</th>
            <th scope="col">Event Date</th>
            <th scope="col">Specific Event</th>
            <th scope="col">Name</th>
            <th scope="col">Finish</th>
        </tr>
        </thead>
        <tbody>
        @if($eventResults->isEmpty())
            <tr class="warning"><td colspan="5">Currently No Weeks</td></tr>
        @else
            @foreach($eventResults as $result)
                <tr>
                    <td>{{ $result['eventName'] }}</td>
                    <td>{{ date('m-d-Y', strtotime($result['eventDate'])) }}</td>
                    <td>{{ $result['specificEvent'] }}</td>
                    <td>{{ $result['name'] }}</td>
                    <td>{{ $result['place'] }}</td>
                </tr>
            @endforeach
        @endif
    </table>

@stop