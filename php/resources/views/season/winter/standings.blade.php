<div class="panel panel-default">
    <div class="panel-heading">
        <h3 class="panel-title">{{$season->name}}
            @if($seasonPart == 'pre')
                Pre Season
            @else
                Regular Season
            @endif</h3>
    </div>
    <table class="table table-striped table-hover">
        <tbody>
        @foreach($divStandings as $divStanding)
            <tr style=" background-color: #7FC3FF; font-weight: bold;">
                <td>Division {{ $divStanding->name }}</td>
                @if($season->isUsingMatchPoints)
                <td style="text-align: center;">P</td>
                @endif
                <td style="text-align: center;">W</td>
                <td style="text-align: center;">L</td>
                <td style="text-align: center;">%</td>
            </tr>
            @foreach($divStanding->standings as $standing)
                <tr>
                    <td><a href="{{route('team.show', ['id' => $standing->teamId])}}">{{ $standing->name }}</a></td>
                    @if($season->isUsingMatchPoints)
                    <td style="text-align: center; vertical-align: middle;">{{ $standing->matchPoints }}</td>
                    @endif
                    <td style="text-align: center; vertical-align: middle;">{{ $standing->wonPoints }}</td>
                    <td style="text-align: center; vertical-align: middle;">{{ $standing->lossPoints }}</td>
                    <td style="text-align: center; vertical-align: middle;">{{ round($standing->percentage * 100, 1) }}%</td>
                </tr>
            @endforeach
        @endforeach
        </tbody>
    </table>
</div>