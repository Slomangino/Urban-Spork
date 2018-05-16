import React from 'react';
import UserTable from "./UserTable";
import UrbanSporkAPI from '../api/UrbanSporkAPI';
import moment from 'moment';
import {Row,Col,Input,Button,Table} from "reactstrap";
import jsPDF from 'jspdf'
import ReactDOMServer from 'react-dom/server';
import FontAwesomeIcon from '@fortawesome/react-fontawesome';
import {faFilePdf} from '@fortawesome/fontawesome-free-solid';




export default class SystemReport extends React.Component {
    state = {
        Data: [],
        SystemsList: [],
        SelectedSystem: ""
    };

    componentWillMount() {
        this.getData();
        this.getSystemDropDown();
    }

    columns = [
        {accessor: 'forFullName', Header: 'Name'},
        {accessor: 'byFullName', Header: 'Granted By'},
        {
            accessor: 'timestamp',
            Header: 'Date',
            Cell: ({value}) => moment.utc(value).format('ddd MMM D YYYY').toString()
        },
    ];

    styles = {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center',
        textAlign: 'center'
    };

    getData = () => {
        const Data = UrbanSporkAPI.getSystemsActivity();
        Data.then(data => this.setState({Data: data})).catch(() => this.setState({Data: []}));
    };


    getSystemDropDown = () => {
        const Systems = UrbanSporkAPI.getSystemsDropDown();
        Systems.then(data => this.setState({SystemsList: data})).catch(() => this.setState({SystemsList: []}));
    };

    updateTable = (system) => {
        console.log(system.target.options[system.target.options.selectedIndex].text);
        this.setState({SelectedSystem: system.target.options[system.target.options.selectedIndex].text});
        let payload = {
            PermissionId: system.target.value,
        };
        const newData = UrbanSporkAPI.getSystemReport(payload);
        newData.then(data => this.setState({Data: data})).catch(() => this.setState({Data: []}));

    };

    formatReport = () => {
        let pdfBody = this.state.Data.map((entry, index) => (
            <tr key={index}>
                <td>{index + 1}</td>
                <td>{entry.forFullName}</td>
                <td>{entry.byFullName}</td>
                <td>{entry.timestamp}</td>
            </tr>
        ));
        return pdfBody;
    };

    exportToPDF = () => {
        let pdf = new jsPDF('p', 'pt', 'letter');
        let rows = this.formatReport();
        let table = (
            <Table >
                <thead style={{height:20}}>

                <th>#</th>
                <th>Name</th>
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
                        <h1>{this.state.SelectedSystem} System Report</h1>
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
                pdf.save(system + 'SystemReport.pdf');
            }
        );
    };

    render() {
        const AllSystems = this.state.SystemsList.map((System, index) => (

            <option name={System.permissionName} value={System.permissionID} key={System.permissionID} >{System.permissionName}</option>

        ));
        return (
            <div>
                <div >
                    <Row style={{height: 50}}>
                    </Row>
                    <Row style={this.styles}>
                        <Col md={3}>
                            <Input type="select"  id="SelectSystem" onChange={e => {this.updateTable(e)}}>
                                <option>
                                    Select System
                                </option>
                                {AllSystems}
                            </Input>
                        </Col>
                        <Col md={6}>
                            <h1>System Report</h1>
                        </Col>
                        <Col md={3}>
                            <Button onClick={() => {this.exportToPDF()}}>
                                <FontAwesomeIcon icon={faFilePdf}/>


                            </Button>
                        </Col>
                    </Row>
                </div>
                <div>
                    <UserTable columns={this.columns} data={this.state.Data}/>
                </div>
            </div>
        )
    }
}