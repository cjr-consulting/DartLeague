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
    {!! Form::label('userId', 'User') !!}
    {!! Form::select('userId', $users, null, ['class' => 'form-control', 'placeholder' => 'Select User']) !!}
</div>
<div class="row">
    <div class="col-sm-6 col-md-6">
    <div class="form-group">
        {!! Form::label('firstName', 'First Name') !!}
        {!! Form::text('firstName', null, ['class' => 'form-control']) !!}
    </div>
    </div>
    <div class="col-sm-6 col-md-6">
    <div class="form-group">
        {!! Form::label('lastName', 'Last Name') !!}
        {!! Form::text('lastName', null, ['class' => 'form-control']) !!}
    </div>
    </div>
</div>
<div class="row">
    <div class="col-sm-6 col-md-6">
        <div class="form-group">
            {!! Form::label('nickname', 'Nickname') !!}
            {!! Form::text('nickname', null, ['class' => 'form-control']) !!}
        </div>
    </div>
    <div class="col-sm-6 col-md-6">
        <div class="form-group">
            {!! Form::label('email', 'Email Address') !!}
            {!! Form::text('email', null, ['class' => 'form-control']) !!}
        </div>
    </div>
</div>
<div class="row">
    <div class="col-sm-5 col-md-5">
    <div class="form-group">
        {!! Form::label('homePhone', 'Home Phone') !!}
        {!! Form::text('homePhone', null, ['class' => 'form-control']) !!}
    </div>
    </div>
    <div class="col-sm-5 col-md-5">
    <div class="form-group">
        {!! Form::label('cellPhone', 'Cell Phone') !!}
        {!! Form::text('cellPhone', null, ['class' => 'form-control']) !!}
    </div>
    </div>
    <div class="col-sm-2 col-md-2">
        <div class="form-group">
            {!! Form::label('shirtSize', 'Shirt Size') !!}
            {!! Form::text('shirtSize', null, ['class' => 'form-control']) !!}
        </div>
    </div>
</div>
<div class="form-group">
    {!! Form::label('address1', 'Street') !!}
    {!! Form::text('address1', null, ['class' => 'form-control']) !!}
    {!! Form::text('address2', null, ['class' => 'form-control']) !!}
</div>
<div class="row">
    <div class="col-sm-8 col-md-8">
        <div class="form-group">
            {!! Form::label('city', 'City') !!}
            {!! Form::text('city', null, ['class' => 'form-control']) !!}
        </div>
    </div>
    <div class="col-sm-2 col-md-2">
        <div class="form-group">
            {!! Form::label('state', 'State') !!}
            {!! Form::text('state', null, ['class' => 'form-control']) !!}
        </div>
    </div>
    <div class="col-sm-2 col-md-2">
        <div class="form-group">
            {!! Form::label('zip', 'Zip') !!}
            {!! Form::text('zip', null, ['class' => 'form-control']) !!}
        </div>
    </div>
</div>
<div class="checkbox">
    <label>
        {!! Form::hidden('acceptEmail', false) !!}
        {!! Form::checkBox('acceptEmail') !!}
        Accept Email
    </label>
</div>
<div class="checkbox">
    <label>
        {!! Form::hidden('acceptText', false) !!}
        {!! Form::checkBox('acceptText') !!}
        Accept Text
    </label>
</div>