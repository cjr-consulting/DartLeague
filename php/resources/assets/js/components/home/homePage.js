import React from 'react';

var HomePage = React.createClass({
    render: function() {
        return (
            <div className="container-fluid">
                <div className="row">
                    <div className="col-md-10">
                        Welcome to the first of many Components.
                    </div>
                    <div className="col-md-2">
                    </div>
                </div>
            </div>
        );
    }
});

export default HomePage;