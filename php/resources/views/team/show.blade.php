@extends('layouts.master')

@section('content')
<div class="team-page">
    <h2>{{ $seasonTeam->team->name }} Team</h2>
    <div class="container-fluid">
        <div class="row">
            <div class="col-sm-12 col-md-4">{!! Form::select('selectedSeason', $seasons, $season->id, ['id' => 'selectedSeason', 'class' => 'form-control']) !!}</div>
        </div>
        <div class="row">
            <div class="col-sm-8 col-md-8">
                <div class="stats">
                    <div class="table-responsive">
                        <table class="table table-condensed table-striped table-hover">
                            <thead>
                            <tr>
                                <th colspan="4" rowspan="2">
                                    {!! Form::select('seasonPart', array('whole' => 'Whole Season', 'pre' => 'Pre Season', 'regular' => 'Regular Season'), $statPart, ['id' => 'statPart', 'class' => 'form-control']) !!}
                                </th>
                                <th></th>
                                <th colspan="4" style="text-align: center;">Singles</th>
                                <th colspan="4" style="text-align: center;">Doubles</th>
                                <th colspan="2" style="text-align: center;">Triples</th>
                            </tr>
                            <tr>
                                <th style="text-align: center;">Overall</th>
                                <th colspan="2" style="text-align: center;">Cricket</th>
                                <th colspan="2" style="text-align: center;">'01</th>
                                <th colspan="2" style="text-align: center;">Cricket</th>
                                <th colspan="2" style="text-align: center;">501</th>
                                <th colspan="2" style="text-align: center;">'01</th>
                            </tr>
                            <tr>
                                <th>Team Name</th>
                                <th title='Weeks Played the whole season'>WP</th>
                                <th title='Games played in the selected season'>GP</th>
                                <th style="text-align: center;">Record</th>
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
                            @if($teamStats->isEmpty())
                                <tr class="warning">
                                    <td colspan="20">Currently No Stats</td>
                                </tr>
                            @else
                                @foreach($teamStats as $teamStat)
                                    <tr class="team-stat-row info">
                                        <td colspan="3">{{$teamStat->teamName}}</td>
                                        <td style="text-align: center;">{{$teamStat->pointsWon}} - {{$teamStat->pointsLost}}</td>
                                        <td style="text-align: center;">{{ round($teamStat->overallWin * 100, 1) }} %</td>
                                        <td style="text-align: center;">{{$teamStat->gameCricket->won}}</td>
                                        <td style="text-align: center;" class="loss-column">{{$teamStat->gameCricket->lost}}</td>
                                        <td style="text-align: center;">{{$teamStat->game301->won}}</td>
                                        <td style="text-align: center;" class="loss-column">{{$teamStat->game301->lost}}</td>
                                        <td style="text-align: center;">{{$teamStat->gameDoubleCricket->won}}</td>
                                        <td style="text-align: center;" class="loss-column">{{$teamStat->gameDoubleCricket->lost}}</td>
                                        <td style="text-align: center;">{{$teamStat->game501->won}}</td>
                                        <td style="text-align: center;" class="loss-column">{{$teamStat->game501->lost}}</td>
                                        <td style="text-align: center;">{{$teamStat->game801->won}}</td>
                                        <td style="text-align: center;" class="loss-column">{{$teamStat->game801->lost}}</td>
                                    </tr>
                                    @foreach($playerStats->where('teamId', $teamStat->teamId)->sortByDesc(function($stat){return sprintf('%-12s%s', $stat->overallWin, $stat->gamesPlayed);}) as $player)
                                        <tr>
                                            <td><a href="{{route('player.show', ['id' => $player->playerId, 'season' => $season->id])}}">{{$player->playerName}}</a></td>
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
                                @endforeach
                            @endif
                            </tbody>
                        </table>
                    </div>
                </div>

                <div class="schedule">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">Schedule</h3>
                        </div>
                        @foreach($teamSchedule as $schedule)
                            <div class="week-schedule clearfix">
                                <div class="date pull-left">{{ date('M d', strtotime($schedule->date)) }}</div>
                                @if($schedule->isBye)
                                    <div class="bye pull-left">BYE</div>
                                @elseif($schedule->awayScore == 0 && $schedule->homeScore == 0)
                                    <div class="team-name  pull-left">
                                        @if($schedule->awayTeamId == $seasonTeam->team->id)
                                            <div class="team home pull-left"><strong>@</strong> <a href="{{route('team.show', ['id' => $schedule->homeTeamId, 'season' => $season->id])}}">{{$schedule->homeTeamName}}</a></div>
                                        @else
                                            <div class="team away pull-left"><a href="{{route('team.show', ['id' => $schedule->awayTeamId, 'season' => $season->id])}}">vs {{$schedule->awayTeamName}}</a></div>
                                        @endif
                                    </div>
                                @else
                                <div class="team-name pull-left">
                                    @if($schedule->awayTeamId == $seasonTeam->team->id)
                                        @ {{$schedule->homeTeamName}}
                                    @else
                                        vs {{$schedule->awayTeamName}}
                                    @endif
                                        @if(!$schedule->hasScorecard && ($schedule->awayScore != 0 || $schedule->homeScore != 0))<i title="Scorecard isn't entered" class="fa fa-bolt"></i>@endif
                                </div>
                                <div class="result pull-left">
                                    @if($schedule->awayTeamId == $seasonTeam->team->id)
                                        {{$schedule->awayScore}} - {{$schedule->homeScore}}
                                    @else
                                        {{$schedule->homeScore}} - {{$schedule->awayScore}}
                                    @endif
                                </div>
                                <div class="win-loss pull-left">
                                    <a title="View the scorecard" href="{{route($isBoardMember ? 'season.match.edit' : 'season.match.show', ['seasonId' => $season->id, 'id' => $schedule->matchId])}}">
                                        <span class="label {{ ($schedule->awayScore > $schedule->homeScore && $schedule->awayTeamId == $seasonTeam->team->id)
                                     || ($schedule->awayScore < $schedule->homeScore && $schedule->homeTeamId == $seasonTeam->team->id)? 'label-success': 'label-danger' }}">
                                            {{ ($schedule->awayScore > $schedule->homeScore && $schedule->awayTeamId == $seasonTeam->team->id)
                                     || ($schedule->awayScore < $schedule->homeScore && $schedule->homeTeamId == $seasonTeam->team->id)? 'W': 'L' }}</span>
                                    </a>
                                </div>
                                @endif
                            </div>
                        @endforeach
                    </div>
                </div>

            </div>
            <div class="col-sm-4 col-md-4">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">Standings</h3>
                    </div>
                    <table class="table table-striped table-hover">
                        <tbody>
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
                                    <td><a href="{{route('team.show', ['id' => $standing->teamId, 'season' => $season->id])}}">{{ $standing->name }}</a></td>
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

                <div class="awards">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">Awards</h3>
                        </div>
                @if($awards->isEmpty())
                    <div class="panel-body">No Awards Yet.</div>
                @endif
                @foreach($awards as $award)
                    <div class="award-tag clearfix">
                        <div class="award-header clearfix">
                            <div class="award-year pull-left">{{ date('Y', strtotime($award->date)) }}</div>
                            <div class="award-date">{{ date('m-d', strtotime($award->date)) }}</div>
                        </div>
                        <div class="award-body clearfix">
                            <div class="award-img {{$award->awardImage}} pull-left"></div>
                            <div class="name-tag pull-left">
                                <div class="player-name">{{ $award->playerName }}</div>
                                <div class="player-team">{{ $award->teamName }}</div>
                            </div>
                            @if($award->value > 0)<div class="award-value pull-right"><span class="badge">{{ $award->value }}</span></div>@endif
                        </div>
                    </div>
                @endforeach
                </div>
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
                $('#statPart').change(function() {
                    window.document.location = buildQuery();
                });
                $('#selectedSeason').change(function() {
                    window.document.location = buildQuery();
                });
            });

            function buildQuery()
            {
                var query = '?',
                    statPart = $('#statPart').val(),
                        seasonId = $('#selectedSeason').val();

                query = query + 'statpart=' + encodeURI(statPart) + '&season=' + encodeURI(seasonId);

                return query;

            }
        })(jQuery);
    </script>
@stop