const managerReducerDefaultState = {};

export default (state = managerReducerDefaultState, action) => {
    switch (action.type) {
        case 'SET_MANAGER_ID':
            return {...state, id: action.id};
            
        case 'SET_MANAGER_NAME':
            return {...state, name: action.name};
        default:
            return state;
    }
};
