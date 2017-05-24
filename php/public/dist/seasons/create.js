System.register(['aurelia-framework', 'aurelia-router', 'seasons/season-provider', 'aurelia-logging'], function (_export) {
    'use strict';

    var inject, Router, SeasonProvider, LogManager, logger, Season;

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

            Season = (function () {
                _createClass(Season, null, [{
                    key: 'inject',
                    value: [Router, SeasonProvider],
                    enumerable: true
                }]);

                function Season(router, seasonProvider) {
                    _classCallCheck(this, Season);

                    this.name = '';
                    this.copyPreviousYearTeams = false;
                    this.isCurrent = false;

                    this.router = router;
                    this.seasonProvider = seasonProvider;
                }

                _createClass(Season, [{
                    key: 'submit',
                    value: function submit() {
                        var _this = this;

                        logger.info('trying to submit');
                        this.seasonProvider.create({
                            name: this.name,
                            startYear: this.startYear,
                            endYear: this.endYear,
                            seasonType: 'Winter Season',
                            isCurrent: this.isCurrent
                        }).then(function (response) {
                            _this.router.navigate('seasons');
                        });
                    }
                }]);

                return Season;
            })();

            _export('Season', Season);
        }
    };
});
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNlYXNvbnMvY3JlYXRlLmpzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiI7OztvREFLTSxNQUFNLEVBRUMsTUFBTTs7Ozs7Ozs7dUNBUFgsTUFBTTs7b0NBQ04sTUFBTTs7b0RBQ04sY0FBYzs7Ozs7QUFHaEIsa0JBQU0sR0FBRyxVQUFVLENBQUMsU0FBUyxDQUFDLG1CQUFtQixDQUFDOztBQUUzQyxrQkFBTTs2QkFBTixNQUFNOzsyQkFDQyxDQUFDLE1BQU0sRUFBRSxjQUFjLENBQUM7Ozs7QUFLN0IseUJBTkYsTUFBTSxDQU1ILE1BQU0sRUFBRSxjQUFjLEVBQUM7MENBTjFCLE1BQU07O3lCQUVmLElBQUksR0FBRyxFQUFFO3lCQUNULHFCQUFxQixHQUFHLEtBQUs7eUJBQzdCLFNBQVMsR0FBRyxLQUFLOztBQUdiLHdCQUFJLENBQUMsTUFBTSxHQUFHLE1BQU0sQ0FBQztBQUNyQix3QkFBSSxDQUFDLGNBQWMsR0FBRyxjQUFjLENBQUM7aUJBQ3hDOzs2QkFUUSxNQUFNOzsyQkFXVCxrQkFBRTs7O0FBQ0osOEJBQU0sQ0FBQyxJQUFJLENBQUMsa0JBQWtCLENBQUMsQ0FBQztBQUNoQyw0QkFBSSxDQUFDLGNBQWMsQ0FBQyxNQUFNLENBQ3RCO0FBQ0ksZ0NBQUksRUFBRSxJQUFJLENBQUMsSUFBSTtBQUNmLHFDQUFTLEVBQUUsSUFBSSxDQUFDLFNBQVM7QUFDekIsbUNBQU8sRUFBRSxJQUFJLENBQUMsT0FBTztBQUNyQixzQ0FBVSxFQUFFLGVBQWU7QUFDM0IscUNBQVMsRUFBRSxJQUFJLENBQUMsU0FBUzt5QkFDNUIsQ0FBQyxDQUFDLElBQUksQ0FBQyxVQUFBLFFBQVEsRUFBRztBQUNmLGtDQUFLLE1BQU0sQ0FBQyxRQUFRLENBQUMsU0FBUyxDQUFDLENBQUM7eUJBQ25DLENBQUMsQ0FBQztxQkFDVjs7O3VCQXZCUSxNQUFNIiwiZmlsZSI6InNlYXNvbnMvY3JlYXRlLmpzIiwic291cmNlc0NvbnRlbnQiOlsiaW1wb3J0IHtpbmplY3R9IGZyb20gJ2F1cmVsaWEtZnJhbWV3b3JrJztcbmltcG9ydCB7Um91dGVyfSBmcm9tICdhdXJlbGlhLXJvdXRlcic7XG5pbXBvcnQge1NlYXNvblByb3ZpZGVyfSBmcm9tICdzZWFzb25zL3NlYXNvbi1wcm92aWRlcic7XG5pbXBvcnQgKiBhcyBMb2dNYW5hZ2VyIGZyb20gJ2F1cmVsaWEtbG9nZ2luZyc7XG5cbmNvbnN0IGxvZ2dlciA9IExvZ01hbmFnZXIuZ2V0TG9nZ2VyKCdzZWFzb24tbWFuYWdlbWVudCcpO1xuXG5leHBvcnQgY2xhc3MgU2Vhc29uIHtcbiAgICBzdGF0aWMgaW5qZWN0ID0gW1JvdXRlciwgU2Vhc29uUHJvdmlkZXJdO1xuICAgIG5hbWUgPSAnJztcbiAgICBjb3B5UHJldmlvdXNZZWFyVGVhbXMgPSBmYWxzZTtcbiAgICBpc0N1cnJlbnQgPSBmYWxzZTtcblxuICAgIGNvbnN0cnVjdG9yKHJvdXRlciwgc2Vhc29uUHJvdmlkZXIpe1xuICAgICAgICB0aGlzLnJvdXRlciA9IHJvdXRlcjtcbiAgICAgICAgdGhpcy5zZWFzb25Qcm92aWRlciA9IHNlYXNvblByb3ZpZGVyO1xuICAgIH1cblxuICAgIHN1Ym1pdCgpe1xuICAgICAgICBsb2dnZXIuaW5mbygndHJ5aW5nIHRvIHN1Ym1pdCcpO1xuICAgICAgICB0aGlzLnNlYXNvblByb3ZpZGVyLmNyZWF0ZShcbiAgICAgICAgICAgIHtcbiAgICAgICAgICAgICAgICBuYW1lOiB0aGlzLm5hbWUsXG4gICAgICAgICAgICAgICAgc3RhcnRZZWFyOiB0aGlzLnN0YXJ0WWVhcixcbiAgICAgICAgICAgICAgICBlbmRZZWFyOiB0aGlzLmVuZFllYXIsXG4gICAgICAgICAgICAgICAgc2Vhc29uVHlwZTogJ1dpbnRlciBTZWFzb24nLFxuICAgICAgICAgICAgICAgIGlzQ3VycmVudDogdGhpcy5pc0N1cnJlbnRcbiAgICAgICAgICAgIH0pLnRoZW4ocmVzcG9uc2UgPT57XG4gICAgICAgICAgICAgICAgdGhpcy5yb3V0ZXIubmF2aWdhdGUoJ3NlYXNvbnMnKTtcbiAgICAgICAgICAgIH0pO1xuICAgIH1cbn0iXSwic291cmNlUm9vdCI6Ii9zb3VyY2UvIn0=