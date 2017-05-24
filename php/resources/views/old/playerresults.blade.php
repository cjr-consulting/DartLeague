@extends('layouts.master')

@section('content')
<?php
$player= $_GET['pid'];
$sort = $_GET['sid'];
$sp ="       ";
switch ($sort) {
   case 1:
         $sort = "Week ASC, GameNumber ASC";
         break;
   case 2:
         $sort = "gtype ASC, Solo ASC, GameType ASC, Week ASC, GameNumber ASC";
         break;
   case 3:
         $sort = "solo ASC, gtype ASC, GameType ASC, Week ASC, GameNumber ASC";
         break;
}

$dbcnx = mysqli_connect("localhost", "trenton_webuser", "d@rts", "trenton_GTDL");

if (!$dbcnx) {
  echo( "<P>Unable to connect to the " .
        "database server at this time.</P>" );
  exit();
}

$pheader= mysqli_query($dbcnx,
 "SELECT concat (PlayerFirst,' ',PlayerLast) as PlayerName, T_Teams.TeamName, T_Teams.TeamID
from T_Players, T_TeamPlayers, T_Teams
Where T_Players.PlayerID=T_TeamPlayers.PlayerID
and T_TeamPlayers.TeamID = T_Teams.TeamID
and T_TeamPlayers.TeamPlayerID= $player ");

	if (!$pheader) {
  echo("<P>Error performing query: " .
       mysql_error() . "</P>");
  exit();

}

$row = mysqli_fetch_assoc($pheader);
?>
<div style="text-align: center;">
  <h1><?php echo $row['PlayerName']?><br></h1>
  <h3><a href= "{{ asset('old/rosters') }}?tid=<?php echo $row['TeamID']?>&sid=1 "> <?php echo $row['TeamName']?></a></h3>
</div>
<?php

$plist= mysqli_query($dbcnx,
 "select itable.*, concat (p2.PlayerFirst,' ', p2.PlayerLast) as Opp1, md.teamplayerID as Opp1ID, '' as Opp2 ,'' as Opp2ID, '' as Partner1, '' as Partner1ID
From T_MatchDetails md, T_Players p2 , T_TeamPlayers tp2,
(SELECT concat (PlayerFirst,' ', PlayerLast) PlayerFull,  If(Win = 'TRUE', 'Win','Loss') As WStatus, Win,T_Seasons.SeasonID, concat ('H',left(WeekHalf,1), 'W',  WeekNumber,' - ',Date_Format(WeekDate,'%c-%d-%y')) as week, concat(GameNumber,' - ', GameType) as game, HT.TeamID hid, AT.TeamID aid, PT.TeamID as pid,
if(AT.TeamID = PT.TeamID, concat ('at ', HT.TeamName),concat('vs. ',AT.TeamName)) AS vsmatch,

if(AT.TeamID = PT.TeamID,HT.TeamID, AT.TeamID) AS oID,GameType,
if(T_GameTypes.GameTypeID = '1','Cricket',if(T_GameTypes.GameTypeID = '3','Cricket','x01')) as gtype,
if(T_GameTypes.GameTypeID = '1','Singles',if(T_GameTypes.GameTypeID = '2','Singles','Team')) as solo, T_MatchDetails.teamplayerID, T_MatchDetails.matchid, T_MatchDetails.gamenumber, T_MatchDetails.HorA,
T_TeamPlayers.TeamPlayerID as tp, T_MatchDetails.GameTypeID, T_MatchDetails.GamePlayerPos
FROM `T_MatchDetails`
Join T_TeamPlayers ON (T_MatchDetails.TeamPlayerID = T_TeamPlayers.TeamPlayerID)
Join T_Players ON (T_TeamPlayers.PlayerID = T_Players.PlayerID)
Join T_GameTypes ON (T_MatchDetails.GameTypeID = T_GameTypes.GameTypeID)
Join T_Matches ON (T_MatchDetails.MatchID= T_Matches.MatchID)
Join T_Teams as HT ON (T_Matches.HomeTeam=HT.TeamID)
Join T_Teams as AT ON (T_Matches.AwayTeam=AT.TeamID)
Join T_Teams as PT ON (T_TeamPlayers.TeamID = PT.TeamID)
Join T_Weeks ON (T_Matches.WeekID = T_Weeks.WeekID)
Join T_Seasons ON (T_Seasons.SeasonID=T_Weeks.SeasonID)
WHERE T_TeamPlayers.TeamPlayerID = $player
And T_Seasons.SeasonID = (Select SeasonID from T_Seasons Where Current = '1') ) As itable

Where  (md.matchid = itable.matchid and md.gamenumber = itable.gamenumber and md.teamplayerid <> itable.teamplayerid and md.HorA <> itable.HorA)
and md.teamplayerid = tp2.teamplayerid
and tp2.playerID = p2.playerID
and itable.solo = 'Singles'

UNION



SELECT concat (T_Players.PlayerFirst,' ', T_Players.PlayerLast) PlayerFull,  If(T_MatchDetails.Win = 'TRUE', 'Win','Loss') As WStatus, T_MatchDetails.Win,T_Seasons.SeasonID, concat ('H',left(WeekHalf,1), 'W',  WeekNumber,' - ',Date_Format(WeekDate,'%c-%d-%y')) as week, concat(T_MatchDetails.GameNumber,' - ', GameType) as game, HT.TeamID, AT. TeamID, PT.TeamID,
if(AT.TeamID = PT.TeamID, concat ('at ', HT.TeamName),concat('vs. ',AT.TeamName)) AS vsmatch,

if(AT.TeamID = PT.TeamID,HT.TeamID, AT.TeamID) AS oID,GameType,
if(T_GameTypes.GameTypeID = '1','Cricket',if(T_GameTypes.GameTypeID = '3','Cricket','x01')) as gtype,
if(T_GameTypes.GameTypeID = '1','Singles',if(T_GameTypes.GameTypeID = '2','Singles','Team')) as solo,T_MatchDetails.TeamPlayerID, T_MatchDetails.MatchID, T_MatchDetails.GameNumber, T_MatchDetails.HorA, T_MatchDetails.TeamPlayerID, T_MatchDetails.GameTypeID, T_MatchDetails.GamePlayerPos,
concat (O1.PlayerFirst,' ', O1.PlayerLast) AS Opp1,MD2.TeamPlayerID as Opp1ID,  concat (O2.PlayerFirst,' ', O2.PlayerLast) AS Opp2, MD3.TeamPlayerID as Opp2ID, concat (P1.PlayerFirst,' ', P1.PlayerLast) AS Partner1, MD4.TeamPlayerID as Partner1ID

FROM `T_MatchDetails`

Join T_TeamPlayers ON (T_MatchDetails.TeamPlayerID = T_TeamPlayers.TeamPlayerID)
Join T_Players ON (T_TeamPlayers.PlayerID = T_Players.PlayerID)
Join T_GameTypes ON (T_MatchDetails.GameTypeID = T_GameTypes.GameTypeID)
Join T_Matches ON (T_MatchDetails.MatchID= T_Matches.MatchID)
Join T_Teams as HT ON (T_Matches.HomeTeam=HT.TeamID)
Join T_Teams as AT ON (T_Matches.AwayTeam=AT.TeamID)
Join T_Teams as PT ON (T_TeamPlayers.TeamID = PT.TeamID)
Join T_Weeks ON (T_Matches.WeekID = T_Weeks.WeekID)
Join T_Seasons ON (T_Seasons.SeasonID=T_Weeks.SeasonID)
Join T_MatchDetails as MD2 ON (T_MatchDetails.MatchID = MD2.MatchID AND T_MatchDetails.GameNumber = MD2.GameNumber and T_MatchDetails.GamePlayerPos = MD2.GamePlayerPos and T_MatchDetails.TeamPlayerID <> MD2.TeamPlayerID)
Join T_TeamPlayers as TPO1 ON (MD2.TeamPlayerID = TPO1.TeamPlayerID )
Join T_Players AS O1 ON ( TPO1.PlayerID = O1.PlayerID)
Join T_MatchDetails as MD3 ON (T_MatchDetails.MatchID = MD3.MatchID AND T_MatchDetails.GameNumber = MD3.GameNumber and T_MatchDetails.GamePlayerPos <> MD3.GamePlayerPos and T_MatchDetails.TeamPlayerID <> MD3.TeamPlayerID AND T_MatchDetails.HorA <> MD3.HorA)
Join T_TeamPlayers as TPO2 ON (MD3.TeamPlayerID = TPO2.TeamPlayerID )
Join T_Players AS O2 ON ( TPO2.PlayerID = O2.PlayerID)

Join T_MatchDetails as MD4 ON (T_MatchDetails.MatchID = MD4.MatchID AND T_MatchDetails.GameNumber = MD4.GameNumber and T_MatchDetails.GamePlayerPos <> MD4.GamePlayerPos and T_MatchDetails.TeamPlayerID <> MD4.TeamPlayerID AND T_MatchDetails.HorA =MD4.HorA)
Join T_TeamPlayers as TPP1 ON (MD4.TeamPlayerID = TPP1.TeamPlayerID )
Join T_Players AS P1 ON ( TPP1.PlayerID = P1.PlayerID)


WHERE T_TeamPlayers.TeamPlayerID = $player
And T_Seasons.SeasonID = (Select SeasonID from T_Seasons Where Current = '1')
And T_MatchDetails.GameTypeID IN (3,4)

UNION

select itable.*, 'Triple Game' as Opp1, md.teamplayerID as Opp1ID, '' as Opp2 ,'' as Opp2ID, '' as Partner1, '' as Partner1ID
From T_MatchDetails md, T_Players p2 , T_TeamPlayers tp2,
(SELECT concat (PlayerFirst,' ', PlayerLast) PlayerFull,  If(Win = 'TRUE', 'Win','Loss') As WStatus, Win,T_Seasons.SeasonID, concat ('H',left(WeekHalf,1), 'W',  WeekNumber,' - ',Date_Format(WeekDate,'%c-%d-%y')) as week, concat(GameNumber,' - ', GameType) as game, HT.TeamID hid, AT.TeamID aid, PT.TeamID as pid,
if(AT.TeamID = PT.TeamID, concat ('at ', HT.TeamName),concat('vs. ',AT.TeamName)) AS vsmatch,

if(AT.TeamID = PT.TeamID,HT.TeamID, AT.TeamID) AS oID,GameType,
if(T_GameTypes.GameTypeID = '1','Cricket',if(T_GameTypes.GameTypeID = '3','Cricket','x01')) as gtype,
if(T_GameTypes.GameTypeID = '1','Singles',if(T_GameTypes.GameTypeID = '2','Singles','Team')) as solo, T_MatchDetails.teamplayerID, T_MatchDetails.matchid, T_MatchDetails.gamenumber, T_MatchDetails.HorA,
T_TeamPlayers.TeamPlayerID as tp, T_MatchDetails.GameTypeID, T_MatchDetails.GamePlayerPos
FROM `T_MatchDetails`
Join T_TeamPlayers ON (T_MatchDetails.TeamPlayerID = T_TeamPlayers.TeamPlayerID)
Join T_Players ON (T_TeamPlayers.PlayerID = T_Players.PlayerID)
Join T_GameTypes ON (T_MatchDetails.GameTypeID = T_GameTypes.GameTypeID)
Join T_Matches ON (T_MatchDetails.MatchID= T_Matches.MatchID)
Join T_Teams as HT ON (T_Matches.HomeTeam=HT.TeamID)
Join T_Teams as AT ON (T_Matches.AwayTeam=AT.TeamID)
Join T_Teams as PT ON (T_TeamPlayers.TeamID = PT.TeamID)
Join T_Weeks ON (T_Matches.WeekID = T_Weeks.WeekID)
Join T_Seasons ON (T_Seasons.SeasonID=T_Weeks.SeasonID)
WHERE T_TeamPlayers.TeamPlayerID = $player
And T_Seasons.SeasonID = (Select SeasonID from T_Seasons Where Current = '1') ) As itable

Where  (md.matchid = itable.matchid and md.gamenumber = itable.gamenumber and md.teamplayerid <> itable.teamplayerid and md.HorA <> itable.HorA and md.GamePlayerPos = itable.GamePlayerPos)
and md.teamplayerid = tp2.teamplayerid
and tp2.playerID = p2.playerID
and itable.GameTypeID= '5'

Order by $sort " );

	if (!$plist) {
    echo("<P>Error performing query: ".mysql_error()."</P>");
       exit();
  }
?>
<table class="table table-bordered table-condensed">
<thead>
<tr valign="center">
  <th scope="col"><a href= "{{ asset('old/playerresults') }}?pid=<?php echo $player?>&sid=1 "> Sort on Week</a></p></th>
  <th scope="col">Game (sort on:)<br/> <a href= "{{ asset('old/playerresults') }}?pid=<?php echo $player?>&sid=2 ">Type </a> or <a href= "{{ asset('old/playerresults') }}?pid=<?php echo $player?>&sid=3 ">Players</a></p></th>
  <th scope="col">Against Team</th>
  <th scope="col">Partner</th>
  <th scope="col">Result</th>
  <th scope="col">Opponent</th>
</tr>
</thead>
<?php
while ( $row = mysqli_fetch_assoc($plist) ) {
?>
 		<tr>
 		    <td><?php echo $row['week']?></td>
        <td><?php echo $row['game']?></td>
        <td><a href= "{{ asset('old/rosters') }}?tid=<?php echo $row['oID']?>&sid=1 "> <?php echo $row['vsmatch']?></td>
        <td><a href= "{{ asset('old/playerresults') }}?pid=<?php echo $row['Partner1ID']?>&sid=1 "> <?php echo $row['Partner1']?></a></td>
        <td><?php echo $row['WStatus']?></td>

   <?php
if ($row['GameTypeID'] <3 )
{ ?>
        <td> <a href= "{{ asset('old/playerresults') }}?pid=<?php echo $row['Opp1ID']?>&sid=1 "> <?php echo $row['Opp1']?></a></td>
<?php
}
  elseif ($row['GameTypeID'] <5 )
{ ?>
        <td> <a href= "{{ asset('old/playerresults') }}?pid=<?php echo $row['Opp1ID']?>&sid=1 "> <?php echo $row['Opp1']?></a> <br> <a href= "{{ asset('old/playerresults') }}?pid=<?php echo $row['Opp2ID']?>&sid=1 "> <?php echo $row['Opp2']?></a></td>
<?php
}
else
{
?>
        <td><?php echo $row['Opp1']?></td>
<?php
}
?>
  </tr>
<?php
}
?>
</table>
@stop
