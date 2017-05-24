@extends('layouts.master')

@section('content')
    <h3>Season {{ $season->name }} -
    @if($seasonPart == 'pre')
        Pre Season
    @else
        Regular Season
    @endif
    </h3>
    <div style="margin-right: 15px;margin-left: 15px;" class="season-standings">
        <nav class="no-print">
            <ul class="pager">
                <li class="previous {{$previousWeekDate == null ? 'disabled' : ''}}"><a href="{{route('season.show', ['id' => $seasonId, 'week' => $previousWeekDate])}}"><span aria-hidden="true">&larr;</span> {{$previousWeekDate == null ? 'Older' : date('m-d-Y', strtotime($previousWeekDate))}}</a></li>
                @if(isset($resultWeekDate))
                <li><strong>{{  date('m-d-Y', strtotime($resultWeekDate)) }}</strong></li>
                @else
                    <li><strong>Season Starting</strong></li>
                @endif
                <li class="next {{$nextWeekDate == null ? 'disabled' : ''}}"><a href="{{route('season.show', ['id' => $seasonId, 'week' => $nextWeekDate])}}">{{$nextWeekDate == null ? 'Next' : date('m-d-Y', strtotime($nextWeekDate))}} <span aria-hidden="true">&rarr;</span></a></li>
            </ul>
        </nav>
        <div class="container-fluid">
        <div class="row">
            <div class="col-sm-7 ol-md-7">
                @if(isset($resultWeekDate))
                    @include('season.winter.weeklyresults')
                    @include('season.winter.weeklyawards')
                @else
                    <strong>No Results Yet</strong>
                @endif
            </div>
            <div class="col-sm-5 col-md-5">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">Standings</h3>
                    </div>
                    <table class="table table-striped table-hover">
                        <tbody>
                        @if(!isset($divStandings) || sizeof($divStandings) <= 0)
                            <tr><td colspan="5" class="no-games">No Standings</td></tr>
                        @endif
                        @foreach($divStandings as $divStanding)
                            <tr style=" background-color: #7FC3FF; font-weight: bold;">
                                <td>Division {{ $divStanding->name }}</td>
                                @if($season->isUsingMatchPoints)
                                <td style="text-align: center;">P</td>
                                @endif
                                <td style="text-align: center;">W</td>
                                <td style="text-align: center;">L</td>
                                <td style="text-align: center;">%</td>
                            </tr>
                            @foreach($divStanding->standings as $standing)
                                <tr>
                                    <td><a href="{{route('team.show', ['id' => $standing->teamId])}}">{{ $standing->name }}</a></td>
                                    @if($season->isUsingMatchPoints)
                                    <td style="text-align: center; vertical-align: middle;">{{ $standing->matchPoints }}</td>
                                    @endif
                                    <td style="text-align: center; vertical-align: middle;">{{ $standing->wonPoints }}</td>
                                    <td style="text-align: center; vertical-align: middle;">{{ $standing->lossPoints }}</td>
                                    <td style="text-align: center; vertical-align: middle;">{{ round($standing->percentage * 100, 1) }}%</td>
                                </tr>
                            @endforeach
                        @endforeach
                        </tbody>
                    </table>
                </div>
                <div class="panel panel-default">
                    <div class="panel-heading">
                    @if(isset($nextWeekDate))
                        <h3 class="panel-title">Schedule {{ date('m-d-Y', strtotime($nextWeekDate)) }}</h3>
                    @else
                        <h4>No Available Schedule at this Time</h4>
                    @endif
                    </div>
                    <table class="table table-condensed table-striped table-hover">
                        <tbody>
                    @foreach($nextWeekSchedules as $schedule)
                        @foreach($schedule->weeks as $week)
                            @if($week->matches->isEmpty())
                                <tr class="warning"><td colspan="3">Currently No Games</td></tr>
                            @else
                                @foreach($week->matches as $match)
                                    <tr>
                                        <td style="text-align: right; vertical-align: middle;"><a href="{{route('team.show', ['id' => $match->awayTeamId])}}">{{ $match->awayTeam->name }}</a></td>
                                        <td style="width: 10px; text-align: center; vertical-align: middle; font-weight: bold;">@</td>
                                        <td style=" vertical-align: middle; font-weight: bold;"><a href="{{route('team.show', ['id' => $match->homeTeamId])}}">{{ $match->homeTeam->name }}</a></td>
                                    </tr>
                                @endforeach
                                @if(sizeof($week->byeTeamNames) > 0)
                                    <tr>
                                        <td colspan="3"><strong>Bye:</strong> <i>{{ join(', ', $week->byeTeamNames) }}</i></td>
                                    </tr>
                                @endif
                                @endif
                        @endforeach
                    @endforeach
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        </div>
    </div>
@stop

@section('scripts')
    <script>
    (function($){
        $(document).ready(function($) {
            $(".clickable-row").click(function() {
                window.document.location = $(this).data("href");
            });
        });
    })(jQuery);
    </script>
@stop
