<div class="panel panel-default">
    <div class="panel-heading">
        @if($viewAllAwards)
            <h3 class="panel-title">Awards <small><a href="{{route('season.show', ['id' => $seasonId, 'week' => $resultWeekDate])}}">View weekly awards</a></small></h3>
        @else
            <h3 class="panel-title">Awards on {{  date('m-d-Y', strtotime($resultWeekDate)) }} <small><a href="{{route('season.show', ['id' => $seasonId, 'week' => $resultWeekDate, 'allAwards' => true])}}">View all awards</a></small></h3>
        @endif
    </div>
    <table class="table table-striped table-hover table-condensed">
        <tbody>
        {{ $awardType = '' }}
        @if($awards->isEmpty())
            <tr class="no-games"><td>No Awards</td></tr>
        @endif
        @foreach($awards as $award)
            @if($award->awardType != $awardType)
                <tr style="text-align: center; background-color: #7FC3FF; font-weight: bold;"><td colspan="4">{{ $awardType = $award->awardType }}</td></tr>
            @endif
            <tr>
                <td style="text-align: center; vertical-align: middle;">@if($award->value > 0){{ $award->value }}@endif</td>
                <td style="vertical-align: middle;">{{ $award->playerName }}</td>
                <td style="vertical-align: middle;">{{ $award->teamName }}</td>
                <td style="text-align: center; vertical-align: middle;">{{ date('m-d', strtotime($award->date)) }}</td>
            </tr>
        @endforeach
        </tbody>
    </table>
</div>