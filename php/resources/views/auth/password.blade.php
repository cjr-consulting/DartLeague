@extends('layouts.master')
@section('content')
<form method="POST" action="/password/email" class="single-form">
    {!! csrf_field() !!}

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
        <button type="submit" class="btn btn-default">
            Send Password Reset Link
        </button>
    </div>
</form>
@stop