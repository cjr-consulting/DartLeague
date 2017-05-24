System.register(['aurelia-framework', 'aurelia-http-client', 'fetch'], function (_export) {
    'use strict';

    var inject, HttpClient, SeasonProvider;

    var _createClass = (function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ('value' in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; })();

    function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError('Cannot call a class as a function'); } }

    return {
        setters: [function (_aureliaFramework) {
            inject = _aureliaFramework.inject;
        }, function (_aureliaHttpClient) {
            HttpClient = _aureliaHttpClient.HttpClient;
        }, function (_fetch) {}],
        execute: function () {
            SeasonProvider = (function () {
                _createClass(SeasonProvider, [{
                    key: 'seasons',
                    get: function get() {
                        return [{
                            id: 2,
                            name: 'Winter 2014 - 2015',
                            startYear: 2014,
                            endYear: 2015,
                            teams: [{
                                id: 1,
                                team: { id: 1, name: 'Team 1' },
                                preDiv: '1',
                                regularDiv: '',
                                players: [{
                                    id: 1,
                                    member: { id: 1, name: 'Player Name 1' },
                                    role: 'player'
                                }, {
                                    id: 3,
                                    member: { id: 3, name: 'Player Name 2' },
                                    role: 'captain'
                                }]
                            }, {
                                id: 2,
                                team: { id: 2, name: 'Team 2' },
                                preDiv: '2',
                                regularDiv: '',
                                players: [{
                                    id: 2,
                                    member: { id: 2, name: 'Player Name 2' },
                                    role: 'player'
                                }]
                            }, {
                                id: 3,
                                team: { id: 5, name: 'Team 3' },
                                preDiv: '2',
                                regularDiv: ''
                            }]
                        }, {
                            id: 1,
                            name: 'Winter 2013 - 2014',
                            startYear: 2013,
                            endYear: 2014,
                            teams: [{
                                id: 4,
                                team: { id: 1, name: 'Team 1' },
                                preDiv: '1',
                                regularDiv: ''
                            }, {
                                id: 5,
                                team: { id: 2, name: 'Team 2' },
                                preDiv: '2',
                                regularDiv: ''
                            }, {
                                id: 6,
                                team: { id: 5, name: 'Team 3' },
                                preDiv: '2',
                                regularDiv: ''
                            }]
                        }];
                    }
                }], [{
                    key: 'inject',
                    value: [HttpClient],
                    enumerable: true
                }]);

                function SeasonProvider(http) {
                    _classCallCheck(this, SeasonProvider);

                    var token = document.querySelector("meta[name=csrf-token]").getAttribute("content");
                    this.http = http.configure(function (x) {
                        x.withHeader("X-CSRF-Token", token);
                    });
                }

                _createClass(SeasonProvider, [{
                    key: 'create',
                    value: function create(season) {
                        return this.http.post("/api/season", season);
                    }
                }, {
                    key: 'getSeasons',
                    value: function getSeasons() {
                        return this.http.get('/api/season');
                    }
                }, {
                    key: 'getSeasonById',
                    value: function getSeasonById(id) {
                        return this.seasons.find(function (element) {
                            return element.id == id;
                        });
                    }
                }, {
                    key: 'getTeamsBySeasonId',
                    value: function getTeamsBySeasonId(id) {
                        return this.seasons.find(function (element) {
                            return element.id == id;
                        }).teams;
                    }
                }, {
                    key: 'getTeamById',
                    value: function getTeamById(id) {
                        var season = this.seasons.find(function (season) {
                            return season.teams.find(function (team) {
                                return team.id == id;
                            }) !== undefined;
                        });

                        return season.teams.find(function (team) {
                            return team.id == id;
                        });
                    }
                }]);

                return SeasonProvider;
            })();

            _export('SeasonProvider', SeasonProvider);
        }
    };
});
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNlYXNvbnMvc2Vhc29uLXByb3ZpZGVyLmpzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiI7Ozs0QkFJYSxjQUFjOzs7Ozs7Ozt1Q0FKbkIsTUFBTTs7NENBQ04sVUFBVTs7O0FBR0wsMEJBQWM7NkJBQWQsY0FBYzs7eUJBRVosZUFBRztBQUNWLCtCQUFPLENBQ0g7QUFDSSw4QkFBRSxFQUFFLENBQUM7QUFDTCxnQ0FBSSxFQUFFLG9CQUFvQjtBQUMxQixxQ0FBUyxFQUFFLElBQUk7QUFDZixtQ0FBTyxFQUFFLElBQUk7QUFDYixpQ0FBSyxFQUFFLENBQ0g7QUFDSSxrQ0FBRSxFQUFFLENBQUM7QUFDTCxvQ0FBSSxFQUFFLEVBQUUsRUFBRSxFQUFFLENBQUMsRUFBRSxJQUFJLEVBQUUsUUFBUSxFQUFFO0FBQy9CLHNDQUFNLEVBQUUsR0FBRztBQUNYLDBDQUFVLEVBQUUsRUFBRTtBQUNkLHVDQUFPLEVBQUUsQ0FDTDtBQUNJLHNDQUFFLEVBQUUsQ0FBQztBQUNMLDBDQUFNLEVBQUUsRUFBRSxFQUFFLEVBQUUsQ0FBQyxFQUFFLElBQUksRUFBRSxlQUFlLEVBQUU7QUFDeEMsd0NBQUksRUFBRSxRQUFRO2lDQUNqQixFQUNEO0FBQ0ksc0NBQUUsRUFBRSxDQUFDO0FBQ0wsMENBQU0sRUFBRSxFQUFFLEVBQUUsRUFBRSxDQUFDLEVBQUUsSUFBSSxFQUFFLGVBQWUsRUFBRTtBQUN4Qyx3Q0FBSSxFQUFFLFNBQVM7aUNBQ2xCLENBQ0o7NkJBQ0osRUFDRDtBQUNJLGtDQUFFLEVBQUUsQ0FBQztBQUNMLG9DQUFJLEVBQUUsRUFBRSxFQUFFLEVBQUUsQ0FBQyxFQUFFLElBQUksRUFBRSxRQUFRLEVBQUM7QUFDOUIsc0NBQU0sRUFBRSxHQUFHO0FBQ1gsMENBQVUsRUFBRSxFQUFFO0FBQ2QsdUNBQU8sRUFBRSxDQUNMO0FBQ0ksc0NBQUUsRUFBRSxDQUFDO0FBQ0wsMENBQU0sRUFBRSxFQUFDLEVBQUUsRUFBRSxDQUFDLEVBQUUsSUFBSSxFQUFFLGVBQWUsRUFBRTtB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SUFBSSxFQUFDO0FBQ25DLG1DQUFPLElBQUksQ0FBQyxFQUFFLElBQUksRUFBRSxDQUFDO3lCQUN4QixDQUFDLENBQUM7cUJBQ047Ozt1QkFuSFEsY0FBYyIsImZpbGUiOiJzZWFzb25zL3NlYXNvbi1wcm92aWRlci5qcyIsInNvdXJjZXNDb250ZW50IjpbImltcG9ydCB7aW5qZWN0fSBmcm9tICdhdXJlbGlhLWZyYW1ld29yayc7XG5pbXBvcnQge0h0dHBDbGllbnR9IGZyb20gJ2F1cmVsaWEtaHR0cC1jbGllbnQnO1xuaW1wb3J0ICdmZXRjaCc7XG5cbmV4cG9ydCBjbGFzcyBTZWFzb25Qcm92aWRlciB7XG4gICAgc3RhdGljIGluamVjdCA9IFtIdHRwQ2xpZW50XTtcbiAgICBnZXQgc2Vhc29ucygpIHtcbiAgICAgICAgcmV0dXJuIFtcbiAgICAgICAgICAgIHtcbiAgICAgICAgICAgICAgICBpZDogMixcbiAgICAgICAgICAgICAgICBuYW1lOiAnV2ludGVyIDIwMTQgLSAyMDE1JyxcbiAgICAgICAgICAgICAgICBzdGFydFllYXI6IDIwMTQsXG4gICAgICAgICAgICAgICAgZW5kWWVhcjogMjAxNSxcbiAgICAgICAgICAgICAgICB0ZWFtczogW1xuICAgICAgICAgICAgICAgICAgICB7XG4gICAgICAgICAgICAgICAgICAgICAgICBpZDogMSxcbiAgICAgICAgICAgICAgICAgICAgICAgIHRlYW06IHsgaWQ6IDEsIG5hbWU6ICdUZWFtIDEnIH0sXG4gICAgICAgICAgICAgICAgICAgICAgICBwcmVEaXY6ICcxJyxcbiAgICAgICAgICAgICAgICAgICAgICAgIHJlZ3VsYXJEaXY6ICcnLFxuICAgICAgICAgICAgICAgICAgICAgICAgcGxheWVyczogW1xuICAgICAgICAgICAgICAgICAgICAgICAgICAgIHtcbiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgaWQ6IDEsXG4gICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIG1lbWJlcjogeyBpZDogMSwgbmFtZTogJ1BsYXllciBOYW1lIDEnIH0sXG4gICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIHJvbGU6ICdwbGF5ZXInXG4gICAgICAgICAgICAgICAgICAgICAgICAgICAgfSxcbiAgICAgICAgICAgICAgICAgICAgICAgICAgICB7XG4gICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIGlkOiAzLFxuICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICBtZW1iZXI6IHsgaWQ6IDMsIG5hbWU6ICdQbGF5ZXIgTmFtZSAyJyB9LFxuICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICByb2xlOiAnY2FwdGFpbidcbiAgICAgICAgICAgICAgICAgICAgICAgICAgICB9XG4gICAgICAgICAgICAgICAgICAgICAgICBdXG4gICAgICAgICAgICAgICAgICAgIH0sXG4gICAgICAgICAgICAgICAgICAgIHtcbiAgICAgICAgICAgICAgICAgICAgICAgIGlkOiAyLFxuICAgICAgICAgICAgICAgICAgICAgICAgdGVhbTogeyBpZDogMiwgbmFtZTogJ1RlYW0gMid9LFxuICAgICAgICAgICAgICAgICAgICAgICAgcHJlRGl2OiAnMicsXG4gICAgICAgICAgICAgICAgICAgICAgICByZWd1bGFyRGl2OiAnJyxcbiAgICAgICAgICAgICAgICAgICAgICAgIHBsYXllcnM6IFtcbiAgICAgICAgICAgICAgICAgICAgICAgICAgICB7XG4gICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIGlkOiAyLFxuICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICBtZW1iZXI6IHtpZDogMiwgbmFtZTogJ1BsYXllciBOYW1lIDInIH0sXG4gICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIHJvbGU6ICdwbGF5ZXInXG4gICAgICAgICAgICAgICAgICAgICAgICAgICAgfVxuICAgICAgICAgICAgICAgICAgICAgICAgXVxuICAgICAgICAgICAgICAgICAgICB9LFxuICAgICAgICAgICAgICAgICAgICB7XG4gICAgICAgICAgICAgICAgICAgICAgICBpZDogMyxcbiAgICAgICAgICAgICAgICAgICAgICAgIHRlYW06IHsgaWQ6IDUsIG5hbWU6ICdUZWFtIDMnfSxcbiAgICAgICAgICAgICAgICAgICAgICAgIHByZURpdjogJzInLFxuICAgICAgICAgICAgICAgICAgICAgICAgcmVndWxhckRpdjogJydcbiAgICAgICAgICAgICAgICAgICAgfVxuICAgICAgICAgICAgICAgIF1cbiAgICAgICAgICAgIH0sXG4gICAgICAgICAgICB7XG4gICAgICAgICAgICAgICAgaWQ6IDEsXG4gICAgICAgICAgICAgICAgbmFtZTogJ1dpbnRlciAyMDEzIC0gMjAxNCcsXG4gICAgICAgICAgICAgICAgc3RhcnRZZWFyOiAyMDEzLFxuICAgICAgICAgICAgICAgIGVuZFllYXI6IDIwMTQsXG4gICAgICAgICAgICAgICAgdGVhbXM6IFtcbiAgICAgICAgICAgICAgICAgICAge1xuICAgICAgICAgICAgICAgICAgICAgICAgaWQ6IDQsXG4gICAgICAgICAgICAgICAgICAgICAgICB0ZWFtOiB7IGlkOiAxLCBuYW1lOiAnVGVhbSAxJ30sXG4gICAgICAgICAgICAgICAgICAgICAgICBwcmVEaXY6ICcxJyxcbiAgICAgICAgICAgICAgICAgICAgICAgIHJlZ3VsYXJEaXY6ICcnXG4gICAgICAgICAgICAgICAgICAgIH0sXG4gICAgICAgICAgICAgICAgICAgIHtcbiAgICAgICAgICAgICAgICAgICAgICAgIGlkOiA1LFxuICAgICAgICAgICAgICAgICAgICAgICAgdGVhbTogeyBpZDogMiwgbmFtZTogJ1RlYW0gMid9LFxuICAgICAgICAgICAgICAgICAgICAgICAgcHJlRGl2OiAnMicsXG4gICAgICAgICAgICAgICAgICAgICAgICByZWd1bGFyRGl2OiAnJ1xuICAgICAgICAgICAgICAgICAgICB9LFxuICAgICAgICAgICAgICAgICAgICB7XG4gICAgICAgICAgICAgICAgICAgICAgICBpZDogNixcbiAgICAgICAgICAgICAgICAgICAgICAgIHRlYW06IHsgaWQ6IDUsIG5hbWU6ICdUZWFtIDMnfSxcbiAgICAgICAgICAgICAgICAgICAgICAgIHByZURpdjogJzInLFxuICAgICAgICAgICAgICAgICAgICAgICAgcmVndWxhckRpdjogJydcbiAgICAgICAgICAgICAgICAgICAgfVxuICAgICAgICAgICAgICAgIF1cbiAgICAgICAgICAgIH1cbiAgICAgICAgXTtcbiAgICB9XG5cbiAgICBjb25zdHJ1Y3RvcihodHRwKSB7XG4gICAgICAgIHZhciB0b2tlbiA9IGRvY3VtZW50LnF1ZXJ5U2VsZWN0b3IoXCJtZXRhW25hbWU9Y3NyZi10b2tlbl1cIikuZ2V0QXR0cmlidXRlKFwiY29udGVudFwiKTtcbiAgICAgICAgdGhpcy5odHRwID0gaHR0cC5jb25maWd1cmUoeD0+e1xuICAgICAgICAgICAgeC53aXRoSGVhZGVyKFwiWC1DU1JGLVRva2VuXCIsIHRva2VuKTtcbiAgICAgICAgfSk7XG4gICAgfVxuXG4gICAgY3JlYXRlKHNlYXNvbil7XG4gICAgICAgIHJldHVybiB0aGlzLmh0dHAucG9zdChcIi9hcGkvc2Vhc29uXCIsIHNlYXNvbik7XG4gICAgfVxuXG4gICAgZ2V0U2Vhc29ucygpe1xuICAgICAgICByZXR1cm4gdGhpcy5odHRwLmdldCgnL2FwaS9zZWFzb24nKTtcbiAgICB9XG5cbiAgICBnZXRTZWFzb25CeUlkKGlkKSB7XG4gICAgICAgIHJldHVybiB0aGlzLnNlYXNvbnMuZmluZChmdW5jdGlvbihlbGVtZW50KXtcbiAgICAgICAgICAgIHJldHVybiBlbGVtZW50LmlkID09IGlkO1xuICAgICAgICB9KTtcbiAgICB9XG5cbiAgICBnZXRUZWFtc0J5U2Vhc29uSWQoaWQpIHtcbiAgICAgICAgcmV0dXJuIHRoaXMuc2Vhc29ucy5maW5kKGZ1bmN0aW9uKGVsZW1lbnQpIHtcbiAgICAgICAgICAgIHJldHVybiBlbGVtZW50LmlkID09IGlkO1xuICAgICAgICB9KS50ZWFtcztcbiAgICB9XG5cbiAgICBnZXRUZWFtQnlJZChpZCl7XG4gICAgICAgIGxldCBzZWFzb24gPSB0aGlzLnNlYXNvbnMuZmluZChmdW5jdGlvbihzZWFzb24pe1xuICAgICAgICAgICAgcmV0dXJuIHNlYXNvbi50ZWFtcy5maW5kKGZ1bmN0aW9uKHRlYW0pe1xuICAgICAgICAgICAgICAgIHJldHVybiB0ZWFtLmlkID09IGlkO1xuICAgICAgICAgICAgfSkgIT09IHVuZGVmaW5lZDtcbiAgICAgICAgfSk7XG5cbiAgICAgICAgcmV0dXJuIHNlYXNvbi50ZWFtcy5maW5kKGZ1bmN0aW9uKHRlYW0pe1xuICAgICAgICAgICAgcmV0dXJuIHRlYW0uaWQgPT0gaWQ7XG4gICAgICAgIH0pO1xuICAgIH1cbn0iXSwic291cmNlUm9vdCI6Ii9zb3VyY2UvIn0=