/*eslint-disable strict */ //Disabling check because we can't run strict mode. Need global vars.
$ = jQuery = require('jquery');

var React = require('react');
var ReactRouter = require('react-router');
var Router = ReactRouter.Router;
var routes = require('./routes');

React.render(<Router>{routes}</Router>, document.getElementById('app'));
