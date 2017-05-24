@extends('layouts.master')

@section('content')
<!-- Begin copy from fill.html------------------------------------------------->
<h1 align="center">
    Greater Trenton Dart League </br> Player Activity in Events
</h1>


<?php
$dbcnx = mysqli_connect("localhost", "trenton_webuser", "d@rts", "trenton_GTDL");

if (!$dbcnx) {
    echo( "<P>Unable to connect to the " .
            "database server at this time.</P>" );
    exit();
}
?>

<?php

$gtdlresults= mysqli_query($dbcnx,
        "SELECT EventName, Date_Format (IFNULL(EventEnd,EventDate), '%M %D %Y') AS EDate,   concat( PlayerFirst,' ' ,PlayerLast) AS Fullname, Activity, Place, PlayerLast,DateDiff(IFNULL(EventEnd,EventDate), NOW())AS DAYSTIL, EventOrder

FROM `T_EventResults`
Join T_Events on T_EventResults.EventID= T_Events.EventID
Join T_Players on T_EventResults.PlayerID = T_Players.PlayerID
Having DAYSTIL > -365

Order by DAYSTIL DESC, EventOrder ASC, PlayerLast ASC " );

if (!$gtdlresults) {
    echo("<P>Error performing query: " .
            mysqli_error($dbcnx) . "</P>");
    exit();
}


?>
    <table class="table table-bordered table-condensed" style="width: 80%; margin-right: auto; margin-left: auto;">
        <tbody>
        <tr valign="center">
            <th scope="col">
                <p>Event Name</p></th>
            <th scope="col">
                <p>Event Date</p></th>
            <th scope="col">
                <p>Specific Event</p></th>
            <th scope="col">
                <p>Name</p></th>
            <th scope="col">
                <p>Finish</p></th>

        </tr></tbody>

        <?php
        while ( $row = mysqli_fetch_assoc($gtdlresults) ) {
        ?>
        <tr>

            <td>  <?php echo $row['EventName']?></td>
            <td>  <?php echo $row['EDate']?></td>
            <td>  <?php echo $row['Activity']?></td>
            <td>  <?php echo $row['Fullname']?></td>
            <td>  <?php echo $row['Place']?></td>



        </tr>
        <?php
        }

        ?>

    </table>

@stop