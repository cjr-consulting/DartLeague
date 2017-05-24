<!doctype html>
<html>
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Greater Trenton Dart League</title>
    <link href="{{ asset('bootstrap/css/bootstrap.min.css') }}" rel="stylesheet">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css">
    <!--[if lt IE 9]>
    <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
    <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
    <link href="{{ asset('css/app.css') }}" rel="stylesheet" type="text/css">
    <link href="{{ asset('css/app.css') }}" rel="stylesheet" type="text/css">
    <link rel="apple-touch-icon" sizes="57x57" href="/apple-icon-57x57.png">
    <link rel="apple-touch-icon" sizes="60x60" href="/apple-icon-60x60.png">
    <link rel="apple-touch-icon" sizes="72x72" href="/apple-icon-72x72.png">
    <link rel="apple-touch-icon" sizes="76x76" href="/apple-icon-76x76.png">
    <link rel="apple-touch-icon" sizes="114x114" href="/apple-icon-114x114.png">
    <link rel="apple-touch-icon" sizes="120x120" href="/apple-icon-120x120.png">
    <link rel="apple-touch-icon" sizes="144x144" href="/apple-icon-144x144.png">
    <link rel="apple-touch-icon" sizes="152x152" href="/apple-icon-152x152.png">
    <link rel="apple-touch-icon" sizes="180x180" href="/apple-icon-180x180.png">
    <link rel="icon" type="image/png" sizes="192x192"  href="/android-icon-192x192.png">
    <link rel="icon" type="image/png" sizes="32x32" href="/favicon-32x32.png">
    <link rel="icon" type="image/png" sizes="96x96" href="/favicon-96x96.png">
    <link rel="icon" type="image/png" sizes="16x16" href="/favicon-16x16.png">
    <link rel="manifest" href="/manifest.json">
    <meta name="msapplication-TileColor" content="#ffffff">
    <meta name="msapplication-TileImage" content="/ms-icon-144x144.png">
</head>
<body>
<div class="container-fluid">
    <div id="header">
        <a href={{ route('home') }}><img src="{{ asset('images/header.jpg') }}" class="img-rounded" alt="Trenton NJ Dart League"/></a>
    </div>
</div>
<div class="container-fluid">
    @include('shared.defaultnav', ['navtype' => 'manageleague'])
</div>
<div class="container-fluid">
    <div id="body-content">
        @yield('content')
    </div>
    <div id="footer">
        <div style="width: 190px;" class="pull-left">
            <a href="http://www.facebook.com/GreaterTrentonDartLeague" target="_blank"><img src="{{ asset('images/facebook.png') }}" title="Facebook"></a>
            <a href="https://www.google.com/calendar/embed?mode=AGENDA&height=600&wkst=1&bgcolor=%23FFFFFF&src=4l5p0pp047evrtopfk6s78tumg%40group.calendar.google.com&color=%23711616&src=phillyqcedl%40gmail.com&color=%232F6309&ctz=America%2FNew_York" target="_blank"><img src="{{ asset('images/gc_button1_en.gif') }}" title="Google Calendar"/></a>
            <a href="{{asset('old/suggestionbox')}}"><img src="{{ asset('images/sbox.jpg') }}" title="Suggestions"/></a>
        </div>
        <div style="display: block" class="pull-right">
            <img src="{{ asset('images/footer-copy.png')}}" alt="footer"/>
        </div>
        <div class="clearfix"></div>
    </div>
</div>
<script src="{{ asset('scripts/jquery-1.11.3.min.js') }}"></script>
<script src="{{ asset('bootstrap/js/bootstrap.min.js')}}"></script>
<script type="text/javascript" src="//cdn.raygun.io/raygun4js/raygun.min.js"></script>
<script>
    Raygun.init('ceYSG2SJCchoiSbs1+/kzw==', {
        allowInsecureSubmissions: true,
        ignoreAjaxAbort: true,
        ignoreAjaxError: true,
        excludedHostnames: ['localhost', 'trentondarts.app']
    }).attach();
</script>
@yield('scripts')
</body>
</html>