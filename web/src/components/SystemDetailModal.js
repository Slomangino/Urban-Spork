import React from 'react';
import {Button, Modal, ModalHeader, ModalBody, ModalFooter} from 'reactstrap';
import StaticSystemDetail from './StaticSystemDetail';
import EditSystemDetail from "./AddSystemDetail";
import UrbanSporkAPI from "../api/UrbanSporkAPI";


class SystemDetailModal extends React.Component {

    componentWillReceiveProps(nextProps) {
        if(nextProps.isOpen){
            // const userData = UrbanSporkAPI.getUserFullData(nextProps.userId);
            // userData.then(data => this.setState({userData: data}))
        }
    }

    state = {
        edit: true,
        addSystemButton: false,
        system:
            {
                name:"",
                description:"",
                logoURL:""
            },
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

    handleOnSave = () => {
        let newSystem = {
            Name: this.state.system.name,
            Description: this.state.system.description,
            Image: this.state.system.logoURL
        };

        UrbanSporkAPI.addSystem(newSystem);
        this.setState({edit: false});
        this.forceUpdate();
    };


    render() {
        return (
            <div>
                <Modal isOpen={this.props.isOpen} toggle={this.handleOnClose}>
                    <ModalHeader toggle={this.handleOnClose}>Add A System</ModalHeader>
                    <ModalBody>
                        <div>
                            {this.state.edit?
                                <EditSystemDetail/>:
                                <StaticSystemDetail props={this.state.system}/>}
                        </div>
                    </ModalBody>
                    <ModalFooter>
                        {this.state.edit?
                            <Button color="success" onClick={this.handleOnSave}
                                    active={!this.state.edit}>Add System</Button>
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

export default SystemDetailModal;