import React from 'react';
import {Button, Modal, ModalHeader, ModalBody, ModalFooter} from 'reactstrap';
import UrbanSporkAPI from "../api/UrbanSporkAPI";
import {faCheckCircle} from "@fortawesome/fontawesome-free-solid/index";
import FontAwesomeIcon from "@fortawesome/react-fontawesome";
import AddTemplate from './AddTemplate';
import RemoveTemplate from './RemoveTemplate';

export default class TemplateModal extends React.Component {

    state = {
        edit: true,
        addTemplate: true,
        idForTemplateToRemove: "",
        templateToAdd: "",
        templateTitle: "",
        templatePermissions: {},
        confirmButton: ""
    }

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

        var permissionsToAdd = {}

        this.state.templatePermissions.forEach(permission => {
            permissionsToAdd[permission.permissionID] = permission.permissionName
        });

        var template = {
            Name: this.state.templateTitle,
            TemplatePermissions: permissionsToAdd
        }

        UrbanSporkAPI.addTemplate(template);
        this.setState({edit: false});
        this.forceUpdate();
    }

    handleOnRemove = () => {
        let templateToRemove = {
            Id : this.state.idForTemplateToRemove,
        };

        UrbanSporkAPI.removeTemplate(templateToRemove);
        this.setState({edit: false});
        this.forceUpdate();
    };

    toggleAddButton = (templateForAdd) => {
        this.setState({templateToAdd: templateForAdd});
        (this.state.templateTitle != "" && templateForAdd.length >= 1) ? this.setState({confirmButton: true}): this.setState({confirmButton: false});
    };

    toggleRemoveButton = (templateForRemoval) => {

        this.setState({idForTemplateToRemove: templateForRemoval.id});
        this.setState({templateTitle: templateForRemoval.name})

        templateForRemoval.id.length > 1 ? this.setState({confirmButton: true}): this.setState({confirmButton: false});
    };

    updateTemplateTitleForAdd = (templateTitleForAdd) => {
        this.setState({templateTitle: templateTitleForAdd});
    };

    updateTemplatePermissionsForAdd = (templatePermissionsForAdd) => {
        this.setState({templatePermissions: templatePermissionsForAdd});
        this.toggleAddButton(templatePermissionsForAdd);
    }

    updateTemplateForRemove = (templateForRemove) => {
        this.setState({idForTemplateToRemove: templateForRemove.id});
        this.setState({templateTitle: templateForRemove.name});

        this.toggleRemoveButton(templateForRemove);
    };



    render() {
        return (
            <div>
                <Modal isOpen={this.props.isOpen} toggle={this.handleOnClose}>
                    <ModalHeader
                        toggle={this.handleOnClose}>{this.props.addTemplate ? "Add" : "Remove"} Template</ModalHeader>
                    <ModalBody>
                        <div>
                            {this.state.edit ?
                                (this.props.addTemplate ?
                                    <AddTemplate updateDepartment={this.updateTemplateForAdd}
                                                 templateTitle={this.updateTemplateTitleForAdd}
                                                templatePermissions={this.updateTemplatePermissionsForAdd} />
                                    :
                                    <RemoveTemplate TemplateSelected={this.updateTemplateForRemove}
                                                    templates={this.props.templates}/>):
                                <h6><FontAwesomeIcon icon={faCheckCircle}/>{" "} The {this.state.templateTitle}{' '}
                                was successfully {this.props.addTemplate? "added":"removed"}!</h6>
                            }
                        </div>
                    </ModalBody>
                    <ModalFooter>
                        {this.state.edit?
                            <Button color={this.props.addTemplate? "success":"danger"}
                                    onClick={this.props.addTemplate? this.handleOnAdd : this.handleOnRemove}
                                    disabled={!this.state.confirmButton}
                                    active={!this.state.edit}>{this.props.addTemplate? "Add":"Remove"} Template</Button>
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
        )
    }
}