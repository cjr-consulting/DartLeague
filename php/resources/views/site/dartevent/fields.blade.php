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
    <div class="col-md-6">
        <div class="form-group">
            {!! Form::label('eventContact', 'Contact') !!}
            {!! Form::text('eventContact', null, ['class' => 'form-control']) !!}
        </div>
    </div>
    <div class="col-md-6">
        <div class="form-group">
            {!! Form::label('eventContact2', 'Second Contact') !!}
            {!! Form::text('eventContact2', null, ['class' => 'form-control']) !!}
        </div>
    </div>
</div>

<div class="row">
    <div class="col-md-4">
        <div class="form-group">
            {!! Form::label('eventDate', 'Event Date') !!}
            {!! Form::date('eventDate', null, ['class' => 'form-control']) !!}
        </div>
    </div>
    <div class="col-md-4">
        <div class="form-group">
            {!! Form::label('eventEndDate', 'End Date') !!}
            {!! Form::date('eventEndDate', null, ['class' => 'form-control']) !!}
        </div>
    </div>
    <div class="col-md-4">
        <div class="form-group">
            {!! Form::label('eventTypeId', 'Event Type') !!}
            {!! Form::select('eventTypeId', $eventTypes, null, ['class' => 'form-control', 'placeholder' => 'Select Event Type']) !!}
        </div>
    </div>
</div>

<div class="row">
    <div class="col-md-3">
        <div class="form-group">
            {!! Form::label('registrationStartTime', 'Reg Start Time') !!}
            {!! Form::text('registrationStartTime', null, ['class' => 'form-control']) !!}
        </div>
    </div>
    <div class="col-md-3">
        <div class="form-group">
            {!! Form::label('registrationEndTime', 'Reg End Time') !!}
            {!! Form::text('registrationEndTime', null, ['class' => 'form-control']) !!}
        </div>
    </div>
    <div class="col-md-3">
        <div class="form-group">
            {!! Form::label('dartStart', 'Darts Start Time') !!}
            {!! Form::text('dartStart', null, ['class' => 'form-control']) !!}
        </div>
    </div>
    <div class="col-md-3">
        <div class="form-group">
            {!! Form::label('dartType', 'Darts Type') !!}
            {!! Form::select('dartType', ['Steel' => 'Steel', 'Soft Tip' => 'Soft Tip'], null, ['class' => 'form-control', 'placeholder' => 'Select Dart Type']) !!}
        </div>
    </div>
</div>

<div class="row">
    <div class="col-md-4">
        <div class="form-group">
            {!! Form::label('hostName', 'Host Name') !!}
            {!! Form::text('hostName', null, ['class' => 'form-control']) !!}
        </div>
    </div>
    <div class="col-md-4">
        <div class="form-group">
            {!! Form::label('hostPhone', 'Host Phone') !!}
            {!! Form::text('hostPhone', null, ['class' => 'form-control']) !!}
        </div>
    </div>
    <div class="col-md-4">
        <div class="form-group">
            {!! Form::label('hostUrl', 'Host Url') !!}
            {!! Form::url('hostUrl', null, ['class' => 'form-control']) !!}
        </div>
    </div>
</div>

<div class="form-group">
    {!! Form::label('locationName', 'Location Name') !!}
    {!! Form::text('locationName', null, ['class' => 'form-control']) !!}
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

@if(isset($dartEvent) && $dartEvent->imageFileId != null && $dartEvent->imageFileId > 0)
    <img src="{{route('file.get', ['id' => \TrentonDarts\LeagueManagement\Services\BrowsableFileService::getHashedFileId($dartEvent->imageFileId)])}}"/>
@endif

<div class="form-group">
    <label for="eventImage">Event Image <small>(image of 750w x 110h)</small></label>
    <input type="file" id="eventImage" name="eventImage" class="form-control" />
</div>

@if(isset($dartEvent) && $dartEvent->posterFileId != null && $dartEvent->posterFileId > 0)
    <a href="{{route('file.get', ['id' => \TrentonDarts\LeagueManagement\Services\BrowsableFileService::getHashedFileId($dartEvent->posterFileId)])}}">Poster document exists</a>
@endif

<div class="form-group">
    <label for="posterDocument">Poster Document <small>(PDF document recommended)</small></label>
    <input type="file" id="posterDocument" name="posterDocument" class="form-control" />
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
    {!! Form::label('mapUrl', 'Map Url') !!}
    {!! Form::url('mapUrl', null, ['class' => 'form-control']) !!}
</div>

<div class="form-group">
    {!! Form::label('description', 'Description') !!}
    {!! Form::textarea('description', null, ['class' => 'form-control']) !!}
</div>


<script src="{{URL::asset('scripts/ckeditor/ckeditor.js')}}"></script>
<script>
    CKEDITOR.replace('description');
</script>