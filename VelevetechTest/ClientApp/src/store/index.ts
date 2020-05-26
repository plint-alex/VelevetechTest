import { reducer as reduxFormReducer } from 'redux-form';
import * as Students from './Students';
import * as Authentication from './Authentication';
import * as Layout from './Layout';
import * as Notifier from './Notifier';

// The top-level state object
export interface ApplicationState {
}

// Whenever an action is dispatched, Redux will update each top-level application state property using
// the reducer with the matching name. It's important that the names match exactly, and that the reducer
// acts on the corresponding ApplicationState property type.
export const reducers = {
    authentication: Authentication.reducer,
    layout: Layout.reducer,
    notifier: Notifier.reducer,
    students: Students.reducer,
    form: reduxFormReducer,
};

// This type can be used as a hint on action creators so that its 'dispatch' and 'getState' params are
// correctly typed to match your store.
export interface AppThunkAction<TAction> {
    (dispatch: (action: TAction) => void, getState: () => ApplicationState): void;
}
