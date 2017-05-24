@if (count($errors) > 0)
    <div class="alert alert-danger">
        <ul>
            @foreach ($errors->all() as $error)
                <li>{{ $error }}</li>
            @endforeach
        </ul>
    </div>
@endif
<div class="row">
    <div class="col-xs-6 col-md-5">
        <div class="form-group">
            {!! Form::label('awayTeamId', 'Away Team') !!}
            {!! Form::select('awayTeamId', $teams, null, ['class' => 'form-control', 'placeholder' => 'Select Team']) !!}
        </div>
    </div>
    <div class="col-md-2 hidden-xs text-center" style="font-weight: bold;">@</div>
    <div class="col-xs-6 col-md-5">
        <div class="form-group">
            {!! Form::label('homeTeamId', 'Home Team') !!}
            {!! Form::select('homeTeamId', $teams, null, ['class' => 'form-control', 'placeholder' => 'Select Team']) !!}
        </div>
    </div>
</div>

<div class="form-group">
    {!! Form::label('matchTypeId', 'Match Type') !!}
    {!! Form::select('matchTypeId', $matchTypes, null, ['class' => 'form-control', 'placeholder' => 'Select Match Type']) !!}
</div>
