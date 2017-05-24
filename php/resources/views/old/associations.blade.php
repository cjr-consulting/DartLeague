@extends('layouts.master')

@section('content')

<?php
  $associd= $_GET['ai'];
?>
<table style="width: 50%; margin-right:auto; margin-left: auto;" class="table table-bordered table-condensed">
  <tr>
    <td align="center"><a href="{{ asset('old/associations') }}?ai=2"> NJ Darts</a> </td>
     <td align="center"><a href="{{ asset('old/associations') }}?ai=1"> Delaware Valley Darts </a> </td>
  </tr>
</table>

@include('old.associationlist', ['associd' => $associd])

@stop
