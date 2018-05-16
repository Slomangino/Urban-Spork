import React from 'react';
import {Button, FormGroup, Input, Label} from "reactstrap";
import {setTextFilter} from "../actions/filters";
import {connect} from "react-redux";
import selectUsers from "../selectors/users";

const OffBoardingReportPartial = (props) => (
    <div>

        <h1 className={"text-center"}>Off-Boarding Report</h1>
        <h5 className={"text-center"}>Lists all systems to be revoked when a person parts an organization</h5>

        <div className={'user-management-nav'} style={{justifyContent: 'space-around'}}>
            <Button color={'success'} onClick={props.handleOnClick}>User Report</Button>

            <FormGroup>
                <Input
                    type={'text'}
                    value={props.filters.text}
                    onChange={props.onInputChange}
                    placeholder={'Search for User'}/>
            </FormGroup>

        </div>
        <div>
            <Label for="users">Users:</Label>
            <Input type="select" name="users" id="users" onClick={(e)=> console.log('Permission id', e.target.id)} multiple>
                {/*
                        This is a jumble of references, but the function I created doesn't work on it...
                        so I am leaving it like this.
                    */}
                {
                    props.userData && Object.keys(props.userData.permissionList).map((permission, i) => {
                        if (props.userData.permissionList[permission].permissionStatus !== 'Revoked') {
                            return <option
                                key={i} value={i}
                                id={permission}>{props.userData.permissionList[permission].permissionName}
                            </option>;
                        }
                    })
                }
            </Input>

            <Button color="danger" style={{marginTop: "20px"}}>Edit Permissions</Button>
        </div>

        <div>
            <Label for="permissions">Permissions:</Label>
            <Input type="select" name="permissions" id="permissions" onClick={(e)=> console.log('Permission id', e.target.id)} multiple>
                {/*
                        This is a jumble of references, but the function I created above doesn't work on it...
                        so I am leaving it like this.
                    */}
                {
                    props.userData && Object.keys(props.userData.permissionList).map((permission, i) => {
                        if (props.userData.permissionList[permission].permissionStatus !== 'Revoked') {
                            return <option
                                key={i} value={i}
                                id={permission}>{props.userData.permissionList[permission].permissionName}
                                </option>;
                        }
                    })
                }
            </Input>

            <Button color="danger" style={{marginTop: "20px"}}>Edit Permissions</Button>
        </div>
    </div>
);


export class OffBoardingReport extends React.Component {
    state = {
        selectedUser: undefined,
    };


    // TODO: Have to apply the GET inside of handleOnClick and set the return inside of state.userData

    handleOnClick = () => {
        console.log(this.props.users);

        if (this.props.users.length === 1) {
            this.setState({selectedUser: this.props.users});
        }
    };

    onInputChange = (e) => {
        this.props.setTextFilter(e.target.value);
    };

    render() {
        return (
            <OffBoardingReportPartial
                handleOnClick={this.handleOnClick}
                onInputChange={this.onInputChange}
                users={this.props.users}
                selectedUser={this.state.selectedUser}
                filters={this.props.filters}
                userData={this.state.userData}
            />
        )
    }
}

const mapStateToProps = (state) => {
    return {
        users: selectUsers(state.users, state.filters),
        filters: state.filters
    }
};

const mapDispatchToProps = (dispatch)  => ({
    setTextFilter: (text) => dispatch(setTextFilter(text)),
    // getUserData: () => dispatch(getUsersData())
});


export default connect(mapStateToProps, mapDispatchToProps)(OffBoardingReport);
