import React from 'react';
import {Button, Col, Form, FormGroup, Input, Label, Media} from 'reactstrap';

const StaticSystemDetail = (props) => (
    <div>
        <div style={{display: 'flex', justifyContent: 'space-evenly'}}>
            {/*
            Left Side of the modal, is the User Info
            */}
            <Form>
                <FormGroup row>
                    <Col md={20}>
                        <Label style={{whiteSpace: 'nowrap'}} color={"muted"} sm={'3'} for={"firstName"}>First
                            System Name:</Label>
                    </Col>

                    <Col md={"auto"}>
                        <Input id={"systemName"} plaintext>{/*props.userData.firstName*/}</Input>
                    </Col>
                </FormGroup>


                <FormGroup row>
                    <Col md={20}>
                        <Label style={{whiteSpace: 'nowrap'}} color={"muted"} sm={'3'} for={"lastName"}>Last
                            System Description:</Label>
                    </Col>

                    <Col md={"auto"}>
                        <Input id={"systemDescription"} plaintext>{/*props.userData.lastName*/}</Input>
                    </Col>
                </FormGroup>

                <FormGroup row>
                    <Col sm={20}>
                        <Label for="email" sm={'3'}>System Logo:</Label>
                    </Col>

                    <Col sm={"auto"}>
                        <Media left href="#">
                            <Media object data-src="https://static.brandfolder.com/slack/logo/slack-primary-logo.png" alt="Generic placeholder image" />
                        </Media>
                    </Col>
                </FormGroup>

            </Form>
        </div>

    </div>
);

export default StaticSystemDetail;