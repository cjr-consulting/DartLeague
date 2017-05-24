System.register(['aurelia-framework', 'seasons/season-provider', 'fetch'], function (_export) {
    'use strict';

    var inject, SeasonProvider, Seasons;

    var _createClass = (function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ('value' in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; })();

    function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError('Cannot call a class as a function'); } }

    return {
        setters: [function (_aureliaFramework) {
            inject = _aureliaFramework.inject;
        }, function (_seasonsSeasonProvider) {
            SeasonProvider = _seasonsSeasonProvider.SeasonProvider;
        }, function (_fetch) {}],
        execute: function () {
            Seasons = (function () {
                _createClass(Seasons, null, [{
                    key: 'inject',
                    value: [SeasonProvider],
                    enumerable: true
                }]);

                function Seasons(seasonProvider) {
                    _classCallCheck(this, Seasons);

                    this.heading = 'Seasons';
                    this.seasons = [];

                    this.seasonProvider = seasonProvider;
                }

                _createClass(Seasons, [{
                    key: 'activate',
                    value: function activate() {
                        var _this = this;

                        this.seasonProvider.getSeasons().then(function (response) {
                            if (response.statusCode == 200) _this.seasons = JSON.parse(response.response);
                        });
                    }
                }]);

                return Seasons;
            })();

            _export('Seasons', Seasons);
        }
    };
});
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNlYXNvbnMvaW5kZXguanMiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6Ijs7O2dDQUlhLE9BQU87Ozs7Ozs7O3VDQUpaLE1BQU07O29EQUNOLGNBQWM7OztBQUdULG1CQUFPOzZCQUFQLE9BQU87OzJCQUNBLENBQUMsY0FBYyxDQUFDOzs7O0FBSXJCLHlCQUxGLE9BQU8sQ0FLSixjQUFjLEVBQUM7MENBTGxCLE9BQU87O3lCQUVoQixPQUFPLEdBQUcsU0FBUzt5QkFDbkIsT0FBTyxHQUFHLEVBQUU7O0FBR1Isd0JBQUksQ0FBQyxjQUFjLEdBQUcsY0FBYyxDQUFDO2lCQUN4Qzs7NkJBUFEsT0FBTzs7MkJBU1Isb0JBQUU7OztBQUNOLDRCQUFJLENBQUMsY0FBYyxDQUFDLFVBQVUsRUFBRSxDQUMzQixJQUFJLENBQUMsVUFBQSxRQUFRLEVBQUk7QUFDZCxnQ0FBRyxRQUFRLENBQUMsVUFBVSxJQUFJLEdBQUcsRUFDekIsTUFBSyxPQUFPLEdBQUcsSUFBSSxDQUFDLEtBQUssQ0FBQyxRQUFRLENBQUMsUUFBUSxDQUFDLENBQUM7eUJBQ3BELENBQUMsQ0FBQztxQkFDVjs7O3VCQWZRLE9BQU8iLCJmaWxlIjoic2Vhc29ucy9pbmRleC5qcyIsInNvdXJjZXNDb250ZW50IjpbImltcG9ydCB7aW5qZWN0fSBmcm9tICdhdXJlbGlhLWZyYW1ld29yayc7XG5pbXBvcnQge1NlYXNvblByb3ZpZGVyfSBmcm9tICdzZWFzb25zL3NlYXNvbi1wcm92aWRlcic7XG5pbXBvcnQgJ2ZldGNoJztcblxuZXhwb3J0IGNsYXNzIFNlYXNvbnN7XG4gICAgc3RhdGljIGluamVjdCA9IFtTZWFzb25Qcm92aWRlcl07XG4gICAgaGVhZGluZyA9ICdTZWFzb25zJztcbiAgICBzZWFzb25zID0gW107XG5cbiAgICBjb25zdHJ1Y3RvcihzZWFzb25Qcm92aWRlcil7XG4gICAgICAgIHRoaXMuc2Vhc29uUHJvdmlkZXIgPSBzZWFzb25Qcm92aWRlcjtcbiAgICB9XG5cbiAgICBhY3RpdmF0ZSgpe1xuICAgICAgICB0aGlzLnNlYXNvblByb3ZpZGVyLmdldFNlYXNvbnMoKVxuICAgICAgICAgICAgLnRoZW4ocmVzcG9uc2UgPT4ge1xuICAgICAgICAgICAgICAgIGlmKHJlc3BvbnNlLnN0YXR1c0NvZGUgPT0gMjAwKVxuICAgICAgICAgICAgICAgICAgICB0aGlzLnNlYXNvbnMgPSBKU09OLnBhcnNlKHJlc3BvbnNlLnJlc3BvbnNlKTtcbiAgICAgICAgICAgIH0pO1xuICAgIH1cbn1cbiJdLCJzb3VyY2VSb290IjoiL3NvdXJjZS8ifQ==