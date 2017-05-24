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
    {!! Form::label('name', 'Name') !!}
    {!! Form::text('name', null, ['class' => 'form-control']) !!}
</div>
<div class="row">
    <div class="col-md-4">
        <div class="form-group">
            {!! Form::label('contactName', 'Contact Name') !!}
            {!! Form::text('contactName', null, ['class' => 'form-control']) !!}
        </div>
    </div>
    <div class="col-md-4">
        <div class="form-group">
            {!! Form::label('type', 'Sponsor Type') !!}
            {!! Form::select('type', $sponsorTypes, null, ['class' => 'form-control', 'placeholder' => 'Select Sponsor Type']) !!}
        </div>
    </div>
    <div class="col-md-4">
        <div class="form-group">
            {!! Form::label('phone', 'Phone') !!}
            {!! Form::text('phone', null, ['class' => 'form-control']) !!}
        </div>
    </div>
</div>

<div class="form-group">
    {!! Form::label('address1', 'Address') !!}
    {!! Form::text('address1', null, ['class' => 'form-control']) !!}
    {!! Form::text('address2', null, ['class' => 'form-control']) !!}
</div>

<div class="row">
    <div class="col-md-8">
        <div class="form-group">
            {!! Form::label('city', 'City') !!}
            {!! Form::text('city', null, ['class' => 'form-control']) !!}
        </div>
    </div>
    <div class="col-md-2">
        <div class="form-group">
            {!! Form::label('state', 'State') !!}
            {!! Form::text('state', null, ['class' => 'form-control']) !!}
        </div>
    </div>
    <div class="col-md-2">
        <div class="form-group">
            {!! Form::label('zip', 'Zip') !!}
            {!! Form::text('zip', null, ['class' => 'form-control']) !!}
        </div>
    </div>
</div>

<div class="form-group">
    {!! Form::label('url', 'Web Site Url') !!}
    {!! Form::url('url', null, ['class' => 'form-control']) !!}
</div>

<div class="form-group">
    {!! Form::label('facebookUrl', 'Facebook Url') !!}
    {!! Form::url('facebookUrl', null, ['class' => 'form-control']) !!}
</div>

<div class="form-group">
    {!! Form::label('email', 'Email') !!}
    {!! Form::email('email', null, ['class' => 'form-control']) !!}
</div>

<div class="form-group">
    {!! Form::label('mapUrl', 'Map Url') !!}
    {!! Form::url('mapUrl', null, ['class' => 'form-control']) !!}
</div>
<div class="row">
    <div class="col-md-6">
        <div class="form-group">
            {!! Form::label('description', 'Description') !!}
            {!! Form::textarea('description', null, ['class' => 'form-control']) !!}
        </div>
    </div>
    <div class="col-md-6">
        <div class="form-group">
            {!! Form::label('comments', 'Comments') !!}
            {!! Form::textarea('comments', null, ['class' => 'form-control']) !!}
        </div>
    </div>
</div>