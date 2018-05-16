import {applyMiddleware, combineReducers, compose, createStore} from 'redux';
import usersReducer from '../reducers/users';
import filtersReducer from '../reducers/filters';
import managerReducer from '../reducers/manager';
import permissionsReducer from '../reducers/permissions';
import thunk from 'redux-thunk';


const composeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;

export default () => {
    const store = createStore(
        combineReducers({
            manager: managerReducer,
            users: usersReducer,
            filters: filtersReducer,
            permissions: permissionsReducer

        }),

        composeEnhancers(applyMiddleware(thunk))
        // window.__REDUX_DEVTOOLS_EXTENSION__ && window.__REDUX_DEVTOOLS_EXTENSION__()
    );

    return store;
};
