import React from 'react';
import {Button, Modal, ModalHeader, ModalBody, ModalFooter} from 'reactstrap';
import {faCheckCircle} from '@fortawesome/fontawesome-free-solid'
import FontAwesomeIcon from "@fortawesome/react-fontawesome";
import AddPosition from "./AddPosition";
import UrbanSporkAPI from '../api/UrbanSporkAPI';
import RemovePosition from "./RemovePosition";

class PositionsModal extends React.Component {

    constructor(props) {
        super();

        this.state = {
            edit: true,
            addPositions: props.addPosition,
            addPositionsButton: false,
            department: "",
            positionId: "",
            position: ""
        };
    }

    handleOnCancel = () => {
        if (!this.state.edit) {
            this.setState({edit: false});
        } else {
            this.setState({position: ""});
            this.setState({department: ""});
            this.setState({positionId: ""});
            this.props.toggle();
        }
    };

    handleOnClose = () => {
        this.setState({position: ""});
        this.setState({department: ""});
        this.setState({positionId: ""});
        this.props.toggle();
        this.setState({edit: true});
        this.setState({addPositionsButton: false})
    };

    handleOnAdd = () => {

        this.createPosition();

        this.setState({edit: false});
    };

    handleOnRemove = () => {

        this.removePosition();

        this.setState({edit: false});
    };

    createPosition= () => {

        var request = {
            PositionName: this.state.position,
            DepartmentName: this.state.department
        };

        UrbanSporkAPI.addPosition(request);
    };

    removePosition= () => {
        UrbanSporkAPI.removePosition(this.state.positionId);
    };

    titleUpdated = (data) => {
        this.setState({position: data});

        this.toggleAddButton();
    };

    toggleAddButton = () => {

        console.log("ToggleAddButton was called!")
        if (this.state.position === "" && this.state.department === ""){
            this.setState({addPositionsButton: false})
        } else {
            this.setState({addPositionsButton: true});
        }
    };

    updateDepartment = (department) => {
        this.setState({department: department.target.value});
        this.toggleAddButton();
    };

    updatePositionId = (positionId) => {
        this.setState({positionId: positionId});
    };

    updatePosition = (positionTitle) => {
        this.setState({position: positionTitle});
        this.toggleAddButton();
    };

    render() {
        return (
            <div>
                <Modal isOpen={this.props.isOpen} toggle={this.handleOnClose}>
                    <ModalHeader toggle={this.handleOnClose}>{this.props.addPosition? "Add ":"Remove "}Position</ModalHeader>
                    <ModalBody>
                        <div>
                            {this.state.edit ?
                                (this.props.addPosition ?
                                    <AddPosition DepartmentSelected={this.updateDepartment}
                                             AddButton={this.titleUpdated} department={this.props.departments}/>:<RemovePosition DepartmentSelected={this.updateDepartment}
                                                 AddButton={this.titleUpdated} department={this.props.departments}
                                                    positionId={this.updatePositionId}
                                                 position={this.updatePosition}/>) :
                                <h6><FontAwesomeIcon icon={faCheckCircle}/> {" "} The position with the title
                                    of {this.state.position}, was {this.props.addPosition ? "added to " : "removed from "} the {this.state.department} department!</h6>
                            }
                        </div>
                    </ModalBody>
                    <ModalFooter>
                        {this.state.edit ?
                            <Button color={this.props.addPosition ? "success" : "danger"}
                                    onClick={this.props.addPosition ? this.handleOnAdd : this.handleOnRemove}
                                    disabled={!this.state.addPositionsButton}
                                    active={!this.state.edit}>{this.props.addPosition? "Add ":"Remove "} Position</Button>
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

export default PositionsModal;