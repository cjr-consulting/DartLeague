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
            {!! Form::label('sponsorId', 'Sponsor') !!}
            {!! Form::select('sponsorId', $sponsors, null, ['class' => 'form-control', 'placeholder' => 'Select Sponsor']) !!}
        </div>
    </div>
</div>
<div class="form-group">
    {!! Form::label('notes', 'Notes') !!}
    {!! Form::textarea('notes', null, ['class' => 'form-control']) !!}
</div>