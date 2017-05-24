<?php

namespace TrentonDarts\Stats;

use Illuminate\Support\Collection;
use Illuminate\Support\Facades\DB;
use TrentonDarts\LeagueManagement\Models\WinterSeason;
use TrentonDarts\LeagueManagement\Models\WinterSeasonMatch;
use TrentonDarts\MatchDomain\Models\MatchResult;
use TrentonDarts\Stats\Models\MatchStat;

class MatchStatsRepository
{
    public function updateMatchStats(MatchResult $result)
    {
        $statAway = MatchStat::where('matchId', $result->getMatchId())->where('teamId', $result->awayTeamId)->first();
        if(!$statAway)
            $statAway = new MatchStat();

        $statAway->seasonId = $result->seasonId;
        $statAway->seasonPart = $result->seasonPart;
        $statAway->matchId = $result->getMatchId();
        $statAway->division = $result->division;
        $statAway->date = $result->date;
        $statAway->teamId = $result->awayTeamId;
        $statAway->teamName = $result->awayTeamName;
        $statAway->pointsWon = $result->getAwayScore();
        $statAway->pointsLost = $result->getHomeScore();
        $statAway->matchPoints = $result->getAwayMatchPoints();
        $statAway->homeMatch = 0;
        $statAway->hasScorecard = $result->getHasScorecard();
        $statAway->save();

        $statHome = MatchStat::where('matchId', $result->getMatchId())->where('teamId', $result->homeTeamId)->first();
        if(!$statHome)
            $statHome = new MatchStat();

        $statHome->seasonId = $result->seasonId;
        $statHome->seasonPart = $result->seasonPart;
        $statHome->matchId = $result->getMatchId();
        $statHome->division = $result->division;
        $statHome->date = $result->date;
        $statHome->teamId = $result->homeTeamId;
        $statHome->teamName = $result->homeTeamName;
        $statHome->pointsWon = $result->getHomeScore();
        $statHome->pointsLost = $result->getAwayScore();
        $statHome->matchPoints = $result->getHomeMatchPoints();
        $statHome->homeMatch = 1;
        $statHome->hasScorecard = $result->getHasScorecard();

        $statHome->save();
    }

    public function getStandingsForSeasonPart($id, $seasonPart, $asOfWeekDate = null)
    {
        $asOfWeekDate = $asOfWeekDate == null ? date('Y-m-d') : $asOfWeekDate;
        $matchStats = DB::table('winter_stats_matches')
            ->select(DB::raw('teamId, SUM(matchPoints) as matchPoints, SUM(CASE WHEN pointsWon > pointsLost THEN 1 ELSE 0 END) as matchesWon, SUM(CASE WHEN pointsLost > pointsWon THEN 1 ELSE 0 END) as matchesLost, SUM(pointsWon) as pointsWon, SUM(pointsLost) as pointsLost'))
            ->where('seasonId', $id)
            ->where('seasonPart', $seasonPart)
            ->where('date', '<=', $asOfWeekDate)
            ->groupBy('teamId')
            ->get();
        return new Collection($matchStats);
    }

    public function getStandingsForSeason($id, $asOfWeekDate = null)
    {
        $asOfWeekDate = $asOfWeekDate == null ? date('Y-m-d') : $asOfWeekDate;
        $matchStats = DB::table('winter_stats_matches')
            ->select(DB::raw('teamId, SUM(matchPoints) as matchPoints, SUM(CASE WHEN pointsWon > pointsLost THEN 1 ELSE 0 END) as matchesWon, SUM(CASE WHEN pointsLost > pointsWon THEN 1 ELSE 0 END) as matchesLost, SUM(pointsWon) as pointsWon, SUM(pointsLost) as pointsLost'))
            ->where('seasonId', $id)
            ->where('date', '<=', $asOfWeekDate)
            ->groupBy('teamId')
            ->get();
        return new Collection($matchStats);
    }

    public function getMatchResultsForSeason($seasonId)
    {
        $matchStats = DB::table('winter_stats_matches')
            ->select(DB::raw('*'))
            ->where('seasonId', $seasonId)
            ->orderBy('matchId', 'homeMatch')
            ->get();
        $matches = [];

        foreach($matchStats as $match)
        {
            if(isset($matches[$match->matchId]))
                $result = $matches[$match->matchId];
            else
                $result = new \TrentonDarts\Stats\Models\MatchResult();

            $result->matchId = (int) $match->matchId;
            $result->hasScorecard = $match->hasScorecard;
            if($match->homeMatch == 0){
                $result->awayTeamId = (int) $match->teamId;
                $result->awayTeamName = $match->teamName;
                $result->awayScore = (int) $match->pointsWon;
            }
            else {
                $result->homeTeamId = (int) $match->teamId;
                $result->homeTeamName = $match->teamName;
                $result->homeScore = (int) $match->pointsWon;
            }

            $matches[$result->matchId] = $result;
        }

        return new \TrentonDarts\Stats\Models\MatchResults($matches);
    }
}