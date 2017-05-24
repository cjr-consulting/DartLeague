@extends('layouts.master')

@section('content')
<?php
$inc=1;
$cy= $_GET['syear'];
if ($cy <2006 )
 {
  $cy=2014;
 }

$py=$cy-$inc;
$ny=$cy+$inc;
$lastyear=2014;
$firstyear=2009;

if ($cy == $lastyear) { ?>
    <table style="width: 50%;" class="table table-bordered table-condensed" align = "center">
  <tr>
    <td align="center"><a href="{{ asset('old/gtdlhistory') }}?syear=<?php echo $py; ?>"> <<< PREVIOUS SEASON</a> </td>
    <td align="center" size ="5" ><?php echo $cy?> - <?php echo $ny?></td>
  </tr>
</table>
<?php
} elseif ($cy == $firstyear) { ?>
    <table style="width: 50%;" class="table table-bordered table-condensed" align = "center">
  <tr>
    <td align="center" size ="5" ><?php echo $cy?> - <?php echo $ny?></td>
    <td align="center"><a href="{{ asset('old/gtdlhistory') }}syear=<?php echo $ny; ?>"> NEXT SEASON>>> </a> </td>
  </tr>
</table>
<?php
} else { ?>
    <table style="width: 50%;" class="table table-bordered table-condensed" align = "center">
  <tr>
    <td align="center"><a href="{{ asset('old/gtdlhistory') }}?syear=<?php echo $py; ?>"> <<< PREVIOUS SEASON</a> </td>
    <td align="center" size ="5" ><?php echo $cy?> - <?php echo $ny?></td>
    <td align="center"><a href="{{ asset('old/gtdlhistory') }}?syear=<?php echo $ny; ?>"> NEXT SEASON>>> </a> </td>
  </tr>
</table>
<?php
}
?>

<h1 align="center">
<?php echo $cy ?> - <?php echo $ny ?>  Greater Trenton Dart League <br/> Season Recap
</h1>

<?php
$dbcnx = mysqli_connect("localhost", "trenton_webuser", "d@rts", "trenton_GTDL");

if (!$dbcnx) {
  echo( "<P>Unable to connect to the " .
        "database server at this time.</P>" );
  exit();
}

$doclist= mysqli_query($dbcnx,
 " SELECT * ,T_Seasons.SeasonName, T_Seasons.SeasonStart
FROM `T_Links`
Join T_Seasons on T_Links.SeasonID=T_Seasons.SeasonID
Where T_Seasons.SeasonStart=$cy ") ;

	if (!$doclist) {
  echo("<P>Error performing query: " .
       mysql_error() . "</P>");
  exit();
}
  ?>
<br/>

<table align="center" style="width: 50%;" class="table table-bordered table-condensed">
<thead>
<tr valign="center">
  <th style="text-align: center;">Documents and attachments for this year are below</th>
</tr>
</thead>
<tbody>
<?php
while ( $row = mysqli_fetch_assoc($doclist) ) {
 ?>
 <tr>
   <td align="center"><a href= <?php echo $row['DocURL']?> target="_blank"> <?php echo $row['DocName'] ?> </a> </td>
 </tr>
<?php
}
?>
</tbody>
</table>

<?php
$playoffs= mysqli_query($dbcnx,
 "SELECT T_Seasons.SeasonName, T_Teams.TeamName, T_Teams.Div2nd, T_Teams.Place, T_Teams.Picture, concat('Captain: ', T_Players.PlayerFirst,' ',T_Players.PlayerLast) As FullName ,'1' AS St
FROM `T_Seasons`
Join T_Teams On T_Seasons.SeasonID=T_Teams.SeasonID
Join T_Players ON T_Teams.CaptainID = T_Players.PlayerID
WHERE T_Seasons.SeasonStart=$cy
And T_Teams.Place>0
UNION
SELECT T_Seasons.SeasonName, T_Teams.TeamName, 'Memorial Tournament' As Div2nd, T_Teams.Memorial AS Place, T_Teams.MemorialPic, concat('Captain: ', T_Players.PlayerFirst,' ',T_Players.PlayerLast) As FullName ,'2' AS St
FROM `T_Seasons`
Join T_Teams On T_Seasons.SeasonID=T_Teams.SeasonID
Join T_Players ON T_Teams.CaptainID = T_Players.PlayerID
WHERE T_Seasons.SeasonStart=$cy
And T_Teams.Memorial>0
UNION
SELECT T_Seasons.SeasonName, T_Teams.TeamName,'Mr. Trenton' as Div2nd, T_TeamPlayers.Mrtrenton as Place, T_TeamPlayers.Mrtrentonpic, concat( T_Players.PlayerFirst,' ',T_Players.PlayerLast) As FullName,'3' AS St
FROM `T_TeamPlayers`
JOIN T_Players ON T_TeamPlayers.PlayerID = T_Players.PlayerID
JOIN T_Teams ON T_TeamPlayers.TeamID = T_Teams.TeamID
JOIN T_Seasons ON T_Teams.SeasonID = T_Seasons.SeasonID

WHERE T_TeamPlayers.Mrtrenton>0
And T_Seasons.SeasonStart = $cy

Order by St, Div2nd ASC, Place ASC " );

	if (!$playoffs) {
  echo("<P>Error performing query: " .
       mysql_error() . "</P>");
  exit();
}
?>

<table class="table table-bordered table-condensed">
<tbody>
<tr valign="center">
<th colspan="4">Playoff Results and Images for <?php echo $cy?> -  <?php echo $ny?></th> </tr>
<tr valign="center">
<th scope="col">
<p>Team Name</p></th>
<th scope="col">
<p>Division</p></th>
<th scope="col">
<p>Place</p></th>
<th scope="col">
<p>Image</p></th>

</tr></tbody>

<?php
while ( $row = mysqli_fetch_assoc($playoffs) ) {
?>
 	<tr>
 		<td><?php echo $row['TeamName']?><br/><?php echo $row['FullName']?> </td>
 		<td><?php echo $row['Div2nd']?></td>
 		<td><?php echo $row['Place']?></td>
 		<td><img border="0" src="  <?php echo $row['Picture']?> "width="400" height="300"> </td>
  </tr>
<?php
}
?>

</table>
@stop
