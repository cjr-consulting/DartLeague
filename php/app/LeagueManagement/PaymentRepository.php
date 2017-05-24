<?php

namespace TrentonDarts\LeagueManagement;


use TrentonDarts\LeagueManagement\Models\WinterSeasonPlayerPayment;
use TrentonDarts\LeagueManagement\Models\WinterSeasonTeamPayment;

class PaymentRepository
{
    public function getTeamPayments($seasonId)
    {
        return WinterSeasonTeamPayment::where('seasonId', $seasonId)->get();
    }

    public function updatePayment($seasonId, $teamId, $paymentStatus) {
        $payment = WinterSeasonTeamPayment::where('seasonId', $seasonId) ->where('teamId', $teamId)->first();
        if($payment == null) {
            $payment = WinterSeasonTeamPayment::create(['seasonId'=>$seasonId, 'teamId' => $teamId, 'paymentStatus' => $paymentStatus]);
        }

        $payment->paymentStatus = $paymentStatus;

        $payment->save();
    }

    public function getPlayerPayments($seasonId)
    {
        return WinterSeasonPlayerPayment::where('seasonId', $seasonId)->get();
    }

    public function updatePlayerPayment($seasonId, $playerId, $paymentStatus)
    {
        $payment = WinterSeasonPlayerPayment::where('seasonId', $seasonId)->where('playerId', $playerId)->first();
        if($payment == null)
            $payment = WinterSeasonPlayerPayment::create(['seasonId'=>$seasonId, 'playerId' => $playerId, 'paymentStatus' => $paymentStatus]);

        $payment->paymentStatus = $paymentStatus;

        $payment->save();
    }
}