@extends('layouts.master')

@section('content')
  <div class="page-header">
    <h1 style="text-align: center;">Welcome to The Greater Trenton Dart League</h1>
    <div class="important-message">
      {!! $welcomeMessage !!}
    </div>
  </div>

  <div class="container-fluid">

    <div class="row">
      <div class="col-sm-7 col-md-8">
        @if(isset($titleEvent))
        <div class="well well-sm ">
          @include('titleevent')
        </div>
        @endif
        <?php include 'documents/welcome.php'; ?>
        @include('shared.eventlist')
      </div>
      <div class="col-sm-5 col-md-4">
        @include('season.winter.standings')
      </div>
    </div>
    <div class="row">
      <div class="col-sm-12 col-md-12">
        <div class="alert alert-success" style="text-align: center;">
          <h4>Interesting <a href="http://www.si.com/vault/1975/02/17/557875/eight-feet-away-from-glory" target="_blank"> Sports Illustrated article</a> on darts from 1975.  Our own Conrad Daniels is a key part!</h4>
          <p>
            <a href="/documents/static/conrad_outs.pdf" target="_blank">Conrad Daniels Out Shots </a>
          </p>
        </div>
      </div>
    </div>
  </div>
@stop
