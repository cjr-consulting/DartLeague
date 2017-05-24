<?php

namespace TrentonDarts\Http\Controllers;

use Illuminate\Http\Request;

use Illuminate\Support\Facades\Auth;
use TrentonDarts\Http\Requests;
use TrentonDarts\Http\Controllers\Controller;
use TrentonDarts\LeagueManagement\Models\BoardMember;
use TrentonDarts\LeagueManagement\Models\WinterSeason;
use TrentonDarts\LeagueManagement\Models\WinterSeasonTeam;
use TrentonDarts\MatchDomain\MatchRepository;
use TrentonDarts\Stats\AwardStatRepository;
use TrentonDarts\Stats\MatchStatsRepository;
use TrentonDarts\Stats\PlayerGameRepository;
use TrentonDarts\Stats\TeamGameRepository;

class TeamController extends Controller
{
    private $matchStatsRepo;

    public function __construct()
    {
        $this->matchStatsRepo = new MatchStatsRepository();
    }
    /**
     * Display a listing of the resource.
     *
     * @return Response
     */
    public function index()
    {
        $season = WinterSeason::where('isCurrent', 1)->first();
        if($season == null)
            $season = WinterSeason::orderBy('endYear', 'desc')->first();

        $seasonTeams = $season->teams;
        foreach($seasonTeams as $seasonTeam)
        {
            $seasonTeam->captains = $seasonTeam->teamPlayers()->whereIn('role', ['Captain', 'Co-Captain'])->get();
        }

        return view('team.list', [
            'season' => $season,
            'seasonTeams' => $seasonTeams
        ]);
    }

    /**
     * Show the form for creating a new resource.
     *
     * @return Response
     */
    public function create()
    {
        //
    }

    /**
     * Store a newly created resource in storage.
     *
     * @param  Request  $request
     * @return Response
     */
    public function store(Request $request)
    {
        //
    }

    public function show($id, Request $request)
    {
        $seasonId = $request->query('season', 'current');
        if(is_numeric($seasonId)) {
            $season = WinterSeason::where('id', $seasonId)->firstOrFail();
        } else {
            $season = WinterSeason::where('isCurrent', 1)->firstOrFail();
        }

        $seasons = WinterSeason::orderBy('endYear')->pluck('name', 'id');
        $seasonPart = $request->query('part', $this->getCurrentSeasonPart($season));
        $divName = 'preSeasonDiv';
        if($seasonPart == 'regular')
            $divName = 'regularSeasonDiv';

        $seasonTeam = WinterSeasonTeam::where('teamId', $id)->where('seasonId', $season->id)->firstOrFail();

        $teamSchedule = $this->getTeamSchedule($season, $seasonTeam->teamId);
        $divStandings = $this->getDivisionStandings($season, $divName, $seasonTeam[$divName]);
        $awards = $this->getAwards($season->id)->where('teamId', $seasonTeam->teamId);

        $teamStatRepo = new TeamGameRepository();
        $playerStatRepo = new PlayerGameRepository();

        $statPart = 'whole';
        if(!$season->accumulatePointsForAllParts) {
            $statPart = $seasonPart;
        }

        $statPart = $request->query('statpart', $statPart);

        $teamStats = $teamStatRepo->getTeamStatsForSeasonPart($season->id, $statPart, '')->where('teamId', $seasonTeam->teamId);
        $playerStats = $playerStatRepo->getPlayerStatsForSeasonPart($season->id, $statPart, '')->where('teamId', $seasonTeam->teamId);

        return view('team.show',[
            'seasons' => $seasons,
            'season' => $season,
            'seasonTeam' => $seasonTeam,
            'statPart' => $statPart,
            'divStandings' => $divStandings,
            'awards' => $awards,
            'teamStats' => $teamStats,
            'playerStats' => $playerStats,
            'teamSchedule' => $teamSchedule,
            'isBoardMember' => $this->isBoardMember()
        ]);
    }

    /**
     * Show the form for editing the specified resource.
     *
     * @param  int  $id
     * @return Response
     */
    public function edit($id)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     *
     * @param  Request  $request
     * @param  int  $id
     * @return Response
     */
    public function update(Request $request, $id)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     *
     * @param  int  $id
     * @return Response
     */
    public function destroy($id)
    {
        //
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
        $datesAfterToday = $season->weeks()->orderBy('date', 'desc')->where('weekType', '<>', 'post')->where('date', '>=', date('Y-m-d'))->first();
        if(!$lower) {
            if(!$datesAfterToday)
                return 'regular';
            return 'pre';
        }
        return $lower->weekType;
    }

    /**
     * @return bool
     */
    private function isBoardMember()
    {
        $user = Auth::user();
        $isBoardMember = false;
        if ($user) {
            $isBoardMember = BoardMember::where('userId', $user->id)->count() > 0;
            return $isBoardMember;
        }
        return $isBoardMember;
    }

    private function getAwards($id)
    {
        $awardStatRepo = new AwardStatRepository();
        $awards = $awardStatRepo->getAwardStatsForSeason($id);
        foreach($awards as $award){
            switch($award->awardType) {
                case "High On":
                    $award->awardImage = 'high-on';
                    break;
                case "High Out":
                    $award->awardImage = 'high-out';
                    break;
                case "Round 9":
                    $award->awardImage = 'round-9';
                    break;
                case "T 71":
                    $award->awardImage = 'ton-71';
                    break;
                case "T 74":
                    $award->awardImage = 'ton-74';
                    break;
                case "T 77":
                    $award->awardImage = 'ton-77';
                    break;
                case "T 80":
                    $award->awardImage = 'ton-80';
                    break;
            }
        }
        return $awards;
    }

    /**
     * @param $season
     * @param $divName
     * @param $division
     * @return array
     * @internal param $divisions
     * @internal param $seasonTeam
     */
    private function getDivisionStandings($season, $divName, $division)
    {
        $divStandings = [];
        $seasonPart = $this->getCurrentSeasonPart($season);

        if($season->accumulatePointsForAllParts) {
            $standings = $this->matchStatsRepo->getStandingsForSeason($season->id);
        } else {
            $standings = $this->matchStatsRepo->getStandingsForSeasonPart($season->id, $seasonPart);
        }

        $div = new DivStandings();
        $div->name = $division;
        foreach ($season->teams()->where($divName, $division)->get() as $scheduleTeam) {
            $standing = $standings->where('teamId', $scheduleTeam->teamId)->first();
            $divStanding = new DivStanding();
            $divStanding->matchPoints = 0;
            $divStanding->wonPoints = 0;
            $divStanding->percentage = 0;
            $divStanding->lossPoints = 0;
            $divStanding->ppts = 0;

            $divStanding->name = $scheduleTeam->team->name;
            if($standing) {
                $divStanding->teamId = $scheduleTeam->team->id;
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
        return $divStandings;
    }

    private function getTeamSchedule($season, $teamId)
    {
        $matchStatsRepo = new MatchStatsRepository();
        $matchResults = $matchStatsRepo->getMatchResultsForSeason($season->id);

        $schedules = [];

        foreach ($season->weeks()->orderBy('date')->get() as $week) {
            $schedule = new Schedule();
            $schedule->date = $week->date;
            $schedule->isAwayGame = false;
            $schedule->seasonPart = $week->weekType == 'pre' ? 'Pre' : 'Regular';

            $match = $week->matches()->where(function($query) use ($teamId){
                    $query
                        ->where('homeTeamId', $teamId)
                        ->orWhere('awayTeamId', $teamId);
                })->first();

            if($match == null) {
                $schedule->isBye = true;
            }
            else {
                $schedule->matchId = $match->id;
                $matchResult = $matchResults->getMatchResultByMatchId($match->id);
                $schedule->awayTeamId = $match->awayTeamId;
                $schedule->awayTeamName = $match->awayTeam->name;
                $schedule->homeTeamId = $match->homeTeamId;
                $schedule->homeTeamName = $match->homeTeam->name;
                $schedule->hasScorecard = $matchResult->hasScorecard;

                $schedule->homeScore = $matchResult->homeScore;
                $schedule->awayScore = $matchResult->awayScore;

                $schedule->isAwayGame = $match->awayTeamId == $teamId;
            }

            array_push($schedules, $schedule);
        }

        return $schedules;
    }
}

class Schedule
{
    public $isAwayGame;
    public $isBye;
    public $date;
    public $seasonPart;
    public $awayTeamId;
    public $awayTeamName;
    public $awayScore;
    public $homeTeamId;
    public $homeTeamName;
    public $homeScore;
    public $matchId;
    public $hasScorecard;
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