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
    <div class="col-md-6">
        <div class="form-group">
            {!! Form::label('name', 'Name') !!}
            {!! Form::text('name', null, ['class' => 'form-control']) !!}
        </div>
    </div>
    <div class="col-md-6">
        <div class="form-group">
            {!! Form::label('userId', 'User') !!}
            {!! Form::select('userId', $users, null, ['class' => 'form-control', 'placeholder' => 'Select User']) !!}
        </div>
    </div>
</div>
<div class="row">
    <div class="col-md-4">
        <div class="form-group">
            {!! Form::label('position', 'Position') !!}
            {!! Form::text('position', null, ['class' => 'form-control']) !!}
        </div>
    </div>
    <div class="col-md-4">
        <div class="form-group">
            {!! Form::label('startSeasonId', 'Starting Season') !!}
            {!! Form::select('startSeasonId', $seasons, null, ['class' => 'form-control', 'placeholder' => 'Select Season']) !!}
        </div>
    </div>
    <div class="col-md-4">
        <div class="form-group">
            {!! Form::label('endSeasonId', 'Ending Season') !!}
            {!! Form::select('endSeasonId', $seasons, null, ['class' => 'form-control', 'placeholder' => 'Select Season']) !!}
        </div>
    </div>
</div>