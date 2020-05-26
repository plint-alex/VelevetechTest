const enqueueSnackbarType = 'ENQUEUE_SNACKBAR';
const closeSnackbarType = 'CLOSE_SNACKBAR';
const removeSnackbar = 'REMOVE_SNACKBAR';

const initialState = {
    notifications: [],
};

export const actionCreators = {

    enqueueSnackbar: notification => {
        const key = notification.options && notification.options.key;

        return {
            type: enqueueSnackbarType,
            notification: {
                ...notification,
                key: key || new Date().getTime() + Math.random(),
            },
        };
    },

    closeSnackbar: key => ({
        type: closeSnackbarType,
        dismissAll: !key, // dismiss all if no key has been defined
        key,
    }),

    removeSnackbar: key => ({
        type: removeSnackbar,
        key,
    }),
}

export const reducer = (state, action) => {
    state = state || initialState;
    switch (action.type) {
        case enqueueSnackbarType:
            return {
                ...state,
                notifications: [
                    ...state.notifications,
                    {
                        key: action.key,
                        ...action.notification,
                    },
                ],
            };

        case closeSnackbarType:
            return {
                ...state,
                notifications: state.notifications.map(notification => (
                    (action.dismissAll || notification.key === action.key)
                        ? { ...notification, dismissed: true }
                        : { ...notification }
                )),
            }

        case removeSnackbar:
            return {
                ...state,
                notifications: state.notifications.filter(
                    notification => notification.key !== action.key,
                ),
            };

        default:
            return state;
    }
};

