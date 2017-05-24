@extends('layouts.master')

@section('content')
<?php

$league= $_GET['lid'];

$dbcnx = mysqli_connect("localhost", "trenton_webuser", "d@rts", "trenton_GTDL");

if (!$dbcnx) {
  echo( "<P>Unable to connect to the " .
        "database server at this time.</P>" );
  exit();
}

$leaguename= mysqli_query($dbcnx,
 "SELECT *, T_LeagueDetails.LeagueID from T_Leagues
Left Join T_LeagueDetails ON (T_LeagueDetails.LeagueID=T_Leagues.LeagueID)
Where T_Leagues.LeagueID=$league" );

	if (!$leaguename) {
  echo("<P>Error performing query: " .
       mysql_error() . "</P>");
  exit();
}
  ?>
<?php
while ( $row = mysqli_fetch_assoc($leaguename) ) {
?>
<h1 style="text-align:center;"><?php echo $row['LeagueName']?></h1>
<?php
if ($row['LeagueID'] !== "" )
{

$leaguedetails= mysqli_query($dbcnx,
 "SELECT * from T_LeagueDetails
Join (T_Leagues) on (T_Leagues.LeagueID=T_LeagueDetails.LeagueID)
Where T_Leagues.LeagueID=$league
Order by LeagueName ASC" );

	if (!$leaguedetails) {
    echo("<P>Error performing query: ".mysql_error()."</P>");
    exit();
  }
?>
<table style="width: 90%; margin-left: auto; margin-right: auto;" class="table table-bordered table-condensed">
<thead>
<tr valign="center">
  <th scope="col"><p>League</p></th>
  <th scope="col"><p>Night<br/>Played</p></th>
  <th scope="col"><p>Singles<br/>League</p></th>
  <th scope="col"><p>Summer<br/>League</p></th>
  <th scope="col"><p>Weekly<br/>LOD</p></th>
  <th scope="col"><p>Info</p></th>
</tr>
</thead>

<?php
while ( $row = mysqli_fetch_assoc($leaguedetails) ) {
?>
  <tr>
	  <td><?php echo $row['LeagueName']?></td>
    <td><?php echo $row['NightPlayed']?></td>
    <td><?php echo $row['SinglesLeague']?></td>
    <td><?php echo $row['SummerLeague']?></td>
    <td><?php echo $row['Luck']?></td>
    <td><?php echo $row['LuckInfo']?></td>
  </tr>
<?php
}
?>
</table>

<?php
} else {
?>
No Information Provided <br>
 <?php
}
}
?>
<div style="text-align: center;">
Do you have information about the above dart league?  If so <br/>
<a href="mailto:webmaster@trentondarts.com">Contact the GTDL webmaster</a><br />
</div>
@stop
