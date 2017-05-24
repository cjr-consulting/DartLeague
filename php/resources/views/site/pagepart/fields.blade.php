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
    <div class="col-xs-12 col-sm-6">
        <div class="form-group">
            {!! Form::label('name', 'Name') !!}
            {!! Form::text('name', null, ['class' => 'form-control']) !!}
        </div>
    </div>
    <div class="col-xs-12 col-sm-6">
        <div class="form-group">
            {!! Form::label('description', 'Description') !!}
            {!! Form::textarea('description', null, ['class' => 'form-control', 'rows' => 2]) !!}
        </div>
    </div>
</div>

<div class="form-group">
    {!! Form::label('html', 'Html') !!}
    {!! Form::textarea('html', null, ['class' => 'form-control']) !!}
</div>

<script src="{{URL::asset('scripts/ckeditor/ckeditor.js')}}"></script>
<script>
    CKEDITOR.stylesSet.add('my_style', [
        { name: 'Center Block',  element: 'div', attributes: { class: 'important-message' } },
    ]);
    CKEDITOR.config.stylesSet = 'my_style';
    CKEDITOR.replace('html');
</script>