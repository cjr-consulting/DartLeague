<?php

namespace TrentonDarts\Http\Controllers;

use Illuminate\Http\Request;

use Illuminate\Support\Collection;
use Illuminate\Support\Facades\Auth;
use TrentonDarts\Http\Requests;
use TrentonDarts\Http\Controllers\Controller;
use TrentonDarts\LeagueManagement\Models\BoardMember;
use TrentonDarts\LeagueManagement\Models\Player;
use TrentonDarts\LeagueManagement\Models\WinterSeason;
use TrentonDarts\MatchDomain\Services\PlayerHistoryService;
use TrentonDarts\Stats\PlayerGameRepository;

class PlayerController extends Controller
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
     * @param  int  $id
     * @return Response
     */
    public function show($id, Request $request)
    {
        $seasonId = $request->query('season', 'current');
        if(is_numeric($seasonId)) {
            $season = WinterSeason::where('id', $seasonId)->firstOrFail();
        } else {
            $season = WinterSeason::where('isCurrent', 1)->firstOrFail();
        }

        $playerTeam = $this->getPlayersTeam($season, (int) $id);

        $player = Player::findOrFail($id);
        $playerHistoryService = new PlayerHistoryService();
        $gameHistories = $playerHistoryService->getPlayerGameHistory($season->id, $id);

        $playerStatRepo = new PlayerGameRepository();
        $playerStats = $playerStatRepo->getPlayerStatsForSeasonPart($season->id, 'whole', '')->where('teamId', $playerTeam->id);

        $groupBy = $request->query('groupby', 'match');
        $viewName = 'player.show';
        if($groupBy == 'gameType') {
            $viewName = 'player.show-groupby-gametype';
            $gameHistories = $gameHistories->sortBy(function($history){
                return sprintf('%-12s%s', $history->gameType, date('Y-m-d', strtotime($history->date)));
            });
        }
        return view($viewName, [
            'season' => $season,
            'playerStats' => $playerStats,
            'playerTeam' => $playerTeam,
            'player' => $player,
            'gameHistories' => $gameHistories,
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

    private function getPlayersTeam($season, $playerId)
    {
        foreach($season->teams as $seasonTeam)
        {
            foreach($seasonTeam->teamPlayers as $teamPlayer)
            {
                if($teamPlayer->playerId == $playerId)
                    return $seasonTeam->team;
            }
        }

        return null;
    }
}
