System.register(['aurelia-framework', 'aurelia-router', 'seasons/season-provider', 'aurelia-logging', 'aurelia-dialog', 'seasons/teamEdit'], function (_export) {
    'use strict';

    var inject, Router, SeasonProvider, LogManager, DialogService, SeasonTeamEdit, logger, Teams;

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
        }, function (_aureliaDialog) {
            DialogService = _aureliaDialog.DialogService;
        }, function (_seasonsTeamEdit) {
            SeasonTeamEdit = _seasonsTeamEdit.SeasonTeamEdit;
        }],
        execute: function () {
            logger = LogManager.getLogger('season-management');

            Teams = (function () {
                _createClass(Teams, null, [{
                    key: 'inject',
                    value: [Router, SeasonProvider, DialogService],
                    enumerable: true
                }]);

                function Teams(router, seasonProvider, dialogService) {
                    _classCallCheck(this, Teams);

                    this.teams = [];
                    this.team = { leagueTeamId: 0, preDiv: "2", regularDiv: "" };

                    this.router = router;
                    this.seasonProvider = seasonProvider;
                    this.dialogService = dialogService;
                }

                _createClass(Teams, [{
                    key: 'activate',
                    value: function activate(params) {
                        this.season = this.seasonProvider.getSeasonById(params.id);
                        this.teams = this.seasonProvider.getTeamsBySeasonId(params.id);
                    }
                }, {
                    key: 'addTeam',
                    value: function addTeam() {
                        var _this = this;

                        logger.debug('submitting for dialog.');
                        this.dialogService.open({ viewModel: SeasonTeamEdit, model: this.team }).then(function () {
                            console.log(_this.team);
                            console.log('good');
                        })['catch'](function () {
                            console.log('bad');
                        });
                    }
                }]);

                return Teams;
            })();

            _export('Teams', Teams);
        }
    };
});
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNlYXNvbnMvdGVhbXMuanMiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6Ijs7O21GQU9NLE1BQU0sRUFFQyxLQUFLOzs7Ozs7Ozt1Q0FUVixNQUFNOztvQ0FDTixNQUFNOztvREFDTixjQUFjOzs7OzJDQUVkLGFBQWE7OzhDQUNiLGNBQWM7OztBQUVoQixrQkFBTSxHQUFHLFVBQVUsQ0FBQyxTQUFTLENBQUMsbUJBQW1CLENBQUM7O0FBRTNDLGlCQUFLOzZCQUFMLEtBQUs7OzJCQUNFLENBQUMsTUFBTSxFQUFFLGNBQWMsRUFBRSxhQUFhLENBQUM7Ozs7QUFLNUMseUJBTkYsS0FBSyxDQU1GLE1BQU0sRUFBRSxjQUFjLEVBQUUsYUFBYSxFQUFDOzBDQU56QyxLQUFLOzt5QkFHZCxLQUFLLEdBQUcsRUFBRTt5QkFDVixJQUFJLEdBQUcsRUFBRSxZQUFZLEVBQUUsQ0FBQyxFQUFFLE1BQU0sRUFBRSxHQUFHLEVBQUUsVUFBVSxFQUFFLEVBQUUsRUFBRTs7QUFHbkQsd0JBQUksQ0FBQyxNQUFNLEdBQUcsTUFBTSxDQUFDO0FBQ3JCLHdCQUFJLENBQUMsY0FBYyxHQUFHLGNBQWMsQ0FBQztBQUNyQyx3QkFBSSxDQUFDLGFBQWEsR0FBRyxhQUFhLENBQUM7aUJBQ3RDOzs2QkFWUSxLQUFLOzsyQkFZTixrQkFBQyxNQUFNLEVBQUM7QUFDWiw0QkFBSSxDQUFDLE1BQU0sR0FBRyxJQUFJLENBQUMsY0FBYyxDQUFDLGFBQWEsQ0FBQyxNQUFNLENBQUMsRUFBRSxDQUFDLENBQUM7QUFDM0QsNEJBQUksQ0FBQyxLQUFLLEdBQUcsSUFBSSxDQUFDLGNBQWMsQ0FBQyxrQkFBa0IsQ0FBQyxNQUFNLENBQUMsRUFBRSxDQUFDLENBQUM7cUJBQ2xFOzs7MkJBRU0sbUJBQUU7OztBQUNMLDhCQUFNLENBQUMsS0FBSyxDQUFDLHdCQUF3QixDQUFDLENBQUM7QUFDdkMsNEJBQUksQ0FBQyxhQUFhLENBQUMsSUFBSSxDQUFDLEVBQUUsU0FBUyxFQUFFLGNBQWMsRUFBRSxLQUFLLEVBQUUsSUFBSSxDQUFDLElBQUksRUFBQyxDQUFDLENBQUMsSUFBSSxDQUFDLFlBQU07QUFDL0UsbUNBQU8sQ0FBQyxHQUFHLENBQUMsTUFBSyxJQUFJLENBQUMsQ0FBQztBQUN2QixtQ0FBTyxDQUFDLEdBQUcsQ0FBQyxNQUFNLENBQUMsQ0FBQzt5QkFDdkIsQ0FBQyxTQUFNLENBQUMsWUFBTTtBQUNYLG1DQUFPLENBQUMsR0FBRyxDQUFDLEtBQUssQ0FBQyxDQUFDO3lCQUN0QixDQUFDLENBQUM7cUJBQ047Ozt1QkF6QlEsS0FBSyIsImZpbGUiOiJzZWFzb25zL3RlYW1zLmpzIiwic291cmNlc0NvbnRlbnQiOlsiaW1wb3J0IHtpbmplY3R9IGZyb20gJ2F1cmVsaWEtZnJhbWV3b3JrJztcbmltcG9ydCB7Um91dGVyfSBmcm9tICdhdXJlbGlhLXJvdXRlcic7XG5pbXBvcnQge1NlYXNvblByb3ZpZGVyfSBmcm9tICdzZWFzb25zL3NlYXNvbi1wcm92aWRlcic7XG5pbXBvcnQgKiBhcyBMb2dNYW5hZ2VyIGZyb20gJ2F1cmVsaWEtbG9nZ2luZyc7XG5pbXBvcnQge0RpYWxvZ1NlcnZpY2V9IGZyb20gJ2F1cmVsaWEtZGlhbG9nJztcbmltcG9ydCB7U2Vhc29uVGVhbUVkaXR9IGZyb20gJ3NlYXNvbnMvdGVhbUVkaXQnO1xuXG5jb25zdCBsb2dnZXIgPSBMb2dNYW5hZ2VyLmdldExvZ2dlcignc2Vhc29uLW1hbmFnZW1lbnQnKTtcblxuZXhwb3J0IGNsYXNzIFRlYW1zIHtcbiAgICBzdGF0aWMgaW5qZWN0ID0gW1JvdXRlciwgU2Vhc29uUHJvdmlkZXIsIERpYWxvZ1NlcnZpY2VdO1xuXG4gICAgdGVhbXMgPSBbXTtcbiAgICB0ZWFtID0geyBsZWFndWVUZWFtSWQ6IDAsIHByZURpdjogXCIyXCIsIHJlZ3VsYXJEaXY6IFwiXCIgfTtcblxuICAgIGNvbnN0cnVjdG9yKHJvdXRlciwgc2Vhc29uUHJvdmlkZXIsIGRpYWxvZ1NlcnZpY2Upe1xuICAgICAgICB0aGlzLnJvdXRlciA9IHJvdXRlcjtcbiAgICAgICAgdGhpcy5zZWFzb25Qcm92aWRlciA9IHNlYXNvblByb3ZpZGVyO1xuICAgICAgICB0aGlzLmRpYWxvZ1NlcnZpY2UgPSBkaWFsb2dTZXJ2aWNlO1xuICAgIH1cblxuICAgIGFjdGl2YXRlKHBhcmFtcyl7XG4gICAgICAgIHRoaXMuc2Vhc29uID0gdGhpcy5zZWFzb25Qcm92aWRlci5nZXRTZWFzb25CeUlkKHBhcmFtcy5pZCk7XG4gICAgICAgIHRoaXMudGVhbXMgPSB0aGlzLnNlYXNvblByb3ZpZGVyLmdldFRlYW1zQnlTZWFzb25JZChwYXJhbXMuaWQpO1xuICAgIH1cblxuICAgIGFkZFRlYW0oKXtcbiAgICAgICAgbG9nZ2VyLmRlYnVnKCdzdWJtaXR0aW5nIGZvciBkaWFsb2cuJyk7XG4gICAgICAgIHRoaXMuZGlhbG9nU2VydmljZS5vcGVuKHsgdmlld01vZGVsOiBTZWFzb25UZWFtRWRpdCwgbW9kZWw6IHRoaXMudGVhbX0pLnRoZW4oKCkgPT4ge1xuICAgICAgICAgICAgY29uc29sZS5sb2codGhpcy50ZWFtKTtcbiAgICAgICAgICAgIGNvbnNvbGUubG9nKCdnb29kJyk7XG4gICAgICAgIH0pLmNhdGNoKCgpID0+IHtcbiAgICAgICAgICAgIGNvbnNvbGUubG9nKCdiYWQnKTtcbiAgICAgICAgfSk7XG4gICAgfVxufSJdLCJzb3VyY2VSb290IjoiL3NvdXJjZS8ifQ==