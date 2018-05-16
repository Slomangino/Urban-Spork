import * as types from "../actions/types/actionTypes";

const permissionsReducerDefaultState = [];

export default (state = permissionsReducerDefaultState, action) => {
    switch (action.type) {
        case  types.GET_ALL_PERMISSIONS:
            return [...action.permissions];
        default:
            return state;
    }
}