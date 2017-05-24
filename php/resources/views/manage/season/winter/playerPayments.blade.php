@extends('layouts.managemaster')

@section('content')
    <div class="single-form seasons-management">
        <h2>Season {{ $season->name }}</h2>
        <div class="panel panel-default">
            <div class="panel-heading"><strong>Players for {{ $seasonTeam->team->name }}</strong></div>
            <div class="table-responsive">
                <table class="table table-striped table-hover">
                    <thead>
                    <tr>
                        <th width="100px"></th>
                        <th>Team</th>
                        <th style="text-align: center">Team Status</th>
                    </tr>
                    </thead>
                    <tbody>
                    @if($teamPlayers->isEmpty())
                        <tr class="warning"><td colspan="6">Currently No Teams</td></tr>
                    @else
                        @foreach($teamPlayers->sortBy(function($tplayer){return $tplayer->player->name;}) as $teamPlayer)
                            <tr>
                                <td>
                                    <div class="btn-group" role="group">
                                        <button class="btn btn-default" data-toggle="modal" data-target="#paymentModal" data-playerid="{{ $teamPlayer->playerId }}" data-playername="{{ $teamPlayer->player->name }}" data-paymentstatus="{{$teamPlayer->paymentStatus}}" title="{{ $teamPlayer->player->name }}"><i class="fa fa-pencil-square-o fa-lg"></i></button>
                                    </div>
                                </td>
                                <td>{{ $teamPlayer->player->name }}</td>
                                <td style="text-align: center">
                                    <span class="label label-{{$teamPlayer->paymentStatus == 'Unpaid' ? 'danger' : 'success'}}">{{ $teamPlayer->paymentStatus }}</span>
                                </td>
                            </tr>
                        @endforeach
                    @endif
                    </tbody>
                </table>
            </div>
        </div>
        <a class="btn btn-link" href="{{route('manage.seasonTeamPayments', ['leagueId' => $leagueId, 'id' => $season->id]) }}" title="Back">back to season</a>
    </div>

    <div class="modal fade" id="paymentModal" tabindex="-1" role="dialog" aria-labelledby="paymentModalLabel">
        <div id="awardForm">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    {!! Form::model($payment, ['route' => ['manage.seasonPlayerPayments.update', $leagueId, $season->id, $teamId], 'method' => 'post', 'class' => 'single-form']) !!}
                    <input type="hidden" id="playerId" name="playerId"/>
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="myModalLabel">Payment</h4>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <label>Payment Status</label>
                            <select id="paymentStatus" name="paymentStatus" class="form-control">
                                <option>Select Status</option>
                                <option value="Unpaid">Unpaid</option>
                                <option value="Paid">Paid</option>
                                <option value="Board Member">Board Member</option>
                                <option value="Earned">Earned</option>
                            </select>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        <button type="submit" class="btn btn-primary">Save changes</button>
                    </div>
                    {!! Form::close() !!}
                </div>
            </div>
        </div>
    </div>
@stop

@section('scripts')
    <script>
        $('#paymentModal').on('show.bs.modal', function (event) {
            var button = $(event.relatedTarget);
            var playerId = button.data('playerid');
            var playerName = button.data('playername');
            var paymentStatus = button.data('paymentstatus');
            document.getElementById('paymentStatus').value = paymentStatus;

            var modal = $(this)
            modal.find('#myModalLabel').text('Payment for ' + playerName)
            modal.find('#playerId').val(playerId)
        })
    </script>
@stop