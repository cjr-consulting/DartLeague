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
    {!! Form::label('teamId', 'Team') !!}
    {!! Form::select('teamId', $teams, null, ['class' => 'form-control', 'placeholder' => 'Select Team']) !!}
</div>

<div class="form-group">
    {!! Form::label('preSeasonDiv', 'Pre Season Div') !!}
    {!! Form::text('preSeasonDiv', null, ['class' => 'form-control']) !!}
</div>

<div class="form-group">
    {!! Form::label('regularSeasonDiv', 'Regular Season Div') !!}
    {!! Form::text('regularSeasonDiv', null, ['class' => 'form-control']) !!}
</div>
