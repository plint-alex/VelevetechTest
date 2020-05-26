
const loadingType = 'LOADING';
const loadingCompleteType = 'LOADING_COMPLETE';

const toggleDrawerType = 'TOGGLE_DRAWER';

const callUsDialogOpenType = 'COLL_US_DIALOG_OPEN';

const initialState = { loading: false, mobileOpen: false };

export const actionCreators = {
    loading: () => {
        return { type: loadingType };
    },
    loadingComplete: () => {
        return { type: loadingCompleteType };
    },
    toggleDrawer: () => {
        return { type: toggleDrawerType };
    },
    callUsDialogOpen: (value) => {
        return { type: callUsDialogOpenType, callUsDialogOpen: value };
    },
};

export const reducer = (state, action) => {
    state = state || initialState;

    if (action.type === callUsDialogOpenType) {
        const newState = {
            ...state,
            loading: true
        };
        return newState;
    }
    if (action.type === loadingCompleteType) {
        return {
            ...state,
            loading: false
        };
    }
    if (action.type === toggleDrawerType) {
        return {
            ...state,
            mobileOpen: !state.mobileOpen
        };
    }
    if (action.type === callUsDialogOpenType) {
        const newState = {
            ...state,
            callUsDialogOpen: action.callUsDialogOpen,
        };
        return newState;
    }
    return state;
};