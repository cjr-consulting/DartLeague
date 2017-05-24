System.register(['aurelia-framework', 'aurelia-fetch-client', 'fetch'], function (_export) {
    'use strict';

    var inject, HttpClient, LeagueProvider;

    var _createClass = (function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ('value' in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; })();

    function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError('Cannot call a class as a function'); } }

    return {
        setters: [function (_aureliaFramework) {
            inject = _aureliaFramework.inject;
        }, function (_aureliaFetchClient) {
            HttpClient = _aureliaFetchClient.HttpClient;
        }, function (_fetch) {}],
        execute: function () {
            LeagueProvider = (function () {
                _createClass(LeagueProvider, null, [{
                    key: 'inject',
                    value: [HttpClient],
                    enumerable: true
                }]);

                function LeagueProvider(http) {
                    _classCallCheck(this, LeagueProvider);

                    this.http = http;
                }

                _createClass(LeagueProvider, [{
                    key: 'getTeams',
                    value: function getTeams() {
                        return [{
                            id: 1,
                            name: "Team 1"
                        }, {
                            id: 2,
                            name: "Team 2"
                        }];
                    }
                }]);

                return LeagueProvider;
            })();

            _export('LeagueProvider', LeagueProvider);
        }
    };
});
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNlYXNvbnMvbGVhZ3VlLXByb3ZpZGVyLmpzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiI7Ozs0QkFJYSxjQUFjOzs7Ozs7Ozt1Q0FKbkIsTUFBTTs7NkNBQ04sVUFBVTs7O0FBR0wsMEJBQWM7NkJBQWQsY0FBYzs7MkJBQ1AsQ0FBQyxVQUFVLENBQUM7Ozs7QUFFakIseUJBSEYsY0FBYyxDQUdYLElBQUksRUFBRTswQ0FIVCxjQUFjOztBQUluQix3QkFBSSxDQUFDLElBQUksR0FBRyxJQUFJLENBQUM7aUJBQ3BCOzs2QkFMUSxjQUFjOzsyQkFPZixvQkFBRztBQUNQLCtCQUFPLENBQ0g7QUFDSSw4QkFBRSxFQUFFLENBQUM7QUFDTCxnQ0FBSSxFQUFFLFFBQVE7eUJBQ2pCLEVBQ0Q7QUFDSSw4QkFBRSxFQUFFLENBQUM7QUFDTCxnQ0FBSSxFQUFFLFFBQVE7eUJBQ2pCLENBQ0osQ0FBQztxQkFDTDs7O3VCQWxCUSxjQUFjIiwiZmlsZSI6InNlYXNvbnMvbGVhZ3VlLXByb3ZpZGVyLmpzIiwic291cmNlc0NvbnRlbnQiOlsiaW1wb3J0IHtpbmplY3R9IGZyb20gJ2F1cmVsaWEtZnJhbWV3b3JrJztcbmltcG9ydCB7SHR0cENsaWVudH0gZnJvbSAnYXVyZWxpYS1mZXRjaC1jbGllbnQnO1xuaW1wb3J0ICdmZXRjaCc7XG5cbmV4cG9ydCBjbGFzcyBMZWFndWVQcm92aWRlciB7XG4gICAgc3RhdGljIGluamVjdCA9IFtIdHRwQ2xpZW50XTtcblxuICAgIGNvbnN0cnVjdG9yKGh0dHApIHtcbiAgICAgICAgdGhpcy5odHRwID0gaHR0cDtcbiAgICB9XG5cbiAgICBnZXRUZWFtcygpIHtcbiAgICAgICAgcmV0dXJuIFtcbiAgICAgICAgICAgIHtcbiAgICAgICAgICAgICAgICBpZDogMSxcbiAgICAgICAgICAgICAgICBuYW1lOiBcIlRlYW0gMVwiXG4gICAgICAgICAgICB9LFxuICAgICAgICAgICAge1xuICAgICAgICAgICAgICAgIGlkOiAyLFxuICAgICAgICAgICAgICAgIG5hbWU6IFwiVGVhbSAyXCJcbiAgICAgICAgICAgIH1cbiAgICAgICAgXTtcbiAgICB9XG59Il0sInNvdXJjZVJvb3QiOiIvc291cmNlLyJ9