import React from 'react';
import {Col, Form, FormGroup, Input, Label} from 'reactstrap';
import PermissionMultiselectComponent from "./PermissionMultiselectComponent";


export default class EditUserDetailComponent extends React.Component {
    state = {...this.props.userData};

    render() {
        return (
            <div style={{display: 'flex', justifyContent: 'space-around'}}>
                <div>
                    <Form style={{width: "480px"}}>
                        <FormGroup row>
                            <Label color={"muted"} sm={"3"} for={"firstName"}>
                                First Name:
                            </Label>
                            <Col sm={20}>
                                <Input
                                    type="text"
                                    name="firstName"
                                    id={"firstName"}
                                    defaultValue={this.state.firstName}
                                    onChange={(e) => {
                                        this.props.onDataChange(e)
                                    }}
                                />
                            </Col>
                        </FormGroup>

                        <FormGroup row>
                            <Label color={"muted"} sm={"3"} for={"lastName"}>
                                Last Name:
                            </Label>
                            <Col sm={20}>
                                <Input
                                    type="text"
                                    name="lastName"
                                    id={"lastName"}
                                    defaultValue={this.state.lastName}
                                    onChange={(e) => {
                                        this.props.onDataChange(e)
                                    }}
                                />
                            </Col>
                        </FormGroup>

                        <FormGroup row>
                            <Label for="position" sm={"3"}>
                                Title:
                            </Label>

                            <Col sm={20}>
                                <Input
                                    type="text"
                                    name="position"
                                    id="position"
                                    defaultValue={this.state.position}
                                    onChange={(e) => {
                                        this.props.onDataChange(e)
                                    }}
                                />
                            </Col>
                        </FormGroup>

                        <FormGroup row>
                            <Label for="department" sm={"3"}>
                                Department:
                            </Label>

                            <Col sm={20}>
                                <Input
                                    type="text"
                                    name="department"
                                    id="department"
                                    defaultValue={this.state.department}
                                    onChange={(e) => {
                                        this.props.onDataChange(e)
                                    }}
                                />
                            </Col>
                        </FormGroup>

                        <FormGroup row>
                            <Label for="email" sm={"3"}>
                                Email:
                            </Label>

                            <Col sm={20}>
                                <Input
                                    type="email"
                                    name="email"
                                    id="email"
                                    defaultValue={this.state.email}
                                    onChange={(e) => {
                                        this.props.onDataChange(e)
                                    }}
                                />
                            </Col>
                        </FormGroup>
                    </Form>
                </div>
                <PermissionMultiselectComponent
                    selectedPermissions={this.props.selectedPermissions}
                    setPermissions={this.props.setPermissions}
                />
            </div>
        )
    }

}



