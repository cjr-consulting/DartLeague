System.register(['aurelia-framework', 'aurelia-fetch-client', 'fetch'], function (_export) {
  'use strict';

  var inject, HttpClient, Users;

  var _createClass = (function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ('value' in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; })();

  function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError('Cannot call a class as a function'); } }

  return {
    setters: [function (_aureliaFramework) {
      inject = _aureliaFramework.inject;
    }, function (_aureliaFetchClient) {
      HttpClient = _aureliaFetchClient.HttpClient;
    }, function (_fetch) {}],
    execute: function () {
      Users = (function () {
        _createClass(Users, null, [{
          key: 'inject',
          value: [HttpClient],
          enumerable: true
        }]);

        function Users(http) {
          _classCallCheck(this, Users);

          this.heading = 'Github Users';
          this.users = [];

          http.configure(function (config) {
            config.useStandardConfiguration().withBaseUrl('https://api.github.com/');
          });

          this.http = http;
        }

        _createClass(Users, [{
          key: 'activate',
          value: function activate() {
            var _this = this;

            return this.http.fetch('users').then(function (response) {
              return response.json();
            }).then(function (users) {
              return _this.users = users;
            });
          }
        }]);

        return Users;
      })();

      _export('Users', Users);
    }
  };
});
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInVzZXJzLmpzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiI7OzswQkFJYSxLQUFLOzs7Ozs7OztpQ0FKVixNQUFNOzt1Q0FDTixVQUFVOzs7QUFHTCxXQUFLO3FCQUFMLEtBQUs7O2lCQUNBLENBQUMsVUFBVSxDQUFDOzs7O0FBSWpCLGlCQUxBLEtBQUssQ0FLSixJQUFJLEVBQUM7Z0NBTE4sS0FBSzs7ZUFFaEIsT0FBTyxHQUFHLGNBQWM7ZUFDeEIsS0FBSyxHQUFHLEVBQUU7O0FBR1IsY0FBSSxDQUFDLFNBQVMsQ0FBQyxVQUFBLE1BQU0sRUFBSTtBQUN2QixrQkFBTSxDQUNILHdCQUF3QixFQUFFLENBQzFCLFdBQVcsQ0FBQyx5QkFBeUIsQ0FBQyxDQUFDO1dBQzNDLENBQUMsQ0FBQzs7QUFFSCxjQUFJLENBQUMsSUFBSSxHQUFHLElBQUksQ0FBQztTQUNsQjs7cUJBYlUsS0FBSzs7aUJBZVIsb0JBQUU7OztBQUNSLG1CQUFPLElBQUksQ0FBQyxJQUFJLENBQUMsS0FBSyxDQUFDLE9BQU8sQ0FBQyxDQUM1QixJQUFJLENBQUMsVUFBQSxRQUFRO3FCQUFJLFFBQVEsQ0FBQyxJQUFJLEVBQUU7YUFBQSxDQUFDLENBQ2pDLElBQUksQ0FBQyxVQUFBLEtBQUs7cUJBQUksTUFBSyxLQUFLLEdBQUcsS0FBSzthQUFBLENBQUMsQ0FBQztXQUN0Qzs7O2VBbkJVLEtBQUsiLCJmaWxlIjoidXNlcnMuanMiLCJzb3VyY2VzQ29udGVudCI6WyJpbXBvcnQge2luamVjdH0gZnJvbSAnYXVyZWxpYS1mcmFtZXdvcmsnO1xuaW1wb3J0IHtIdHRwQ2xpZW50fSBmcm9tICdhdXJlbGlhLWZldGNoLWNsaWVudCc7XG5pbXBvcnQgJ2ZldGNoJztcblxuZXhwb3J0IGNsYXNzIFVzZXJze1xuICBzdGF0aWMgaW5qZWN0ID0gW0h0dHBDbGllbnRdO1xuICBoZWFkaW5nID0gJ0dpdGh1YiBVc2Vycyc7XG4gIHVzZXJzID0gW107XG5cbiAgY29uc3RydWN0b3IoaHR0cCl7XG4gICAgaHR0cC5jb25maWd1cmUoY29uZmlnID0+IHtcbiAgICAgIGNvbmZpZ1xuICAgICAgICAudXNlU3RhbmRhcmRDb25maWd1cmF0aW9uKClcbiAgICAgICAgLndpdGhCYXNlVXJsKCdodHRwczovL2FwaS5naXRodWIuY29tLycpO1xuICAgIH0pO1xuXG4gICAgdGhpcy5odHRwID0gaHR0cDtcbiAgfVxuXG4gIGFjdGl2YXRlKCl7XG4gICAgcmV0dXJuIHRoaXMuaHR0cC5mZXRjaCgndXNlcnMnKVxuICAgICAgLnRoZW4ocmVzcG9uc2UgPT4gcmVzcG9uc2UuanNvbigpKVxuICAgICAgLnRoZW4odXNlcnMgPT4gdGhpcy51c2VycyA9IHVzZXJzKTtcbiAgfVxufVxuIl0sInNvdXJjZVJvb3QiOiIvc291cmNlLyJ9