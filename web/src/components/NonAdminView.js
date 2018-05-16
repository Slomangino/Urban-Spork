import React from 'react';
import {Input, Form, Label, FormGroup, Button, Row, Col} from "reactstrap";
import UrbanSporkAPI from '../api/UrbanSporkAPI';
import UserTable from "./UserTable";
import moment from 'moment';
import {connect} from "react-redux";




class NonAdminView extends React.Component {

    state = {
        SystemData: [],
        UserID: this.props.managerId,
        Reason:"",
        System:undefined,
        requestData:[],
    };

    componentDidMount = () => {
        this.getData();
        this.getPendingRequestsForUser();
    };

    getPendingRequestsForUser = () => {
        console.log(this.state.UserID);
        let payload = {
            UserID: this.state.UserID
        };

        let userRequests = UrbanSporkAPI.getRequestsByID(payload);
        userRequests.then(data => this.setState({requestData: data})).catch(() => this.setState({requestData: []}));
    };

    updateSystem = (system) => {
       this.setState({System: system.target.value});
    };

    getData = () => {
        let Systems = UrbanSporkAPI.getSystemsDropDown();
        Systems.then(data => {
            this.setState({SystemData: data})
        });
    };

    onInputChange = (data) => {
        this.setState({Reason: data.target.value});
    };

    Submit = () =>{
      let payload = {
            ForId: this.state.UserID,
            ById: this.state.UserID,
            Requests: {
                [this.state.System]:{
                    Reason : this.state.Reason
                }
            }
        };
        let submitRequest = UrbanSporkAPI.requestPermission(payload);
        submitRequest.then(() => {
                this.getPendingRequestsForUser();
                this.reason.value = "";
            }

        );
    };

    columns = [
        {accessor: 'permissionName', Header: 'Request'},
        {
            accessor: 'dateOfRequest',
            Header: 'Date',
            Cell: ({value}) => moment.utc(value).format('ddd MMM D YYYY').toString()
        },
        // {accessor: 'reason', Header: 'Request Notes'}
    ];

    render() {
        const AllSystems = this.state.SystemData.map((System, index) => (

            <option value={System.permissionID} key={System.permissionID} >{System.permissionName}</option>

        ));

        return (

            <div>
                <Row style={{
                    flex: 1,
                    justifyContent: 'center',
                    alignItems: 'center',
                }}>
                    <h1>Request Dashboard</h1>
                </Row>
                <br/>
                <Row>
                    <Col md={5}>
                        <Row style={{marginLeft: 100}}>
                            <Form>
                                <FormGroup row>
                                    <Label >Select System</Label>
                                    <Input type="select" id="System" onChange={e => {this.updateSystem(e)}}>
                                        <option> </option>
                                        {AllSystems}
                                    </Input>
                                </FormGroup>
                                <br/>
                                <FormGroup row>
                                    <Label for="Reason">Reason For Access</Label>
                                    <Input type="text" id="Reason" ref={el => this.reason = el} placeholder="Why do you need this?" onChange={evt => {this.onInputChange(evt)}} />
                                </FormGroup>
                                <br/>
                                <Button onClick={this.Submit} >Request Access</Button>
                            </Form>

                        </Row>
                    </Col>
                    <Col md={7}>
                        <Row style={{marginLeft: 100}}>
                            <UserTable columns={this.columns} data={this.state.requestData}/>
                        </Row>
                    </Col>
                </Row>


            </div>
        )
    }
}

const mapStateToProps = (state) => {
    return {
        managerId: state.manager.id
    }
};
export default  connect(mapStateToProps)(NonAdminView);