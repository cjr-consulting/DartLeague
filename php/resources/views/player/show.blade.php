@extends('layouts.master')

@section('content')
    <div id="player-sheet">
        <h2>{{$player->name}} on <a href="{{route('team.show', ['id' => $playerTeam->id, 'season' => $season->id])}}">{{$playerTeam->name}}</a></h2>
        <h4>{{$season->name}} Season <small><a href="{{route('player.show', ['id' => $player->id, 'season' => $season->id, 'groupby' => 'gameType'])}}">group by game type</a></small></h4>
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-9 col-md-8">
                    <div class="game-history">
                <table class="table table-striped table-condensed">
                    <thead>
                    <tr>
                        <th style="text-align: center;">Game Type</th>
                        <th style="text-align: center;">#</th>
                        <th>Partner</th>
                        <th style="text-align: center;">Result</th>
                        <th>Opponents</th>
                    </tr>
                    </thead>
                    <tbody>
                    {{--*/ $matchId = 0 /*--}}
                    @if($gameHistories->isEmpty())
                        <tr class="warning"><td colspan="5">Currently No Matches</td></tr>
                    @else
                    @foreach($gameHistories as $gameHistory)
                        @if($matchId != $gameHistory->matchId)
                            {{--*/ $matchId = $gameHistory->matchId /*--}}
                            <tr class="info">
                                <td colspan="5">
                                    <strong>
                                    <a href="{{route($isBoardMember ? 'season.match.edit' : 'season.match.show', ['seasonId' => $season->id, 'id' => $gameHistory->matchId])}}" title="View Match" style="margin-right: 10px;"><i class="fa fa-sticky-note-o"></i> {{date('m-d-Y', strtotime($gameHistory->date)) }} @if($gameHistory->seasonPart == 'pre') Pre @else Reg @endif</a>
                                    <a href="{{route('team.show', ['id' => $gameHistory->opponentTeamId, 'season' => $season->id])}}">{{$gameHistory->isHomeGame ? 'vs' : '@'}} {{$gameHistory->opponentTeamName}}</a>
                                    </strong>
                                </td>
                            </tr>
                        @endif
                        <tr>
                            <td style="vertical-align: middle;text-align: center;">{{$gameHistory->gameType}}</td>
                            <td style="vertical-align: middle;text-align: center;">{{$gameHistory->gameOrder}}</td>
                            <td style="vertical-align: middle">
                                @foreach($gameHistory->teamPlayers as $teamMate)
                                    <div><a href="{{route('player.show', ['id' => $teamMate->id, 'season' => $season->id])}}">{{$teamMate->name}}</a></div>
                                @endforeach
                            </td>
                            <td style="vertical-align: middle;text-align: center;">
                                @if($gameHistory->isWon)
                                    <span class="label label-success">Won</span>
                                @else
                                    <span class="label label-danger">Lost</span>
                                @endif
                            </td>
                            <td style="vertical-align: middle">
                                @foreach($gameHistory->opponents as $opponent)
                                    <div><a href="{{route('player.show', ['id' => $opponent->id, 'season' => $season->id])}}">{{$opponent->name}}</a></div>
                                @endforeach
                            </td>
                        </tr>
                    @endforeach
                    @endif
                    </tbody>
                </table>
            </div>
                </div>
                <div class="col-sm-3 col-md-4">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h4>{{$playerTeam->name}}</h4>
                        </div>
                    <table class="table table-condensed">
                        <thead>
                        <tr>
                            <th>Player</th>
                            <th style="text-align: center;">W-L</th>
                        </tr>
                        </thead>
                        <tbody>
                        @foreach($playerStats->sortByDesc(function($stat){return sprintf('%-12s%s', $stat->overallWin, $stat->gamesPlayed);}) as $playerStat)
                            <tr class="{{$playerStat->playerId == $player->id ? 'warning' : ''}}">
                                <td><a href="{{route('player.show', ['id' => $playerStat->playerId, 'season' => $season->id])}}">{{$playerStat->playerName}}</a></td>
                                <td style="text-align: center;">{{$playerStat->gamesWon}} - {{$playerStat->gamesLost}}</td>
                            </tr>
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
@stop