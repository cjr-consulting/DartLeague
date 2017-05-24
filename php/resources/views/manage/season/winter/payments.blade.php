@extends('layouts.managemaster')

@section('content')
    <div class="single-form seasons-management">
        <h2>Teams for Season {{ $season->name }}</h2>
        <div class="panel panel-default">
            <div class="panel-heading"> <strong>Team</strong></div>
            <div class="table-responsive">
                <table class="table table-striped table-hover">
                    <thead>
                    <tr>
                        <th width="100px"></th>
                        <th>Team</th>
                        <th style="text-align: center">Team Status</th>
                        <th style="text-align: center">Players</th>
                    </tr>
                    </thead>
                    <tbody>
                    @if($seasonTeams->isEmpty())
                        <tr class="warning"><td colspan="6">Currently No Teams</td></tr>
                    @else
                        @foreach($seasonTeams->sortBy(function($team){return $team->team->name;}) as $seasonTeam)
                            <tr>
                                <td>
                                    <div class="btn-group" role="group">
                                        <button class="btn btn-default" data-toggle="modal" data-target="#paymentModal" data-teamid="{{ $seasonTeam->team->id }}" data-teamname="{{ $seasonTeam->team->name }}" data-paymentstatus="{{$seasonTeam->paymentStatus}}" title="{{ $seasonTeam->team->name }}"><i class="fa fa-pencil-square-o fa-lg"></i></button>
                                        <a class="btn btn-primary" href="{{ route('manage.seasonPlayerPayments', ['leagueId' => $leagueId, 'seasonId' => $season->id, 'teamId' => $seasonTeam->team->id]) }}"><i class="fa fa-eye"></i></a>
                                    </div>
                                </td>
                                <td>{{ $seasonTeam->team->name }}</td>
                                <td style="text-align: center">
                                    <span class="label label-{{$seasonTeam->paymentStatus == 'Unpaid' ? 'danger' : 'success'}}">{{ $seasonTeam->paymentStatus }}</span></td>
                                <td style="text-align: center">
                                    @if($seasonTeam->outstandingPlayerPayments > 0)
                                        <span class="badge">{{ $seasonTeam->outstandingPlayerPayments }}</span>
                                    @else
                                        <span class="label label-success">All Paid</span>
                                    @endif
                                </td>
                            </tr>
                        @endforeach
                    @endif
                    </tbody>
                </table>
            </div>
        </div>
        <a class="btn btn-link" href="{{route('manage.season.show', ['leagueId' => $leagueId, 'id' => $season->id]) }}" title="Back">back to season</a>
    </div>

    <div class="modal fade" id="paymentModal" tabindex="-1" role="dialog" aria-labelledby="paymentModalLabel">
        <div id="awardForm">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    {!! Form::model($payment, ['route' => ['manage.seasonTeamPayment.update', $leagueId, $season->id], 'method' => 'post', 'class' => 'single-form']) !!}
                    <input type="hidden" id="teamId" name="teamId"/>
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
        var teamId = button.data('teamid');
        var teamName = button.data('teamname');
        var paymentStatus = button.data('paymentstatus');
        document.getElementById('paymentStatus').value = paymentStatus;

        var modal = $(this)
        modal.find('#myModalLabel').text('Payment for ' + teamName)
        modal.find('#teamId').val(teamId)
    })
</script>
@stop