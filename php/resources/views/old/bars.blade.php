@extends('layouts.master')

@section('content')
<h1 style="text-align: center">2014-2015 Greater Trenton Dart League</h1>
<h2 style="text-align: center"><a href="https://maps.google.com/maps/ms?msid=213093344398242645697.0004d758955d29604ecbf&msa=0&ll=40.212047,-74.718361&spn=0.090191,0.168743">GTDL Sponsor Map - Find us here</a></h2>
<div style="text-align: center"><a href="https://mapsengine.google.com/map/edit?mid=zLyNyjI06bP0.kk7uxNW5mwRA">NJ Dart League Map - Looking for somewhere else to play?</a></div>
<?php
  $dbcnx = mysqli_connect("localhost", "trenton_webuser", "d@rts", "trenton_GTDL");

  if (!$dbcnx) {
    echo( "<P>Unable to connect to the " .
          "database server at this time.</P>" );
    exit();
  }

  $teamlist = mysqli_query($dbcnx,
   "SELECT
    SponsorName
    ,concat(SponsorAddress,'<br> ',SponsorCity,' ',SponsorState,' ',SponsorZip) AS FullAddress
    ,SponsorPhone
    ,SponsorURL
    ,SponsorFB
    ,SponsorMap
    ,TeamName
    ,CaptainID
    ,concat(Cap1.PlayerFirst,' ',Cap1.PlayerLast) AS CaptainName
    ,T_Teams.TeamName
    ,concat('mailto:',Cap1.PlayerEmail) AS PEmail
    ,Captain2ID
    ,concat(Cap2.PlayerFirst,' ',Cap2.PlayerLast) AS CaptainName2
    ,concat('mailto:',Cap2.PlayerEmail) AS PEmail2
    ,Cap1.EmailDisplay as CapShow
    ,Cap2.EmailDisplay as Cap2Show
    ,T_Seasons.Current
    ,T_Teams.SeasonID
    ,T_Teams.TeamID
  FROM
    T_Sponsors Join (T_Teams) ON  (T_Sponsors.SponsorID=T_Teams.SponsorID)
    Left Join (T_Seasons) ON (T_Teams.SeasonID=T_Seasons.SeasonID)
    Left Join T_Players AS Cap1 ON (T_Teams.CaptainID = Cap1.PlayerID)
    Left Join T_Players AS Cap2 ON (T_Teams.Captain2ID = Cap2.PlayerID)
  Where
    T_Seasons.Current = '1'
    AND T_Teams.TeamName != 'Bye'
    And T_Sponsors.SponsorType ='T'
  ORDER BY T_Teams.TeamName ASC");

  	if (!$teamlist) {
    echo("<P>Error performing query: " .
         mysql_error() . "</P>");
    exit();
  }
?>

<table class="table table-bordered table-condensed">
<thead>
  <tr valign="center">
    <th>Sponsor</th>
    <th>Address</th>
    <th>Phone</th>
    <th>Social Media</th>
    <th>Team<br/>Team Captains</th>
    <th>Team<br/>Roster</th>
  </tr>
</thead>
</tbody>
<?php
  while ( $row = mysqli_fetch_assoc($teamlist) ) {
?>
  <tr>
<?php
  if ($row['SponsorURL'] <> "" )
  {
?>
    <td><a href= <?php echo $row['SponsorURL']?> target="_blank"> <?php echo $row['SponsorName'] ?> </td>
<?php
  }	else {
?>
    <td><?php echo $row['SponsorName'] ?></td>
 <?php
  }
?>
    <td><a href= <?php echo $row['SponsorMap']?> target="_blank"> <?php echo $row['FullAddress'] ?></a></td>
    <td><?php echo $row['SponsorPhone']?></td>
<?php
  if ($row['SponsorFB'] !== "" )
  {
?>
    <td align="center"><a href= <?php echo $row['SponsorFB']?>  target="_blank"><img alt="" src="http://www.trentondarts.com/images/facebook.png" title="Facebook" width="32" /></a></td>
<?php
  }	else {
?>
  <td></td>
<?php
  }
?>
  <td><?php echo $row['TeamName']; ?><br/><a href= <?php echo $row['PEmail']?> ><?php echo $row['CaptainName']?> </a>  <br>  <a href= <?php echo $row['PEmail2']?> > <?php echo $row['CaptainName2']?></a></td>
  <td><a href="{{ asset('old/rosters') }}?tid=<?php echo $row['TeamID']?>" > Roster </td>
</tr>
<?php
}
?>
</tbody>
</table>

@stop
