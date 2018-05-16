import React from 'react';
import {Button, Modal, ModalHeader, ModalBody, ModalFooter} from 'reactstrap';
import {faCheckCircle} from '@fortawesome/fontawesome-free-solid'
import FontAwesomeIcon from "@fortawesome/react-fontawesome";
import AddSystem from "./AddSystem";
import UrbanSporkAPI from "../api/UrbanSporkAPI";
import EditSystem from "./EditSystem";
import {connect} from "react-redux";


class SystemModal extends React.Component {


    state = {
        edit: true,
        addSystemButton: false,
        system:
            {
                name:"",
                description:"",
                logoURL:"",
            },

        systemName: "",
        systemDescription: "",
        systemLogoURL: "",
        systemId: ""
    };

    handleOnCancel = () => {
        this.setState({systemName: "", systemLogoURL: "", systemDescription: ""});

        if (!this.state.edit) {
            this.setState({edit: false});
        } else {
            // this.props.addSystemToggle();
            this.props.toggle();
        }
    };

    handleOnClose = () => {
        this.setState({systemName: "", systemLogoURL: "", systemDescription: "", systemId: ""});
        // this.props.addSystemToggle();
        this.props.toggle();
        this.setState({edit: true});
        this.setState({addSystemButton: true})
    };

    handleOnSave = () => {

        let newSystem = {
            Name: this.state.systemName,
            Description: this.state.systemDescription,
            Image: this.state.systemLogoURL,
            IsActive: true
        };

        UrbanSporkAPI.addSystem(newSystem)
            .then(() => {
                    this.setState({edit: false})
                    this.setState({systemName: "", systemLogoURL: "", systemDescription: ""});
               });

        this.setState({systemName: "", systemLogoURL: "", systemDescription: ""});

        // this.setState({})
    };

    handleOnUpdateSystem = () => {

        let newSystem = {
            Name: this.state.systemName,
            Description: this.state.systemDescription,
            Image: this.state.systemLogoURL,
            IsActive: true,
            UpdatedById: this.props.managerId,
            Id: this.state.systemId
        };

        UrbanSporkAPI.updatePermission(newSystem)
            .then(() => {
                this.setState({edit: false})
                this.setState({systemName: "", systemLogoURL: "", systemDescription: ""});
            });

        this.setState({systemName: "", systemLogoURL: "", systemDescription: ""});
    };

    onInputChange = (e) => {

        console.log("target is "+ e.target.id)
        console.log("value is "+e.target.value)

        this.setState({[e.target.id]: e.target.value});

        // this.toggleAddButton();
    };

    onIdChange = (e) => {
        this.setState({systemId: e})
    }

    onEditInputChange = (e) => {

        this.setState({[e.target.id]: e.target.value});

        // if(this.state.systemId != null && this.state.)
        this.setState({addSystemButton: true});
    };

    toggleAddButton = (data) => {
        this.setState({system:data});

        const fields = this.state.systemDescription.length > 0 && this.state.systemName.length > 0;

        if(fields && this.state.addSystemButton === false){

            this.setState({addSystemButton: fields})
        }

        if(fields === false && this.state.addSystemButton === true){
            this.setState({addSystemButton: false})
        }
        this.setState({system:data});
        this.setState({addDepartmentButton: data.length > 0});

    };

    componentDidUpdate(){
        const fields = this.state.systemDescription.length > 0 && this.state.systemName.length > 0;

        if(fields && this.state.addSystemButton === false){

            this.setState({addSystemButton: fields})
        }

        if(fields === false && this.state.addSystemButton === true){
            this.setState({addSystemButton: false})
        }
    }

    render() {
        return (
            <div>
                <Modal isOpen={this.props.isOpen} toggle={this.handleOnClose}>
                    <ModalHeader toggle={this.handleOnClose}>{this.props.addSystem ? "Add":"Edit"} System</ModalHeader>
                    <ModalBody>
                        <div>
                            {this.state.edit?
                                (this.props.addSystem ?
                                <AddSystem onInputChange={this.onInputChange}/>:
                                    <EditSystem onInputChange={this.onEditInputChange} systems={this.props.systems} idChanged={this.onIdChange}/>
                                ):
                                <h6><FontAwesomeIcon icon={faCheckCircle}/> {" "} The {this.state.system.name} system was successfully {this.props.addSystem ? "added" : "updated"}!</h6>
                            }
                        </div>
                    </ModalBody>
                    <ModalFooter>
                        {this.state.edit?
                            (this.props.addSystem ?
                            <Button color="success" onClick={this.handleOnSave} disabled={!this.state.addSystemButton}
                                    active={!this.state.edit}>Add System</Button> :
                                <Button color="success" onClick={this.handleOnUpdateSystem} disabled={!this.state.addSystemButton}
                                        active={!this.state.edit}>Save Changes </Button>)
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
const mapStateToProps = (state) => {
    return {
        managerId: state.manager.id
    }
};
export default connect(mapStateToProps)(SystemModal);