<?php

namespace TrentonDarts\Stats;


use TrentonDarts\LeagueManagement\Models\WinterSeason;
use TrentonDarts\LeagueManagement\Models\WinterSeasonMatch;
use TrentonDarts\LeagueManagement\Models\WinterSeasonTeam;
use TrentonDarts\MatchDomain\Models\MatchResult;
use TrentonDarts\Stats\Models\AwardStat;

class AwardStatRepository
{
    public function updateAwardStats(MatchResult $result)
    {
        foreach($result->getGames() as $game)
        {
            $currentAwards = AwardStat::where('gameId', $game->id)->get();
            foreach($currentAwards as $curAward)
            {
                $f = $this->findAward($game->awards, $curAward->id);
                if(!$f)
                    $curAward->delete();
            }

            foreach($game->awards as $award)
            {
                $a = $currentAwards->where('awardId', $award->id)->first();
                if(!$a) {
                    $a = new AwardStat();
                    $a->awardId = $award->id;
                }
                $team = WinterSeasonTeam::where('seasonId', $result->seasonId)->where('teamId', $result->awayTeamId)->first();
                if($team->teamPlayers->where('playerId', $award->player->id)->isEmpty()){
                    $team = WinterSeasonTeam::where('seasonId', $result->seasonId)->where('teamId', $result->homeTeamId)->first();
                }

                $a->seasonId = $result->seasonId;
                $a->seasonPart = $result->seasonPart;
                $a->date = $result->date;
                $a->matchId = $result->getMatchId();
                $a->division = $result->division;
                $a->teamId = $team->team->id;
                $a->teamName = $team->team->name;
                $a->gameId = $game->id;
                $a->playerId = $award->player->id;
                $a->playerName = $award->player->name;
                $a->awardType = $award->awardType;
                $a->value = $award->value;
                $a->save();
            }
        }
    }

    private function findAward($awards, $id)
    {
        foreach($awards as $a)
            if($a->id == $id)
                return $a;
    }

    public function getAwardStatsForSeasonPart($id, $seasonPart)
    {
        return AwardStat::where('seasonId', $id)
            ->where('seasonPart', $seasonPart)
            ->orderBy('awardType')
            ->orderBy('value', 'desc')
            ->orderBy('date', 'desc')
            ->get();
    }

    public function getAwardStatsForSeason($id, $weekDate = null)
    {
        $query = AwardStat::where('seasonId', $id)
            ->orderBy('awardType')
            ->orderBy('value', 'desc')
            ->orderBy('date', 'desc');

        if($weekDate != null)
            $query->where('date', $weekDate);

        return $query->get();
    }
}