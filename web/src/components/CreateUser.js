import React from 'react';
import {connect} from 'react-redux';
import UrbanSporkAPI from "../api/UrbanSporkAPI";
import {
    Button,
    ButtonGroup,
    Col,
    DropdownItem,
    DropdownMenu,
    DropdownToggle,
    Form,
    FormGroup,
    Input,
    Label,
    ModalBody,
    ModalFooter,
    UncontrolledDropdown
} from "reactstrap";
import PermissionMultiselectComponent from "./PermissionMultiselectComponent";
import FontAwesomeIcon from "@fortawesome/react-fontawesome";
import {faCog} from "@fortawesome/fontawesome-free-solid/index";


const AccessTemplates = (props) => (
    <UncontrolledDropdown direction={'right'}>
        {/*in case we need to disable it*/}
        <DropdownToggle disabled={false}>
            <FontAwesomeIcon icon={faCog}/>
        </DropdownToggle>

        <DropdownMenu>
            <DropdownItem header>Templates</DropdownItem>


            {
                props.templates && props.templates.map((permissionSet, i) => <DropdownItem key={permissionSet.id} id={i}
                                                                                           onClick={(e) => props.onTemplateClick(e.target.id)}>{permissionSet.name}</DropdownItem>)
            }

            {/* need to map the props.Templates here */}

            {/*<DropdownItem disabled={props.isAdmin}>Make Admin</DropdownItem>*/}
            {/*<DropdownItem>Off-Board Report</DropdownItem>*/}
            {/*<DropdownItem>Deactivate</DropdownItem>*/}
        </DropdownMenu>
    </UncontrolledDropdown>
);


const Header = (props) => (
    <div className={'modal-header'} style={{display: 'flex', justifyContent: 'space-between'}}>
        <Button color={'danger'} onClick={props.handleOnBack}>Back</Button>

        {/*<h5 className={'modal-title'}>*/}
        {/*{props.firstName} {props.lastName}*/}
        {/*</h5>*/}
        {
            // need to pass props.templates
            <AccessTemplates
                templates={props.templates}
                onTemplateClick={props.onTemplateClick}
            />
        }
    </div>
);


class CreateUser extends React.Component {

    state = {
        firstName: '',
        lastName: '',
        position: '',
        department: '',
        email: '',

        selectedPermissions: [],
        rSelected: false

        // this.onRadioBtnClick = this.onRadioBtnClick.bind(this);
        // this.onCheckboxBtnClick = this.onCheckboxBtnClick.bind(this);
      };

    styles = {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center',
        textAlign: 'center'
    };

    onRadioBtnClick = (value) => {
        this.setState({ rSelected: value });
      } 

    onTemplateClick = (index) => {
        const listFromTemplate = JSON.parse(this.state.templates[index].templatePermissions);
        const rawList = Object.keys(listFromTemplate).map((permission) =>
            ({
                permissionID: permission,
                permissionName: listFromTemplate[permission]
            })
        );

        console.log(rawList);

        this.setState(prevState => ({selectedPermissions: [...prevState.selectedPermissions, ...rawList]}));
    };
    handleOnCancel = () => this.props.history.push("/users");
    handleOnChange = (e) => {
        this.setState({[e.target.name]: e.target.value});
    };
    handleOnUserCreation = () => {
        const permissionDetail = {
            Reason: 'User was created with these permissions.',
            RequestedBy: this.props.managerId,
        };

        const permissionList = {};

        this.state.selectedPermissions.map((permission) => {
            return permissionList[permission.permissionID] = permissionDetail
        });


        const payload = {
            ById: this.props.managerId,
            FirstName: this.state.firstName,
            LastName: this.state.lastName,
            Email: this.state.email,
            Position: this.state.position,
            Department: this.state.department,
            IsAdmin: this.state.rSelected,
            IsActive: true,
            PermissionList: permissionList,

        };

        UrbanSporkAPI.createUser(payload).then(() => this.props.history.push("/users"))
    };

    componentDidMount() {
        const payload = UrbanSporkAPI.getTemplates();
        payload.then((data) => {
            this.setState({templates: data})
        });
        UrbanSporkAPI.getDepartments().then((data) => this.setState({departments: data}))
    }

    handleDepartmentClick(departmentName) {
        UrbanSporkAPI.getPositionByDepartment(departmentName).then(positions => this.setState({positions: positions}))
    }

    // componentWillMount(){
    // }

    render() {
        return (
            <div>

                <div style={this.styles}>
                    <h1>Create User</h1>
                </div>

                <Header
                    handleOnBack={this.handleOnCancel}
                    templates={this.state.templates}
                    onTemplateClick={this.onTemplateClick}
                />

                <ModalBody>
                    <div style={{display: 'flex', justifyContent: 'space-around'}}>
                        <div>
                            <Form style={{width: "480px"}}>
                                <FormGroup row>
                                    <Label color={"muted"} sm={"3"} for={"firstName"}>
                                        First Name:
                                    </Label>
                                    <Col sm={20}>
                                        <Input id={"firstName"} name={"firstName"}
                                               onChange={e => this.handleOnChange(e)}/>
                                    </Col>
                                </FormGroup>

                                <FormGroup row>
                                    <Label color={"muted"} sm={"3"} for={"lastName"}>
                                        Last Name:
                                    </Label>
                                    <Col sm={20}>
                                        <Input required id={"lastName"} name={"lastName"}
                                               onChange={e => this.handleOnChange(e)}/>
                                    </Col>
                                </FormGroup>

                                <FormGroup row>
                                    <Label for="department" sm={"3"}>
                                        Department:
                                    </Label>

                                    <Col sm={20}>
                                        {/* Hard Coded permissions... I love it!*/}
                                        <Input type="select" name="department" id="department"
                                               onChange={e => {
                                                   this.handleOnChange(e);
                                                   this.handleDepartmentClick(e.target.value);
                                               }}>
                                            <option></option>
                                            {
                                                this.state.departments && this.state.departments.map((department, i) =>
                                                    <option key={i} id={department.id}
                                                            value={department.name}>{department.name}</option>)
                                            }
                                        </Input>
                                    </Col>
                                </FormGroup>

                                {
                                    this.state.positions && this.state.positions.length > 0 &&
                                    <FormGroup row>
                                        <Label for="position" sm={"3"}>
                                            Title:
                                        </Label>

                                        <Col sm={20}>
                                            <Input type="select" name="position" id="position"
                                                   onChange={e => this.handleOnChange(e)}>
                                                <option></option>
                                                {
                                                    this.state.positions && this.state.positions.map((position, i) =>
                                                        <option key={i} id={position.id}
                                                                value={position.positionName}>{position.positionName}</option>)
                                                }
                                            </Input>
                                        </Col>
                                    </FormGroup>
                                }

                                <FormGroup row>
                                    <Label for="email" sm={"3"}>
                                        Email:
                                    </Label>

                                    <Col sm={20}>
                                        <Input type="email" name="email" id="email"
                                               onChange={e => this.handleOnChange(e)}/>
                                    </Col>
                                </FormGroup>

                                <FormGroup row style={{paddingTop: 10}}>
                                    <Label for="admin" sm={"4"}>
                                        Admin Status:
                                    </Label>                                    
                                    <ButtonGroup>
                                        {this.state.rSelected?
                                            <React.Fragment>
                                                <Button color="primary" onClick={() => this.onRadioBtnClick(true)} active={this.state.rSelected === true}>Admin</Button>
                                                <Button color="secondary"  onClick={() => this.onRadioBtnClick(false)} active={this.state.rSelected === false}>Non-Admin</Button>
                                            </React.Fragment>
                                            :
                                            <React.Fragment>
                                                <Button color="secondary" onClick={() => this.onRadioBtnClick(true)} active={this.state.rSelected === true}>Admin</Button>
                                                <Button color="primary"  onClick={() => this.onRadioBtnClick(false)} active={this.state.rSelected === false}>Non-Admin</Button>
                                            </React.Fragment>
                                        }
                                    </ButtonGroup>
                                </FormGroup>
                            </Form>
                        </div>

                        <PermissionMultiselectComponent
                            selectedPermissions={this.state.selectedPermissions}
                            setPermissions={(selectedPermissions) => this.setState({selectedPermissions})}
                        />


                    </div>
                </ModalBody>

                <ModalFooter>
                    <Button onClick={this.handleOnUserCreation} color={'success'}>Create User</Button>
                    {' '}
                    <Button onClick={this.handleOnCancel} color={'secondary'}>Cancel</Button>
                </ModalFooter>
            </div>
        )
    }
}


const mapStateToProps = (state) => {
    return {
        managerId: state.manager.id,
    }
};

export default connect(mapStateToProps)(CreateUser);
