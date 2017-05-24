<nav class="navbar navbar-default">
  <div class="container-fluid">
    <div class="navbar-header">
      <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
        <span class="sr-only">Dart League</span>
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
      </button>
      <a class="navbar-brand" href="{{ route('home') }}"> Trenton Dart League</a>
    </div>
    <div id="navbar" class="navbar-collapse collapse">
      <ul class="nav navbar-nav">
        <!--<li class="active"><a href="#">Home</a></li>-->
        @foreach($navdata as $parentNav)
        <li>
          @if(sizeof($parentNav->SubNavLinks) > 0)
          <a href="{{ $parentNav->Link }}"class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">{{ $parentNav->Title }} <span class="caret"></span></a>
          <ul class="dropdown-menu">
          @foreach($parentNav->SubNavLinks as $item)
            @if($item->IsHeader)
              <li class="dropdown-header">{{ $item->Title}}</li>
            @elseif($item->IsSeperator)
              <li role="separator" class="divider"></li>
            @else
              <li><a href="{{ $item->Link }}">{{ $item->Title }}</a></li>
            @endif
          @endforeach
          </ul>
          @else
            <a href="{{ $parentNav->Link }}">{{ $parentNav->Title }}</a>
          @endif
        </li>
        @endforeach
      </ul>
      @if(isset($usernav))
      <ul class="nav navbar-nav navbar-right">
        <li>
          <a href="#"class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">{{ $usernav->userName }} <span class="caret"></span></a>
          <ul class="dropdown-menu">
            @foreach($usernav->SubNavLinks as $item)
              @if($item->IsHeader)
                <li class="dropdown-header">{{ $item->Title}}</li>
              @elseif($item->IsSeperator)
                <li role="separator" class="divider"></li>
              @else
                <li><a href="{{ $item->Link }}">{{ $item->Title }}</a></li>
              @endif
            @endforeach
          </ul>
        </li>
      </ul>
      @else
        <ul class="nav navbar-nav navbar-right">
          <li>
            <a href="/auth/login">Log in</a>
          </li>
        </ul>
      @endif
      
    </div><!--/.nav-collapse -->
  </div><!--/.container-fluid -->
</nav>
