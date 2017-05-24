@extends('layouts.master')
@section('content')
<form method="POST" action="/auth/register" class="single-form">
    {!! csrf_field() !!}
    <h2>Join Trenton Dart League</h2>

    <div class="form-group">
        <label for="name">Full Name</label>
        <input id="name" type="text" name="name" value="{{ old('name') }}" class="form-control">
        @foreach($errors->get('name') as $error)
            <div class='validation-error'>{{ $error }}</div>
        @endforeach
    </div>

    <div class="form-group">
        <label for="email">Email</label>
        <input type="email" name="email" value="{{ old('email') }}" class="form-control">
        @foreach($errors->get('email') as $error)
            <div class='validation-error'>{{ $error }}</div>
        @endforeach
    </div>

    <div class="form-group">
        <label for="password">Password</label>
        <input type="password" name="password" class="form-control">
        @foreach($errors->get('password') as $error)
            <div class='validation-error'>{{ $error }}</div>
        @endforeach
    </div>

    <div class="form-group">
        <label for="password_confirmation">Confirm Password</label>
        <input type="password" name="password_confirmation" class="form-control">
        @foreach($errors->get('password_confirmation') as $error)
            <div class='validation-error'>{{ $error }}</div>
        @endforeach
    </div>

    <div>
        <button type="submit" class="btn btn-default">Register</button>
    </div>
</form>
@stop

@section('scripts')
    <script>
        (function(d){
            d.getElementById('name').focus();
        })(document);
    </script>
@stop

