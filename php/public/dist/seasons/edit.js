System.register(['aurelia-framework', 'seasons/season-provider'], function (_export) {
    'use strict';

    var inject, SeasonProvider, EditSeason;

    var _createClass = (function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ('value' in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; })();

    function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError('Cannot call a class as a function'); } }

    return {
        setters: [function (_aureliaFramework) {
            inject = _aureliaFramework.inject;
        }, function (_seasonsSeasonProvider) {
            SeasonProvider = _seasonsSeasonProvider.SeasonProvider;
        }],
        execute: function () {
            EditSeason = (function () {
                _createClass(EditSeason, null, [{
                    key: 'inject',
                    value: [SeasonProvider],
                    enumerable: true
                }]);

                function EditSeason(seasonProvider) {
                    _classCallCheck(this, EditSeason);

                    this.seasonProvider = seasonProvider;
                }

                _createClass(EditSeason, [{
                    key: 'activate',
                    value: function activate(params) {
                        this.id = params.id;
                        this.season = this.seasonProvider.getSeasonById(params.id);
                        console.info(this.season);
                    }
                }]);

                return EditSeason;
            })();

            _export('EditSeason', EditSeason);
        }
    };
});
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNlYXNvbnMvZWRpdC5qcyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiOzs7Z0NBR2EsVUFBVTs7Ozs7Ozs7dUNBSGYsTUFBTTs7b0RBQ04sY0FBYzs7O0FBRVQsc0JBQVU7NkJBQVYsVUFBVTs7MkJBQ0gsQ0FBQyxjQUFjLENBQUM7Ozs7QUFFckIseUJBSEYsVUFBVSxDQUdQLGNBQWMsRUFBQzswQ0FIbEIsVUFBVTs7QUFJZix3QkFBSSxDQUFDLGNBQWMsR0FBRyxjQUFjLENBQUM7aUJBQ3hDOzs2QkFMUSxVQUFVOzsyQkFPWCxrQkFBQyxNQUFNLEVBQUM7QUFDWiw0QkFBSSxDQUFDLEVBQUUsR0FBRyxNQUFNLENBQUMsRUFBRSxDQUFDO0FBQ3BCLDRCQUFJLENBQUMsTUFBTSxHQUFHLElBQUksQ0FBQyxjQUFjLENBQUMsYUFBYSxDQUFDLE1BQU0sQ0FBQyxFQUFFLENBQUMsQ0FBQztBQUMzRCwrQkFBTyxDQUFDLElBQUksQ0FBQyxJQUFJLENBQUMsTUFBTSxDQUFDLENBQUM7cUJBQzdCOzs7dUJBWFEsVUFBVSIsImZpbGUiOiJzZWFzb25zL2VkaXQuanMiLCJzb3VyY2VzQ29udGVudCI6WyJpbXBvcnQge2luamVjdH0gZnJvbSAnYXVyZWxpYS1mcmFtZXdvcmsnO1xuaW1wb3J0IHtTZWFzb25Qcm92aWRlcn0gZnJvbSAnc2Vhc29ucy9zZWFzb24tcHJvdmlkZXInO1xuXG5leHBvcnQgY2xhc3MgRWRpdFNlYXNvbiB7XG4gICAgc3RhdGljIGluamVjdCA9IFtTZWFzb25Qcm92aWRlcl07XG5cbiAgICBjb25zdHJ1Y3RvcihzZWFzb25Qcm92aWRlcil7XG4gICAgICAgIHRoaXMuc2Vhc29uUHJvdmlkZXIgPSBzZWFzb25Qcm92aWRlcjtcbiAgICB9XG5cbiAgICBhY3RpdmF0ZShwYXJhbXMpe1xuICAgICAgICB0aGlzLmlkID0gcGFyYW1zLmlkO1xuICAgICAgICB0aGlzLnNlYXNvbiA9IHRoaXMuc2Vhc29uUHJvdmlkZXIuZ2V0U2Vhc29uQnlJZChwYXJhbXMuaWQpO1xuICAgICAgICBjb25zb2xlLmluZm8odGhpcy5zZWFzb24pO1xuICAgIH1cbn0iXSwic291cmNlUm9vdCI6Ii9zb3VyY2UvIn0=