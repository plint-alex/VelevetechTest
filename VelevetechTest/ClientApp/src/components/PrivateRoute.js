import React from 'react';
import { connect } from 'react-redux';
import { Route, Redirect } from 'react-router';

let PrivateRoute = ({ component: Component, ...rest }) => {

    return (
        <Route {...rest} render={props => {
            return rest.isLoggedIn
                ? <Component {...props} {...rest}/>
                : <Redirect to={{ pathname: '/login', state: { from: props.location } }} />
        }} />
    );
};

let mapState = (state) => {

    return {
        isLoggedIn: state.authentication && state.authentication.auth
    };
};

PrivateRoute = connect(mapState)(PrivateRoute);

export { PrivateRoute };