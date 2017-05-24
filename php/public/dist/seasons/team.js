System.register(['aurelia-framework', 'aurelia-router', 'seasons/season-provider', 'aurelia-logging'], function (_export) {
    'use strict';

    var inject, Router, SeasonProvider, LogManager, logger, SeasonTeam;

    var _createClass = (function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ('value' in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; })();

    function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError('Cannot call a class as a function'); } }

    return {
        setters: [function (_aureliaFramework) {
            inject = _aureliaFramework.inject;
        }, function (_aureliaRouter) {
            Router = _aureliaRouter.Router;
        }, function (_seasonsSeasonProvider) {
            SeasonProvider = _seasonsSeasonProvider.SeasonProvider;
        }, function (_aureliaLogging) {
            LogManager = _aureliaLogging;
        }],
        execute: function () {
            logger = LogManager.getLogger('season-management');

            SeasonTeam = (function () {
                _createClass(SeasonTeam, null, [{
                    key: 'inject',
                    value: [Router, SeasonProvider],
                    enumerable: true
                }]);

                function SeasonTeam(router, seasonProvider) {
                    _classCallCheck(this, SeasonTeam);

                    this.router = router;
                    this.seasonProvider = seasonProvider;
                }

                _createClass(SeasonTeam, [{
                    key: 'activate',
                    value: function activate(params) {
                        this.season = this.seasonProvider.getSeasonById(params.seasonId);
                        this.team = this.seasonProvider.getTeamById(params.id);
                    }
                }]);

                return SeasonTeam;
            })();

            _export('SeasonTeam', SeasonTeam);
        }
    };
});
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNlYXNvbnMvdGVhbS5qcyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiOzs7b0RBS00sTUFBTSxFQUVDLFVBQVU7Ozs7Ozs7O3VDQVBmLE1BQU07O29DQUNOLE1BQU07O29EQUNOLGNBQWM7Ozs7O0FBR2hCLGtCQUFNLEdBQUcsVUFBVSxDQUFDLFNBQVMsQ0FBQyxtQkFBbUIsQ0FBQzs7QUFFM0Msc0JBQVU7NkJBQVYsVUFBVTs7MkJBQ0gsQ0FBQyxNQUFNLEVBQUUsY0FBYyxDQUFDOzs7O0FBRTdCLHlCQUhGLFVBQVUsQ0FHUCxNQUFNLEVBQUUsY0FBYyxFQUFFOzBDQUgzQixVQUFVOztBQUlmLHdCQUFJLENBQUMsTUFBTSxHQUFHLE1BQU0sQ0FBQztBQUNyQix3QkFBSSxDQUFDLGNBQWMsR0FBRyxjQUFjLENBQUM7aUJBQ3hDOzs2QkFOUSxVQUFVOzsyQkFRWCxrQkFBQyxNQUFNLEVBQUU7QUFDYiw0QkFBSSxDQUFDLE1BQU0sR0FBRyxJQUFJLENBQUMsY0FBYyxDQUFDLGFBQWEsQ0FBQyxNQUFNLENBQUMsUUFBUSxDQUFDLENBQUM7QUFDakUsNEJBQUksQ0FBQyxJQUFJLEdBQUcsSUFBSSxDQUFDLGNBQWMsQ0FBQyxXQUFXLENBQUMsTUFBTSxDQUFDLEVBQUUsQ0FBQyxDQUFDO3FCQUMxRDs7O3VCQVhRLFVBQVUiLCJmaWxlIjoic2Vhc29ucy90ZWFtLmpzIiwic291cmNlc0NvbnRlbnQiOlsiaW1wb3J0IHtpbmplY3R9IGZyb20gJ2F1cmVsaWEtZnJhbWV3b3JrJztcbmltcG9ydCB7Um91dGVyfSBmcm9tICdhdXJlbGlhLXJvdXRlcic7XG5pbXBvcnQge1NlYXNvblByb3ZpZGVyfSBmcm9tICdzZWFzb25zL3NlYXNvbi1wcm92aWRlcic7XG5pbXBvcnQgKiBhcyBMb2dNYW5hZ2VyIGZyb20gJ2F1cmVsaWEtbG9nZ2luZyc7XG5cbmNvbnN0IGxvZ2dlciA9IExvZ01hbmFnZXIuZ2V0TG9nZ2VyKCdzZWFzb24tbWFuYWdlbWVudCcpO1xuXG5leHBvcnQgY2xhc3MgU2Vhc29uVGVhbSB7XG4gICAgc3RhdGljIGluamVjdCA9IFtSb3V0ZXIsIFNlYXNvblByb3ZpZGVyXTtcblxuICAgIGNvbnN0cnVjdG9yKHJvdXRlciwgc2Vhc29uUHJvdmlkZXIpIHtcbiAgICAgICAgdGhpcy5yb3V0ZXIgPSByb3V0ZXI7XG4gICAgICAgIHRoaXMuc2Vhc29uUHJvdmlkZXIgPSBzZWFzb25Qcm92aWRlcjtcbiAgICB9XG5cbiAgICBhY3RpdmF0ZShwYXJhbXMpIHtcbiAgICAgICAgdGhpcy5zZWFzb24gPSB0aGlzLnNlYXNvblByb3ZpZGVyLmdldFNlYXNvbkJ5SWQocGFyYW1zLnNlYXNvbklkKTtcbiAgICAgICAgdGhpcy50ZWFtID0gdGhpcy5zZWFzb25Qcm92aWRlci5nZXRUZWFtQnlJZChwYXJhbXMuaWQpO1xuICAgIH1cbn0iXSwic291cmNlUm9vdCI6Ii9zb3VyY2UvIn0=