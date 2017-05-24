import React from 'react';
import { Router, IndexRoute, Route } from 'react-router';

let routes = (
    <Route path="/" component={require('./components/app')}>
        <IndexRoute component={require('./components/home/homePage')} />
        <Route path="about" component={require('./components/about/aboutPage')} />
        <Route path="*" component={require('./components/notFoundPage')} />
    </Route>
);

export default routes;