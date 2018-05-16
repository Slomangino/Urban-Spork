import React from 'react';
import {BrowserRouter, Switch, Route} from 'react-router-dom';

import NotFound from '../components/NotFound'

import DashboardPage from '../components/DashboardPage';
import HeaderComponent from '../components/HeaderComponent';
import UserManagementPage from '../components/UserManagementPage';
import CreateUser from "../components/CreateUser";
import Company from "../components/Company";
import SystemReport from "../components/SystemReport";
import SystemActivityReport from "../components/SystemActivityReport";
import ApproverActivityReport from "../components/ApproverActivityReport";
import UserDetailComponent from "../components/UserDetailComponent";


const AppRouter = () => (
    <BrowserRouter>
        <div className={"canvas"}>
            <HeaderComponent/>
            <div className={"lower-canvas"}>
                <Switch>
                    <Route path={"/"} component={DashboardPage} exact={true}/>
                    <Route path={"/users"} component={UserManagementPage}/>
                    <Route path={"/company"} component={Company} exact={true}/>

                    <Route path={"/create-user"} component={CreateUser}/>

                    <Route path={"/user-detail/:id"} component={UserDetailComponent}/>

                    
                    <Route path={"/reports/system-report"} component={SystemReport}/>
                    <Route path={"/reports/system-activity-report"} component={SystemActivityReport}/>
                    <Route path={"/reports/approver-activity-report"} component={ApproverActivityReport}/>

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

export default AppRouter;