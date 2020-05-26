import { applyMiddleware, combineReducers, compose, createStore } from 'redux';
import thunkMiddleware from 'redux-thunk-recursion-detect';
import createThunkErrorHandlerMiddleware from 'redux-thunk-error-handler';
import { connectRouter, routerMiddleware } from 'connected-react-router';
import { History } from 'history';
import { ApplicationState, reducers } from './';
import * as Notifier from './Notifier';

export default function configureStore(history: History, initialState?: ApplicationState) {

    const errorHandler = (err: any) => {
        console.error(err); // write the error to the console
        return (dispatch: any/*, getState*/) => { // or return a thunk

            dispatch(Notifier.actionCreators.enqueueSnackbar({
                message: err.message,
                options: {
                    key: new Date().getTime() + Math.random(),
                    variant: 'error'
                },
            }));
        };
    };

    const errorHandlerMiddleware = createThunkErrorHandlerMiddleware({ onError: errorHandler });

    const middleware = [
        errorHandlerMiddleware,
        thunkMiddleware,
        routerMiddleware(history)
    ];

    const rootReducer = combineReducers({
        ...reducers,
        router: connectRouter(history)
    });

    const enhancers = [];
    const windowIfDefined = typeof window === 'undefined' ? null : window as any;
    if (windowIfDefined && windowIfDefined.__REDUX_DEVTOOLS_EXTENSION__) {
        enhancers.push(windowIfDefined.__REDUX_DEVTOOLS_EXTENSION__());
    }

    return createStore(
        rootReducer,
        initialState,
        compose(applyMiddleware(...middleware), ...enhancers)
    );
}
