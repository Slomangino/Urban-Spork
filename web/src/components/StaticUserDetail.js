import React from 'react';
import {Col, Form, FormGroup, Input, Label, Row} from 'reactstrap';
import UserTable from "./UserTable";
import moment from 'moment';

const renderPermissionList = (permissionList) => {
    Object.keys(permissionList).map((permission, i) => {
        if (permissionList[permission].permissionStatus !== 'Revoked') {
            return <option key={i} value={i} id={permission}>{permissionList[permission].permissionName}</option>;
        }
    })
};

const refactoredObject = (props) => (
    props.map((uHistory) => (
            {
                Event: uHistory.Event,
                TheDate: uHistory.Date,
                PerformedBy: uHistory.PerformedBy,
                System: Object.keys(uHistory.Description)[0],
                Reason: Object.values(uHistory.Description)[0],
            }
        )
    ));

const columns = [
    {accessor: 'Event', Header: 'User Activity'},
    {accessor: 'PerformedBy', Header: 'By'},
    {accessor: 'System', Header: 'Field / System'},
    {accessor: 'Reason', Header: 'Detail'},
    {
        accessor: 'TheDate',
        Header: 'Date',
        Cell: ({value}) => moment.utc(value).format('ddd MMM D YYYY').toString()
    },
];


const StaticUserDetail = (props) => (
    <div style={{display: 'flex', justifyContent: 'space-evenly', flexDirection: 'column'}}>
        <div>
            <Row style={{display: 'flex', justifyContent: 'space-evenly'}}>
                {/*
            Left Side of the modal, is the User Info
            */}
                <div>
                    <Form>
                        <FormGroup row>
                            <Col md={20}>
                                <Label style={{whiteSpace: 'nowrap'}} color={"muted"} sm={'3'} for={"firstName"}>First
                                    Name:</Label>
                            </Col>

                            <Col md={"auto"}>
                                <Input id={"firstName"} plaintext>{props.userData.firstName}</Input>
                            </Col>
                        </FormGroup>


                        <FormGroup row>
                            <Col md={20}>
                                <Label style={{whiteSpace: 'nowrap'}} color={"muted"} sm={'3'} for={"lastName"}>Last
                                    Name:</Label>
                            </Col>

                            <Col md={"auto"}>
                                <Input id={"lastName"} plaintext>{props.userData.lastName}</Input>
                            </Col>
                        </FormGroup>


                        <FormGroup row>
                            <Col md={20}>
                                <Label style={{paddingRight: "5px"}} for="department" sm={'3'}>Department:</Label>
                            </Col>

                            <Col md={"auto"}>
                                <Input plaintext type="text" name="department"
                                       id="department">{props.userData.department}</Input>
                            </Col>
                        </FormGroup>

                        <FormGroup row>
                            <Col md={20}>
                                <Label for="title" sm={'3'}>Title:</Label>
                            </Col>

                            <Col md={"auto"}>
                                <Input style={{paddingLeft: "45px"}} plaintext type="text" name="title"
                                       id="title">{props.userData.position}</Input>
                            </Col>
                        </FormGroup>

                        <FormGroup row>
                            <Col sm={20}>
                                <Label for="email" sm={'3'}>Email:</Label>
                            </Col>

                            <Col sm={"auto"}>
                                <Input plaintext type="email" name="email" id="email">{props.userData.email}</Input>
                            </Col>
                        </FormGroup>

                    </Form>
                </div>
                <br/>
                {/*
            Right Side of the modal, is the User Permissions
            */}
                <div>
                    <Label for="permissions">Permissions:</Label>
                    {
                        /**
                         THE FOLLOWING CODE IS A BIT HARD TO FOLLOW, SO I AM ADDING THIS COMMENT
                         **/
                        // I turn this permission Objects into an array of objects & check it's length
                        // if the length is greater than 0 ( if there are permissions to show )
                        // I map them over and as long as the permission is not marked as 'revoked',
                        // I add it as a new `option` of the input field.

                        Object.keys(props.userData.permissionList).length > 0 ?

                            <Input type="select" name="permissions" id="permissions" multiple>
                                {
                                    Object.keys(props.userData.permissionList).map((permission, i) => {
                                        if (props.userData.permissionList[permission].permissionStatus === 'Granted') {
                                            return <option key={i} value={i}
                                                           id={permission}>{props.userData.permissionList[permission].permissionName}</option>;
                                        }
                                    })
                                }
                            </Input> :
                            // If there are no permissions
                            <p>No permissions for this user.</p>
                    }

                    {/*<Button color="danger" style={{marginTop: "20px"}} onClick={props.handleOnEditPermissions}>Edit Permissions</Button>*/}
                </div>
            </Row>
        </div>

        <div>
            <Row style={{display: 'flex', justifyContent: 'space-evenly'}} >
                <div>

                    <div style={{paddingTop: '20px', width: '800px'}}>
                        <h2 style={{textAlign: "center"}}>{props.userData.fullName}'s History</h2>
                        <UserTable columns={columns} data={refactoredObject(props.userHistory)}/>
                    </div>


                </div>
            </Row>

        </div>

    </div>
);

export default StaticUserDetail;