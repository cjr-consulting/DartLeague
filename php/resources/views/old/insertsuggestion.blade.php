@extends('layouts.master')

@section('content')
<?php
$con = mysqli_connect("localhost", "trenton_webuser", "d@rts", "trenton_GTDL");

if (!$con)
{
die('Could not connect: ' . mysql_error());
}

mysqli_select_db($con, "trenton_GTDL");

$suggestion= $_POST['a'];
$playername= $_POST['b'];
$method= $_POST['how'];
$methoddata= $_POST['d'];



$suggestion= mysqli_real_escape_string($con, $suggestion);
$playername= mysqli_real_escape_string($con, $playername);
$method= mysqli_real_escape_string($con, $method);
$methoddata= mysqli_real_escape_string($con, $methoddata);


$query = "INSERT INTO `trenton_GTDL`.`T_Suggestions` (`SuggestionID`, `Comment`, `Playername`, `Method`, `MethodData`, `Created`, `AssignedTo`, `Status`) VALUES (NULL, '$suggestion', '$playername', '$method', '$methoddata', CURRENT_TIMESTAMP, '', '');  ";


mysqli_query($con, $query);

echo "<h2>Thank You for your feedback!!! <br><br>A board member will be assigned to talk to you if necessary to get more information.</h2>";



mysqli_close($con);
?>
<br><br><h1> Click the GTDL icon in the header or this link to return to <a href="http://www.trentondarts.com">Trentondarts.com</a></h1><br><br>
@stop