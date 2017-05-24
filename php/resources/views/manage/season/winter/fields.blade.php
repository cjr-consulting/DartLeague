@if (count($errors) > 0)
    <div class="alert alert-danger">
        <ul>
            @foreach ($errors->all() as $error)
                <li>{{ $error }}</li>
            @endforeach
        </ul>
    </div>
@endif

<div class="checkbox">
    <label>
        {!! Form::hidden('isCurrent', false) !!}
        {!! Form::checkBox('isCurrent') !!}
        Current
    </label>
</div>

<div class="form-group">
    {!! Form::label('name', 'Season Name') !!}
    {!! Form::text('name', null, ['class' => 'form-control']) !!}
</div>

<div class="row">
    <div class="col-md-6">
        <div class="form-group">
            {!! Form::label('seasonType', 'Season Type') !!}
            {!! Form::select('seasonType', $seasonTypes, null, ['class' => 'form-control', 'placeholder' => 'Select Season Type']) !!}
        </div>
    </div>
    <div class="col-md-6">
        <div class="form-group">
            {!! Form::label('defaultMatchTypeId', 'Default Match Type') !!}
            {!! Form::select('defaultMatchTypeId', $matchTypes, null, ['class' => 'form-control', 'placeholder' => 'Select Match Type']) !!}
        </div>
    </div>
</div>
<div class="row">
    <div class="col-md-3">
        <div class="form-group">
            {!! Form::label('startYear', 'Start Year') !!}
            {!! Form::select('startYear', $years, null, ['class' => 'form-control', 'placeholder' => 'Select Start Year']) !!}
        </div>
    </div>
    <div class="col-md-3">
        <div class="form-group">
            {!! Form::label('endYear', 'End Year') !!}
            {!! Form::select('endYear', $years, null, ['class' => 'form-control', 'placeholder' => 'Select End Year']) !!}
        </div>
    </div>
</div>
<div class="row">
    <div class="col-md-12">
        <div class="checkbox">
            <label>
                {!! Form::hidden('accumulatePointsForAllParts', false) !!}
                {!! Form::checkBox('accumulatePointsForAllParts') !!}
                Accumulate Points For Both Halves
            </label>
        </div>
    </div>
</div>
<div class="row">
    <div class="col-md-4">
        <div class="checkbox">
            <label>
            {!! Form::hidden('isUsingMatchPoints', false) !!}
            {!! Form::checkBox('isUsingMatchPoints') !!}
            Use Match Points
            </label>
        </div>
    </div>
    <div class="col-md-8">
        <div class="form-group">
            {!! Form::label('winPoints', 'Win Points') !!}
            {!! Form::text('winPoints', null, ['class' => 'form-control']) !!}
        </div>
        <div class="form-group">
            {!! Form::label('halfPoints', 'Half Points') !!}
            {!! Form::text('halfPoints', null, ['class' => 'form-control']) !!}
        </div>
        <div class="form-group">
            {!! Form::label('minPointForHalfPoints', 'Min Score for Half Points') !!}
            {!! Form::text('minPointForHalfPoints', null, ['class' => 'form-control']) !!}
        </div>
    </div>
</div>
