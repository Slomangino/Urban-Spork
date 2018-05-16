import React from 'react';
import {connect} from 'react-redux';
import {
    Button,
    Col,
    DropdownItem,
    DropdownMenu,
    DropdownToggle,
    ModalBody,
    ModalFooter,
    Row,
    Table,
    UncontrolledDropdown
} from 'reactstrap';
import ReactDOMServer from 'react-dom/server';
import {faCog} from '@fortawesome/fontawesome-free-solid'
import FontAwesomeIcon from "@fortawesome/react-fontawesome";
import UrbanSporkAPI from "../api/UrbanSporkAPI";
import StaticUserDetail from "./StaticUserDetail";
import EditUserDetail from "./EditUserDetail";
import jsPDF from 'jspdf'


class UserDetailComponent extends React.Component {
    state = {
        edit: false,
        History: [],
        // selectedPermissions: [],
    };

    handleOnEdit = () => {
        this.setState({edit: true})
    };


    handleOnCancel = () => {
        this.setState((prevState) => ({userData: prevState.originalData, editPermissions: false}));

        if (this.state.edit) {
            this.setState({edit: false});
        } else {
            this.props.toggle();
        }
    };


    handleOnSave = () => {
        const permissionDetail = {
            Reason: 'Permission added by admin.'
        };

        const permissionList = {};

        this.state.selectedPermissions.map((permission) => {
            return permissionList[permission.permissionID] = permissionDetail
        });

        const data = {
            ForID: this.state.userData.userId,
            FirstName: this.state.userData.firstName,
            LastName: this.state.userData.lastName,
            Email: this.state.userData.email,
            Position: this.state.userData.position,
            Department: this.state.userData.department,
            IsAdmin: this.state.userData.isAdmin,
        };

        UrbanSporkAPI.updateUserDetails(data)
            .then(() => UrbanSporkAPI.grantRevokePermissions(
                {
                    ForId: this.state.userData.userId,
                    ById: this.props.managerId,
                    Permissions: permissionList,
                }
                ).then(() => {

                    //    MESSSSSSYYYYYY

                    const userId = this.props.match.params.id;
                    const userData = UrbanSporkAPI.getUserFullData(userId);
                    console.log('user id ', userId);
                    userData
                        .then(data => {
                            this.setState({userData: data, originalData: data});
                            const selectedPermissions = [];

                            Object.keys(data.permissionList).map((permission) => {
                                if (data.permissionList[permission].permissionStatus === 'Granted') {
                                    return selectedPermissions.push({
                                        permissionID: permission,
                                        permissionName: data.permissionList[permission].permissionName
                                    });
                                }
                            });

                            this.setState({selectedPermissions: selectedPermissions});
                        });

                    const userHistory = UrbanSporkAPI.getUserHistory(userId);
                    userHistory.then(data => {
                        this.setState({History: data});
                    });

                    // //END OF MESSSYYYY

                }).then(() => this.setState({edit: false})
                )
            )
    };

    // this handles the input change in the user info form fields
    handleOnDataChange = (e) => {
        const changedData = {[e.target.id]: e.target.value};

        // this replaces ONLY the changed data values in the old user data.         aa7acd83-c512-453b-a0f4-8757de0b2997
        this.setState((prevState) => ({userData: {...prevState.userData, ...changedData}}));
    };

    formatReport = () => {
        let pdfBody = Object.values(this.state.userData.permissionList).map((permission, i) => {
            if (permission.permissionStatus === 'Granted') {
                return <tr key={i}>
                    <td>{i + 1}</td>
                    <td>{permission.permissionName}</td>
                    <td>{permission.approverFirstName} {permission.approverLastName}</td>
                    <td>{permission.timeStamp}</td>
                </tr>

            }
        });


        return pdfBody;
    };

    exportToPDF = () => {
        console.log("export entered");
        let pdf = new jsPDF('p', 'pt', 'letter');
        let rows = this.formatReport();
        let table = (
            <Table>
                <thead style={{height: 20}}>

                <th>#</th>
                <th>System Name</th>
                <th>Approved By</th>
                <th>Date</th>

                </thead>
                <tbody>
                {rows}
                </tbody>
            </Table>
        );

        let page = (
            <Row>
                <Row>
                    <Col md={4}>

                    </Col>
                    <Col md={4} textalign="center">
                        <h1>Off-Boarding Report</h1>
                    </Col>
                    <Col md={4}>

                    </Col>
                </Row>
                <Row>
                    {table}
                </Row>
            </Row>
        );
        let source = ReactDOMServer.renderToStaticMarkup(page);

        let margins = {
            top: 50,
            left: 60,
            width: 545
        };

        let system = this.state.SelectedSystem;
        console.log("system: " + system);

        pdf.fromHTML(
            source // HTML string or DOM elem ref.
            , margins.left // x coord
            , margins.top // y coord
            , {
                'width': margins.width // max width of content on PDF
                ,
            },
            function (dispose) {

                // dispose: object with X, Y of the last line add to the PDF
                // this allow the insertion of new lines after html
                pdf.save('OffBoardingReport.pdf');
            }
        );
    };

    onActivateDeactivate = () => () => {
        const data = {UserId: this.state.userData.userId, ById: this.props.managerId};
        if (this.state.isActive) {
            return UrbanSporkAPI.disableUser(data).then(() => this.handleOnSave())
        }
        return UrbanSporkAPI.enableUser(data).then(() => this.handleOnSave())
    };

    handleOnBack = () => this.props.history.push("/users");

    componentDidMount() {
        const userId = this.props.match.params.id;
        const userData = UrbanSporkAPI.getUserFullData(userId);
        console.log('user id ', userId);
        userData
            .then(data => {
                this.setState({userData: data, originalData: data});
                const selectedPermissions = [];

                Object.keys(data.permissionList).map((permission) => {
                    if (data.permissionList[permission].permissionStatus === 'Granted') {
                        return selectedPermissions.push({
                            permissionID: permission,
                            permissionName: data.permissionList[permission].permissionName
                        });
                    }
                });

                this.setState({selectedPermissions: selectedPermissions});
            });

        const userHistory = UrbanSporkAPI.getUserHistory(userId);
        userHistory.then(data => {
            this.setState({History: data});
        });
    }

    render() {
        const Header = (props) => (
            <div className={'modal-header'} style={{display: 'flex', justifyContent: 'space-between'}}>
                <Button color={'danger'} onClick={props.handleOnBack}>Back</Button>
                <h5 className={'modal-title'}>
                    {props.firstName} {props.lastName}
                </h5>
                <div style={{display: 'flex'}}>
                    {
                        !this.state.edit &&
                        <div style={{paddingRight: '5px'}}>
                            <Button color="primary" onClick={this.handleOnEdit} active={!this.state.edit}>Edit</Button>
                        </div>
                    }
                    {
                        // If it's not in edit mode show the options button
                        <Options style={{paddingLeft: '5px'}} edit={props.edit} isActive={props.isActive}
                                 isAdmin={props.isAdmin}/>
                    }
                </div>

            </div>
        );

        // this is the options button component
        const Options = (props) => (
            <UncontrolledDropdown direction={'right'}>
                <DropdownToggle disabled={props.edit}>
                    <FontAwesomeIcon icon={faCog}/>
                </DropdownToggle>

                <DropdownMenu>
                    <DropdownItem header>Options</DropdownItem>
                    <DropdownItem onClick={this.exportToPDF}>Off-Board Report</DropdownItem>

                    {/*<DropdownItem onClick={()=> this.setState({isAdmin: true})} disabled={props.isAdmin}>Make Admin</DropdownItem>*/}
                    <DropdownItem
                        onClick={() => {
                            this.setState((prevState) => ({
                                userData: {
                                    ...prevState.userData,
                                    isAdmin: !prevState.userData.isAdmin
                                }
                            }), this.handleOnSave);
                        }}
                    >{props.isAdmin ? 'Remove Admin' : 'Make Admin'}</DropdownItem>

                    {
                        !this.state.userData.isActive && <DropdownItem onClick={() => {
                            this.setState((prevState) => ({
                                userData: {
                                    ...prevState.userData,
                                    isActive: !prevState.userData.isActive
                                }
                            }), () => UrbanSporkAPI.enableUser({
                                UserId: this.state.userData.userId,
                                ById: this.props.managerId
                            }).then(() => this.handleOnSave()));
                        }}
                        >Activate</DropdownItem>
                    }


                    {
                        this.state.userData.isActive && <DropdownItem onClick={() => {
                            this.setState((prevState) => ({
                                userData: {
                                    ...prevState.userData,
                                    isActive: !prevState.userData.isActive
                                }
                            }), () => UrbanSporkAPI.disableUser({
                                UserId: this.state.userData.userId,
                                ById: this.props.managerId
                            }).then(() => this.handleOnSave()));
                        }}
                        >Deactivate</DropdownItem>
                    }

                </DropdownMenu>
            </UncontrolledDropdown>
        );

        let userHist = this.state.History.map((history, index) => (
            {
                Event: history.eventType,
                Date: history.timeStamp,
                PerformedBy: history.operator,
                Description: JSON.parse(history.description),
            }
        ));

        return (
            <div>
                <h1 className={'titles'}>User Details</h1>
                {this.state.userData &&
                <div>
                    <Header
                        handleOnBack={this.handleOnBack}
                        edit={this.state.edit}
                        firstName={this.state.userData.firstName}
                        lastName={this.state.userData.lastName}
                        isAdmin={this.state.userData.isAdmin}
                        isActive={this.state.userData.isActive}
                    />

                    <ModalBody>
                        <div>
                            {
                                // the check to see if we have the userData loaded needs to go first
                                this.state.userData && (this.state.edit ?
                                        <EditUserDetail
                                            onDataChange={this.handleOnDataChange}
                                            userData={this.state.userData}
                                            selectedPermissions={this.state.selectedPermissions}
                                            setPermissions={(selectedPermissions) => this.setState({selectedPermissions})}
                                        />
                                        :
                                        <StaticUserDetail
                                            userData={this.state.userData}
                                            handleOnEditPermissions={this.handleOnEditPermissions}
                                            userHistory={userHist}
                                        />
                                )
                            }
                        </div>
                    </ModalBody>
                    <ModalFooter>
                        {this.state.edit &&
                        <div>
                            <Button
                                color="success" onClick={this.handleOnSave}
                                active={!this.state.edit}>Save
                            </Button>
                            {' '}
                            <Button color="secondary" onClick={this.handleOnCancel}>Cancel</Button>
                        </div>

                        }
                    </ModalFooter>
                </div>
                }
            </div>
        )
    }
}

const mapStateToProps = (state) => {
    return {
        managerId: state.manager.id,
    }
};

export default connect(mapStateToProps)(UserDetailComponent);
