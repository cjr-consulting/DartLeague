<?php

namespace TrentonDarts\LeagueManagement\Services;

use TrentonDarts\LeagueManagement\Models\WinterSeasonTeam;
use TrentonDarts\LeagueManagement\PaymentRepository;

class PaymentService
{
    private $paymentRepository;
    private $teamPayments;
    private $playerPayments;

    public function __construct(PaymentRepository $paymentRepository)
    {
        $this->paymentRepository = $paymentRepository;
    }

    public function howManyOutstandingPaymentsForSeason($seasonId)
    {
        if($this->teamPayments == null)
            $this->teamPayments = $this->paymentRepository->getTeamPayments($seasonId);
        $teams = WinterSeasonTeam::where('seasonId', $seasonId)->get();
        $outstandingPayments = 0;

        foreach($teams as $team) {
            $payment = $this->teamPayments->where('teamId', $team->teamId)->first();
            if($payment == null || $payment->paymentStatus == 'unpaid')
                $outstandingPayments++;
        }

        return $outstandingPayments;
    }

    public function howManyOutstandingPaymentsForTeam($seasonId, $teamId)
    {
        if($this->playerPayments == null)
            $this->playerPayments = $this->paymentRepository->getPlayerPayments($seasonId);
        $team = WinterSeasonTeam::where('seasonId', $seasonId)->where('teamId', $teamId)->first();
        $outstandingPayments = 0;

        foreach($team->teamPlayers as $player)
        {
            $payment = $this->playerPayments->where('playerId', $player->playerId)->first();
            if($payment == null || $payment->paymentStatus == 'unpaid')
                $outstandingPayments++;
        }

        return $outstandingPayments;
    }

    public function getTeamPaymentStatus($seasonId, $teamId)
    {
        if($this->teamPayments == null)
            $this->teamPayments = $this->paymentRepository->getTeamPayments($seasonId);
        $payment = $this->teamPayments->where('teamId', $teamId)->first();
        if($payment == null)
            return 'Unpaid';
        return $payment->paymentStatus;
    }

    public function getPlayerPaymentStatus($seasonId, $playerId)
    {
        if($this->playerPayments == null)
            $this->playerPayments = $this->paymentRepository->getPlayerPayments($seasonId);
        $payment = $this->playerPayments->where('playerId', $playerId)->first();
        if($payment == null)
            return 'Unpaid';
        return $payment->paymentStatus;
    }

    public function saveTeamPayment($seasonId, $teamId, $paymentStatus)
    {
        $this->paymentRepository->updatePayment($seasonId, $teamId, $paymentStatus);
    }

    public function savePlayerPayment($seasonId, $playerId, $paymentStatus)
    {
        $this->paymentRepository->updatePlayerPayment($seasonId, $playerId, $paymentStatus);
    }
}