@extends('layouts.master')

@section('content')
    <div id="scoresheet">
        <h2>Season {{ $season->name }}</h2>
        <div class="container-fluid">
            {!! Form::select('matchesThisWeek', $matchesThisWeek, $selectedMatchRoute, ['id' => 'matchesThisWeek', 'class' => 'form-control', 'placeholder' => 'Select Match']) !!}
            <h3>{{ $match->awayTeam->name }} <strong>@ {{ $match->homeTeam->name }}</strong></h3>
        </div>
        <div class="container-fluid" data-bind="visible: !hasScorecard()">
            <div class="row header">
                <div class="col-xs-4 col-sm-4 col-md-4 truncate" style="text-align: center;">
                    [H] {{ $match->homeTeam->name }}
                </div>
                <div class="col-xs-3 col-sm-4 col-md-4" style="text-align: center;">
                    <span data-bind="html: homeScoreOverride"></span>-<span data-bind="html: awayScoreOverride"></span><br/>
                    No Scorecard Available
                </div>
                <div class="col-xs-4 col-sm-4 col-md-4 truncate" style="text-align: center;">
                    [A] {{ $match->awayTeam->name }}
                </div>
            </div>
        </div>

        <div class="container-fluid" data-bind="visible: hasScorecard()">
            <div class="row header">
                <div class="col-xs-4 col-sm-4 col-md-4 truncate" style="text-align: center;">
                    [H] {{ $match->homeTeam->name }}
                </div>
                <div class="col-xs-3 col-sm-4 col-md-4" style="text-align: center;"><span data-bind="html: homeScore"></span>-<span data-bind="html: awayScore"></span></div>
                <div class="col-xs-4 col-sm-4 col-md-4 truncate" style="text-align: center;">
                    [A] {{ $match->awayTeam->name }}
                </div>
            </div>
        </div>
        <div class="container-fluid score-sheet" data-bind="foreach: gameGroups, visible: hasScorecard()">
            <div class="row">
                <div class="col-sm-12 col-md-12 game-group-header" data-bind="html: name">
                </div>
            </div>
            <div class="row" data-bind="template: {name: 'gameTemplate', foreach: games}">
            </div>
        </div>
        <div class="container-fluid" data-bind="visible: hasScorecard()">
            <div class="row footer">
                <div class="col-xs-5 col-sm-4 col-md-4 truncate" style="text-align: center;">
                    [H] {{ $match->homeTeam->name }}
                </div>
                <div class="col-xs-2 col-sm-4 col-md-4" style="text-align: center;"><span data-bind="html: homeScore"></span>-<span data-bind="html: awayScore"></span></div>
                <div class="col-xs-5 col-sm-4 col-md-4 truncate" style="text-align: center;">
                    [A] {{ $match->awayTeam->name }}
                </div>
            </div>
        </div>
        <div>
            <a class="btn btn-default" href="{{ $redirectUrl }}"  >Back</a>
        </div>
    </div>

    <script type="text/html" id="gameTemplate">
        <div class="row game">
            <div class="col-xs-5 col-sm-4 col-md-4 players home truncate">
                <div data-bind="if: forfeitedBy == 'home'" class="forfeit">Forfeit</div>
                <div data-bind="if: homePlayer">
                    <span data-bind="html: homePlayer().name"></span>
                </div>
                <div data-bind="if: homePlayer2">
                    <span data-bind="html: homePlayer2().name"></span>
                </div>
                <div data-bind="if: homePlayer3">
                    <span data-bind="html: homePlayer3().name"></span>
                </div>
            </div>
            <div class="hidden-xs col-sm-1 col-md-1 result home">
                <div data-bind="if: winner() == 'away'"><span class="label label-danger">L</span></div>
                <div data-bind="if: winner() == 'home'"><span class="label label-success">W</span></div>
            </div>
            <div class="col-xs-2 col-sm-2 col-md-2 result home truncate">
                <div><span data-bind="html: homeScore"></span>-<span data-bind="html: awayScore"></span></div>
            </div>
            <div class="hidden-xs col-sm-1 col-md-1 result away">
                <div data-bind="if: winner() == 'away'"><span class="label label-success">W</span></div>
                <div data-bind="if: winner() == 'home'"><span class="label label-danger">L</span></div>
            </div>
            <div class="col-xs-4 col-sm-4 col-md-4 players away truncate">
                <div data-bind="if: forfeitedBy == 'away'" class="forfeit">Forfeit</div>
                <div data-bind="if: awayPlayer">
                    <span data-bind="html: awayPlayer().name"></span>
                </div>
                <div data-bind="if: awayPlayer2">
                    <span data-bind="html: awayPlayer2().name"></span>
                </div>
                <div data-bind="if: awayPlayer3">
                    <span data-bind="html: awayPlayer3().name"></span>
                </div>
            </div>
            <div class=" col-sm-12 col-md-12" data-bind="foreach: awards">
                <div class="center-block" style="text-align: center">
                    <strong><span data-bind="html: awardType"></span></strong>
                    <span data-bind="if: awardValue() !== '' && awardValue() !== 0"> (<span data-bind="html: awardValue"></span>) </span>
                    <span data-bind="html: player().name"></span>
                </div>
            </div>
        </div>
    </script>
@stop

@section('scripts')
    <script src="{{ asset('scripts/lodash.min.js') }}"></script>
    <script src="{{ asset('scripts/postal.min.js') }}"></script>
    <script type="text/javascript" src="//cdnjs.cloudflare.com/ajax/libs/knockout/3.3.0/knockout-min.js" ></script>
    <script>
        (window.ko || document.write("<script src='{{ asset('scripts/knockout-2.2.0.js') }}'><\/script>"));
    </script>
    <script>
        (function(w){
            w.scoreCardConfig = {
                seasonId: "{{ $seasonId }}",
                matchId: "{{ $match->id }}",
                match: {!! $matchResults !!},
                awayPlayers: {!! $awayPlayers !!},
                homePlayers: {!! $homePlayers !!}
            };
        })(window);
    </script>
    <script src="{{ asset('jsapps/scoresheet-edit.js') }}"></script>

    <script>
        (function($){
            $(document).ready(function($) {
                $("#matchesThisWeek").change(function() {
                    window.document.location = $('#matchesThisWeek').val();
                });
            });
        })(jQuery);
    </script>
@stop
