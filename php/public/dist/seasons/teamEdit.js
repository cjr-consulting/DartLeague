System.register(['aurelia-dialog', 'seasons/league-provider'], function (_export) {
    'use strict';

    var DialogController, LeagueProvider, SeasonTeamEdit;

    var _createClass = (function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ('value' in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; })();

    function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError('Cannot call a class as a function'); } }

    return {
        setters: [function (_aureliaDialog) {
            DialogController = _aureliaDialog.DialogController;
        }, function (_seasonsLeagueProvider) {
            LeagueProvider = _seasonsLeagueProvider.LeagueProvider;
        }],
        execute: function () {
            SeasonTeamEdit = (function () {
                _createClass(SeasonTeamEdit, null, [{
                    key: 'inject',
                    value: [DialogController, LeagueProvider],
                    enumerable: true
                }]);

                function SeasonTeamEdit(controller, leagueProvider) {
                    _classCallCheck(this, SeasonTeamEdit);

                    this.controller = controller;
                    this.leagueProvider = leagueProvider;
                }

                _createClass(SeasonTeamEdit, [{
                    key: 'activate',
                    value: function activate(team) {
                        this.team = team;
                        this.leagueTeams = this.leagueProvider.getTeams();
                    }
                }]);

                return SeasonTeamEdit;
            })();

            _export('SeasonTeamEdit', SeasonTeamEdit);
        }
    };
});
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNlYXNvbnMvdGVhbUVkaXQuanMiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6Ijs7OzBDQUdhLGNBQWM7Ozs7Ozs7OzhDQUhuQixnQkFBZ0I7O29EQUNoQixjQUFjOzs7QUFFVCwwQkFBYzs2QkFBZCxjQUFjOzsyQkFDUCxDQUFDLGdCQUFnQixFQUFFLGNBQWMsQ0FBQzs7OztBQUV2Qyx5QkFIRixjQUFjLENBR1gsVUFBVSxFQUFFLGNBQWMsRUFBRTswQ0FIL0IsY0FBYzs7QUFJbkIsd0JBQUksQ0FBQyxVQUFVLEdBQUcsVUFBVSxDQUFDO0FBQzdCLHdCQUFJLENBQUMsY0FBYyxHQUFHLGNBQWMsQ0FBQztpQkFDeEM7OzZCQU5RLGNBQWM7OzJCQVFmLGtCQUFDLElBQUksRUFBQztBQUNWLDRCQUFJLENBQUMsSUFBSSxHQUFHLElBQUksQ0FBQztBQUNqQiw0QkFBSSxDQUFDLFdBQVcsR0FBRyxJQUFJLENBQUMsY0FBYyxDQUFDLFFBQVEsRUFBRSxDQUFDO3FCQUNyRDs7O3VCQVhRLGNBQWMiLCJmaWxlIjoic2Vhc29ucy90ZWFtRWRpdC5qcyIsInNvdXJjZXNDb250ZW50IjpbImltcG9ydCB7RGlhbG9nQ29udHJvbGxlcn0gZnJvbSAnYXVyZWxpYS1kaWFsb2cnO1xuaW1wb3J0IHtMZWFndWVQcm92aWRlcn0gZnJvbSAnc2Vhc29ucy9sZWFndWUtcHJvdmlkZXInO1xuXG5leHBvcnQgY2xhc3MgU2Vhc29uVGVhbUVkaXQge1xuICAgIHN0YXRpYyBpbmplY3QgPSBbRGlhbG9nQ29udHJvbGxlciwgTGVhZ3VlUHJvdmlkZXJdO1xuXG4gICAgY29uc3RydWN0b3IoY29udHJvbGxlciwgbGVhZ3VlUHJvdmlkZXIpIHtcbiAgICAgICAgdGhpcy5jb250cm9sbGVyID0gY29udHJvbGxlcjtcbiAgICAgICAgdGhpcy5sZWFndWVQcm92aWRlciA9IGxlYWd1ZVByb3ZpZGVyO1xuICAgIH1cblxuICAgIGFjdGl2YXRlKHRlYW0pe1xuICAgICAgICB0aGlzLnRlYW0gPSB0ZWFtO1xuICAgICAgICB0aGlzLmxlYWd1ZVRlYW1zID0gdGhpcy5sZWFndWVQcm92aWRlci5nZXRUZWFtcygpO1xuICAgIH1cbn0iXSwic291cmNlUm9vdCI6Ii9zb3VyY2UvIn0=