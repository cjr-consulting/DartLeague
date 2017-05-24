<div class="panel panel-default">
    @if(!isset($resultWeekDate))
        <div class="panel-heading">
            <div class="panel-title">No results available at this time</div>
        </div>
    @else
        <div class="panel-heading">
            <h3 class="panel-title">Weekly Results {{  date('m-d-Y', strtotime($resultWeekDate)) }}</h3>
        </div>
        @if(!isset($divSchedules) || sizeof($divSchedules) <= 0)
            <div class="no-games">Currently No Games</div>
        @endif
        @foreach($divSchedules as $schedule)
            @if(!isset($schedule->weeks) || sizeof($schedule->weeks) <= 0)
                <div class="no-games">Currently No Games</div>
            @endif
            @foreach($schedule->weeks as $week)
                @if($week->matches->isEmpty())
                    <div class="no-games">Currently No Games</div>
                @else
                    <div class="division-matches clearfix">
                        @foreach($week->matches as $match)
                            <a href="{{route($isBoardMember ? 'season.match.edit' : 'season.match.show', ['seasonId' => $seasonId, 'id' => $match->id])}}">
                                <div class="match-result pull-left {{ $match->awayScore == 0 && $match->homeScore == 0 ? 'no-results' : '' }}" >
                                    <div class="away-team clearfix {{ !($match->awayScore == 0 && $match->homeScore == 0) && $match->awayScore > $match->homeScore ? 'winner' : ''}} {{ !($match->awayScore == 0 && $match->homeScore == 0) && $match->awayScore < $match->homeScore ? 'loser' : ''}}">
                                        <div class="team-name pull-left">{{ $match->awayTeam->name }}</div>
                                        <div class="team-score pull-left">{{ $match->awayScore == 0 && $match->homeScore == 0 ? '-' : $match->awayScore }}</div>
                                    </div>
                                    <div class="home-team clearfix {{ !($match->awayScore == 0 && $match->homeScore == 0) && $match->awayScore < $match->homeScore ? 'winner' : ''}} {{ !($match->awayScore == 0 && $match->homeScore == 0) && $match->awayScore > $match->homeScore ? 'loser' : ''}}">
                                        <div class="team-name pull-left">{{ $match->homeTeam->name }} @if(!$match->hasScorecard && ($match->awayScore != 0 || $match->homeScore != 0))<i title="Scorecard isn't entered" class="fa fa-bolt"></i>@endif</div>
                                        <div class="team-score pull-left">{{ $match->awayScore == 0 && $match->homeScore == 0 ? '-' : $match->homeScore }}</div>
                                    </div>
                                </div>
                            </a>
                        @endforeach
                    </div>
                    @if(sizeof($week->byeTeamNames) > 0)
                        <div class="teams-on-bye"><strong>Bye:</strong> <i>{{ join(', ', $week->byeTeamNames) }}</i></div>
                    @endif
                @endif
            @endforeach
        @endforeach
    @endif
</div>