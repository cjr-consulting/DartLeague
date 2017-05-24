<?php

namespace TrentonDarts\MatchDomain;

use Illuminate\Support\Facades\DB;
use TrentonDarts\LeagueManagement\Models\Player;
use TrentonDarts\LeagueManagement\Models\WinterSeason;
use TrentonDarts\LeagueManagement\Models\WinterSeasonMatch;
use TrentonDarts\MatchDomain\Models\GameAward;
use TrentonDarts\MatchDomain\Models\GamePlayer;
use TrentonDarts\MatchDomain\Models\GameResultSnapshot;
use TrentonDarts\MatchDomain\Models\GameRules;
use TrentonDarts\MatchDomain\Models\MatchResult;
use TrentonDarts\MatchDomain\Models\MatchResultSnapshot;
use TrentonDarts\MatchDomain\Models\MatchRules;

class MatchRepository
{
    static function getMatchRulesFromId($matchTypeId)
    {
        $matchResult = DB::select("select * from match_types where id = :id", ['id' => $matchTypeId]);

        $matchRules = new MatchRules();
        $matchRules->id = $matchResult[0]->id;

        $gameRuleResults = DB::select("select * from match_type_game_rules where matchTypeId = :matchTypeId order by orderId", ['matchTypeId' => $matchTypeId]);
        foreach($gameRuleResults as $gameRule){
            $g = new GameRules();
            $g->id = $gameRule->id;
            $g->gameType = $gameRule->gameType;
            $g->doubleIn = $gameRule->doubleIn;
            $g->doubleOut = $gameRule->doubleOut;
            $g->orderId = $gameRule->orderId;
            $g->bestOfNumberOfLegs = $gameRule->bestOfNumberOfLegs;
            $g->numberOfLegs = $gameRule->numberOfLegs;
            $g->whoStarts = $gameRule->whoStarts;
            $g->numberOfPlayers = $gameRule->numberOfPlayers;
            $g->gamePointValue = $gameRule->gamePointValue;
            $g->legPointValue = $gameRule->legPointValue;
            $g->forfeitIfNoPlayers = $gameRule->forfeitIfNoPlayers;
            $g->groupName = $gameRule->groupName;
            array_push($matchRules->gameRules, $g);
        }

        return $matchRules;
    }

    public static function getMatchResultsFromMatch($match)
    {
        $players = self::getPlayers();
        $matchDbResults = DB::select("select * from winter_match_results where id = :matchId", ['matchId'=>$match->id]);
        $season = WinterSeason::findOrFail($match->seasonId);
        $matchRules = self::getMatchRulesFromId($match->matchTypeId);
        $matchRules->isUsingMatchPoints = $season->isUsingMatchPoints;
        $matchRules->winPoints = $season->winPoints;
        $matchRules->halfPoints = $season->halfPoints;
        $matchRules->minPointForHalfPoints = $season->minPointForHalfPoints;
        $gameResults = DB::select("select winter_game_results.* from winter_game_results join match_type_game_rules on winter_game_results.gameRuleId = match_type_game_rules.id where matchId = :matchId order by match_type_game_rules.orderId", ['matchId'=>$match->id]);
        $snapshot = new MatchResultSnapshot();
        $week = $match->week;

        $snapshot->matchId = $match->id;
        $snapshot->seasonId = $match->seasonId;
        $snapshot->division = $match->division;
        $snapshot->date  = $week->date;
        $snapshot->seasonPart = $week->weekType;
        $snapshot->awayTeamId = $match->awayTeamId;
        $snapshot->awayTeamName = $match->awayTeam->name;
        $snapshot->homeTeamId = $match->homeTeamId;
        $snapshot->homeTeamName = $match->homeTeam->name;

        if(!empty($matchDbResults)) {
            $matchDbResult = $matchDbResults[0];
            $snapshot->hasScorecard = $matchDbResult->hasScorecard;
            $snapshot->awayScoreOverride = $matchDbResult->awayScoreOverride;
            $snapshot->homeScoreOverride = $matchDbResult->homeScoreOverride;
        }

        foreach($gameResults as $gameResult)
        {
            $g = new GameResultSnapshot();
            $g->id = $gameResult->id;
            $g->gameRules = self::findGameResultById($matchRules->gameRules, $gameResult->gameRuleId);
            $g->awayPlayers = self::getPlayersFromIds($players, $gameResult->awayPlayers);
            $g->homePlayers = self::getPlayersFromIds($players, $gameResult->homePlayers);
            $g->forfeitedBy = $gameResult->forfeitedBy;
            $g->legs = explode(';', $gameResult->legs);
            $g->awards = self::getGameAwards($gameResult->id, $players);
            array_push($snapshot->gameResults, $g);
        }

        $matchResult = new MatchResult();
        $matchResult->loadSnapshot($snapshot);
        $matchResult->loadRules($matchRules);
        return $matchResult;
    }

    private static function findGameResultById($gameRules, $gameRuleId)
    {
        foreach($gameRules as $gameRule)
        {
            if($gameRule->id == $gameRuleId) return $gameRule;
        }
    }

    private static function getPlayers()
    {
        return Player::all();
    }

    private static function getPlayersFromIds($players, $selectedPlayers)
    {
        if($selectedPlayers == null)
            return [];
        $selectedPlayerIds = explode(';', $selectedPlayers);
        $gamePlayers = [];
        foreach($selectedPlayerIds as $id)
        {
            array_push($gamePlayers, self::getPlayerFromId($players, $id));
        }

        return $gamePlayers;
    }

    private static function getPlayerFromId($players, $id)
    {
        foreach($players as $player)
        {
            if($player->id == $id) {
                $gamePlayer = new GamePlayer();
                $gamePlayer->id = $player->id;
                $gamePlayer->name = $player->name;
                return $gamePlayer;
            }
        }
    }

    public static function saveMatchResultsData($matchId, $data)
    {
        $match = WinterSeasonMatch::findOrFail($matchId);
        $matchResults = self::getMatchResultsFromMatch($match);
        $games = $matchResults->getGames();
        if($data != null) {
            $matchResults->setHasScorecard($data['hasScorecard']);

            if (!$matchResults->getHasScorecard()) {
                $matchResults->setAwayScoreOverride($data['awayScoreOverride']);
                $matchResults->setHomeScoreOverride($data['homeScoreOverride']);
            }
        }

        foreach($data['gameGroups'] as $group)
        {
            foreach($group['games'] as $game) {
                $gameResult = self::getGameResultByRuleId($games, $game['id']);
                if (!empty($game['awayPlayer']))
                    $gameResult->setAwayPlayer(self::getGamePlayer($game['awayPlayer']), 0);
                else
                    $gameResult->removeAwayPlayerAtPosition(0);

                if (!empty($game['awayPlayer2']))
                    $gameResult->setAwayPlayer(self::getGamePlayer($game['awayPlayer2']), 1);
                else
                    $gameResult->removeAwayPlayerAtPosition(1);

                if (!empty($game['awayPlayer3']))
                    $gameResult->setAwayPlayer(self::getGamePlayer($game['awayPlayer3']), 2);
                else
                    $gameResult->removeAwayPlayerAtPosition(2);

                if (!empty($game['homePlayer']))
                    $gameResult->setHomePlayer(self::getGamePlayer($game['homePlayer']), 0);
                else
                    $gameResult->removeHomePlayerAtPosition(0);

                if (!empty($game['homePlayer2']))
                    $gameResult->setHomePlayer(self::getGamePlayer($game['homePlayer2']), 1);
                else
                    $gameResult->removeHomePlayerAtPosition(1);

                if (!empty($game['homePlayer3']))
                    $gameResult->setHomePlayer(self::getGamePlayer($game['homePlayer3']), 2);
                else
                    $gameResult->removeHomePlayerAtPosition(2);

                $gameResult->forfeitedBy = '';
                if (count($gameResult->homePlayers) == 0 && count($gameResult->awayPlayers) == 0) {
                    $gameResult->forfeitedBy = '';
                } else if (count($gameResult->homePlayers) == 0) {
                    $gameResult->forfeitedBy = 'home';
                } else if (count($gameResult->awayPlayers) == 0) {
                    $gameResult->forfeitedBy = 'away';
                }

                $legs = [];
                if($game['winner'] != NULL)
                    array_push($legs, $game['winner']);
                $gameResult->setLegs($legs);

                $newAwards = self::convertGameAwards($game['awards']);

                foreach($gameResult->awards as $a)
                {
                    $found = self::findAwardById($newAwards, $a->id);
                    if(!$found)
                    {
                        $deletedItem = self::findAwardById($gameResult->awards, $a->id);
                        if ($deletedItem) {
                            unset($gameResult->awards[$deletedItem->key]);
                        }
                    }
                }

                foreach($newAwards as $award)
                {
                    if($award->id <= 0)
                        array_push($gameResult->awards, $award);
                }
            }
        }

        self::saveMatchResults($matchResults);
    }

    private static function saveMatchResults($matchResults)
    {
        $snapshot = $matchResults->getSnapshot();

        self::updateMatchResults($snapshot);
        foreach ($snapshot->gameResults as $gameResult) {
            self::updateGameResults($gameResult, $snapshot->matchId);
        }
    }

    private static function updateMatchResults($snapshot)
    {
        $matchResult = DB::table("winter_match_results")
            ->where('id', $snapshot->matchId)->first();

        if($matchResult == null){
            DB::table('winter_match_results')
                ->insert([
                    'id'=> $snapshot->matchId,
                    'hasScorecard' => $snapshot->hasScorecard,
                    'awayScoreOverride' => $snapshot->awayScoreOverride,
                    'homeScoreOverride' => $snapshot->homeScoreOverride,
                    'created_at' => DB::raw('now()')
                ]);
        } else {
            DB::table('winter_match_results')
                ->where(['id' => $snapshot->matchId])
                ->update([
                    'hasScorecard' => $snapshot->hasScorecard,
                    'awayScoreOverride' => $snapshot->awayScoreOverride,
                    'homeScoreOverride' => $snapshot->homeScoreOverride,
                    'updated_at' => DB::raw('now()')
                ]);
        }
    }

    private static function updateGameResults($gameResult, $matchId)
    {
        $gameId = $gameResult->id;
        if($gameResult->id > 0){
            DB::table('winter_game_results')
                ->where(['id' => $gameResult->id])
                ->update([
                    'matchId' => $matchId,
                    'homePlayers' => self::convertPlayers($gameResult->homePlayers),
                    'awayPlayers' => self::convertPlayers($gameResult->awayPlayers),
                    'legs' => implode(';', $gameResult->legs),
                    'forfeitedBy' => is_null($gameResult->forfeitedBy) ? '' : $gameResult->forfeitedBy,
                    'gameRuleId' => $gameResult->gameRules->id,
                    'updated_at' => DB::raw('now()')
                ]);
        }
        else {
            $gameId = DB::table('winter_game_results')->insertGetId([
                'matchId' => $matchId,
                'homePlayers' => self::convertPlayers($gameResult->homePlayers),
                'awayPlayers' => self::convertPlayers($gameResult->awayPlayers),
                'legs' => implode(';', $gameResult->legs),
                'forfeitedBy' => is_null($gameResult->forfeitedBy) ? '' : $gameResult->forfeitedBy,
                'gameRuleId' => $gameResult->gameRules->id,
                'created_at' => DB::raw('now()'),
                'updated_at' => DB::raw('now()')
            ]);
        }

        self::updateGameAwards($gameId, $gameResult->awards);
    }

    private static function convertPlayers($players)
    {
        $pids = [];
        foreach($players as $player)
        {
            array_push($pids, $player->id);
        }

        return implode(';', $pids);
    }

    private static function getGameResultByRuleId($games, $id)
    {
        foreach($games as $game)
            if($game->gameRules->id == $id)
                return $game;
    }

    private static function getGamePlayer($playerData)
    {
        if(empty($playerData))
            return null;
        $player = new GamePlayer();
        $player->id = $playerData['id'];
        $player->name = $playerData['name'];
        return $player;
    }

    /**
     * @param $gameId
     * @param $awards
     */
    private static function updateGameAwards($gameId, $awards)
    {
        $gameAwards = DB::select("select * from winter_game_awards where gameId = :gameId", ['gameId' => $gameId]);

        foreach($gameAwards as $a)
        {
            $foundAward = self::findAwardById($awards, $a->id);
            if(!$foundAward) {
                DB::table("winter_game_awards")->where("id", $a->id)->delete();
            }
        }

        foreach($awards as $award)
        {
            if($award->id > 0)
                continue;
            $award->id = DB::table('winter_game_awards')->insertGetId([
                'gameId' => $gameId,
                'playerId' => $award->player->id,
                'awardType' => $award->awardType,
                'value' => $award->value,
                'created_at' => DB::raw('now()'),
                'updated_at' => DB::raw('now()')
            ]);
        }
    }

    private static function findAwardById($awards, $id)
    {
        foreach($awards as $key => $a){
            if($a->id == $id){
                $a->key = $key;
                return $a;
            }
        }

        return null;
    }

    private static function getGameAwards($id, $players)
    {
        $awards = [];
        $gameAwards = DB::select("select * from winter_game_awards where gameId = :gameId", ['gameId' => $id]);
        foreach($gameAwards as $a)
        {
            $award = new GameAward();
            $award->id = $a->id;
            $award->gameId = $id;
            $award->player = self::getGamePlayer($players->where('id', $a->playerId)->first());
            $award->awardType = $a->awardType;
            $award->value = $a->value;
            array_push($awards, $award);
        }

        return $awards;
    }

    private static function convertGameAwards($awards)
    {
        $newAwards = [];
        foreach($awards as $award)
        {
            $newAward = new GameAward();
            $newAward->id = $award['id'];
            $newAward->gameId = $award['gameId'];
            $newAward->awardType = $award['awardType'];
            $newAward->value = $award['awardValue'];
            $player = new GamePlayer();
            $player->id = $award['player']['id'];
            $player->name = $award['player']['name'];
            $newAward->player = $player;
            array_push($newAwards, $newAward);
        }

        return $newAwards;
    }
}
