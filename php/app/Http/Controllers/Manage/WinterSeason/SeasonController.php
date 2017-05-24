<?php

namespace TrentonDarts\Http\Controllers\Manage\WinterSeason;

use Illuminate\Http\Request;

use TrentonDarts\Http\Requests;
use TrentonDarts\Http\Requests\StoreLeagueManagementRequest;
use TrentonDarts\Http\Controllers\Controller;

use TrentonDarts\Jobs\updateMatchStats;
use TrentonDarts\LeagueManagement\Models\MatchType;
use TrentonDarts\LeagueManagement\Models\Player;
use TrentonDarts\LeagueManagement\Models\WinterSeason;
use TrentonDarts\LeagueManagement\Models\WinterSeasonTeamPlayer;
use TrentonDarts\LeagueManagement\PaymentRepository;
use TrentonDarts\LeagueManagement\Services\PaymentService;

use TrentonDarts\MatchDomain\MatchRepository;
use TrentonDarts\MatchDomain\Models\GamePlayer;
use TrentonDarts\Stats\AwardStatRepository;
use TrentonDarts\Stats\MatchStatsRepository;
use TrentonDarts\Stats\PlayerGameRepository;
use TrentonDarts\Stats\TeamGameRepository;

class SeasonController extends Controller
{
    private $paymentService;

    public function __construct()
    {
        $paymentRepo = new PaymentRepository();
        $this->paymentService = new PaymentService($paymentRepo);
    }
    /**
     * Display a listing of the resource.
     *
     * @return Response
     */
    public function index($leagueId)
    {
        $seasons = WinterSeason::where('leagueId', $leagueId)->orderBy('startYear', 'desc')->get();
        return view('manage.season.list', ['leagueId' => $leagueId, 'seasons' => $seasons]);
    }

    /**
     * Show the form for creating a new resource.
     *
     * @return Response
     */
    public function create($leagueId)
    {
        $years = ['2010' => '2010', '2011' => '2011', '2012' => '2012', '2013' => '2013', '2014' => '2014', '2015' => '2015', '2016' => '2016', '2017' => '2017'];
        $seasonTypes = ['Winter Season' => 'Winter Season'];
        $matchTypes = MatchType::orderBy('name')->get()->lists('name', 'id');
        return view('manage.season.winter.create', ['leagueId' => $leagueId, 'years' => $years, 'seasonTypes' => $seasonTypes, 'matchTypes' => $matchTypes]);
    }

    /**
     * Store a newly created resource in storage.
     *
     * @param  Request  $request
     * @return Response
     */
    public function store($leagueId, StoreLeagueManagementRequest $request)
    {
        $this->validate($request, [
            'name' => 'required|unique:winter_seasons|max:255'
            ]);

        $season = WinterSeason::create($request->input());
        $season->leagueId = $leagueId;
        $season->save();

        return redirect()->route('manage.season.index', ['leagueId' => $leagueId]);
    }

    /**
     * Display the specified resource.
     *
     * @param  int  $id
     * @return Response
     */
    public function show($leagueId, $id)
    {
        $season = WinterSeason::findOrFail($id);
        $outstandingPayments = $this->paymentService->howManyOutstandingPaymentsForSeason($id);
        foreach($season->teams as $team) {
            $outstandingPayments += $this->paymentService->howManyOutstandingPaymentsForTeam($id, $team->teamId);
        }

        return view('manage.season.winter.show', [
            'leagueId' => $leagueId,
            'paymentStillOutStanding' => $outstandingPayments,
            'season' => $season]);
    }

    /**
     * Show the form for editing the specified resource.
     *
     * @param  int  $id
     * @return Response
     */
    public function edit($leagueId, $id)
    {
        $season = WinterSeason::findOrFail($id);
        $years = ['2010' => '2010', '2011' => '2011', '2012' => '2012', '2013' => '2013', '2014' => '2014', '2015' => '2015', '2016' => '2016', '2017' => '2017'];
        $seasonTypes = ['Winter Season' => 'Winter Season'];
        $matchTypes = MatchType::orderBy('name')->get()->lists('name', 'id');
        return view('manage.season.winter.edit', ['leagueId' => $leagueId, 'season' => $season, 'years' => $years, 'seasonTypes' => $seasonTypes, 'matchTypes' => $matchTypes]);
    }

    /**
     * Update the specified resource in storage.
     *
     * @param  Request  $request
     * @param  int  $id
     * @return Response
     */
    public function update(StoreLeagueManagementRequest $request, $leagueId, $id)
    {
        $this->validate($request, [
            'name' => 'required|unique:winter_seasons,name,'.$id.'|max:255'
            ]);
        $season = WinterSeason::findOrFail($id);
        $season->update($request->input());
        $season->save();

        return redirect()->route('manage.season.index', ['leagueId' => $leagueId]);
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

    public function payments($leagueId, $seasonId)
    {
        $season = WinterSeason::findOrFail($seasonId);
        $seasonTeams = $season->teams;
        foreach($seasonTeams as $seasonTeam)
        {
            $seasonTeam->paymentStatus = $this->paymentService->getTeamPaymentStatus($seasonId, $seasonTeam->team->id);
            $seasonTeam->outstandingPlayerPayments = $this->paymentService->howManyOutstandingPaymentsForTeam($seasonId, $seasonTeam->team->id);
        }

        $payment = new \stdClass();
        $payment->paymentStatus = '';
        $payment->teamId = 0;
        return view('manage.season.winter.payments', [
            'season' => $season,
            'leagueId' => $leagueId,
            'payment' => $payment,
            'seasonTeams' => $seasonTeams]);
    }

    public function paymentStore(StoreLeagueManagementRequest $request, $leagueId, $seasonId)
    {
        var_dump($request->input());
        $teamId = $request->input('teamId');
        $paymentStatus = $request->input('paymentStatus');
        $this->paymentService->saveTeamPayment($seasonId, $teamId, $paymentStatus);
        return redirect()->route('manage.seasonTeamPayments', ['leagueId' => $leagueId, 'seasonId' => $seasonId]);
    }

    public function playerPayments($leagueId, $seasonId, $teamId)
    {
        $season = WinterSeason::findOrFail($seasonId);

        $seasonTeam = $season->teams()->where('teamId', $teamId)->first();
        $teamPlayers = $seasonTeam->teamPlayers;
        foreach($teamPlayers as $player)
        {
            $player->paymentStatus = $this->paymentService->getPlayerPaymentStatus($seasonId, $player->playerId);
        }

        $payment = new \stdClass();
        $payment->paymentStatus = '';
        $payment->playerId = 0;
        return view('manage.season.winter.playerPayments', [
            'season' => $season,
            'leagueId' => $leagueId,
            'teamId' => $teamId,
            'seasonTeam' => $seasonTeam,
            'payment' => $payment,
            'teamPlayers' => $teamPlayers]);
    }

    public function playerPaymentStore(StoreLeagueManagementRequest $request, $leagueId, $seasonId, $teamId)
    {
        $playerId = $request->input('playerId');
        $paymentStatus = $request->input('paymentStatus');
        $this->paymentService->savePlayerPayment($seasonId, $playerId, $paymentStatus);
        return redirect()->route('manage.seasonPlayerPayments', ['leagueId' => $leagueId, 'seasonId' => $seasonId, 'teamId' => $teamId]);
    }

    public function updateStats($leagueId, $seasonId, StoreLeagueManagementRequest $request)
    {
        set_time_limit(300);
        $season = WinterSeason::findOrFail($seasonId);
        $outstandingPayments = $this->paymentService->howManyOutstandingPaymentsForSeason($season->id);
        foreach($season->teams as $team) {
            $outstandingPayments += $this->paymentService->howManyOutstandingPaymentsForTeam($season->id, $team->teamId);
        }

        foreach ($season->weeks as $week) {
            foreach ($week->matches as $match) {
                $this->dispatch(new updateMatchStats($match->id));
            }
        }

        return view('manage.season.winter.show', [
            'leagueId' => $leagueId,
            'paymentStillOutStanding' => $outstandingPayments,
            'season' => $season]);
    }

    public function playerExport($leagueId, $seasonId)
    {
        $data = [];
        $season = WinterSeason::findOrFail($seasonId);
        foreach($season->teams as $seasonTeam) {
            $team = $seasonTeam->team;
            foreach ($seasonTeam->teamPlayers as $teamPlayer) {
                $player = $teamPlayer->player;
                if($player != null) {
                    $paymentStatus = $this->paymentService->getPlayerPaymentStatus($seasonId, $player->id);
                    $row = [
                        'Team Name' => $team->name,
                        'Role' => $teamPlayer->role,
                        'First Name' => $player->firstName,
                        'Last Name' => $player->lastName,
                        'Nickname' => $player->nickname,
                        'Shirt Size' => $player->shirtSize,
                        'Email' => $player->email,
                        'Home Phone' => $player->homePhone,
                        'Cell Phone' => $player->cellPhone,
                        'Payment Status' => $paymentStatus
                    ];
                    array_push($data, $row);
                }
            }
        }

        header('Content-Disposition: attachment; filename="playersExport.csv"');
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
}
