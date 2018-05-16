import React from 'react';
import {Col, Form, FormGroup, Input, Label} from 'reactstrap';
import PermissionMultiselectComponent from "./PermissionMultiselectComponent";

export default class AddTemplate extends React.Component {

    state = {
        Permissions: [],
        TemplateToAdd: {
            Name: "",
            TemplatePermissions: {

            }
        }
    }

    onInputChange = (e) => {
        this.props.templateTitle(e.target.value);
    };

    onPermissionsChanged = (e) => {
        this.setState({Permissions: e});
        this.props.templatePermissions(e);
    }


    render(){
        return(
            <div>
                <Form>
                    <FormGroup row>
                        <Col md={6}>
                            <Label for="TemplateName">
                                Template Name
                            </Label>
                        </Col>

                        <Col md={6}>
                            <Input placeholder={this.state.InputPlaceholder} id={"TemplateName"} onChange={e => {this.onInputChange(e)}}/>
                        </Col>

                        <Col md={6}>
                            <br/>
                            <Label for="PermissionSelect">
                                Permissions
                            </Label>
                        </Col>

                        <Col md={5}>
                            <br/>
                            <PermissionMultiselectComponent id={"PermissionSelect"} selectedPermissions = {this.state.Permissions}
                                setPermissions = {e => {this.onPermissionsChanged(e)}}/>
                        </Col>

                    </FormGroup>

                </Form>
            </div>
        );
    }
}