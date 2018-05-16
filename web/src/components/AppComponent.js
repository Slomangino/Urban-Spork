import React from 'react';
import {connect} from "react-redux";

import AppRouter from "../routers/AppRouter";
import LogInPage from "./LogInPage";
import {getUsersData} from "../actions/users";
import {setManagerName, setManagerId} from "../actions/manager";
import {getAllPermissions} from "../actions/permissions";
import NonAdminRouter from "../routers/NonAdminRouter";


class AppComponent extends React.Component{

    state = {
        logIn: true,
        isAdmin: undefined,
        isNonAdmin: undefined,
        allowAccess: true,
        managerId: undefined
    };


    accessGranted = () => {
        // this.setState(() => ({allowAccess: true, managerId}));
        this.props.getUserData();
        this.props.getAllPermissions();

    };


    isAdmin = (b, id, name) => {
        console.log(b);

        this.props.setManagerId(id);
        this.props.setManagerName(name);

        if (b == "true") {

            this.setState({isAdmin: true});
            this.setState({isNonAdmin: false});
            this.setState({logIn: false});
        } else {
            console.log("Non-Admin User");

            this.setState({isNonAdmin: true});
            this.setState({isAdmin: false});
            this.setState({logIn: false});
        }
    };



    render() {
        return (
            <div>
                {}
                {this.state.logIn  && <LogInPage isAdmin={this.isAdmin}/> }
                {this.state.isAdmin && ( <AppRouter grantAccess={this.accessGranted()} /> )}
                {this.state.isNonAdmin && (<NonAdminRouter/>)}
                {/*{this.accessGranted()}*/}
                {/*{this.state.allowAccess? ( <AppRouter /> ) : ( <LogInPage accessGranted={this.accessGranted}/>)}*/}
            </div>
        )
    }

}

const mapDispatchToProps = (dispatch)  => ({
    getUserData: () => dispatch(getUsersData()),
    setManagerId: (id) => dispatch(setManagerId(id)),
    setManagerName: (name) => dispatch(setManagerName(name)),
    getAllPermissions: () => dispatch(getAllPermissions()),
});

export default connect(undefined,mapDispatchToProps)(AppComponent)