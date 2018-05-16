import NonAdminView from "../components/NonAdminView";
import React from 'react';
import {BrowserRouter, Switch, Route} from 'react-router-dom';
import NotFound from '../components/NotFound'
import UserManagementPage from '../components/UserManagementPage';
import NonAdminHeader from '../components/NonAdminHeader'



const NonAdminRouter = () => (
    <BrowserRouter>
        <div className={"canvas"}>
            <NonAdminHeader/>
            <div className={"lower-canvas"}>
                <Switch>
                    <Route path={"/"} component={NonAdminView} exact={true}/>
                    <Route path="/users" component={UserManagementPage}/>


                    {/*
                    This needs to be the last component route
                    it acts as a default case
                    */}
                    <Route component={NotFound}/>
                </Switch>
            </div>

        </div>
    </BrowserRouter>
);

export default NonAdminRouter;