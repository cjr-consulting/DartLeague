@extends('layouts.master')

@section('content')
<form action="{{asset('old/insertsuggestion')}}" method="post">
    {!! Form::token() !!}}}
    Tell us your idea: <textarea name="a" rows="5" cols="40"></textarea> <br><br>
    Tell us who you are:  <input type="text" name="b"> <br><br>

    <input type="radio" name="how" value="Phone">Phone
    <input type="radio" name="how" value="Email">Email
    <input type="radio" name="how" value="InPerson">In Person<br><br>

    And what/where is your phone number, email or best location to find you: <input type="text" name="d"> </br></br>

    <input type="submit">
</form>
@stop