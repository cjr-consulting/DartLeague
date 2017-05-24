System.register(['bootstrap', 'bootstrap/css/bootstrap.css!'], function (_export) {
  'use strict';

  var App;

  var _createClass = (function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ('value' in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; })();

  function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError('Cannot call a class as a function'); } }

  return {
    setters: [function (_bootstrap) {}, function (_bootstrapCssBootstrapCss) {}],
    execute: function () {
      App = (function () {
        function App() {
          _classCallCheck(this, App);
        }

        _createClass(App, [{
          key: 'configureRouter',
          value: function configureRouter(config, router) {
            config.title = 'Darts';
            config.map([{ route: ['', 'welcome'], name: 'welcome', moduleId: 'welcome', nav: true, title: 'Welcome' }, { route: 'users', name: 'users', moduleId: 'users', nav: true, title: 'Github Users' }, { route: 'seasons', name: 'seasons', moduleId: 'seasons/index', nav: true, title: 'Seasons' }, { route: 'seasons/create', name: 'createSeason', moduleId: 'seasons/create', title: 'Season' }, { route: 'seasons/:id', name: 'seasonEdit', moduleId: 'seasons/edit', title: 'Season' }, { route: 'seasons/:id/teams', name: 'seasonTeams', moduleId: 'seasons/teams', title: 'Season Teams' }, { route: 'seasons/:seasonId/teams/:id', name: 'seasonTeamEdit', moduleId: 'seasons/team', title: 'Season Team' }]);

            this.router = router;
          }
        }]);

        return App;
      })();

      _export('App', App);
    }
  };
});
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImFwcC5qcyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiOzs7TUFHYSxHQUFHOzs7Ozs7Ozs7QUFBSCxTQUFHO2lCQUFILEdBQUc7Z0NBQUgsR0FBRzs7O3FCQUFILEdBQUc7O2lCQUNDLHlCQUFDLE1BQU0sRUFBRSxNQUFNLEVBQUM7QUFDN0Isa0JBQU0sQ0FBQyxLQUFLLEdBQUcsT0FBTyxDQUFDO0FBQ3ZCLGtCQUFNLENBQUMsR0FBRyxDQUFDLENBQ1QsRUFBRSxLQUFLLEVBQUUsQ0FBQyxFQUFFLEVBQUMsU0FBUyxDQUFDLEVBQUcsSUFBSSxFQUFFLFNBQVMsRUFBUSxRQUFRLEVBQUUsU0FBUyxFQUFPLEdBQUcsRUFBRSxJQUFJLEVBQUcsS0FBSyxFQUFDLFNBQVMsRUFBRSxFQUN4RyxFQUFFLEtBQUssRUFBRSxPQUFPLEVBQVUsSUFBSSxFQUFFLE9BQU8sRUFBVSxRQUFRLEVBQUUsT0FBTyxFQUFTLEdBQUcsRUFBRSxJQUFJLEVBQUcsS0FBSyxFQUFDLGNBQWMsRUFBRSxFQUM3RyxFQUFFLEtBQUssRUFBRSxTQUFTLEVBQVEsSUFBSSxFQUFFLFNBQVMsRUFBUSxRQUFRLEVBQUUsZUFBZSxFQUFFLEdBQUcsRUFBRSxJQUFJLEVBQUUsS0FBSyxFQUFDLFNBQVMsRUFBRSxFQUN4RyxFQUFFLEtBQUssRUFBRSxnQkFBZ0IsRUFBRSxJQUFJLEVBQUUsY0FBYyxFQUFFLFFBQVEsRUFBRSxnQkFBZ0IsRUFBWSxLQUFLLEVBQUUsUUFBUSxFQUFFLEVBQ3hHLEVBQUUsS0FBSyxFQUFFLGFBQWEsRUFBSSxJQUFJLEVBQUUsWUFBWSxFQUFLLFFBQVEsRUFBRSxjQUFjLEVBQVksS0FBSyxFQUFFLFFBQVEsRUFBRSxFQUN0RyxFQUFFLEtBQUssRUFBRSxtQkFBbUIsRUFBSyxJQUFJLEVBQUUsYUFBYSxFQUFFLFFBQVEsRUFBRSxlQUFlLEVBQVMsS0FBSyxFQUFFLGNBQWMsRUFBRSxFQUMvRyxFQUFFLEtBQUssRUFBRSw2QkFBNkIsRUFBSyxJQUFJLEVBQUUsZ0JBQWdCLEVBQUUsUUFBUSxFQUFFLGNBQWMsRUFBRyxLQUFLLEVBQUUsYUFBYSxFQUFFLENBRXJILENBQUMsQ0FBQzs7QUFFSCxnQkFBSSxDQUFDLE1BQU0sR0FBRyxNQUFNLENBQUM7V0FFdEI7OztlQWhCVSxHQUFHIiwiZmlsZSI6ImFwcC5qcyIsInNvdXJjZXNDb250ZW50IjpbImltcG9ydCAnYm9vdHN0cmFwJztcbmltcG9ydCAnYm9vdHN0cmFwL2Nzcy9ib290c3RyYXAuY3NzISc7XG5cbmV4cG9ydCBjbGFzcyBBcHAge1xuICBjb25maWd1cmVSb3V0ZXIoY29uZmlnLCByb3V0ZXIpe1xuICAgIGNvbmZpZy50aXRsZSA9ICdEYXJ0cyc7XG4gICAgY29uZmlnLm1hcChbXG4gICAgICB7IHJvdXRlOiBbJycsJ3dlbGNvbWUnXSwgIG5hbWU6ICd3ZWxjb21lJywgICAgICAgbW9kdWxlSWQ6ICd3ZWxjb21lJywgICAgICBuYXY6IHRydWUsICB0aXRsZTonV2VsY29tZScgfSxcbiAgICAgIHsgcm91dGU6ICd1c2VycycsICAgICAgICAgbmFtZTogJ3VzZXJzJywgICAgICAgICBtb2R1bGVJZDogJ3VzZXJzJywgICAgICAgIG5hdjogdHJ1ZSwgIHRpdGxlOidHaXRodWIgVXNlcnMnIH0sXG4gICAgICB7IHJvdXRlOiAnc2Vhc29ucycsICAgICAgIG5hbWU6ICdzZWFzb25zJywgICAgICAgbW9kdWxlSWQ6ICdzZWFzb25zL2luZGV4JywgbmF2OiB0cnVlLCB0aXRsZTonU2Vhc29ucycgfSxcbiAgICAgIHsgcm91dGU6ICdzZWFzb25zL2NyZWF0ZScsIG5hbWU6ICdjcmVhdGVTZWFzb24nLCBtb2R1bGVJZDogJ3NlYXNvbnMvY3JlYXRlJywgICAgICAgICAgIHRpdGxlOiAnU2Vhc29uJyB9LFxuICAgICAgeyByb3V0ZTogJ3NlYXNvbnMvOmlkJywgICBuYW1lOiAnc2Vhc29uRWRpdCcsICAgIG1vZHVsZUlkOiAnc2Vhc29ucy9lZGl0JywgICAgICAgICAgIHRpdGxlOiAnU2Vhc29uJyB9LFxuICAgICAgeyByb3V0ZTogJ3NlYXNvbnMvOmlkL3RlYW1zJywgICAgbmFtZTogJ3NlYXNvblRlYW1zJywgbW9kdWxlSWQ6ICdzZWFzb25zL3RlYW1zJywgICAgICAgIHRpdGxlOiAnU2Vhc29uIFRlYW1zJyB9LFxuICAgICAgeyByb3V0ZTogJ3NlYXNvbnMvOnNlYXNvbklkL3RlYW1zLzppZCcsICAgIG5hbWU6ICdzZWFzb25UZWFtRWRpdCcsIG1vZHVsZUlkOiAnc2Vhc29ucy90ZWFtJywgIHRpdGxlOiAnU2Vhc29uIFRlYW0nIH1cblxuICAgIF0pO1xuXG4gICAgdGhpcy5yb3V0ZXIgPSByb3V0ZXI7XG5cbiAgfVxufSJdLCJzb3VyY2VSb290IjoiL3NvdXJjZS8ifQ==