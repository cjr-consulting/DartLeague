@extends('layouts.master')
@section('content')
<form method="POST" action="/auth/login" class="single-form">
    {!! csrf_field() !!}
    <h2>Login</h2>
    <div class="form-group">
        <label for="email">Email</label>
        <input id="email" type="email" name="email" value="{{ old('email') }}" class="form-control">
    </div>

    <div class="form-group">
        <label for="password">Password</label>
        <input type="password" name="password" id="password" class="form-control">
    </div>
    @foreach($errors->all() as $error)
        <div class='validation-error'>{{ $error }}</div>
    @endforeach
    <div class="checkbox">
      <label>
        <input type="checkbox" name="remember"> Remember Me
      </label>
    </div>

    <div>
        <button type="submit" class="btn btn-default">Login</button>
        <a href="{{route('password.reset')}}">forgot password?</a>
    </div>
</form>
@stop

@section('scripts')
    <script>
    (function(d){
        d.getElementById('email').focus();
    })(document);
    </script>
@stop
