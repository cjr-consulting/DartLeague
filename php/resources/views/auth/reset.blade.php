@extends('layouts.master')
@section('content')
    <form method="POST" action="/password/reset" class="single-form">
        {!! csrf_field() !!}
        <input type="hidden" name="token" value="{{ $token }}">

        @if (count($errors) > 0)
            <ul>
                @foreach ($errors->all() as $error)
                    <li>{{ $error }}</li>
                @endforeach
            </ul>
        @endif

        <div class="form-group">
            <label for="email">Email</label>
            <input type="email" name="email" value="{{ old('email') }}" class="form-control">
        </div>

        <div class="form-group">
            <label for="password">Password</label>
            <input type="password" name="password" class="form-control">
        </div>

        <div class="form-group">
            <label for="password_confirmation">Confirm Password</label>
            <input type="password" name="password_confirmation" class="form-control">
        </div>

        <div class="form-group">
            <button type="submit" class="btn btn-default">
                Reset Password
            </button>
        </div>
    </form>
@stop