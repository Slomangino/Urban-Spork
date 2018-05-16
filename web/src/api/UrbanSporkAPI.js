import React from "react";
import {connect} from "react-redux";
import {getUsersData} from "../actions/users";

export class UrbanSporkAPI extends React.Component {

    static getAllUsers() {
        return fetch('http://localhost:5000/api/user/getusermanagementprojection')
            .then(response => {
                console.log('UserData gotten');
                return response.json();
            }).catch(error => {
                return error;
            });
    }

    static getAllPermissions() {
        return fetch('http://localhost:5000/api/permission/getSystemDropdown')
            .then(response => {
                return response.json();
            }).catch(error => {
                return error;
            });
    }

    static getTemplates() {
        return fetch('http://localhost:5000/api/permission/getTemplates')
            .then(response => {
                return response.json();
            }).catch(error => {
                return error;
            });
    }

    // http://localhost:5000/api/user/id/6da06fc4-02cb-4ce3-b63f-112596fe6bff
    static getUserFullData(id) {
        return fetch(`http://localhost:5000/api/user/id/${id}`).then(response => {
            return response.json();
        }).catch(error => {
            return error;
        });
    }

    static getPendingRequests() {
        return fetch('http://localhost:5000/api/permission/getpendingrequests').then(response => {
            return response.json();
        }).catch(error => {
            console.log(error);
            return [];
        });
    }

    static getPermissions() {
        return fetch('http://localhost:5000/api/permission').then(response => {
            return response.json();
        }).catch(error => {
            console.log(error);
            return [];
        });
    }

    static getDepartments() {
        return fetch('http://localhost:5000/api/department').then(response => {
            return response.json();
        }).catch(error => {
            console.log(error);
            return [];
        });
    }

    static getTemplates() {
        return fetch('http://localhost:5000/api/permission/getTemplates').then(response => {
            return response.json();
        }).catch(error => {
            console.log(error);
            return [];
        });
    }

    static getSystemDashboard() {
        return fetch('http://localhost:5000/api/user/getSystemDashboard').then(response => {
            return response.json();
        }).catch(error => {
            console.log(error);
            return [];
        });
    }

    static getSystemsActivity() {
        return fetch('http://localhost:5000/api/user/getSystemActivityProjection').then(response => {
            return response.json();
        }).catch(error => {
            console.log(error);
            return [];
        });
    }

    static getSystemActivityReport(payload) {
        return fetch(`http://localhost:5000/api/user/getSystemActivityProjection?PermissionId=${payload.PermissionId}`).then(response => {
            return response.json();
        }).catch(error => {
            console.log(error);
            return [];
        });
    }

    static getRequestsByID(payload) {
        return fetch(`http://localhost:5000/api/permission/getPendingRequestsById?id=${payload.UserID}`).then(response => {
            return response.json();
        }).catch(error => {
            console.log(error);
            return [];
        });
    }

    static getSystemsDropDown() {
        return fetch('http://localhost:5000/api/permission/getSystemDropdown').then(response => {
            return response.json();
        }).catch(error => {
            console.log(error);
            return [];
        });
    }

    static getLogginDropDown() {
        return fetch('http://localhost:5000/api/user/getloginusers').then(response => {
            return response.json();
        }).catch(error => {
            console.log(error);
            return [];
        });
    }

    static getApproverDropDown() {
        return fetch('http://localhost:5000/api/user/getApproverList').then(response => {
            return response.json();
        }).catch(error => {
            console.log(error);
            return [];
        });
    }

    static getUserHistory(userID){
        return fetch(`http://localhost:5000/api/user/getUserHistory?UserId=${userID}`).then(response => {
            return response.json();
        }).catch(error => {
            console.log(error);
            return [];
        });
    }

    static getSystemReport(systemID){
        return fetch(`http://localhost:5000/api/user/getSystemReport?PermissionId=${systemID.PermissionId}`).then(response => {
            return response.json();
        }).catch(error => {
            console.log(error);
            return [];
        });
    }

    static getApproverActivity() {
        return fetch('http://localhost:5000/api/user/getapproveractivity').then(response => {
            return response.json();
        }).catch(error => {
            console.log(error);
            return [];
        });
    }

    static getApproverActivityReport(payload) {
        return fetch(`http://localhost:5000/api/user/getapproveractivity?ApproverId=${payload.ApproverId}`).then(response => {
            return response.json();
        }).catch(error => {
            console.log(error);
            return [];
        });
    }

    // TODO: This is not working, talk to Stephen/Tyler(s) about it.
    static getOffBoardingReport(id) {
        return fetch(`http://localhost:5000/api/user/offboard/${id}`).then(response => {
            return response.json();
        }).catch(error => {
            console.log(error);
            return [];
        });
    }

    static updateUserDetails(data) {
        return fetch('http://localhost:5000/api/user/update', {
            body: JSON.stringify(data),
            credentials: 'same-origin',
            headers: {
                'content-type': 'application/json'
            },
            method: 'PUT'
        }).then(response => {
            return response.json();
        }).catch(error => {
            console.log(data);
            console.log(error);
        });
    }

    static enableUser(data) {
        return fetch('http://localhost:5000/api/user/enable', {
            body: JSON.stringify(data),
            credentials: 'same-origin',
            headers: {
                'content-type': 'application/json'
            },
            method: 'PUT'
        }).then(response => {

            console.log('disable ', data);
            return response.json();
        }).catch(error => {
            console.log(data);
            console.log(error);
        });
    }

    static disableUser(data) {
        return fetch('http://localhost:5000/api/user/disable', {
            body: JSON.stringify(data),
            credentials: 'same-origin',
            headers: {
                'content-type': 'application/json'
            },
            method: 'PUT'
        }).then(response => {

            console.log('disable ', data);
            return response.json();
        }).catch(error => {
            console.log(data);
            console.log(error);
        });
    }

    static grantRevokePermissions(data) {
        return fetch('http://localhost:5000/api/user/grantRevokePermissions', {
            body: JSON.stringify(data),
            credentials: 'same-origin',
            headers: {
                'content-type': 'application/json'
            },
            method: 'PUT'
        }).then(response => {
            return response.json();
        }).catch(error => {
            console.log(data);
            console.log(error);
        });
    }

    static grantPermission(data) {
        return fetch('http://localhost:5000/api/user/grantPermissions', {
            body: JSON.stringify(data),
            credentials: 'same-origin',
            headers: {
                'content-type': 'application/json'
            },
            method: 'PUT'
        }).then(response => {
            return response.json();
        }).catch(error => {
            console.log(data);
            console.log(error);
        });
    }

    static denyPermission(data) {
        return fetch('http://localhost:5000/api/user/denyPermissions', {
            body: JSON.stringify(data),
            credentials: 'same-origin',
            headers: {
                'content-type': 'application/json'
            },
            method: 'PUT'
        }).then(response => {
            return response.json();
        }).catch(error => {
            console.log(data);
            console.log(error);
        });
    }

    static updatePermission(data) {
        return fetch('http://localhost:5000/api/permission/update', {
            body: JSON.stringify(data),
            credentials: 'same-origin',
            headers: {
                'content-type': 'application/json'
            },
            method: 'PUT'
        }).then(response => {
            return response.json();
        }).catch(error => {
            console.log(data);
            console.log(error);
        });
    }

    static requestPermission(data) {
        return fetch('http://localhost:5000/api/user/requestPermissions',{
            body: JSON.stringify(data),
            credentials: 'same-origin',
            headers: {
                'content-type': 'application/json'
            },
            method: 'PUT'
        }).then(response => {
            return response.json();
        }).catch(error => {
            console.log(data);
            console.log(error);
        });
    }

    static grantPermissions(data) {
        return fetch('http://localhost:5000/api/user/grantPermissions',{
            body: JSON.stringify(data),
            credentials: 'same-origin',
            headers: {
                'content-type': 'application/json'
            },
            method: 'PUT'
        }).then(response => {
            return response.json();
        }).catch(error => {
            console.log(data);
            console.log(error);
        });
    }

    static createUser(data) {
        return fetch('http://localhost:5000/api/user/createuser', {
            body: JSON.stringify(data),
            credentials: 'same-origin',
            headers: {
                'content-type': 'application/json'
            },
            method: 'POST'
        }).then(response => {
            return response.json();
        }).catch(error => {
            console.log('data', data);
            console.log('error', error);
        });
    }

    static addDepartment(data) {
        return fetch('http://localhost:5000/api/department/create', {
            body: JSON.stringify(data),
            credentials: 'same-origin',
            headers: {
                'content-type': 'application/json'
            },
            method: 'POST'
        }).then(response => {
            return response.json();
        }).catch(error => {

            console.log('data', data);
            console.log('error', error);
        });
    }

    static removeDepartmentByName(data) {
        return fetch('http://localhost:5000/api/department/removeByName',{
            body: JSON.stringify(data),
            credentials: 'same-origin',
            headers: {
                'content-type': 'application/json'
            },
            method: 'PUT'
        }).then(response => {
            return response.json();
        }).catch(error => {

            console.log('data', data);
            console.log('error', error);
        });
    }

    static addSystem(data) {
        return fetch('http://localhost:5000/api/permission/create',{
            body: JSON.stringify(data),
            credentials: 'same-origin',
            headers: {
                'content-type': 'application/json'
            },
            method: 'POST'
        }).then(response => {
            return response.json();
        }).catch(error => {

            console.log('data', data);
            console.log('error', error);
        });
    }

    static addPosition(data) {
        return fetch('http://localhost:5000/api/position/create',{
            body: JSON.stringify(data),
            credentials: 'same-origin',
            headers: {
                'content-type': 'application/json'
            },
            method: 'POST'
        }).then(response => {
            return response.json();
        }).catch(error => {

            console.log('data', data);
            console.log('error', error);
        });
    }

    static removePosition(data) {
        return fetch(`http://localhost:5000/api/position/remove?id=${data}`, {
            credentials: 'same-origin',
                headers: {
                'content-type': 'application/json'
            },
            method: 'PUT'
        }).then(response => {
            return response.json();
        }).catch(error => {
            console.log(error);
            return [];
        });
    }

    static getPositionByDepartment(department) {
        return fetch(`http://localhost:5000/api/position/getByDepartment?name=${department}`).then(response => {
            return response.json();
        }).catch(error => {
            console.log(error);
            return [];
        });
    }

    static addTemplate(data) {
        return fetch('http://localhost:5000/api/permission/createPermissionTemplate',{
            body: JSON.stringify(data),
            credentials: 'same-origin',
            headers: {
                'content-type': 'application/json'
            },
            method: 'POST'
        }).then(response => {
            return response.json();
        }).catch(error => {

            console.log('data', data);
            console.log('error', error);
        });
    }

    static removeTemplate(data) {
        return fetch('http://localhost:5000/api/permission/deletePermissionTemplate',{
            body: JSON.stringify(data),
            credentials: 'same-origin',
            headers: {
                'content-type': 'application/json'
            },
            method: 'PUT'
        }).then(response => {
            return response.json();
        }).catch(error => {

            console.log('data', data);
            console.log('error', error);
        });
    }
}


const mapDispatchToProps = (dispatch) => ({
    getUsersData: () => dispatch(getUsersData())
});
export default connect(undefined, mapDispatchToProps)(UrbanSporkAPI);
