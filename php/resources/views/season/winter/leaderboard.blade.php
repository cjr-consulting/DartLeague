@extends('layouts.master')

@section('content')
    <style media="print" type="text/css">
        @page {size: landscape;}
    </style>
    <div class="season-leaderboard">
    <h3>Season {{ $season->name }}</h3>
    <div style="margin-right: 15px;margin-left: 15px;">
        <div class="row">
            <div class="col-xs-12 col-sm-3">
                <div class="form-group">
                    <label for="seasonPart">Season Part</label>
                    {!! Form::select('seasonPart', array('whole' => 'Whole Season', 'pre' => 'Pre Season', 'regular' => 'Regular Season'), $seasonPart, ['id' => 'seasonPart', 'class' => 'form-control']) !!}
                </div>
            </div>
            <div class="col-xs-12 col-sm-3">
                <div class="form-group">
                    <label for="division">Division</label>
                    {!! Form::select('division', $divisions, $division, ['id' => 'division', 'class' => 'form-control', 'placeholder' => 'All Divisions']) !!}
                </div>
            </div>
            <div class="col-xs-12 col-sm-3">
                <div class="form-group">
                    <label for="leaderboard">Leaderboard</label>
                    {!! Form::select('leaderboard', $leaderboards, $leaderboard, ['id' => 'leaderboard', 'class' => 'form-control', 'placeholder' => 'Select']) !!}
                </div>
            </div>
        </div>
    </div>
    <div class="table-responsive">
        <table class="table table-condensed table-striped table-hover">
            <thead>
            <tr>
                <th colspan="11"></th>
                <th colspan="4" style="text-align: center;">Singles</th>
                <th colspan="4" style="text-align: center;">Doubles</th>
                <th colspan="2" style="text-align: center;">Triples</th>
            </tr>
            <tr>
                <th colspan="5"></th>
                <th style="text-align: center;">Overall</th>
                <th style="text-align: center;">Singles</th>
                <th style="text-align: center;">Doubles</th>
                <th style="text-align: center;">T '01</th>
                <th style="text-align: center;">Cricket</th>
                <th style="text-align: center;">01</th>
                <th colspan="2" style="text-align: center;">Cricket</th>
                <th colspan="2" style="text-align: center;">'01</th>
                <th colspan="2" style="text-align: center;">Cricket</th>
                <th colspan="2" style="text-align: center;">501</th>
                <th colspan="2" style="text-align: center;">'01</th>
            </tr>
            <tr>
                <th></th>
                <th>Team Name</th>
                <th title='Weeks Played the whole season'>WP</th>
                <th title='Games played in the selected season'>GP</th>
                <th style="text-align: center;">Record</th>
                <th style="text-align: center;">Win %</th>
                <th style="text-align: center;">Win %</th>
                <th style="text-align: center;">Win %</th>
                <th style="text-align: center;">Win %</th>
                <th style="text-align: center;">Win %</th>
                <th style="text-align: center;">Win %</th>
                <th style="text-align: center;">W</th>
                <th style="text-align: center;">L</th>
                <th style="text-align: center;">W</th>
                <th style="text-align: center;">L</th>
                <th style="text-align: center;">W</th>
                <th style="text-align: center;">L</th>
                <th style="text-align: center;">W</th>
                <th style="text-align: center;">L</th>
                <th style="text-align: center;">W</th>
                <th style="text-align: center;">L</th>
            </tr>
            </thead>
            <tbody>
            {{--*/ $position = 1 /*--}}
            @if($playerStats->isEmpty())
                <tr class="warning">
                    <td colspan="21">Currently No Stats</td>
                </tr>
            @else
                @foreach($playerStats->sortByDesc(function($stat)use($leaderboard)
                    {
                        $sortable = '';
                        switch($leaderboard){
                            case 'overall':
                                $sortable = sprintf('%-12s%s', $stat->overallWin, $stat->gamesPlayed);
                                break;
                            case 'singles':
                                $sortable = sprintf('%-12s%s', $stat->singlesWin, $stat->gamesPlayed);
                                break;
                            case 'doubles':
                                $sortable = sprintf('%-12s%s', $stat->doublesWin, $stat->gamesPlayed);
                                break;
                            case '801':
                                $sortable = sprintf('%-12s%s', $stat->eight01Win, $stat->gamesPlayed);
                                break;
                            case 'cricket':
                                $sortable = sprintf('%-12s%s', $stat->cricketWin, $stat->gamesPlayed);
                                break;
                            case '01':
                                $sortable = sprintf('%-12s%s', $stat->oh1Win, $stat->gamesPlayed);
                                break;
                            case 'singles-301':
                                $sortable = sprintf('%-12s%s', $stat->singles301Win, $stat->game301->played);
                                break;
                            case 'singles-cricket':
                                $sortable = sprintf('%-12s%s', $stat->singlesCricketWin, $stat->gameCricket->played);
                                break;
                            case 'doubles-cricket':
                                $sortable = sprintf('%-12s%s', $stat->doublesCricketWin, $stat->gameDoubleCricket->played);
                                break;
                            case 'doubles-501':
                                $sortable = sprintf('%-12s%s', $stat->doubles501Win, $stat->game501->played);
                                break;
                            case 'triples-801':
                                $sortable = sprintf('%-12s%s', $stat->triples801Win, $stat->game801->played);
                                break;
                            }

                        return $sortable;
                    }) as $player)
                    <tr>
                        <td>{{$position++}}</td>
                        <td><a href="{{route('player.show', ['id' => $player->playerId])}}">{{$player->playerName}}</a></td>
                        <td style="text-align: center;">{{$player->weeksPlayed}}</td>
                        <td style="text-align: center;">{{$player->gamesPlayed}}</td>
                        <td style="text-align: center;">{{$player->gamesWon}} - {{$player->gamesLost}}</td>
                        <td style="text-align: center;">
                            @if($player->overallWin > 0)
                                {{ round($player->overallWin * 100, 1) }} %
                            @else
                                -
                            @endif
                        </td>
                        <td style="text-align: center;">
                            @if($player->singlesWin > 0)
                                {{ round($player->singlesWin * 100, 1) }} %
                            @else
                                -
                            @endif
                        </td>
                        <td style="text-align: center;">
                            @if($player->doublesWin > 0)
                                {{ round($player->doublesWin * 100, 1) }} %
                            @else
                                -
                            @endif
                        </td>
                        <td style="text-align: center;">
                            @if($player->eight01Win > 0)
                                {{ round($player->eight01Win * 100, 1) }} %
                            @else
                                -
                            @endif
                        </td>
                        <td style="text-align: center;">
                            @if($player->cricketWin > 0)
                                {{ round($player->cricketWin * 100, 1) }} %
                            @else
                                -
                            @endif
                        </td>
                        <td style="text-align: center;">
                            @if($player->oh1Win > 0)
                                {{ round($player->oh1Win * 100, 1) }} %
                            @else
                                -
                            @endif
                        </td>
                        <td style="text-align: center;">{{$player->gameCricket->won}}</td>
                        <td style="text-align: center;" class="loss-column">{{$player->gameCricket->lost}}</td>
                        <td style="text-align: center;">{{$player->game301->won}}</td>
                        <td style="text-align: center;" class="loss-column">{{$player->game301->lost}}</td>
                        <td style="text-align: center;">{{$player->gameDoubleCricket->won}}</td>
                        <td style="text-align: center;" class="loss-column">{{$player->gameDoubleCricket->lost}}</td>
                        <td style="text-align: center;">{{$player->game501->won}}</td>
                        <td style="text-align: center;" class="loss-column">{{$player->game501->lost}}</td>
                        <td style="text-align: center;">{{$player->game801->won}}</td>
                        <td style="text-align: center;" class="loss-column">{{$player->game801->lost}}</td>
                    </tr>
                @endforeach
            @endif
            </tbody>
        </table>
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
                $("#seasonPart").change(function() {
                    window.document.location = buildQuery();
                });
                $("#division").change(function() {
                    window.document.location = buildQuery();
                });
                $("#leaderboard").change(function() {
                    window.document.location = buildQuery();
                });
            });

            function buildQuery()
            {
                var query = '?',
                        seasonPart = $('#seasonPart').val(),
                        division = $('#division').val(),
                        leaderboard = $('#leaderboard').val();

                query = query + 'seasonPart=' + encodeURI(seasonPart);
                if(leaderboard) {
                    query = query + '&leaderboard=' + encodeURI(leaderboard);
                }

                if(division){
                    query = query + '&division=' + encodeURI(division);
                }

                return query;
            }
        })(jQuery);
    </script>
@stop
