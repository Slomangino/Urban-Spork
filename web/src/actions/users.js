import * as types from './types/actionTypes';
import UrbanSporkAPI from '../api/UrbanSporkAPI';


export const getUsersData = () => (dispatch) => {
    return UrbanSporkAPI.getAllUsers()

        .then(payload => {

        dispatch(getUsersDataSuccess(payload))
    })
        .catch( error => {
        throw (error);
    });
};


const getUsersDataSuccess = (users) => ({
    type: types.GET_USERS_DATA,
    users
});


export const getUserFullData = (id) => (dispatch) => {
    return UrbanSporkAPI.getUserFullData(id)

        .then(payload =>{
            dispatch(getUserFullDataSuccess(payload))
        })

        .catch(error => {
            throw (error);
        })
};

const getUserFullDataSuccess = (userData) => ({
    type: types.GET_USER_FULL_DATA,
    userData
});

// ADD_USER
export const addUser = (user) => ({
    type: types.ADD_USER,
    user
});

// export const startAddUser = (userData = {}) => {
//     return (dispatch) => {
//         // default data
//         const {
//             name = 'Anon',
//             email = 'n@email.com',
//             department = 'Development',
//             title = 'No title'
//         } = userData;
//
//         const user = {name, email, department, title};
//
//         return database.ref('users').push(user).then((payload) => {
//             dispatch(addUser({
//                 id: payload.key,
//                 ...user
//             }));
//         });
//     };
// };


// REMOVE_USER
export const removeUser = ({id} = {}) => ({
    type: 'REMOVE_USER',
    id
});

// EDIT_USER
export const editUser = (id, updates) => ({
    type: 'EDIT_USER',
    id,
    updates
});
