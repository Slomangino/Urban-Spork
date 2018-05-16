import * as types from "./types/actionTypes";
import UrbanSporkAPI from "../api/UrbanSporkAPI";


export const getAllPermissions = () => (dispatch) => {
    return UrbanSporkAPI.getAllPermissions()

        .then(payload => {
            console.log('payload ', payload)
            dispatch(getAllPermissionsSuccess(payload))
        })
        .catch( error => {
            throw (error);
        });
};


const getAllPermissionsSuccess = (permissions) => ({
    type: types.GET_ALL_PERMISSIONS,
    permissions
});


