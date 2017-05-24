@extends('layouts.master')

@section('content')
<div class="margin-right: 15px;margin-left: 15px;">
    <div id="scoresheet">
        <h2>Season {{ $season->name }}</h2>
        <div class="container-fluid">
            {!! Form::select('matchesThisWeek', $matchesThisWeek, $selectedMatchRoute, ['id' => 'matchesThisWeek', 'class' => 'form-control', 'placeholder' => 'Select Match']) !!}
            <h3>{{ $match->awayTeam->name }} <strong>@ {{ $match->homeTeam->name }}</strong></h3>
        </div>
        <div class="container-fluid" data-bind="visible: !hasScorecard()">
            <div class="row header">
                <div class="col-xs-4 col-md-4 truncate" style="text-align: center;">
                    [H] {{ $match->homeTeam->name }}
                </div>
                <div class="col-xs-3 col-md-4" style="text-align: center;">
                    <div class="checkbox"><label><input type="checkbox" data-bind="checked: hasScorecard"/>Has Score Card</label></div>
                    <div class="form-inline">
                        <input class="form-control" style="width: 50px;" type="text" data-bind="value: homeScoreOverride"/> -
                        <input class="form-control" style="width: 50px;" type="text" data-bind="value: awayScoreOverride"/>
                    </div>
                </div>
                <div class="col-xs-4 col-md-4 truncate" style="text-align: center;">
                    [A] {{ $match->awayTeam->name }}
                </div>
            </div>
        </div>
        <div class="container-fluid" data-bind="visible: hasScorecard()">
            <div class="row header">
                <div class="col-xs-4 col-md-4 truncate" style="text-align: center;">
                    [H] {{ $match->homeTeam->name }}
                </div>
                <div class="col-xs-3 col-md-4" style="text-align: center;">
                    <div class="checkbox"><label><input type="checkbox" data-bind="checked: hasScorecard"/>Has Score Card</label></div>
                    <span data-bind="html: homeScore"></span>-<span data-bind="html: awayScore"></span>
                </div>
                <div class="col-xs-4 col-md-4 truncate" style="text-align: center;">
                    [A] {{ $match->awayTeam->name }}
                </div>
            </div>
        </div>
        <div class="container-fluid score-sheet" data-bind="foreach: gameGroups, visible: hasScorecard()">
            <div class="row">
                <div class="col-md-12 game-group-header" data-bind="html: name">
                </div>
            </div>
            <div class="row" data-bind="template: {name: template, foreach: games, as: 'game'}">
            </div>
        </div>
        <div class="container-fluid" data-bind="visible: hasScorecard()">
            <div class="row footer">
                <div class="col-xs-5 col-md-4 truncate" style="text-align: center;">
                    [H] {{ $match->homeTeam->name }}
                </div>
                <div class="col-md-1"><button class="btn btn-warning" data-bind="click: scoreAllHome" title="Select win for all home teams">H</button></div>
                <div class="col-xs-2 col-md-2" style="text-align: center;"><span data-bind="html: homeScore"></span>-<span data-bind="html: awayScore"></span></div>
                <div class="col-md-1"><button class="btn btn-warning" data-bind="click: scoreAllAway" title="Select win for all away teams">A</button></div>
                <div class="col-xs-5 col-md-4 truncate" style="text-align: center;">
                    [A] {{ $match->awayTeam->name }}
                </div>
            </div>
        </div>
        <div>
            <button class="btn btn-default" data-bind="click: saveMatch">Save</button>
            <button class="btn btn-danger" data-bind="click: cancelMatchEdit">Cancel</button>
        </div>
    </div>

    <script type="text/html" id="gameSinglePlayer">
        <div class="row game">
            <div class="col-md-4 players home">
                <select class="form-control" data-bind="options: game.homePlayers, optionsText: 'name', optionsCaption: 'Choose Player', value: game.homePlayer"></select>
            </div>
            <div class="col-md-1 result home">
                <input type="radio" tabindex="-1" data-bind="checked: game.winner, enable: game.homeScoreable" value="home"/>
            </div>
            <div class="col-md-2 result home">
                <div><span data-bind="html: game.homeScore"></span> - <span data-bind="html: game.awayScore"></span></div>
                <div><button class="btn btn-default" data-bind="click: game.newAward" tabindex="-1" title="New Award"><i class="fa fa-cube"></i></button></div>
            </div>
            <div class="col-md-1 result away">
                <input type="radio" tabindex="-1" data-bind="checked: game.winner, enable: game.awayScoreable" value="away"/>
            </div>
            <div class="col-md-4 players away">
                <select class="form-control" data-bind="options: game.awayPlayers, optionsText: 'name', optionsCaption: 'Choose Player', value: game.awayPlayer"></select>
            </div>

            <div class="col-md-12" data-bind="foreach: game.awards">
                <div class="center-block" style="text-align: center">
                    <strong><span data-bind="html: awardType"></span></strong>
                    <span data-bind="if: awardValue() !== '' && awardValue() !== 0"> (<span data-bind="html: awardValue"></span>) </span>
                    <span data-bind="html: player().name"></span>
                    <button class="btn btn-xs btn-danger" data-bind="click: game.deleteAward"><i class="fa fa-minus-square"></i></button>
                </div>
            </div>
        </div>
    </script>

    <script type="text/html" id="gameDoublePlayer">
        <div class="row game">
            <div class="col-md-4 players home" data-bind="css: { playerWarning: game.homePlayerMessage() !== '' }">
                <span data-bind="html: game.homePlayerMessage"></span>
                <select class="form-control" data-bind="options: game.homePlayers, optionsText: 'name', optionsCaption: 'Choose Player', value: game.homePlayer"></select>
                <select class="form-control" data-bind="options: game.homePlayers, optionsText: 'name', optionsCaption: 'Choose Player', value: game.homePlayer2"></select>
            </div>
            <div class="col-md-1 result home">
                <input type="radio" tabindex="-1" data-bind="checked: game.winner, enable: game.homeScoreable" value="home"/>
            </div>
            <div class="col-md-2 result home">
                <div><span data-bind="html: game.homeScore"></span> - <span data-bind="html: game.awayScore"></span></div>
                <div><button class="btn btn-default" tabindex="-1" data-bind="click: game.newAward" title="New Award"><i class="fa fa-cube"></i></button></div>
            </div>
            <div class="col-md-1 result away">
                <input type="radio" tabindex="-1" data-bind="checked: game.winner, enable: game.awayScoreable" value="away"/>
            </div>
            <div class="col-md-4 players away" data-bind="css: { playerWarning: game.awayPlayerMessage() !== '' }">
                <span data-bind="html: game.awayPlayerMessage"></span>
                <select class="form-control" data-bind="options: game.awayPlayers, optionsText: 'name', optionsCaption: 'Choose Player', value: game.awayPlayer"></select>
                <select class="form-control" data-bind="options: game.awayPlayers, optionsText: 'name', optionsCaption: 'Choose Player', value: game.awayPlayer2"></select>
            </div>
            <div class="col-md-12" data-bind="foreach: game.awards">
                <div class="center-block" style="text-align: center">
                    <strong><span data-bind="html: awardType"></span></strong>
                    <span data-bind="if: awardValue() !== '' && awardValue() !== 0"> (<span data-bind="html: awardValue"></span>) </span>
                    <span data-bind="html: player().name"></span>
                    <button class="btn btn-xs btn-danger" data-bind="click: game.deleteAward"><i class="fa fa-minus-square"></i></button>
                </div>
            </div>
        </div>
    </script>

    <script type="text/html" id="gameTriplePlayer">
        <div class="row game">
            <div class="col-md-4 players home" data-bind="css: { playerWarning: game.homePlayerMessage() !== '' }">
                <span data-bind="html: game.homePlayerMessage"></span>
                <select class="form-control" data-bind="options: game.homePlayers, optionsText: 'name', optionsCaption: 'Choose Player', value: game.homePlayer"></select>
                <select class="form-control" data-bind="options: game.homePlayers, optionsText: 'name', optionsCaption: 'Choose Player', value: game.homePlayer2"></select>
                <select class="form-control" data-bind="options: game.homePlayers, optionsText: 'name', optionsCaption: 'Choose Player', value: game.homePlayer3"></select>
            </div>
            <div class="col-md-1 result home">
                <input type="radio" tabindex="-1" data-bind="checked: game.winner, enable: game.homeScoreable" value="home"/>
            </div>
            <div class="col-md-2 result home">
                <div><span data-bind="html: game.homeScore"></span> - <span data-bind="html: game.awayScore"></span></div>
                <div><button class="btn btn-default" tabindex="-1" data-bind="click: game.newAward" title="New Award"><i class="fa fa-cube"></i></button></div>
            </div>
            <div class="col-md-1 result away">
                <input type="radio" tabindex="-1" data-bind="checked: game.winner, enable: game.awayScoreable" value="away"/>
            </div>
            <div class="col-md-4 players away" data-bind="css: { playerWarning: game.awayPlayerMessage() !== '' }">
                <span data-bind="html: game.awayPlayerMessage"></span>
                <select class="form-control" data-bind="options: game.awayPlayers, optionsText: 'name', optionsCaption: 'Choose Player', value: game.awayPlayer"></select>
                <select class="form-control" data-bind="options: game.awayPlayers, optionsText: 'name', optionsCaption: 'Choose Player', value: game.awayPlayer2"></select>
                <select class="form-control" data-bind="options: game.awayPlayers, optionsText: 'name', optionsCaption: 'Choose Player', value: game.awayPlayer3"></select>
            </div>
            <div class="col-md-12" data-bind="foreach: awards">
                <div class="center-block" style="text-align: center">
                    <strong><span data-bind="html: awardType"></span></strong>
                    <span data-bind="if: awardValue() !== '' && awardValue() !== 0"> (<span data-bind="html: awardValue"></span>) </span>
                    <span data-bind="html: player().name"></span>
                    <button class="btn btn-xs btn-danger" data-bind="click: game.deleteAward"><i class="fa fa-minus-square"></i></button>
                </div>
            </div>
        </div>
    </script>
</div>
    <div class="modal fade" id="awardModal" tabindex="-1" role="dialog" aria-labelledby="awardModalLabel">
        <div id="awardForm">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="myModalLabel">Award</h4>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <label>Player</label>
                            <select class="form-control" data-bind="options: gamePlayers, optionsText: 'name', optionsCaption: 'Choose Player', value: player"></select>
                        </div>
                        <div class="form-group">
                            <label>Award</label>
                            <select class="form-control" data-bind="options: awardTypes, value: awardType"></select>
                        </div>
                        <div class="form-group">
                            <label>Value</label>
                            <input type="text" class="form-control" data-bind="value: awardValue"/>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        <button type="button" class="btn btn-primary" data-bind="click: save">Save changes</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
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
                homePlayers: {!! $homePlayers !!},
                redirectUrl: "{!! $redirectUrl !!}"
            };
        })(window);
    </script>
    <script src="{{ asset('jsapps/scoresheet-edit.js?v=1.1') }}"></script>

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