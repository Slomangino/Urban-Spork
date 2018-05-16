import React from 'react';
import {Button, Modal, ModalHeader, ModalBody, ModalFooter} from 'reactstrap';
import {faCheckCircle} from '@fortawesome/fontawesome-free-solid';
import FontAwesomeIcon from "@fortawesome/react-fontawesome";
import AddDepartment from "./AddDepartment";
import UrbanSporkAPI from "../api/UrbanSporkAPI";
import RemoveDepartment from "./RemoveDepartment";


class DepartmentsModal extends React.Component {

    state = {
        edit: true,
        addDepartmentButton: false,
        departments: ""
    };

    handleOnCancel = () => {
        if (!this.state.edit) {
            this.setState({edit: false});
        } else {
            this.props.toggle();
        }
    };

    handleOnClose = () => {
        this.props.toggle();
        this.setState({edit: true});
    };

    handleOnAdd = () => {
        let newDepartment = {
            Name:this.state.departments,
        };

        UrbanSporkAPI.addDepartment(newDepartment);

        this.setState({edit: false});
        this.forceUpdate();
    };

    handleOnRemove = () => {

        let department = {
            Name:this.state.departments,
        };

        UrbanSporkAPI.removeDepartmentByName(department);
        this.setState({edit: false});
        this.forceUpdate();
    };

    toggleAddButton = (data) => {
        this.setState({departments:data});
        data.length > 0 ? this.setState({addDepartmentButton: true}): this.setState({addDepartmentButton: false});

    };

    updateDepartment = (department) => {
        this.setState({departments: department});
        this.toggleAddButton(department);
    };

    updateDepartmentForRemove = (department) => {
        this.setState({departments: department});
        this.toggleAddButton(department);
    };

    render() {
        return (
            <div>
                <Modal isOpen={this.props.isOpen} toggle={this.handleOnClose}>
                    <ModalHeader toggle={this.handleOnClose}>{this.props.addDepartment ? "Add":"Remove"} Department</ModalHeader>
                    <ModalBody>
                        <div>
                            {this.state.edit?
                                (this.props.addDepartment ?
                                    <AddDepartment updateDepartment={this.updateDepartment}
                                                   AddButton={this.titleUpdated} department={this.props.departments}/>
                                    :
                                <RemoveDepartment DepartmentSelected={this.updateDepartmentForRemove}
                                             AddButton={this.titleUpdated} departments={this.props.departments}/>):
                                <h6><FontAwesomeIcon icon={faCheckCircle}/> {" "} The {this.state.departments}{' '}
                                 department was successfully {this.props.addDepartment? "added":"removed"}!</h6>
                            }
                        </div>
                    </ModalBody>
                    <ModalFooter>
                        {this.state.edit?
                            <Button color={this.props.addDepartment? "success":"danger"}
                                    onClick={this.props.addDepartment? this.handleOnAdd : this.handleOnRemove}
                                    disabled={!this.state.addDepartmentButton}
                                    active={!this.state.edit}>{this.props.addDepartment? "Add":"Remove"} Department</Button>
                            :
                            <Button color="success" onClick={this.handleOnClose}
                                    active={this.state.edit}>Done</Button>
                        }{' '}
                        {this.state.edit &&
                        <Button color="secondary" onClick={this.handleOnCancel}
                                active={this.state.edit}>Cancel</Button>
                        }{' '}
                    </ModalFooter>
                </Modal>
            </div>
        );
    }
}

export default DepartmentsModal;