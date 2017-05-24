@extends('layouts.dartsfordreams')

@section('content')
<div class="dartsfordreams">
<img style="margin:0px auto;display:block" src="/images/dartsfordreams-header.jpg" class="img-thumbnail"/>
<?php
$dbcnx = mysqli_connect("localhost", "trenton_webuser", "d@rts", "trenton_DFD");

if (!$dbcnx) {
  echo( "<P>Unable to connect to the " .
        "database server at this time.</P>" );
  exit();
}

$maxyear= mysqli_query($dbcnx,
 " SELECT max(T_Events.Year) as lastevent, min(T_Events.Year) as firstevent from T_Events ") ;

	if (!$maxyear) {
  echo("<P>Error performing query: " .
       mysql_error() . "</P>");
  exit();
}

while ( $row = mysqli_fetch_assoc($maxyear) ) {
                $ly= $row['lastevent']   ;
                $fy= $row['firstevent']   ;
              }
  ?>

<!--  Do increments for navigation. -->
<?php
$inc=1;
$ey = 0;
if(isset($_GET['eyear']))
  $ey= $_GET['eyear'];

if ($ey <$fy)
 {
  $ey=$ly;
 }

$py=$ey-$inc;
$ny=$ey+$inc;

if ($ey == $ly) { ?>
    <table style="width: 30%; margin-left:auto; margin-right: auto;" class="table table-bordered table-condensed">
  <tr>
    <td align="center" class="navigation"><a href="dfdhistory?eyear=<?php echo $py; ?>"> <<< PREVIOUS YEAR </a> </td>
    <td align="center" size ="4" class="navigationYear"><?php echo $ey ?></td>
  </tr>
</table>
<?php
} elseif ($ey == $fy) { ?>
    <table style="width: 30%; margin-left:auto; margin-right: auto;" class="table table-bordered table-condensed">
  <tr>
    <td align="center" size ="4" class="navigationYear"><?php echo $ey ?></td>
    <td align="center" class="navigation"><a href="dfdhistory?eyear=<?php echo $ny; ?>"> NEXT YEAR >>> </a> </td>
  </tr>
</table>

<?php
} else {
?>
    <table style="width: 40%; margin-left:auto; margin-right: auto;" class="table table-condensed">
  <tr>
    <td align="center" class="navigation"><a href="dfdhistory?eyear=<?php echo $py; ?>"> <<< PREVIOUS YEAR </a> </td>
    <td align="center" size ="4" class="navigationYear"><?php echo $ey ?></td>
    <td align="center" class="navigation"><a href="dfdhistory?eyear=<?php echo $ny; ?>"> NEXT YEAR >>> </a> </td>
  </tr>
</table>
<?php
}

if (!$dbcnx) {
  echo( "<P>Unable to connect to the " .
        "database server at this time.</P>" );
  exit();
}

$winnerlist= mysqli_query($dbcnx,
 "SELECT
    Date_Format(Date, '%M %D, %Y') AS EDate
    ,Year
    ,concat(EventName1,' ', EventName2) as EName
    ,Venue
    ,Story
    ,Format(FinalTotal,0) AS TD
    ,Attendees
    ,Darts
    ,Darts*4 AS TDarts
    ,WM.Name as MensWinner, WM.Total as MensTotal
    ,WW.Name as WomensWinner, WW.Total as WomensTotal
    ,WT.Name as TeamWinner, WT.Total as TeamTotal
    ,WY.Name as YouthWinner, WY.Total as YouthTotal
    ,wlow
    ,whigh
    ,weather
 FROM
  `T_Events`Left Join T_Winners AS WM
    on (WM.EventID= T_Events.EventID And WM.Category = 'Male')
  Left Join T_Winners AS WW
    on (WW.EventID= T_Events.EventID And WW.Category = 'Female')
  Left Join T_Winners AS WT
    on (WT.EventID= T_Events.EventID And WT.Category = 'Team')
  LEFT Join T_Winners AS WY
    on (WY.EventID= T_Events.EventID And WY.Category = 'Youth')
Having Year = $ey ") ;

	if (!$winnerlist) {
  echo("<P>Error performing query: " .
       mysql_error() . "</P>");
  exit();
}

while ( $row = mysqli_fetch_assoc($winnerlist) ) {
 ?>

<div class="announcement"><?php echo $row['EDate']?><nbsp> - <nbsp>
<?php echo $row['EName']?><br>
<?php echo $row['Venue']?><br>
</div>
<div class="weather">
  The weather for the day was... Low temperature was <?php echo $row['wlow']?>, high temperature was <?php echo $row['whigh']?>.<br> <?php echo $row['weather']?>
</div>
<table style="width: 90%; margin-left:auto; margin-right: auto;" class="table table-bordered table-condensed">
<thead>
  <tr>
    <th colspan="7" style="text-align: center">This year, the players shot <?php echo $row['Darts'] ?> darts for a high score.<br/>  The team event was a total of <?php echo $row['TDarts'] ?> darts.  Winners for the year are</th> </tr>
  </tr>
  <tr>
    <th scope="col" style="text-align: center" >Mens</th>
    <th scope="col" style="text-align: center" >Womens</th>
    <th scope="col" style="text-align: center" >Team</th>
    <th scope="col" style="text-align: center" >Youth</th>
  </tr>
</thead>
  <tr>
    <td align = "center"><?php echo $row['MensWinner'] ?> <br> <?php echo $row['MensTotal'] ?> </td>
    <td align = "center"><?php echo $row['WomensWinner'] ?> <br> <?php echo $row['WomensTotal'] ?> </td>
    <td align = "center"><?php echo $row['TeamWinner'] ?> <br> <?php echo $row['TeamTotal'] ?> </td>
    <td align = "center"><?php echo $row['YouthWinner'] ?> <br> <?php echo $row['YouthTotal'] ?> </td>
  </tr>
</table>

<table style="width: 30%; margin-left:auto; margin-right: auto;" class="table table-bordered table-condensed">
<tr valign="center">
  <th scope="col" style="text-align: center" ><p>Donation Totals</p></th>
  <th scope="col" style="text-align: center" ><p>Attendees</p></th>
</tr>
<tbody>
  <tr>
    <td align ="center"><font size="5" color="red">$<?php echo $row['TD']?></font></td>
    <td align ="center"><?php echo $row['Attendees']?></td>
  </tr>
</tbody>
</table>

<table style="width: 80%; margin-left:auto; margin-right: auto;" class="table table-bordered table-condensed">
  <tr valign="center">
    <th scope="col" style="text-align: center" >A little story about this years event from the event director</th>
  </tr>
<tbody>
  <tr>
    <td><?php echo $row['Story']?></td>
  </tr>
</tr>
</tbody>
</table>
<?php
}

$doclist= mysqli_query($dbcnx,
 " SELECT T_Events.Year, concat (eventname1,' ',eventname2) as ename, T_Links.DocName, T_Links.DocURL, T_Links.DocType

FROM T_Events
Join `T_Links`  on T_Links.EventID=T_Events.EventID
Where T_Events.Year = $ey And T_Links.Active =1

Order by Year ASC, DocName ") ;

	if (!$doclist) {
  echo("<P>Error performing query: " .
       mysql_error() . "</P>");
  exit();
}
  ?>
<br/>

<table style="width: 50%; margin-left:auto; margin-right: auto;" class="table table-bordered table-condensed">
<thead>
  <tr valign="center">
    <th scope="col">Documents and attachments for this year are below</th>
  </tr>
</thead>

<?php
while ( $row = mysqli_fetch_assoc($doclist) ) {
 ?>
  <tr>
    <td align="center"><a href= <?php echo $row['DocURL']?> target="_blank"> <?php echo $row['DocName'] ?> </a> </td>
  </tr>
<?php
              }
  ?>

</table>
</div>
@stop
