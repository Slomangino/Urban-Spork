import React from 'react';
import UserTable from "./UserTable";
import UrbanSporkAPI from '../api/UrbanSporkAPI';
import moment from 'moment';
import {Row,Col,Input,Button,Table} from "reactstrap";
import jsPDF from 'jspdf'
import ReactDOMServer from 'react-dom/server';
import FontAwesomeIcon from '@fortawesome/react-fontawesome';
import {faFilePdf} from '@fortawesome/fontawesome-free-solid';




export default class ApproverActivityReport extends React.Component {
    state = {
        Data: [],
        AdminList: []
    };

    componentWillMount() {
        this.getData();
        this.getAdminDropDown();
    }

    columns = [
        {accessor: 'permissionName', Header: 'System'},
        {accessor: 'truncatedEventType', Header: 'Activity'},
        {accessor: 'forFullName', Header: 'For'},
        {accessor: 'approverFullName', Header: 'By'},
        {
            accessor: 'timeStamp',
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
        const Data = UrbanSporkAPI.getApproverActivity();
        Data.then(data => this.setState({Data: data})).catch(() => this.setState({Data: []}));
    };


    getAdminDropDown = () => {
        const Admins = UrbanSporkAPI.getApproverDropDown();
        Admins.then(data => this.setState({AdminList: data})).catch(() => this.setState({AdminList: []}));
    };

    updateTable = (admin) => {
        console.log(admin.target.value);
        let payload = {
            ApproverId: admin.target.value,
        };
        const newData = UrbanSporkAPI.getApproverActivityReport(payload);
        newData.then(data => this.setState({Data: data})).catch(() => this.setState({Data: []}));

    };



    formatReport = () => {
        let pdfBody = this.state.Data.map((entry, index) => (
            <tr key={index}>
                <td>{index + 1}</td>
                <td>{entry.permissionName}</td>
                <td>{entry.truncatedEventType}</td>
                <td>{entry.forFullName}</td>
                <td>{entry.byFullName}</td>
                <td>{entry.timeStamp}</td>
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
                <th>System</th>
                <th>Activity</th>
                <th>For</th>
                <th>By</th>
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
                        <h1>Approver Activity Report</h1>
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
                pdf.save('ActivityReport.pdf');
            }
        );
    };

    render() {
        const AllAdmins = this.state.AdminList.map((Admin, index) => (

            <option value={Admin.id} key={index} >{Admin.name}</option>

        ));
        return (
            <div>
                <div >
                    <Row style={{height: 50}}>
                    </Row>
                    <Row style={this.styles}>
                        <Col md={3}>
                            <Input type="select"  id="SelectApprover" onChange={e => {this.updateTable(e)}}>
                                <option>
                                    Select Admin/Approver
                                </option>
                                {AllAdmins}
                            </Input>
                        </Col>
                        <Col md={6}>
                            <h1>Approver Activity Report</h1>
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