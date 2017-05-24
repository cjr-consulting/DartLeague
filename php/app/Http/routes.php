<?php

/*
|--------------------------------------------------------------------------
| Application Routes
|--------------------------------------------------------------------------
|
| Here is where you can register all of the routes for an application.
| It's a breeze. Simply tell Laravel the URIs it should respond to
| and give it the controller to call when that URI is requested.
|
*/
use Illuminate\Http\Request;

/* Test routes */

Route::get('/testemail', function(){
    Mail::send('emails.sample', [], function($message) {
        $message->from('noreply@trentondarts.com');

        $message->to('john.meade@cjrconsultingllc.com', 'John Meade')
        ->subject('Sample Test Email');
    });
    return redirect('home');
});

Route::get('/errortest', function() {
    throw new Exception('Its dead jim.');
    return view('secured');
});

Route::get('/secured', ['middleware' => 'auth', function() {
    return view('secured');
}]);

Route::get('/app', ['middleware' => 'auth', function(){
    return view('app');
}]);

Route::get('/', ['as' => 'home', 'uses' => 'HomeController@index']);
Route::get('/home', ['uses' => 'HomeController@index']);

Route::post('/sms/reply', ['as' => 'sms.reply', function() {
    $content = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n";
    $content .= "<Response><Sms>Thanks for sending a message to Trenton Darts.</Sms></Response>";
    return response($content)
        ->header('content-type', 'text/xml');
}]);

Route::get('sponsor', ['as' => 'sponsor.list', 'uses' => 'LeagueController@sponsors']);

Route::get('/file/{id}', ['as' => 'file.get', 'uses' => 'FileController@viewFile']);

Route::get('/event/results', ['as' => 'event.results', 'uses' => 'EventsController@allResults']);

Route::resource('player', 'PlayerController',
    ['names' => ['index' => 'player.index',
        'create' => 'player.create',
        'store' => 'player.store',
        'show' => 'player.show',
        'edit' => 'player.edit',
        'update' => 'player.update',
        'destroy' => 'player.destroy']]);

Route::resource('team', 'TeamController',
    ['names' => ['index' => 'team.index',
        'create' => 'team.create',
        'store' => 'team.store',
        'show' => 'team.show',
        'edit' => 'team.edit',
        'update' => 'team.update',
        'destroy' => 'team.destroy']]);

Route::resource('season', 'Season\Winter\SeasonController',
    ['names' => ['index' => 'season.index',
        'create' => 'season.create',
        'store' => 'season.store',
        'show' => 'season.show',
        'edit' => 'season.edit',
        'update' => 'season.update',
        'destroy' => 'season.destroy']]);

Route::group(['prefix' => '/season/{seasonId}'], function() {
    Route::get('schedule', ['as' => 'season.schedule', 'uses' => 'Season\Winter\SeasonController@schedule']);
    Route::get('stats', ['as' => 'season.stats', 'uses' => 'Season\Winter\SeasonController@stats']);
    Route::get('statsExport', ['as' => 'season.statsExport', 'uses' => 'Season\Winter\SeasonController@statsExport']);
    Route::get('awards', ['as' => 'season.awards', 'uses' => 'Season\Winter\SeasonController@awards']);
    Route::get('awardsExport', ['as' => 'season.awardsExport', 'uses' => 'Season\Winter\SeasonController@awardsExport']);
    Route::get('match/{id}', ['as' => 'season.match.show', 'uses' => 'Season\Winter\MatchController@show']);
    Route::get('match/{id}/edit', ['as' => 'season.match.edit', 'uses' => 'Season\Winter\MatchController@edit']);
    Route::post('match/{id}', ['as' => 'season.match.update', 'uses' => 'Season\Winter\MatchController@update']);

    Route::get('leaderboard', ['as' => 'season.leaderboard', 'uses' => 'Season\Winter\SeasonController@leaderboard']);
});

Route::group(['middleware' => 'auth', 'prefix' => '/manage/{leagueId}'], function() {
    Route::get('', ['as' => 'manage.league', 'uses' => 'Manage\LeagueController@getLeagueDetail']);

    Route::resource('dartevent/{dartEventId}/result', 'Manage\DartEventResultsController',
        ['names' => ['index' => 'manage.site.dartevent.result.index',
            'create' => 'manage.site.dartevent.result.create',
            'store' => 'manage.site.dartevent.result.store',
            'show' => 'manage.site.dartevent.result.show',
            'edit' => 'manage.site.dartevent.result.edit',
            'update' => 'manage.site.dartevent.result.update',
            'destroy' => 'manage.site.dartevent.result.destroy']]);
    Route::get('dartevent/{dartEventId}/result/{id}/delete',
        ['as' => 'manage.site.dartevent.result.delete',
            'uses' => 'Manage\DartEventResultsController@destroy']);

    Route::resource('dartevent', 'Manage\DartEventsController',
        ['names' => ['index' => 'manage.site.dartevent.index',
            'create' => 'manage.site.dartevent.create',
            'store' => 'manage.site.dartevent.store',
            'show' => 'manage.site.dartevent.show',
            'edit' => 'manage.site.dartevent.edit',
            'update' => 'manage.site.dartevent.update',
            'destroy' => 'manage.site.dartevent.destroy']]);
    Route::get('dartevent/{id}/delete', ['as' => 'manage.site.dartevent.delete', 'uses' => 'Manage\DartEventsController@destroy']);
    Route::get('dartevent/{id}/activate', ['as' => 'manage.site.dartevent.activate', 'uses' => 'Manage\DartEventsController@activate']);

    Route::resource('pagepart', 'Manage\PagePartsController',
        ['names' => ['index' => 'manage.site.pagepart.index',
            'create' => 'manage.site.pagepart.create',
            'store' => 'manage.site.pagepart.store',
            'show' => 'manage.site.pagepart.show',
            'edit' => 'manage.site.pagepart.edit',
            'update' => 'manage.site.pagepart.update',
            'destroy' => 'manage.site.pagepart.destroy']]);
    Route::get('pagepart/{id}/delete', ['as' => 'manage.site.pagepart.delete', 'uses' => 'Manage\PagePartsController@destroy']);
    
    Route::resource('team', 'Manage\TeamController',
        ['names' => ['index' => 'manage.team.index',
            'create' => 'manage.team.create',
            'store' => 'manage.team.store',
            'show' => 'manage.team.show',
            'edit' => 'manage.team.edit',
            'update' => 'manage.team.update',
            'destroy' => 'manage.team.destroy']]);
    Route::get('team/{id}/delete', ['as' => 'manage.team.delete', 'uses' => 'Manage\TeamController@destroy']);

    Route::resource('sponsor', 'Manage\SponsorController',
        ['names' => ['index' => 'manage.sponsor.index',
            'create' => 'manage.sponsor.create',
            'store' => 'manage.sponsor.store',
            'show' => 'manage.sponsor.show',
            'edit' => 'manage.sponsor.edit',
            'update' => 'manage.sponsor.update',
            'destroy' => 'manage.sponsor.destroy']]);
    Route::get('sponsor/{id}/delete', ['as' => 'manage.sponsor.delete', 'uses' => 'Manage\SponsorController@destroy']);

    Route::resource('boardmember', 'Manage\BoardMemberController',
        ['names' => ['index' => 'manage.boardmember.index',
            'create' => 'manage.boardmember.create',
            'store' => 'manage.boardmember.store',
            'show' => 'manage.boardmember.show',
            'edit' => 'manage.boardmember.edit',
            'update' => 'manage.boardmember.update',
            'destroy' => 'manage.boardmember.destroy']]);
    Route::get('boardmember/{id}/delete', ['as' => 'manage.boardmember.delete', 'uses' => 'Manage\BoardMemberController@destroy']);

    Route::resource('player', 'Manage\PlayerController',
        ['names' => ['index' => 'manage.player.index',
                    'create' => 'manage.player.create',
                    'store' => 'manage.player.store',
                    'show' => 'manage.player.show',
                    'edit' => 'manage.player.edit',
                    'update' => 'manage.player.update',
                    'destroy' => 'manage.player.destroy']]);
    Route::get('player/{id}/delete', ['as' => 'manage.player.delete', 'uses' => 'Manage\PlayerController@destroy']);

    Route::resource('winterseason', 'Manage\WinterSeason\SeasonController',
        ['names' => ['index' => 'manage.season.index',
                     'create' => 'manage.season.create',
                     'store' => 'manage.season.store',
                     'show' => 'manage.season.show',
                     'edit' => 'manage.season.edit',
                     'update' => 'manage.season.update',
                     'destroy' => 'manage.season.delete']]);

    Route::get('winterseason/{seasonId}/playersexport', ['as' => 'manage.season.players.export', 'uses' => 'Manage\WinterSeason\SeasonController@playerExport']);
    Route::get('winterseason/{seasonId}/teampayment', ['as' => 'manage.seasonTeamPayments', 'uses' => 'Manage\WinterSeason\SeasonController@payments']);
    Route::post('winterseason/{seasonId}/teampayment', ['as' => 'manage.seasonTeamPayment.update', 'uses' => 'Manage\WinterSeason\SeasonController@paymentStore']);
    Route::get('winterseason/{seasonId}/team/{teamId}/payment', ['as' => 'manage.seasonPlayerPayments', 'uses' => 'Manage\WinterSeason\SeasonController@playerPayments']);
    Route::post('winterseason/{seasonId}/team/{teamId}/payment', ['as' => 'manage.seasonPlayerPayments.update', 'uses' => 'Manage\WinterSeason\SeasonController@playerPaymentStore']);
    Route::get('winterseason/{seasonId}/resetStats', ['as' => 'manage.season.resetstats', 'uses' => 'Manage\WinterSeason\SeasonController@updateStats']);

    Route::resource('winterseason/{seasonId}/team', 'Manage\WinterSeason\SeasonTeamController',
        ['names' => ['index' => 'manage.seasonTeam.index',
            'create' => 'manage.seasonTeam.create',
            'store' => 'manage.seasonTeam.store',
            'show' => 'manage.seasonTeam.show',
            'edit' => 'manage.seasonTeam.edit',
            'update' => 'manage.seasonTeam.update',
            'destroy' => 'manage.seasonTeam.delete']]);

    Route::resource('winterseason/{seasonId}/team/{teamId}/player', 'Manage\WinterSeason\SeasonTeamPlayerController',
        ['names' => ['index' => 'manage.seasonTeam.player.index',
            'create' => 'manage.seasonTeam.player.create',
            'store' => 'manage.seasonTeam.player.store',
            'show' => 'manage.seasonTeam.player.show',
            'edit' => 'manage.seasonTeam.player.edit',
            'update' => 'manage.seasonTeam.player.update',
            'destroy' => 'manage.seasonTeam.player.destroy']]);
    Route::get('winterseason/{seasonId}/team/{teamId}/player/{id}/delete', ['as' => 'manage.seasonTeam.player.delete', 'uses' => 'Manage\WinterSeason\SeasonTeamPlayerController@destroy']);

    Route::resource('winterseason/{seasonId}/week', 'Manage\WinterSeason\SeasonWeekController',
        ['names' => ['index' => 'manage.season.week.index',
            'create' => 'manage.season.week.create',
            'store' => 'manage.season.week.store',
            'show' => 'manage.season.week.show',
            'edit' => 'manage.season.week.edit',
            'update' => 'manage.season.week.update',
            'destroy' => 'manage.season.week.destroy']]);
    Route::get('winterseason/{seasonId}/week/{id}/delete', ['as' => 'manage.season.week.delete', 'uses' => 'Manage\WinterSeason\SeasonWeekController@destroy']);

    Route::resource('winterseason/{seasonId}/week/{weekId}/match', 'Manage\WinterSeason\SeasonMatchController',
        ['names' => ['index' => 'manage.season.match.index',
            'create' => 'manage.season.match.create',
            'store' => 'manage.season.match.store',
            'show' => 'manage.season.match.show',
            'edit' => 'manage.season.match.edit',
            'update' => 'manage.season.match.update',
            'destroy' => 'manage.season.match.destroy']]);
    Route::get('winterseason/{seasonId}/week/{weekId}/match/{id}/delete', ['as' => 'manage.season.match.delete', 'uses' => 'Manage\WinterSeason\SeasonMatchController@destroy']);

});

// Old php scripts
Route::get('/old/{oldpage}', function($oldpage) {
    return view('old.'.$oldpage);
});

Route::post('/old/{oldpage}', function($oldpage) {
    return view('old.'.$oldpage);
});

Route::get('/php/{oldpage}', function($oldpage, Request $request) {
    $oldpage = str_replace('.php', '', $oldpage);
    $queryString = $request->getQueryString();
    if($queryString == null)
        return redirect('/old/'.$oldpage);
    return redirect('/old/'.$oldpage.'?'.$queryString);
});

// Authentication routes...
Route::get('auth/login', 'Auth\AuthController@getLogin');
Route::post('auth/login', 'Auth\AuthController@postLogin');
Route::get('auth/logout', 'Auth\AuthController@getLogout');

// Registration routes...
Route::get('auth/register', 'Auth\AuthController@getRegister');
Route::post('auth/register', 'Auth\AuthController@postRegister');

// Password reset link request routes...
Route::get('password/email', ['as' => 'password.reset', 'uses' => 'Auth\PasswordController@getEmail']);
Route::post('password/email', 'Auth\PasswordController@postEmail');

// Password reset routes...
Route::get('password/reset/{token}', 'Auth\PasswordController@getReset');
Route::post('password/reset', 'Auth\PasswordController@postReset');

Route::group(['prefix' => '/api'], function() {
    Route::resource('season', 'Api\SeasonController',
        ['names' => ['index' => 'api.season.index',
            'create' => 'api.season.create',
            'store' => 'api.season.store',
            'show' => 'api.season.show',
            'edit' => 'api.season.edit',
            'update' => 'api.season.update',
            'destroy' => 'api.season.delete']]);
});
