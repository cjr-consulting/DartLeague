<?php

namespace TrentonDarts\Http\Controllers\Season\Winter;

use Illuminate\Database\Eloquent\Collection;
use Illuminate\Http\Request;

use Illuminate\Support\Facades\Auth;
use TrentonDarts\Http\Requests;
use TrentonDarts\Http\Controllers\Controller;

use TrentonDarts\LeagueManagement\Models\BoardMember;
use TrentonDarts\LeagueManagement\Models\WinterSeasonWeek;
use TrentonDarts\LeagueManagement\Models\WinterSeason;
use TrentonDarts\LeagueManagement\Models\WinterSeasonTeam;
use TrentonDarts\MatchDomain\MatchRepository;
use TrentonDarts\Stats\AwardStatRepository;
use TrentonDarts\Stats\MatchStatsRepository;
use TrentonDarts\Stats\Models\MatchStat;
use TrentonDarts\Stats\PlayerGameRepository;
use TrentonDarts\Stats\TeamGameRepository;

class SeasonController extends Controller
{
    private $leaderboards = [
        'overall' => 'Overall',
        'singles' => 'Singles',
        'doubles' => 'Doubles',
        '801' => '801',
        'cricket' => 'Cricket',
        '01' => '01',
        'singles-301' => 'Singles 301',
        'singles-cricket' => 'Singles Cricket',
        'doubles-501' => 'Doubles 501',
        'doubles-cricket' => 'Doubles Cricket',
        'triples-801' => 'Triples 801'
    ];

    public function index()
    {
        return view('season.winter.index');
    }

    public function create()
    {
    }

    public function store(Request $request)
    {
    }

    public function show(Request $request, $id)
    {
        if(is_numeric($id))
        {
            $season = WinterSeason::findOrFail($id);
        } elseif($id == 'current'){
            $season = WinterSeason::where('isCurrent', 1)->firstOrFail();
        }

        $viewAllAwards = $request->query('allAwards', false);
        $currentWeekDate = $request->query('week', $this->getCurrentWeekDate($season, date('Y-m-d')));
        $seasonPart = $this->getCurrentSeasonPart($season, $currentWeekDate);
        $divName = 'preSeasonDiv';
        if($seasonPart == 'regular')
            $divName = 'regularSeasonDiv';

        $divisions = WinterSeasonTeam::where('seasonId', $season->id)->orderBy($divName)->groupBy($divName)->get()->pluck($divName);
        $divShorts = [];
        foreach($divisions as $division){
            if($division == '') continue;
            $v = substr($division, 0, 1);
            if(!in_array($v, $divShorts)){
                array_push($divShorts, $v);
            }
        }

        $previousWeekDate = $this->getPreviousWeekDate($season, $currentWeekDate);
        $matchStatsRepo = new MatchStatsRepository();
        $matchResults = $matchStatsRepo->getMatchResultsForSeason($season->id);

        $divSchedules = [];
        if(isset($currentWeekDate)) {
                $divTeams = [];
                foreach ($season->teams()->get() as $scheduleTeam) {
                    array_push($divTeams, $scheduleTeam->team->name);
                }

                $divWeeks = [];
                $week = $season->weeks()->where('date', date('Y-m-d', strtotime($currentWeekDate)))->first();
                if ($week) {
                    $byeTeamNames = $divTeams;
                    $matches = $week->matches()->orderBy('division')->get();
                    foreach ($matches as $match) {
                        $matchResult = $matchResults->getMatchResultByMatchId($match->id);
                        $match->awayScore = $matchResult->awayScore;
                        $match->homeScore = $matchResult->homeScore;
                        $match->hasScorecard = $matchResult->hasScorecard;
                        $key = array_search($match->homeTeam->name, $byeTeamNames);
                        if ($key !== false) {
                            unset($byeTeamNames[$key]);
                        }
                        $key = array_search($match->awayTeam->name, $byeTeamNames);
                        if ($key !== false) {
                            unset($byeTeamNames[$key]);
                        }
                    }

                    array_push($divWeeks, new DivWeek($week->date, $matches, $byeTeamNames));
                }

                array_push($divSchedules, new DivisionSchedule('', $divWeeks));
        }

        $nextWeekSchedules = [];
        $nextWeekDate = $this->getNextWeekDate($season, !$currentWeekDate ? date('Y-m-d') : $currentWeekDate);
        if(isset($nextWeekDate)) {

            $byeTeamNames = [];
            foreach ($season->teams()->get() as $scheduleTeam) {
                array_push($byeTeamNames, $scheduleTeam->team->name);
            }

            $divWeeks = [];
            $week = $season->weeks()->where('date', date('Y-m-d', strtotime($nextWeekDate)))->first();

            $matches = new Collection([]);
            if(isset($week)) {
                $matches = $week->matches()->orderBy('division')->get();
                foreach ($matches as $match) {
                    $key = array_search($match->homeTeam->name, $byeTeamNames);
                    if ($key !== false) {
                        unset($byeTeamNames[$key]);
                    }
                    $key = array_search($match->awayTeam->name, $byeTeamNames);
                    if ($key !== false) {
                        unset($byeTeamNames[$key]);
                    }
                }
            }

            array_push($divWeeks, new DivWeek($nextWeekSchedules, $matches, $byeTeamNames));
            array_push($nextWeekSchedules, new DivisionSchedule('', $divWeeks));
        }

        $matchStatsRepo = new MatchStatsRepository();

        $seasonPart = $this->getCurrentSeasonPart($season, $currentWeekDate);
        $divName = 'preSeasonDiv';
        if($seasonPart == 'regular')
            $divName = 'regularSeasonDiv';

        $divisions = WinterSeasonTeam::where('seasonId', $season->id)->orderBy($divName)->groupBy($divName)->get()->pluck($divName);

        if($season->accumulatePointsForAllParts) {
            $standings = $matchStatsRepo->getStandingsForSeason($season->id, $currentWeekDate);
        } else {
            $standings = $matchStatsRepo->getStandingsForSeasonPart($season->id, $seasonPart, $currentWeekDate);
        }

        $divStandings = $this->GetDivisionStandings($divisions, $season, $divName, $standings);

        $awards = $viewAllAwards ? $this->getAwards($season->id) : $this->getAwardsForDate($season->id, $currentWeekDate);

        return view('season.winter.show', [
            'isBoardMember' => $this->isBoardMember(),
            'seasonId' => $id,
            'season' => $season,
            'seasonPart' => $seasonPart,
            'previousWeekDate' => $previousWeekDate,
            'resultWeekDate' => $currentWeekDate,
            'divSchedules' => $divSchedules,
            'nextWeekDate' => $nextWeekDate,
            'nextWeekSchedules' => $nextWeekSchedules,
            'divStandings' => $divStandings,
            'awards' => $awards,
            'viewAllAwards' => $viewAllAwards]);
    }

    public function edit($id)
    {
    }

    public function update(Request $request, $id)
    {
    }

    public function destroy($id)
    {
    }

    public function schedule($id)
    {
        if(is_numeric($id))
        {
            $season = WinterSeason::findOrFail($id);
        } elseif($id == 'current'){
            $season = WinterSeason::where('isCurrent', 1)->firstOrFail();
        }
        $divName = 'preSeasonDiv';
        $divisions = WinterSeasonTeam::where('seasonId', $season->id)->orderBy($divName)->groupBy($divName)->get()->pluck($divName);
        $matchStatsRepo = new MatchStatsRepository();
        $matchResults = $matchStatsRepo->getMatchResultsForSeason($season->id);
        $divShorts = [];
        foreach($divisions as $division){
            if($division == '') continue;
            $v = substr($division, 0, 1);
            if(!in_array($v, $divShorts)){
                array_push($divShorts, $v);
            }
        }

        $divSchedules = [];
        foreach($divShorts as $division) {
            if ($division == '') continue;
            $divTeams = [];
            foreach ($season->teams()->where($divName, 'like', $division.'%')->get() as $scheduleTeam) {
                array_push($divTeams, $scheduleTeam->team->name);
            }

            $divWeeks = [];
            foreach ($season->preSeasonWeeks()->orderBy('date')->get() as $week) {
                $byeTeamNames = $divTeams;
                $matches = $week->matches()->where('division', 'like', $division.'%')->get();
                foreach ($matches as $match) {
                    $matchResult = $matchResults->getMatchResultByMatchId($match->id);
                    $match->awayScore = $matchResult->awayScore;
                    $match->homeScore = $matchResult->homeScore;
                    $match->hasScorecard = $matchResult->hasScorecard;
                    $key = array_search($match->homeTeam->name, $byeTeamNames);
                    if ($key !== false) {
                        unset($byeTeamNames[$key]);
                    }
                    $key = array_search($match->awayTeam->name, $byeTeamNames);
                    if ($key !== false) {
                        unset($byeTeamNames[$key]);
                    }
                }

                array_push($divWeeks, new DivWeek($week->date, $matches, $byeTeamNames));
            }

            array_push($divSchedules, new DivisionSchedule($division, $divWeeks));
        }

        $divName = 'regularSeasonDiv';
        $divisions = WinterSeasonTeam::where('seasonId', $season->id)->orderBy($divName)->groupBy($divName)->get()->pluck($divName);
        $divShorts = [];
        foreach($divisions as $division){
            if($division == '') continue;
            $v = substr($division, 0, 1);
            if(!in_array($v, $divShorts)){
                array_push($divShorts, $v);
            }
        }

        $divRegularSchedules = [];
        foreach($divShorts as $division) {
            if ($divShorts == '') continue;
            $divTeams = [];
            foreach ($season->teams()->where($divName, 'like', $division.'%')->get() as $scheduleTeam) {
                array_push($divTeams, $scheduleTeam->team->name);
            }

            $divWeeks = [];
            foreach ($season->regularSeasonWeeks()->orderBy('date')->get() as $week) {
                $byeTeamNames = $divTeams;
                $matches = $week->matches()->where('division', 'like', $division.'%')->get();
                foreach ($matches as $match) {
                    $matchResult = $matchResults->getMatchResultByMatchId($match->id);
                    $match->awayScore = $matchResult->awayScore;
                    $match->homeScore = $matchResult->homeScore;
                    $match->hasScorecard = $matchResult->hasScorecard;
                    $key = array_search($match->homeTeam->name, $byeTeamNames);
                    if ($key !== false) {
                        unset($byeTeamNames[$key]);
                    }
                    $key = array_search($match->awayTeam->name, $byeTeamNames);
                    if ($key !== false) {
                        unset($byeTeamNames[$key]);
                    }
                }

                array_push($divWeeks, new DivWeek($week->date, $matches, $byeTeamNames));
            }

            array_push($divRegularSchedules, new DivisionSchedule($division, $divWeeks));
        }

        if(sizeof($divRegularSchedules) == 0) $divRegularSchedules = null;
        $isBoardMember = $this->isBoardMember();

        return view('season.winter.schedule',[
            'season' => $season,
            'seasonId' => $id,
            'isBoardMember' => $isBoardMember,
            'divSchedules' => $divSchedules,
            'divRegularSchedules' => $divRegularSchedules]);
    }

    public function stats($id, Request $request)
    {
        if(is_numeric($id))
        {
            $season = WinterSeason::findOrFail($id);
        } elseif($id == 'current'){
            $season = WinterSeason::where('isCurrent', 1)->firstOrFail();
        }

        $leaderboard = $request->query('leaderboard', '');
        $seasonPart = 'whole';
        $seasonPart = $request->query('seasonPart', $seasonPart);

        $divisions = WinterSeasonTeam::where('seasonId', $season->id)->orderBy('preSeasonDiv')->groupBy('preSeasonDiv')->get()->pluck('preSeasonDiv')->toArray();
        $divisions = array_merge($divisions, WinterSeasonTeam::where('seasonId', $season->id)->orderBy('regularSeasonDiv')->groupBy('regularSeasonDiv')->get()->pluck('regularSeasonDiv')->toArray());

        $division = $request->query('division', '');

        $teamStatRepo = new TeamGameRepository();
        $teamStats = $teamStatRepo->getTeamStatsForSeasonPart($season->id, $seasonPart, $division);

        $playerStatRepo = new PlayerGameRepository();
        $playerStats = $playerStatRepo->getPlayerStatsForSeasonPart($season->id, $seasonPart, '');

        $divisionSelect = [];
        foreach($divisions as $div)
        {
            if($div =='') continue;
            $divisionSelect[$div] = $div;
        }

        if($leaderboard != '') {
            $teamStats = $teamStats->sortByDesc(function ($stat) use ($leaderboard) {
                switch ($leaderboard) {
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
                    default:
                        $sortable = $stat->teamName;
                }

                return $sortable;
            });
        }

        return view('season.winter.stats', [
            'season' => $season,
            'seasonId' => $id,
            'seasonPart' => $seasonPart,
            'divisions' => $divisionSelect,
            'division' => $division,
            'teamStats' => $teamStats,
            'playerStats' => $playerStats,
            'leaderboards' => $this->leaderboards,
            'leaderboard' => $leaderboard]);
    }

    public function statsExport($id, Request $request)
    {
        if(is_numeric($id))
        {
            $season = WinterSeason::findOrFail($id);
        } elseif($id == 'current'){
            $season = WinterSeason::where('isCurrent', 1)->firstOrFail();
        }

        $leaderboard = $request->query('leaderboard', '');
        $seasonPart = 'whole';
        $seasonPart = $request->query('seasonPart', $seasonPart);

        $divisions = WinterSeasonTeam::where('seasonId', $season->id)->orderBy('preSeasonDiv')->groupBy('preSeasonDiv')->get()->pluck('preSeasonDiv')->toArray();
        $divisions = array_merge($divisions, WinterSeasonTeam::where('seasonId', $season->id)->orderBy('regularSeasonDiv')->groupBy('regularSeasonDiv')->get()->pluck('regularSeasonDiv')->toArray());

        $division = $request->query('division', '');

        $teamStatRepo = new TeamGameRepository();
        $teamStats = $teamStatRepo->getTeamStatsForSeasonPart($season->id, $seasonPart, $division);

        $playerStatRepo = new PlayerGameRepository();
        $playerStats = $playerStatRepo->getPlayerStatsForSeasonPart($season->id, $seasonPart, '');

        $divisionSelect = [];
        foreach($divisions as $div)
        {
            if($div =='') continue;
            $divisionSelect[$div] = $div;
        }

        if($leaderboard != '') {
            $teamStats = $teamStats->sortByDesc(function ($stat) use ($leaderboard) {
                switch ($leaderboard) {
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
                    default:
                        $sortable = $stat->teamName;
                }

                return $sortable;
            });
        }

        $data = [];
        foreach($teamStats as $teamStat) {
            foreach ($playerStats->where('teamId', $teamStat->teamId) as $playerStat) {
                $row = [
                    "Player Name" => $playerStat->playerName,
                    "Team Name" => $playerStat->teamName,
                    "2nd Div" => $teamStat->regularSeasonDiv,
                    "Weeks Played" => $playerStat->weeksPlayed,
                    "Total Games Played" => $playerStat->gamesPlayed,
                    "Total Games Won" => $playerStat->gamesWon,
                    "Total Games Lost" => $playerStat->gamesLost,
                    "Single Games Played" => $playerStat->game301->played + $playerStat->gameCricket->played,
                    "Single Games Won" => $playerStat->game301->won + $playerStat->gameCricket->won,
                    "Single Games Lost" => $playerStat->game301->lost + $playerStat->gameCricket->lost,
                    "Single Win %" => $playerStat->singlesWin,
                ];

                array_push($data, $row);
            }
        }
        
        header('Content-Disposition: attachment; filename="StatsExport.csv"');
        header("Cache-control: private");
        header("Content-type: application/csv");

        $out = fopen("php://output", "w");
        fputcsv($out, array_keys($data[1]));
        foreach($data as $line)
        {
            fputcsv($out, $line);
        }

        fclose($out);
    }

    public function leaderboard($id, Request $request)
    {
        if(is_numeric($id))
        {
            $season = WinterSeason::findOrFail($id);
        } elseif($id == 'current'){
            $season = WinterSeason::where('isCurrent', 1)->firstOrFail();
        }

        $leaderboard = $request->query('leaderboard', 'overall');
        $seasonPart = 'whole';
        $seasonPart = $request->query('seasonPart', $seasonPart);

        $divisions = WinterSeasonTeam::where('seasonId', $season->id)->orderBy('preSeasonDiv')->groupBy('preSeasonDiv')->get()->pluck('preSeasonDiv')->toArray();
        $divisions = array_merge($divisions, WinterSeasonTeam::where('seasonId', $season->id)->orderBy('regularSeasonDiv')->groupBy('regularSeasonDiv')->get()->pluck('regularSeasonDiv')->toArray());

        $division = $request->query('division', '');

        $playerStatRepo = new PlayerGameRepository();
        $playerStats = $playerStatRepo->getPlayerStatsForSeasonPart($season->id, $seasonPart, $division);

        $divisionSelect = [];
        foreach($divisions as $div)
        {
            if($div =='') continue;
            $divisionSelect[$div] = $div;
        }

        return view('season.winter.leaderboard', [
            'season' => $season,
            'seasonPart' => $seasonPart,
            'divisions' => $divisionSelect,
            'division' => $division,
            'playerStats' => $playerStats,
            'leaderboards' => $this->leaderboards,
            'leaderboard' => $leaderboard]);
    }

    public function leaderboardExport($id, Request $request)
    {
        if(is_numeric($id))
        {
            $season = WinterSeason::findOrFail($id);
        } elseif($id == 'current'){
            $season = WinterSeason::where('isCurrent', 1)->firstOrFail();
        }

        $leaderboard = $request->query('leaderboard', 'overall');
        $seasonPart = 'whole';
        $seasonPart = $request->query('seasonPart', $seasonPart);

        $divisions = WinterSeasonTeam::where('seasonId', $season->id)->orderBy('preSeasonDiv')->groupBy('preSeasonDiv')->get()->pluck('preSeasonDiv')->toArray();
        $divisions = array_merge($divisions, WinterSeasonTeam::where('seasonId', $season->id)->orderBy('regularSeasonDiv')->groupBy('regularSeasonDiv')->get()->pluck('regularSeasonDiv')->toArray());

        $division = $request->query('division', '');

        $playerStatRepo = new PlayerGameRepository();
        $playerStats = $playerStatRepo->getPlayerStatsForSeasonPart($season->id, $seasonPart, $division);

        $divisionSelect = [];
        foreach($divisions as $div)
        {
            if($div =='') continue;
            $divisionSelect[$div] = $div;
        }

        foreach($awards as $award){
            $row = [
                "Player Name" => $award->playerName,
                "Team Name" => $award->teamName,
                "Season Part" => $award->seasonPart == 'pre' ? 'Pre' : 'Regular',
                "Division" => $award->division,
                "Award Date" => date('m-d-Y', strtotime($award->date)),
                "Award Type" => $award->awardType,
                "Award Value" => $award->value > 0 ? $award->value : ''
            ];

            array_push($data, $row);
        }
        
        header('Content-Disposition: attachment; filename="awardsExport.csv"');
        header("Cache-control: private");
        header("Content-type: application/csv");

        $out = fopen("php://output", "w");
        fputcsv($out, array_keys($data[1]));
        foreach($data as $line)
        {
            fputcsv($out, $line);
        }

        fclose($out);
    }

    public function awards($id, Request $request)
    {
        if(is_numeric($id))
        {
            $season = WinterSeason::findOrFail($id);
        } elseif($id == 'current'){
            $season = WinterSeason::where('isCurrent', 1)->firstOrFail();
        }

        $seasonPart = 'whole';
        $seasonPart = $request->query('seasonPart', $seasonPart);

        $divisions = WinterSeasonTeam::where('seasonId', $season->id)->orderBy('preSeasonDiv')->groupBy('preSeasonDiv')->get()->pluck('preSeasonDiv')->toArray();
        $divisions = array_merge($divisions, WinterSeasonTeam::where('seasonId', $season->id)->orderBy('regularSeasonDiv')->groupBy('regularSeasonDiv')->get()->pluck('regularSeasonDiv')->toArray());

        $division = $request->query('division', '');

        $divisionSelect = [];
        foreach($divisions as $div)
        {
            if($div =='') continue;
            $divisionSelect[$div] = $div;
        }

        $awardsRepo = new AwardStatRepository();
        $awards = $awardsRepo->getAwardStatsForSeason($season->id);
        if($seasonPart != 'whole')
            $awards = $awards->where('seasonPart', $seasonPart);

        if($division != '')
            $awards = $awards->where('division', $division);

        $awards = $awards->sortByDesc(function($award){
                return sprintf('%-12s%s', $award->awardType, $award->value);
            });

        return view('season.winter.awards',[
            'season' => $season,
            'seasonId' => $id,
            'seasonPart' => $seasonPart,
            'divisions' => $divisionSelect,
            'division' => $division,
            'awardTypes' => [],
            'awardType' => '',
            'awards' => $awards
        ]);
    }

    public function awardsExport($id, Request $request){
        if(is_numeric($id))
        {
            $season = WinterSeason::findOrFail($id);
        } elseif($id == 'current'){
            $season = WinterSeason::where('isCurrent', 1)->firstOrFail();
        }
        
        $data = [];
        $seasonPart = 'whole';
        $seasonPart = $request->query('seasonPart', $seasonPart);

        $divisions = WinterSeasonTeam::where('seasonId', $season->id)->orderBy('preSeasonDiv')->groupBy('preSeasonDiv')->get()->pluck('preSeasonDiv')->toArray();
        $divisions = array_merge($divisions, WinterSeasonTeam::where('seasonId', $season->id)->orderBy('regularSeasonDiv')->groupBy('regularSeasonDiv')->get()->pluck('regularSeasonDiv')->toArray());

        $division = $request->query('division', '');

        $divisionSelect = [];
        foreach($divisions as $div)
        {
            if($div =='') continue;
            $divisionSelect[$div] = $div;
        }

        $awardsRepo = new AwardStatRepository();
        $awards = $awardsRepo->getAwardStatsForSeason($season->id);
        if($seasonPart != 'whole')
            $awards = $awards->where('seasonPart', $seasonPart);

        if($division != '')
            $awards = $awards->where('division', $division);

        $awards = $awards->sortByDesc(function($award){
            return sprintf('%-12s%s', $award->awardType, $award->value);
        });
        
        foreach($awards as $award){
            $row = [
                "Player Name" => $award->playerName,
                "Team Name" => $award->teamName,
                "Season Part" => $award->seasonPart == 'pre' ? 'Pre' : 'Regular',
                "Division" => $award->division,
                "Award Date" => date('m-d-Y', strtotime($award->date)),
                "Award Type" => $award->awardType,
                "Award Value" => $award->value > 0 ? $award->value : ''
            ];
            
            array_push($data, $row);
        }
        
        header('Content-Disposition: attachment; filename="awardsExport.csv"');
        header("Cache-control: private");
        header("Content-type: application/csv");

        $out = fopen("php://output", "w");
        fputcsv($out, array_keys($data[1]));
        foreach($data as $line)
        {
            fputcsv($out, $line);
        }

        fclose($out);
    }
    
    public function sortTeamsStandings($a, $b)
    {
        if($a->percentage == $b->percentage)
            return 0;
        return ($a->percentage > $b->percentage) ? -1 : 1;
    }

    public function sortTeamsStandingsMatchPoints($a, $b)
    {
        if($a->matchPoints == $b->matchPoints)
            return $this->sortTeamsStandings($a, $b);
        return ($a->matchPoints > $b->matchPoints) ? -1 : 1;
    }

    private function getPreviousWeekDate($season, $weekDate)
    {
        if(!$weekDate)
            return null;

        $lower = $season->weeks()->orderBy('date', 'desc')->where('date', '<', $weekDate)->first();
        if(!$lower)
            return null;

        return $lower->date;
    }

    private function getCurrentWeekDate($season, $weekDate)
    {
        if(!$weekDate)
            return null;

        $lower = $season->weeks()->orderBy('date', 'desc')->where('date', '<=', $weekDate)->first();
        if(!$lower)
            return null;

        return $lower->date;
    }

    private function getNextWeekDate($season, $weekDate)
    {
        $upper = $season->weeks()->orderBy('date')->where('date', '>', $weekDate)->first();
        if(!$upper)
            return null;
        return $upper->date;
    }

    private function getCurrentSeasonPart($season, $weekDate)
    {
        if(!$weekDate) {
            if ($season->weeks()->where('weekType', '=', 'pre')->count() <= 0)
                return 'regular';
        }

        $lower = $season->weeks()->orderBy('date', 'desc')->where('weekType', '<>', 'post')->where('date', '<=', $weekDate)->first();
        if(!$lower)
            return 'pre';
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
        $a = $awardStatRepo->getAwardStatsForSeason($id);
        return $a;
    }

    private function getAwardsForDate($id, $currentWeekDate)
    {
        $awardStatRepo = new AwardStatRepository();
        $a = $awardStatRepo->getAwardStatsForSeason($id, $currentWeekDate);
        return $a;
    }

    /**
     * @param $divisions
     * @param $season
     * @param $divName
     * @param $standings
     * @return array
     */
    private function GetDivisionStandings($divisions, $season, $divName, $standings)
    {
        $divStandings = [];
        foreach ($divisions as $division) {
            if ($division == '') continue;
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
                if ($standing) {
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
        return $divStandings;
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