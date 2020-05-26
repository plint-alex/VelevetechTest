import history from '../history';
import authenticationService from '../services/authenticationService';
import { actionCreators as layoutActions } from './Layout';

const loginSuccessType = 'LOGIN_SUCCESS';
const loginFailureType = 'LOGIN_FAILURE';

export const logoutType = 'LOGOUT';

let auth = authenticationService.getAuth();
const initialState = auth ? { auth } : {};

export const actionCreators = {
    login: (data) => {

        let success = (auth) => { return { type: loginSuccessType, auth }; };

        return async dispatch => {
            dispatch(layoutActions.loading());

            try {
                const auth = await authenticationService.login(data);

                dispatch(success(auth));
                history.push('/');
            }
            finally {
                dispatch(layoutActions.loadingComplete());
            }
        };
    },
    logout: () => {
        return async dispatch => {
            dispatch({ type: logoutType });
            history.push('/');
            await authenticationService.logout();
        };
    },
};

export const reducer = (state, action) => {
    state = state || initialState;

    if (action.type === loginSuccessType) {
        return {
            ...state,
            auth: action.auth,
            error: undefined,
            actualDbVersions: undefined
        };
    }
    if (action.type === loginFailureType) {
        return {
            ...state,
            auth: undefined,
            error: action.error && action.error.message ? action.error.message : action.error
        };
    }

    if (action.type === logoutType) {
        return {};
    }

    return state;
};
