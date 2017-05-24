@extends('layouts.master')

@section('content')
<?php
$team = $_GET['tid'];
$dbcnx = mysqli_connect("localhost", "trenton_webuser", "d@rts", "trenton_GTDL");
if (!$dbcnx) {
  echo( "<P>Unable to connect to the " .
        "database server at this time.</P>" );
  exit();
}

$roster= mysqli_query($dbcnx,
 "SELECT
    T_Players.PlayerID, T_Players.PlayerFirst, T_Players.PlayerLast, concat(PlayerFirst,' ',PlayerLast) As FullName, T_Teams.TeamName, T_Teams.SeasonID, T_TeamPlayers.TeamPlayerCaptain,
    if(T_Players.PlayerID=T_Teams.CaptainID, 'Captain', if(T_Players.PlayerID=T_Teams.Captain2ID, 'Co-Captain',' ')) as captlist,
    if(T_Players.PlayerID=T_Teams.CaptainID, 1, if(T_Players.PlayerID=T_Teams.Captain2ID, 2,3)) as sortplayers,
    T_TeamPlayers.TeamPlayerID
From
  T_Players Join T_TeamPlayers ON (T_TeamPlayers.PlayerID = T_Players.PlayerID)
  Join T_Teams ON (T_Teams.TeamID = T_TeamPlayers.TeamID)
  Join T_Seasons ON (T_Teams.SeasonID = T_Seasons.SeasonID)
Where
  T_Seasons.SeasonID = (Select SeasonID from T_Seasons Where Current = '1')
  And T_Teams.TeamID = $team
Order by sortplayers ASC, PlayerLast ASC ");

$theader= mysqli_query( $dbcnx,
 "SELECT TeamName, Div1st, Div2nd,
 if(Div2nd >' ',  concat('Division - ' , Div2nd) ,'Not Determined Yet') as 2ndhalf
  from T_Teams
Where TeamID =$team " );

	if (!$theader) {
  echo("<P>Error performing query: " .
       mysql_error() . "</P>");
  exit();

}

while ( $row = mysqli_fetch_assoc($theader) ) {
?>
<div style="text-align: center">
  <h2><?php echo $row['TeamName']?></h2>
  <h4>1st Half :  Group - <?php echo $row['Div1st']?></h4>
  <h4>2nd Half :  <?php echo $row['2ndhalf']?></h4>
</div>
<?php
}
?>

<table style="width: 70%; margin-left: auto; margin-right: auto;" class="table table-bordered table-condensed">
<thead>
  <tr valign="center">
    <th>Name</th>
    <th>Captain</th>
    <th>Match Results</th>
  </tr>
</thead>
<?php
while ( $row = mysqli_fetch_assoc($roster) ) {
?>
  <tr>
    <td><?php echo $row['FullName']?></td>
    <td align = "center">  <?php echo $row['captlist']?></td>
    <td> <a href= "{{ asset('old/playerresults') }}?pid=<?php echo $row['TeamPlayerID']?>&sid=1 "> Match Results</a></td>
  </tr>
<?php
}
?>
</table>
@stop
