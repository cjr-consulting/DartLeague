@extends('layouts.master')

@section('content')
<h1 align="center">2014-2015 Greater Trenton Dart League</h1>
<?php
  $order = 1;
  if(isset($_GET['o'])){
    $order = $_GET['o'];
  }

?>
<table style="width: 80%; border-collapse: collapse" summary=""  cellpadding="3" cellspacing="0" align = "center">
<tr>
  <td align="center"><font size = "5" color="red"><a href="{{ asset('old/sponsors') }}?o=1">League Sponsors and Partners</a></font> </td>
  <td align="center"><font size = "5" color="red"><a href="{{ asset('old/sponsors') }}?o=2">Player Companies</a></font> </td>
  <td align="center"><font size = "5" color="red"><a href="{{ asset('old/sponsors') }}?o=3">Charity Partners</a></font> </td>
  <td align="center"><font size = "5" color="red"><a href="{{ asset('old/bars') }}">Team Sponsors</a></font> </td>
</tr>
</table>

<?php
if ($order ==3)
 {
?>
   @include('old.cplist')
<?php
 }
 elseif ($order==2)
 {
?>
   @include('old.pslist')
<?php
 }
 else
 {
?>
   @include('old.lslist')
<?php
 }
?>
@stop
