System.register([], function (_export) {
  'use strict';

  _export('configure', configure);

  function configure(aurelia) {
    aurelia.use.standardConfiguration().developmentLogging().plugin('aurelia-animator-css').plugin('aurelia-dialog');

    aurelia.start().then(function (a) {
      return a.setRoot();
    });
  }

  return {
    setters: [],
    execute: function () {}
  };
});
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbIm1haW4uanMiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6Ijs7Ozs7QUFBTyxXQUFTLFNBQVMsQ0FBQyxPQUFPLEVBQUU7QUFDakMsV0FBTyxDQUFDLEdBQUcsQ0FDUixxQkFBcUIsRUFBRSxDQUN2QixrQkFBa0IsRUFBRSxDQUNwQixNQUFNLENBQUMsc0JBQXNCLENBQUMsQ0FDNUIsTUFBTSxDQUFDLGdCQUFnQixDQUFDLENBQUM7O0FBRTlCLFdBQU8sQ0FBQyxLQUFLLEVBQUUsQ0FBQyxJQUFJLENBQUMsVUFBQSxDQUFDO2FBQUksQ0FBQyxDQUFDLE9BQU8sRUFBRTtLQUFBLENBQUMsQ0FBQztHQUN4QyIsImZpbGUiOiJtYWluLmpzIiwic291cmNlc0NvbnRlbnQiOlsiZXhwb3J0IGZ1bmN0aW9uIGNvbmZpZ3VyZShhdXJlbGlhKSB7XG4gIGF1cmVsaWEudXNlXG4gICAgLnN0YW5kYXJkQ29uZmlndXJhdGlvbigpXG4gICAgLmRldmVsb3BtZW50TG9nZ2luZygpXG4gICAgLnBsdWdpbignYXVyZWxpYS1hbmltYXRvci1jc3MnKVxuICAgICAgLnBsdWdpbignYXVyZWxpYS1kaWFsb2cnKTtcblxuICBhdXJlbGlhLnN0YXJ0KCkudGhlbihhID0+IGEuc2V0Um9vdCgpKTtcbn1cbiJdLCJzb3VyY2VSb290IjoiL3NvdXJjZS8ifQ==