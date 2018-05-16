// Users Reducer
import * as types from '../actions/types/actionTypes';

const usersReducerDefaultState = [];


// const fakeData = [
//     {
//         id: 0,
//         name: 'Anon',
//         email: 'n@email.com',
//         department: 'Development',
//         title: 'Dev'
//     },
//     {
//         id: 1,
//         name: 'Anon2',
//         email: 'n@email.com',
//         department: 'Marketing',
//         title: 'Manager'
//     },
//     {
//         id: 3,
//         name: 'Mike T fields',
//         email: 'n@email.com',
//         department: 'Some other dep',
//         title: 'No title'
//     }
// ];

export default (state = usersReducerDefaultState, action) => {

    switch (action.type) {

        // GET
        case  types.GET_USERS_DATA:
            return [...action.users];

        case types.GET_USER_FULL_DATA:
            return [...action.userData];


        case 'ADD_USER':
            return [
                ...state,
                action.user
            ];


        case 'REMOVE_USER':
            return state.filter(({id}) => id !== action.id);


        case 'EDIT_USER':
            return state.map((user) => {
                if (user.id === action.id) {
                    return {
                        ...user,
                        ...action.updates
                    };
                } else {
                    return user;
                }
            });

        default:
            return state;

    }
};
