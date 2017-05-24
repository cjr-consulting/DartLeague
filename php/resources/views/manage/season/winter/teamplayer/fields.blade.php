@if (count($errors) > 0)
    <div class="alert alert-danger">
        <ul>
            @foreach ($errors->all() as $error)
                <li>{{ $error }}</li>
            @endforeach
        </ul>
    </div>
@endif
<div class="form-group">
    {!! Form::label('playerId', 'Player') !!}
    {!! Form::select('playerId', $players, null, ['class' => 'form-control', 'placeholder' => 'Select Player']) !!}
</div>

<div class="form-group">
    {!! Form::label('role', 'Role') !!}
    {!! Form::select('role', $roles, 'Player', ['class' => 'form-control', 'placeholder' => 'Select Role']) !!}
</div>
