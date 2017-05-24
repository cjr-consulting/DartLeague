@extends('layouts.master')

@section('content')
    <style media="print" type="text/css">
        @page {size: landscape;}
    </style>
    <div class="season-stats">
        <h3>Season {{ $season->name }}</h3>
        <div style="margin-right: 15px;margin-left: 15px;">
            <div class="row">
                <div class="col-xs-12 col-sm-3">
                    <div class="form-group">
                        <label for="seasonPart">Season Part</label>
                        {!! Form::select('seasonPart', array('whole' => 'Whole Season', 'pre' => 'Pre Season', 'regular' => 'Regular Season'), $seasonPart, ['id' => 'seasonPart', 'class' => 'form-control']) !!}
                    </div>
                </div>
                <div class="col-xs-12 col-sm-3">
                    <div class="form-group">
                        <label for="division">Division</label>
                        {!! Form::select('division', $divisions, $division, ['id' => 'division', 'class' => 'form-control', 'placeholder' => 'All Divisions']) !!}
                    </div>
                </div>
                <div class="col-xs-12 col-sm-3">
                    <a href="{{route('season.awardsExport', ['id' => $seasonId])}}">Export All Awards</a>
                </div>
                <!--<div class="col-xs-12 col-sm-3">
                    <div class="form-group">
                        <label for="awardTypes">Award Types</label>
                        {!! Form::select('awardTypes', $awardTypes, $awardType, ['id' => 'awardTypes', 'class' => 'form-control', 'placeholder' => 'Select Type']) !!}
                    </div>
                </div>-->
            </div>
        </div>
        <div class="table-responsive">
            <table class="table table-condensed table-striped table-hover">
                <thead>
                <tr>
                    <th>Player Name</th>
                    <th>Team Name</th>
                    <th style="text-align: center;">Season Part</th>
                    <th style="text-align: center;">Division</th>
                    <th style="text-align: center;">Date</th>
                    <th style="text-align: center;">Award</th>
                    <th style="text-align: center;">Value</th>
                </tr>
                </thead>
                <tbody>
                @if($awards->isEmpty())
                    <tr class="warning">
                        <td colspan="6">Currently No Stats</td>
                    </tr>
                @else
                    @foreach($awards as $award)
                        <tr>
                            <td><a href="{{route('player.show', ['id' => $award->playerId, 'season' => $seasonId])}}">{{$award->playerName}}</a></td>
                            <td><a href="{{route('team.show', ['id' => $award->teamId, 'season' => $seasonId])}}">{{$award->teamName}}</a></td>
                            <td style="text-align: center;">@if($award->seasonPart == 'pre'){{ 'Pre' }}@else{{ 'Regular' }}@endif</td>
                            <td style="text-align: center;">{{ $award->division }}</td>
                            <td style="text-align: center;">{{ date('m-d-Y', strtotime($award->date)) }}</td>
                            <td style="text-align: center;">{{  $award->awardType }}</td>
                            <td style="text-align: center;">@if($award->value > 0){{ $award->value }}@endif</td>
                        </tr>
                    @endforeach
                @endif
                </tbody>
            </table>
        </div>
    </div>
@stop

@section('scripts')
    <script>
        (function($){
            $(document).ready(function($) {
                $(".clickable-row").click(function() {
                    window.document.location = $(this).data("href");
                });
                $("#seasonPart").change(function() {
                    window.document.location = buildQuery();
                });
                $("#division").change(function() {
                    window.document.location = buildQuery();
                });
                $("#awardType").change(function() {
                    window.document.location = buildQuery();
                });
            });

            function buildQuery()
            {
                var query = '?',
                        seasonPart = $('#seasonPart').val(),
                        division = $('#division').val(),
                        awardType = $('#awardType').val();

                query = query + 'seasonPart=' + encodeURI(seasonPart);
                if(awardType) {
                    query = query + '&awardType=' + encodeURI(awardType);
                }

                if(division){
                    query = query + '&division=' + encodeURI(division);
                }

                return query;

            }
        })(jQuery);
    </script>
@stop
