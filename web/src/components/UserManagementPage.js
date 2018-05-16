import React from 'react';
import {connect} from "react-redux";
import UserTable from "./UserTable";
import {Button, FormGroup, Input} from 'reactstrap';
import selectUsers from "../selectors/users";
import {setTextFilter} from "../actions/filters";
import UserDetailsModal from "./UserDetailsModal";
import {editUser, getUsersData} from "../actions/users";
import UserDetailComponent from "./UserDetailComponent";

export class UserManagementPage extends React.Component {
    state = {
    };

    componentWillMount() {
        this.props.getUsersData();
    }

    componentWillReceiveProps(nextProps) {
        this.setState({users: nextProps.users})
    }

    columns = [
        // {accessor: 'id', Header: 'User ID'},
        {accessor: 'name', Header: 'Name'},
        {accessor: 'email', Header: 'Email'},
        {accessor: 'department', Header: 'Department'},
        {accessor: 'title', Header: 'Title'}

    ];

    styles = {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center',
        textAlign: 'center'
    };


    openUserDetail = (selectedUserData) => {

        this.setUserData(selectedUserData.id);
        this.props.history.push({pathname: `/user-detail/${selectedUserData.id}`});


        //proof that is stays in the old state
        // console.log(`The new state field selectedUserId is ${this.state.selectedUserId}`);
    };


    setUserData = (selectedUserId) => this.setState(() => ({selectedUser: selectedUserId}));

    onInputChange = (e) => {
        this.props.setTextFilter(e.target.value);
    };

    render() {
        return (
            <div>
                <div className={'titles'}>
                    <h1>User Management</h1>
                </div>
                <div className={'user-management-nav'}>
                    <Button onClick={() => this.props.history.push("/create-user")} color={'primary'} >Create User</Button>

                    <FormGroup>
                        <Input type={'text'} value={this.props.filters.text} onChange={this.onInputChange} placeholder={'Search'} />
                    </FormGroup>
                </div>

                <div>
                    <UserTable onRowClick={this.openUserDetail} data={this.props.users} columns={this.columns}/>
                </div>

            </div>
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
    getUsersData: () => dispatch(getUsersData())
});


export default connect(mapStateToProps, mapDispatchToProps)(UserManagementPage);

