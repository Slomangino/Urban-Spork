import React from 'react';
import {Button, Modal, ModalBody, ModalFooter, ModalHeader} from 'reactstrap';
import StaticUserDetail from './StaticUserDetail';
import EditUserDetail from "./EditUserDetail";
import UrbanSporkAPI from "../api/UrbanSporkAPI";
import EditUserPermissions from "./EditUserPermissions";


class UserDetailsModal extends React.Component {

    state = {
        edit: false,
        permissionsModalIsOpen: false,
        selectedPermissions: [],
    };

    handleOnEdit = () => {
        this.setState({edit: true})
    };


    // handleOnEditPermissions = () => {
    //     this.setState({permissionsModalIsOpen: true})
    // };


    handleOnCancel = () => {
        this.setState((prevState) => ({userData: prevState.originalData, editPermissions: false}));

        if (this.state.edit) {
            this.setState({edit: false});
        } else {
            this.props.toggle();
        }
    };

    handleOnClose = () => {
        this.setState({edit: false});
        this.props.toggle();
    };


    handlePersmissionsToggle = () => this.setState({permissionsModalIsOpen: !this.state.permissionsModalIsOpen});

    handleOnSave = () => {
        const data = {
            ForID: this.state.userData.userId,
            FirstName: this.state.userData.firstName,
            LastName: this.state.userData.lastName,
            Email: this.state.userData.email,
            Position: this.state.userData.position,
            Department: this.state.userData.department,
            IsAdmin: this.state.userData.isAdmin
        };

        UrbanSporkAPI.updateUserDetails(data)
            .then(this.setState({edit: false}))

    };

    handleOnDataChange = (data) => {
        console.log('the data is ', data);
        this.setState((prevState) => ({userData: {...prevState.userData, ...data}}));
        console.log('The state after change is ', this.state)
    };

    componentWillReceiveProps(nextProps) {
        if (nextProps.isOpen) {
            const userData = UrbanSporkAPI.getUserFullData(nextProps.userId);
            userData
                .then(data => {
                    this.setState({userData: data, originalData: data});


                    return Object.keys(props.userData.permissionList).map((permission, i) => {
                        if (props.userData.permissionList[permission].permissionStatus === 'Granted') {
                            return {
                                permissionID: permission,
                                permissionName: props.userData.permissionList[permission].permissionName,
                            }
                        }
                    })
                })
                .then((selectedPermissions) => {

                    this.setState({selectedPermissions})
                })
        }
    }

    render() {
        return (
            <div>
                <Modal isOpen={this.props.isOpen} toggle={this.handleOnClose}>
                    <ModalHeader toggle={this.handleOnClose}>User Detail</ModalHeader>
                    <ModalBody>
                        <div>
                            {
                                // the check to see if we have the userData loaded needs to go first
                                this.state.userData && (this.state.edit ?
                                        <EditUserDetail
                                            onDataChange={this.handleOnDataChange}
                                            userData={this.state.userData}
                                        />
                                        :
                                        <StaticUserDetail
                                            userData={this.state.userData}
                                            handleOnEditPermissions={this.handleOnEditPermissions}
                                        />
                                )
                            }


                            {
                                this.state.userData && <Modal isOpen={this.state.permissionsModalIsOpen} toggle={this.handlePersmissionsToggle}>

                                    <ModalHeader>User Permissions</ModalHeader>

                                    <ModalBody>
                                        <EditUserPermissions
                                            userData={this.state.userData}
                                        />
                                    </ModalBody>

                                    <ModalFooter>

                                    </ModalFooter>


                                </Modal>
                            }
                        </div>
                    </ModalBody>
                    <ModalFooter>
                        {this.state.edit ?
                            <Button color="success" onClick={this.handleOnSave} active={!this.state.edit}>Save</Button>
                            :
                            <Button color="primary" onClick={this.handleOnEdit} active={!this.state.edit}>Edit</Button>
                        }{' '}
                        <Button color="secondary" onClick={this.handleOnCancel}>Cancel</Button>
                    </ModalFooter>
                </Modal>
            </div>
        );
    }
}

export default UserDetailsModal;