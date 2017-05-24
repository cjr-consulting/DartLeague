<?php

namespace TrentonDarts\Http\ViewComposers;

use Auth;

use Illuminate\Contracts\View\View;
use TrentonDarts\LeagueManagement\Models\BoardMember;
use TrentonDarts\LeagueManagement\Models\WinterSeason;

class NavComposer
{
    public function compose(View $view)
    {
        $navType = "default";

        if ($view->offsetExists('navtype')) {
            $navType = $view->offsetGet('navtype');
        }

        switch($navType){
            case "dartsfordreams":
                $nav = $this->getDartsForDreamsNav();
                break;
            case "manageleague":
                $nav = $this->getManageLeagueNav();
                break;
            default:
                $nav = $this->getDefaultNav();
                break;
        }

        $models = ['navdata' => $nav];
        if (Auth::user()) {
            $userNav = $this->getUserNav(Auth::user());
            $models['usernav'] = $userNav;
        }

        $view->with($models);
    }

    private function getUserNav($user)
    {
        $navItems = [
            new NavLink('Logout', asset('auth/logout'))
        ];
        if(BoardMember::where('userId', $user->id)->count() > 0)
        {
            $navItems = array_merge($navItems, [
                new NavLink('Manage', '', [], true),
                new NavLink('League', asset('manage/1'))
                ]);
        }

        return new UserNav($user->name, $navItems);
    }

    private function getDartsForDreamsNav()
    {
        return [
            new NavLink('Darts for Dreams 12 -  April 29th, 2017', '#',
            [
                new NavLink('Event Flyer',
                  asset('documents/events/2016-2017/flier17.pdf')),
                new NavLink('GTDL Player letter',
                  asset('documents/events/2016-2017/playerletter17.pdf')),
                new NavLink('Player Pledge Sheet',
                    asset('documents/events/2016-2017/pledge17.pdf')),
                new NavLink('Paperwork for Sponsors',
                    asset('documents/events/2016-2017/sponsorpackage17.pdf')),
                new NavLink('Current Donation List',
                    asset('documents/events/2016-2017/donationreport.pdf')),
                new NavLink('Online Donation Form!!',
                    'http://site.wish.org/site/TR/FriendsandFamily/Make-A-WishNewJersey?px=3100639&pg=personal&fr_id=2340', []),
                new NavLink('What\'s a dart-a-thon',
                    asset('documents/events/static/whatisadartathon.pdf'))
            ]),
            new NavLink('History of Events', asset('old/dfdhistory')),
            new NavLink('Return to GTDL', asset('/'))
            ];
    }

    private function getDefaultNav()
    {
        $season = WinterSeason::where('isCurrent', 1)->firstOrFail();
        return [
            new NavLink('Current', '#',
                [
                    new NavLink('Weekly Standings', route('season.show', ['id' => $season->id])),
                    new NavLink('Full Schedule', route('season.schedule', ['seasonId' => $season->id])),
                    new NavLink('Stats', route('season.stats', ['seasonId' => $season->id])),
                    new NavLink('Leaderboards', route('season.leaderboard', ['seasonId' => $season->id])),
                    new NavLink('Awards', route('season.awards', ['seasonId' => $season->id])),
                    new NavLink('Teams', route('team.index'))
                ]),
            new NavLink('Activities and Events', '#',
                [
                    new NavLink('Memorial Tournament Bracket', asset('documents/events/current/memorialtournamentbracket.pdf')),
                    new NavLink('Memorial Tournament Info', asset('documents/events/current/memorialtournamentnotes.pdf')),
                    new NavLink('Mr. Trenton bracket', asset('documents/events/current/mrtrentonbracket.pdf')),
                    new NavLink('Mr. Trenton rules', asset('documents/events/current/mrtrentonnotes.pdf')),
                    new NavLink('All Star Qualifying Info', asset('documents/events/current/allstars.pdf')),
                    //new NavLink('Winter Singles League', 'http://jerseydarts.com/jerseydartscom501league/index2.cfm?leagueid=11&seasonid=71'),
                    new NavLink('Winter Singles League', 'http://app.dartconnect.com/dctvlistings/listings.html#l-gtdls'),
                    new NavLink('Summer Singles Weekly', asset('documents/seasons/current/rptWeeklyReport.pdf')),
                    new NavLink('Summer Singles Schedule', asset('documents/seasons/current/rptFullSchedule.pdf')),
                    new NavLink('NJ State Cricket Championship', asset('documents/events/current/cricketchampionships.pdf')),
                    new NavLink('Darts for Dreams Info', asset('old/charity')),
                    new NavLink('GTDL Player Results at Events', route('event.results'))
                ]),
            new NavLink('League', '#',
                [
                    new NavLink('Where we Play', route('team.index')),
                    new NavLink('Sponsors and Partners', route('sponsor.list')),
                    new NavLink('Darts in the region', asset('old/associations?ai=2')),
                    new NavLink('Board Members', asset('old/board')),
                    new NavLink('Contact',asset(''))
                ]),
            new NavLink('History', '#',
                [
                    new NavLink('2015 - 2016', asset('old/gtdlhistory?syear=2015')),
                    new NavLink('2014 - 2015', asset('old/gtdlhistory?syear=2014')),
                    new NavLink('2013 - 2014', asset('old/gtdlhistory?syear=2013')),
                    new NavLink('2012 - 2013', asset('old/gtdlhistory?syear=2012')),
                    new NavLink('2011 - 2012', asset('old/gtdlhistory?syear=2011')),
                    new NavLink('2010 - 2011', asset('old/gtdlhistory?syear=2010')),
                    new NavLink('2009 - 2010', asset('old/gtdlhistory?syear=2009')),
                ]),
            new NavLink('Other', '#',
                [
                    new NavLink('League Rules', asset('documents/static/gtdlrules.pdf')),
                    new NavLink('Sponsor Paperwork', asset('documents/static/gtdlsponsors.pdf')),
                    new NavLink('Player Registration', asset('documents/static/gtdlplayers.pdf')),
                    new NavLink('Roster Sheet', asset('documents/static/roster.pdf')),
                    new NavLink('Scoresheet', asset('documents/static/scoresheet.pdf')),
                    new NavLink('Chalker Guidelines', asset('documents/static/chalking.pdf')),
                    new NavLink('01 Strategy', asset('documents/static/playersseries1.pdf')),
                    new NavLink('Cricket Strategy', asset('documents/static/playersseries2.pdf'))
                ])
        ];
    }

    private function getManageLeagueNav()
    {
        $leagueId = 1;
        return [
            new NavLink('Home', asset('manage/1')),
            new NavLink('Site', '#',
                [
                    new NavLink('Dart Events', route('manage.site.dartevent.index', ['leagueId' => $leagueId])),
                    new NavLink('Page Content', route('manage.site.pagepart.index', ['leagueId' => $leagueId]))
                ]),
            new NavLink('League', '#',
                [
                    new NavLink('Players', route('manage.player.index', ['leagueId' => $leagueId])),
                    new NavLink('Teams', route('manage.team.index', ['leagueId' => $leagueId])),
                    new NavLink('Sponsors', route('manage.sponsor.index', ['leagueId' => $leagueId])),
                    new NavLink('Board Members', route('manage.boardmember.index', ['leagueId' => $leagueId])),
                ]),
            new NavLink('Seasons', route('manage.season.index', ['leagueId' => $leagueId]))
            ];
    }
}

class UserNav
{
    public $userName = '';
    public $SubNavLinks = [];

    public function __construct($userName, $subNavLinks)
    {
        $this->userName = $userName;
        $this->SubNavLinks = $subNavLinks;
    }
}

class NavLink
{
    public $Title = '';
    public $Link = '';
    public $SubNavLinks = [];
    public $IsHeader = false;
    public $IsSeperator = false;

    public function __construct($title, $link, $subNavLinks = [], $isHeader = false, $isSeperator = false)
    {
        $this->Title = $title;
        $this->Link = $link;
        $this->SubNavLinks = $subNavLinks;
        $this->IsHeader = $isHeader;
        $this->IsSeperator = $isSeperator;
    }
}
