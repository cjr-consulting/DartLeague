<?php

namespace TrentonDarts\Http\ViewComposers;

use Illuminate\Contracts\View\View;
use TrentonDarts\LeagueManagement\Models\WinterSeason;
use TrentonDarts\LeagueManagement\Models\WinterSeasonTeam;
use TrentonDarts\Stats\MatchStatsRepository;

class StandingsComposer
{
    public function compose(View $view)
    {
        $season = WinterSeason::where('isCurrent', 1)->firstOrFail();

        $seasonPart = $this->getCurrentSeasonPart($season);
        $divName = 'preSeasonDiv';
        if($seasonPart == 'regular')
            $divName = 'regularSeasonDiv';

        $divisions = WinterSeasonTeam::where('seasonId', $season->id)->orderBy($divName)->groupBy($divName)->get()->pluck($divName);
        $divSchedules = [];
        $lastWeekDate = $this->getLastWeekDate($season);
        $nextWeekDate = $this->getNextWeekDate($season);

        $divStandings = [];
        $matchStatsRepo = new MatchStatsRepository();
        if($season->accumulatePointsForAllParts) {
            $standings = $matchStatsRepo->getStandingsForSeason($season->id);
        } else {
            $standings = $matchStatsRepo->getStandingsForSeasonPart($season->id, $seasonPart);
        }

        foreach($divisions as $division) {
            if($division =='') continue;
            $div = new DivStandings();
            $div->name = $division;
            foreach ($season->teams()->where($divName, $division)->get() as $scheduleTeam) {
                $standing = $standings->where('teamId', $scheduleTeam->team->id)->first();
                $divStanding = new DivStanding();
                $divStanding->teamId = $scheduleTeam->team->id;
                $divStanding->matchPoints = 0;
                $divStanding->wonPoints = 0;
                $divStanding->percentage = 0;
                $divStanding->lossPoints = 0;
                $divStanding->ppts = 0;

                $divStanding->name = $scheduleTeam->team->name;
                if($standing) {
                    $divStanding->matchPoints = $standing->matchPoints;
                    if(!$season->isUsingMatchPoints) {
                        $divStanding->wonPoints = $standing->pointsWon;
                        $divStanding->lossPoints = $standing->pointsLost;
                    }
                    else {
                        $divStanding->wonPoints = $standing->matchesWon;
                        $divStanding->lossPoints = $standing->matchesLost;
                    }

                    if (($standing->pointsWon + $standing->pointsLost) > 0)
                        $divStanding->percentage = $standing->pointsWon / ($standing->pointsWon + $standing->pointsLost);
                }

                array_push($div->standings, $divStanding);
            }

            if($season->isUsingMatchPoints)
                usort($div->standings, array($this, "sortTeamsStandingsMatchPoints"));
            else
                usort($div->standings, array($this, "sortTeamsStandings"));
            array_push($divStandings, $div);
        }


        $models =  [
            'season' => $season,
            'seasonPart' => $seasonPart,
            'resultWeekDate' => $lastWeekDate,
            'divSchedules' => $divSchedules,
            'nextWeekDate' => $nextWeekDate,
            'divStandings' => $divStandings];

        $view->with($models);
    }

    public function sortTeamsStandings($a, $b)
    {
        if($a->percentage == $b->percentage)
            return 0;
        return ($a->percentage > $b->percentage) ? -1 : 1;
    }

    public function sortTeamsStandingsMatchPoints($a, $b)
    {
        if($a->matchPoints == $b->matchPoints) {
            if ($a->percentage == $b->percentage)
                return 0;
            return ($a->percentage > $b->percentage) ? -1 : 1;
        }

        return ($a->matchPoints > $b->matchPoints) ? -1 : 1;
    }

    private function getLastWeekDate($season)
    {
        $lower = $season->weeks()->orderBy('date', 'desc')->where('date', '<', date('Y-m-d'))->first();
        if(!$lower)
            return null;
        return $lower->date;
    }

    private function getNextWeekDate($season)
    {
        $upper = $season->weeks()->orderBy('date')->where('date', '>=', date('Y-m-d'))->first();
        if(!$upper)
            return null;
        return $upper->date;
    }

    private function getCurrentSeasonPart($season)
    {
        $lower = $season->weeks()->orderBy('date', 'desc')->where('weekType', '<>', 'post')->where('date', '<', date('Y-m-d'))->first();

        if(!$lower) {
            if ($season->weeks()->where('weekType', '=', 'pre')->count() <= 0)
                return 'regular';
            else
                return 'pre';
        }
        return $lower->weekType;
    }
}

class DivisionSchedule
{
    public $name = '';
    public $weeks;

    public function __construct($name, $divWeeks)
    {
        $this->name = $name;
        $this->weeks = $divWeeks;
    }
}

class DivWeek
{
    public $date;
    public $matches;
    public $byeTeamNames = [];

    public function __construct($date, $matches, $byeTeamNames)
    {
        $this->date = $date;
        $this->matches = $matches;
        $this->byeTeamNames = $byeTeamNames;
    }
}

class DivStandings
{
    public $name;
    public $standings = [];
}

class DivStanding
{
    public $teamId;
    public $name;
    public $matchPoints;
    public $wonPoints;
    public $lossPoints;
    public $percentage;
    public $ppts;
}