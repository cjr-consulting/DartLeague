import React from 'react';
import Header from './common/header';

var App = React.createClass({
    render: function() {
        return (
            <div>
                <Header/>
                <div className="container-fluid">
                    {this.props.children}
                </div>
            </div>
        );
    }
});

export default App;