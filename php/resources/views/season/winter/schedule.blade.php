@extends('layouts.master')

@section('content')
    <div class="single-form season-schedule">
        <h2>Season {{ $season->name }}</h2>
        <div class="alert alert-danger">The byes on the schedule are currently inaccurate.</div>
        @if(isset($divSchedules) && sizeof($divSchedules) > 0)
            <h3>Pre Season</h3>
            @foreach($divSchedules as $schedule)
                <div class="division clearfix">
                    <div class="division-title">Division {{ $schedule->name }}</div>
                    @foreach($schedule->weeks as $week)
                        <div class="panel panel-primary week-matches pull-left">
                            <div class="panel-heading">
                                <div class="panel-title title">{{ date('m-d-Y', strtotime($week->date)) }}</div>
                            </div>
                            <div class="panel-body">
                                @if(sizeof($week->matches) == 0)
                                    <div class="no-matches"><strong>No Matches</strong></div>
                                @endif
                                @foreach($week->matches as $match)
                                    <a href="{{ route( $isBoardMember ? 'season.match.edit' : 'season.match.show', ['seasonId' => $seasonId, 'id' => $match->id])}}">
                                        <div class="match-card {{ $match->awayScore == 0 && $match->homeScore == 0 ? 'no-results' : '' }}">
                                                <div class="team away {{ !($match->awayScore == 0 && $match->homeScore == 0) && $match->awayScore > $match->homeScore ? 'winner' : ''}} {{ !($match->awayScore == 0 && $match->homeScore == 0) && $match->awayScore < $match->homeScore ? 'loser' : ''}} clearfix">
                                                    <div class="team-name pull-left">{{ $match->awayTeam->name }}</div>
                                                    <div class="team-score pull-left">{{ $match->awayScore == 0 && $match->homeScore == 0 ? '-' : $match->awayScore }}</div>
                                                </div>
                                                <div class="team home {{ !($match->awayScore == 0 && $match->homeScore == 0) && $match->awayScore < $match->homeScore ? 'winner' : ''}} {{ !($match->awayScore == 0 && $match->homeScore == 0) && $match->awayScore > $match->homeScore ? 'loser' : ''}} clearfix">
                                                    <div class="team-name pull-left">{{ $match->homeTeam->name }} @if(!$match->hasScorecard && ($match->awayScore != 0 || $match->homeScore != 0))<i title="Scorecard isn't entered" class="fa fa-bolt"></i>@endif</div>
                                                    <div class="team-score pull-left">{{ $match->awayScore == 0 && $match->homeScore == 0 ? '-' : $match->homeScore }}</div>
                                                </div>
                                            <div class="clearfix"></div>
                                        </div>
                                    </a>
                                @endforeach
                            </div>
                            <div class="panel-footer">
                                <strong>Bye:</strong>
                                @if(sizeof($week->byeTeamNames) > 0)
                                    {{ join(', ', $week->byeTeamNames) }}
                                @else
                                    None
                                @endif
                            </div>
                        </div>
                    @endforeach
                </div>
            @endforeach
        @endif
        <div class="clearfix"></div>
        <h3>Regular Season</h3>
        @if(isset($divRegularSchedules))
            @foreach($divRegularSchedules as $schedule)
                <div class="division clearfix">
                    <div class="division-title">Division {{ $schedule->name }}</div>
                    @foreach($schedule->weeks as $week)
                        <div class="panel panel-primary week-matches pull-left">
                            <div class="panel-heading">
                                <div class="panel-title title">{{ date('m-d-Y', strtotime($week->date)) }}</div>
                            </div>
                            <div class="panel-body">
                                @if(sizeof($week->matches) == 0)
                                    <div class="no-matches"><strong>No Matches</strong></div>
                                @endif
                                @foreach($week->matches as $match)
                                    <a href="{{ route( $isBoardMember ? 'season.match.edit' : 'season.match.show', ['seasonId' => $seasonId, 'id' => $match->id])}}">
                                        <div class="match-card {{ $match->awayScore == 0 && $match->homeScore == 0 ? 'no-results' : '' }}">
                                            <div class="team away {{ !($match->awayScore == 0 && $match->homeScore == 0) && $match->awayScore > $match->homeScore ? 'winner' : ''}} {{ !($match->awayScore == 0 && $match->homeScore == 0) && $match->awayScore < $match->homeScore ? 'loser' : ''}} clearfix">
                                                <div class="team-name pull-left">{{ $match->awayTeam->name }}</div>
                                                <div class="team-score pull-left">{{ $match->awayScore == 0 && $match->homeScore == 0 ? '-' : $match->awayScore }}</div>
                                            </div>
                                            <div class="team home {{ !($match->awayScore == 0 && $match->homeScore == 0) && $match->awayScore < $match->homeScore ? 'winner' : ''}} {{ !($match->awayScore == 0 && $match->homeScore == 0) && $match->awayScore > $match->homeScore ? 'loser' : ''}} clearfix">
                                                <div class="team-name pull-left">{{ $match->homeTeam->name }} @if(!$match->hasScorecard && ($match->awayScore != 0 || $match->homeScore != 0))<i title="Scorecard isn't entered" class="fa fa-bolt"></i>@endif</div>
                                                <div class="team-score pull-left">{{ $match->awayScore == 0 && $match->homeScore == 0 ? '-' : $match->homeScore }}</div>
                                            </div>
                                            <div class="clearfix"></div>
                                        </div>
                                    </a>
                                @endforeach
                            </div>
                            <div class="panel-footer">
                                <strong>Bye:</strong>
                                @if(sizeof($week->byeTeamNames) > 0)
                                    {{ join(', ', $week->byeTeamNames) }}
                                @else
                                    None
                                @endif
                            </div>
                        </div>
                    @endforeach
                </div>
            @endforeach
        @else
            <div>No Schedule</div>
        @endif
    </div>
@stop
@section('scripts')

    <script type="text/javascript">
        var elems = document.getElementsByClassName('confirmation');
        var confirmIt = function (e) {
            if (!confirm('Are you sure?')) e.preventDefault();
        };
        for (var i = 0, l = elems.length; i < l; i++) {
            elems[i].addEventListener('click', confirmIt, false);
        }
    </script>
@stop
