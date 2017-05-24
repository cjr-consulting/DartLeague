<?php

namespace TrentonDarts\Http\Controllers\Season\Winter;

use Illuminate\Http\Request;

use TrentonDarts\Http\Requests;
use TrentonDarts\Http\Controllers\Controller;
use TrentonDarts\Http\Requests\BoardMemberRequest;
use TrentonDarts\LeagueManagement\Models\WinterSeason;
use TrentonDarts\LeagueManagement\Models\WinterSeasonMatch;
use TrentonDarts\LeagueManagement\Models\WinterSeasonTeam;
use TrentonDarts\MatchDomain\MatchRepository;
use TrentonDarts\MatchDomain\Models\GamePlayer;
use TrentonDarts\Stats\AwardStatRepository;
use TrentonDarts\Stats\MatchStatsRepository;
use TrentonDarts\Stats\PlayerGameRepository;
use TrentonDarts\Stats\TeamGameRepository;

class MatchController extends Controller
{
    /**
     * Display a listing of the resource.
     *
     * @return Response
     */
    public function index()
    {
        //
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

    /**
     * Display the specified resource.
     *
     * @param $seasonId
     * @param  int $id
     * @return Response
     */
    public function show($seasonId, $id, Request $request)
    {
        if(is_numeric($seasonId))
        {
            $season = WinterSeason::findOrFail($seasonId);
        } else {
            $season = WinterSeason::where('isCurrent', 1)->firstOrFail();
        }

        $match = WinterSeasonMatch::findOrFail($id);
        $matchesThisWeek = [];
        foreach(WinterSeasonMatch::where('weekId', $match->weekId)->orderBy('division')->get() as $m){
            $route = route('season.match.show', ['seasonId' => $seasonId, 'id' => $m->id]);
            $matchesThisWeek[$route] = 'Div '.$m->division.' ('.date('m-d-Y', strtotime($m->week->date)).') '.$m->awayTeam->name.' @ '.$m->homeTeam->name;
        }

        $homeTeamMatches = WinterSeasonMatch::where('winter_season_matches.seasonId', $season->id)
            ->where(function($query)use($match){
                $query->where('homeTeamId',$match->homeTeamId)
                    ->orWhere('awayTeamId', $match->homeTeamId);
            })->join('winter_season_weeks', 'winter_season_matches.weekId', '=', 'winter_season_weeks.id')
            ->orderBy('winter_season_weeks.date')->get();

        $awayTeamMatches = WinterSeasonMatch::where('winter_season_matches.seasonId', $season->id)
            ->where(function($query)use($match){
                $query->where('homeTeamId',$match->awayTeamId)
                    ->orWhere('awayTeamId', $match->awayTeamId);
            })->join('winter_season_weeks', 'winter_season_matches.weekId', '=', 'winter_season_weeks.id')
            ->orderBy('winter_season_weeks.date')->get();

        $matchResults = MatchRepository::getMatchResultsFromMatch($match)->getSnapshot();
        $homeTeam = WinterSeasonTeam::where('teamId', $match->homeTeamId)->where('seasonId', $season->id)->first();
        $awayTeam = WinterSeasonTeam::where('teamId', $match->awayTeamId)->where('seasonId', $season->id)->first();
        $awayPlayers = [];
        $homePlayers = [];

        foreach($awayTeam->teamPlayers as $teamPlayer)
        {
            if($teamPlayer->player != null) {
                $p = new GamePlayer();
                $p->id = $teamPlayer->player->id;
                $p->name = $teamPlayer->player->name;
                array_push($awayPlayers, $p);
            }
        }

        foreach($homeTeam->teamPlayers as $teamPlayer)
        {
            if($teamPlayer->player != null) {
                $p = new GamePlayer();
                $p->id = $teamPlayer->player->id;
                $p->name = $teamPlayer->player->name;
                array_push($homePlayers, $p);
            }
        }

        return view('season.winter.match.show',['season' => $season,
            'seasonId' => $seasonId,
            'matchesThisWeek' => $matchesThisWeek,
            'selectedMatchRoute' => route('season.match.show', ['seasonId' => $seasonId, 'id' => $id]),
            'match' => $match,
            'matchResults' => json_encode($matchResults),
            'awayPlayers' => json_encode($awayPlayers),
            'homePlayers' => json_encode($homePlayers),
            'redirectUrl' => $request->server('HTTP_REFERER')]);
    }

    /**
     * Show the form for editing the specified resource.
     *
     * @param Requests\BoardMemberRequest $request
     * @param $seasonId
     * @param  int $id
     * @return Response
     */
    public function edit(BoardMemberRequest $request, $seasonId, $id)
    {
        if(is_numeric($seasonId))
        {
            $season = WinterSeason::findOrFail($seasonId);
        } elseif($seasonId == 'current'){
            $season = WinterSeason::where('isCurrent', 1)->firstOrFail();
        }

        $match = WinterSeasonMatch::findOrFail($id);
        $matchesThisWeek = [];
        foreach(WinterSeasonMatch::where('weekId', $match->weekId)->orderBy('division')->get() as $m){
            $route = route('season.match.edit', ['seasonId' => $seasonId, 'id' => $m->id]);
            $matchesThisWeek[$route] = 'Div '.$m->division.' ('.date('m-d-Y', strtotime($m->week->date)).') '.$m->awayTeam->name.' @ '.$m->homeTeam->name;
        }

        $homeTeamMatches = WinterSeasonMatch::where('winter_season_matches.seasonId', $season->id)
            ->where(function($query)use($match){
                $query->where('homeTeamId',$match->homeTeamId)
                    ->orWhere('awayTeamId', $match->homeTeamId);
            })->join('winter_season_weeks', 'winter_season_matches.weekId', '=', 'winter_season_weeks.id')
            ->orderBy('winter_season_weeks.date')->get();

        $awayTeamMatches = WinterSeasonMatch::where('winter_season_matches.seasonId', $season->id)
            ->where(function($query)use($match){
                $query->where('homeTeamId',$match->awayTeamId)
                    ->orWhere('awayTeamId', $match->awayTeamId);
            })->join('winter_season_weeks', 'winter_season_matches.weekId', '=', 'winter_season_weeks.id')
            ->orderBy('winter_season_weeks.date')->get();

        $matchResults = MatchRepository::getMatchResultsFromMatch($match)->getSnapshot();
        $homeTeam = WinterSeasonTeam::where('teamId', $match->homeTeamId)->where('seasonId', $season->id)->first();
        $awayTeam = WinterSeasonTeam::where('teamId', $match->awayTeamId)->where('seasonId', $season->id)->first();
        $awayPlayers = [];
        $homePlayers = [];

        foreach($awayTeam->teamPlayers as $teamPlayer)
        {
            if($teamPlayer->player != null) {
                $p = new GamePlayer();
                $p->id = $teamPlayer->player->id;
                $p->name = $teamPlayer->player->name;
                array_push($awayPlayers, $p);
            }
        }

        foreach($homeTeam->teamPlayers as $teamPlayer)
        {
            if($teamPlayer->player != null) {
                $p = new GamePlayer();
                $p->id = $teamPlayer->player->id;
                $p->name = $teamPlayer->player->name;
                array_push($homePlayers, $p);
            }
        }

        usort($awayPlayers, array($this, "sortByName"));
        usort($homePlayers, array($this, "sortByName"));
        return view('season.winter.match.edit',[
            'season' => $season,
            'seasonId' => $seasonId,
            'matchesThisWeek' => $matchesThisWeek,
            'selectedMatchRoute' => route('season.match.edit', ['seasonId' => $seasonId, 'id' => $id]),
            'match' => $match,
            'matchResults' => json_encode($matchResults),
            'awayPlayers' => json_encode($awayPlayers),
            'homePlayers' => json_encode($homePlayers),
            'redirectUrl' => $request->server('HTTP_REFERER')]);
    }

    function sortByName($a, $b)
    {
        return strcmp($a->name, $b->name);
    }
    /**
     * Update the specified resource in storage.
     *
     * @param  Request  $request
     * @param  int  $id
     * @return Response
     */
    public function update(BoardMemberRequest $request, $seasonId, $id)
    {
        if(is_numeric($seasonId))
        {
            $season = WinterSeason::findOrFail($seasonId);
        } elseif($seasonId == 'current'){
            $season = WinterSeason::where('isCurrent', 1)->firstOrFail();
        }
        $match = WinterSeasonMatch::findOrFail($id);

        $data = $request->json()->all();

        MatchRepository::saveMatchResultsData($match->id, $data);

        $matchResults = MatchRepository::getMatchResultsFromMatch($match);

        $matchStatRepository = new MatchStatsRepository();
        $matchStatRepository->updateMatchStats($matchResults);

        $teamGameStatRepository = new TeamGameRepository();
        $teamGameStatRepository->updateTeamGameStats($matchResults);

        $playerGameStatRepository = new PlayerGameRepository();
        $playerGameStatRepository->updatePlayerGameStats($matchResults);

        $awardStatRepository = new AwardStatRepository();
        $awardStatRepository->updateAwardStats($matchResults);

        $responseData = new \stdClass();
        $responseData->redirectUrl = route('season.schedule', ['id' => $seasonId]);
        return response(json_encode($responseData), 200)->header('Content-Type','text');
    }

    /**
     * Remove the specified resource from storage.
     *
     * @param  int  $id
     * @return Response
     */
    public function destroy($id)
    {

    }
}
