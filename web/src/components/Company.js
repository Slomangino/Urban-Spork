import React from 'react';
import {Row, Col} from 'reactstrap';
import {faBuilding, faCogs, faIdBadge, faTasks} from '@fortawesome/fontawesome-free-solid'
import UrbanSporkAPI from "../api/UrbanSporkAPI";
import DepartmentsModal from './DepartmentsModal';
import PositionsModal from './PositionsModal';
import TemplateModal from './TemplateModal';
import SystemModal from "./SystemModal";
import CompanyCard from'./CompanyCard';

export default class Company extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            addPosition: true,
            addDepartment: true,
            addTemplate: true,
            addSystem: false,
            PositionsModalIsOpen:false,
            DepartmentsModalIsOpen:false,
            SystemModalIsOpen: false,
            TemplateModalIsOpen: false,
            departments: [],
            templates: [],
            systems: []
        };
    }

    toggleTemplateModal = () => this.setState(() => {
        return ({TemplateModalIsOpen: !this.state.TemplateModalIsOpen});
    });

    toggleDepartmentsModal = () => this.setState(() => {
        return ({DepartmentsModalIsOpen: !this.state.DepartmentsModalIsOpen});
    });

    togglePositionsModal = () => this.setState(() => {
        return ({PositionsModalIsOpen: !this.state.PositionsModalIsOpen});
    });

    toggleModalIsOpen = () => this.setState(() => {
        return ({SystemModalIsOpen: !this.state.SystemModalIsOpen});
    });

    toggleAddSystem = () => this.setState(() => {
        return ({addSystem: true});
    })

    openAddPositionsModal = () => {

        var payload = UrbanSporkAPI.getDepartments();

        payload.then(data => {
            this.setState({departments: data}),
                this.setState({addPosition: true})
        }).then(() => this.togglePositionsModal())
    };

    openRemovePositionsModal = () => {

        var payload = UrbanSporkAPI.getDepartments();


        payload.then(data => {
            this.setState({departments: data}),
                this.setState({addPosition: false})
        }).then(() =>

            this.togglePositionsModal()
        )
    };

    openAddDepartmentsModal = () => {
        var payload = UrbanSporkAPI.getDepartments();

        payload.then(data => {
            this.setState({departments: data}),
                this.setState({addDepartment: true})
        }).then(() => this.toggleDepartmentsModal())
    };

    openRemoveDepartmentsModal = () => {
        var payload = UrbanSporkAPI.getDepartments();

        payload.then(data => {
            this.setState({departments: data}),
                this.setState({addDepartment: false})
        }).then(() => this.toggleDepartmentsModal())
    };

    openAddTemplateModal = () => {
        var payload = UrbanSporkAPI.getTemplates();

        payload.then(data => {
            this.setState({templates: data}),
                this.setState({addTemplate: true})
        }).then(() => this.toggleTemplateModal())
    };

    openRemoveTemplateModal = () => {
        var payload = UrbanSporkAPI.getTemplates();

        payload.then(data => {
            this.setState({templates: data}),
                this.setState({addTemplate: false})
        }).then(() => this.toggleTemplateModal())
    };

    openSystemDetailModal = () => {
        this.toggleAddSystem();
        this.toggleModalIsOpen();
    };

    openEditSystemModal = () => {
        var payload = UrbanSporkAPI.getPermissions();

        payload.then(data => {
            this.setState({systems: data}),
                this.setState({addSystem: false})
        }).then(() => this.toggleModalIsOpen());
    };

    addPosition = (position) => {
        this.setState({addPosition: position})
    }

    styles = {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center',
        textAlign: 'center',
        height: 100
    };

    render() {

        return(
            <div>
                <div >
                    <Row style={this.styles}>
                        <Col md={12} sm={12}>
                            <h1 align="center">Company Management</h1>
                            <hr width={500} color="#000000"/>
                            <br/>
                        </Col>
                    </Row>
                    <Row >
                        <Col md={3} style={{justifyContent:"center"}}>
                            <CompanyCard cardTitle={"Manage Systems"} color={"#217246"}
                                         fontAwesomeIcon={faCogs} addButtonOnClick={this.openSystemDetailModal}
                                         removeButtonOnClick={this.openEditSystemModal}
                                         buttonOneText={"Add New"}
                                        buttonOneColor={"success"}
                                        buttonTwoText={" Edit"}
                                        buttonTwoColor={"warning"}
                                        style={{width: "90px"}}/>
                            <br/>
                        </Col>

                        <Col md={3}>
                            <CompanyCard cardTitle={"Manage Departments"} color={"#9933ff"}
                                         fontAwesomeIcon={faBuilding} addButtonOnClick={this.openAddDepartmentsModal}
                                         removeButtonOnClick={this.openRemoveDepartmentsModal}
                                         buttonOneText={"Add New"}
                                         buttonOneColor={"success"}
                                         buttonTwoText={"Remove"}
                                         buttonTwoColor={"danger"}
                                         style={{width: "90px"}}/>
                            <br/>
                        </Col>

                        <Col md={3}>
                            <CompanyCard cardTitle={"Manage Positions"} color={"#0066cc"}
                                         fontAwesomeIcon={faIdBadge} addButtonOnClick={this.openAddPositionsModal}
                                         removeButtonOnClick={this.openRemovePositionsModal}
                                         buttonOneText={"Add New"}
                                         buttonOneColor={"success"}
                                         buttonTwoText={"Remove"}
                                         buttonTwoColor={"danger"}
                                         style={{width: "90px"}}/>
                            <br/>
                        </Col>

                        <Col md={3}>
                            <CompanyCard cardTitle={"Manage Templates"} color={"#00bdff"}
                                         fontAwesomeIcon={faTasks} addButtonOnClick={this.openAddTemplateModal}
                                         removeButtonOnClick={this.openRemoveTemplateModal}
                                         buttonOneText={"Add New"}
                                         buttonOneColor={"success"}
                                         buttonTwoText={"Remove"}
                                         buttonTwoColor={"danger"}
                                         style={{width: "90px"}}/>
                            <br/>
                        </Col>
                    </Row>
                </div>

                <PositionsModal departments={this.state.departments}
                                isOpen={this.state.PositionsModalIsOpen}
                                toggle={this.togglePositionsModal}
                                addPosition={this.state.addPosition}/>

                <DepartmentsModal isOpen={this.state.DepartmentsModalIsOpen}
                                  toggle={this.toggleDepartmentsModal}
                                  departments={this.state.departments}
                                  addDepartment={this.state.addDepartment}/>

                <SystemModal isOpen={this.state.SystemModalIsOpen}
                             toggle={this.toggleModalIsOpen}
                             systems={this.state.systems}
                             addSystem={this.state.addSystem}
                             addSystemToggle={this.toggleAddSystem}/>

                <TemplateModal isOpen={this.state.TemplateModalIsOpen}
                                  toggle={this.toggleTemplateModal}
                                  templates={this.state.templates}
                                  addTemplate={this.state.addTemplate}/>
            </div>
        )
    }
}