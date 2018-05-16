import React from 'react';
import {Badge, Card, CardBody, CardImg, CardTitle, Col, Row, Table} from "reactstrap";

import Scroll from 'react-scrollbar';
import PendingRequests from './PendingRequests';
import jsPDF from 'jspdf'
import UrbanSporkAPI from "../api/UrbanSporkAPI";
import ReactDOMServer from 'react-dom/server';


export default class DashboardPage extends React.Component {

    componentDidMount = () => {
        console.log('example Component mounted');
        this.getDashboardData();

    };
    getDashboardData = () => {
        let Data = UrbanSporkAPI.getSystemDashboard();
        Data.then(data => {
            this.setState({data: data})
        });
    };
    getReport = (ID) => {
        let payload = {
            PermissionId: ID,
            SearchTerms: "",
            SortDirection: "ASC",
            SortField: "ForFullName",
            EnablePaging: false,
        };
        return UrbanSporkAPI.getSystemReport(payload);
    };
    formatReport = () => {
        let pdfBody = this.state.ReportResults.map((entry, index) => (
            <tr key={index}>
                <td>{index + 1}</td>
                <td>{entry.forFullName}</td>
                <td>{entry.byFullName}</td>
                <td>{entry.timestamp}</td>
            </tr>
        ));
        return pdfBody;
    };
    cardSelected = (SystemId, SystemName) => {
        const payload = this.getReport(SystemId);
        payload
            .then((data) => this.setState({ReportResults: data}))
            .then(() => {
                let pdf = new jsPDF('p', 'pt', 'letter');
                let rows = this.formatReport();
                let table = (
                    <Table>
                        <thead style={{height: 20}}>

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
                                <h1>{SystemName} System Report</h1>
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
                        pdf.save(SystemName + 'ActivityReport.pdf');
                    }
                );
            });
    };

    constructor(props) {
        super(props);
        this.state = {
            data: [],
            ReportResults: [],

        };
    }

    render() {
        const systems = this.state.data.map((_System, index) => (
            <Col key={index}>
                <Card md={3} onClick={() => {
                    this.cardSelected(_System.permissionId, _System.systemName)
                }} style=
                          {{
                              backgroundColor: "#FFFFFF",
                              cursor: "pointer",

                          }}>
                    <br/>
                    <CardTitle
                        style={{textAlign: "center"}}>{_System.logoUrl != null ? '' : _System.systemName}</CardTitle>

                    <CardImg
                        src={_System.logoUrl != null ? _System.logoUrl : 'http://st.motortrend.com/uploads/sites/5/2015/11/noimage.png?interpolation=lanczos-none&fit=around%7C300:200'}
                        width="50%" height="100vw" className="container-fluid"/>
                    <CardBody>
                        <Col md={12}>
                            <Row>
                                <Col md={6}>
                                   <span>
                                       <Badge pill
                                              tyle={{backgroundColor: 'rgb(39, 144, 39)'}}>{_System.pendingRequests} Pending</Badge>
                                   </span>
                                </Col>
                                <Col md={6}>
                                      <span>
                                          <Badge pill
                                                 style={{backgroundColor: 'rgb(19, 79, 88)'}}>{_System.activeUsers} Active Users</Badge>
                                      </span>
                                </Col>
                            </Row>
                        </Col>
                    </CardBody>
                </Card>
                <br/>
            </Col>
        ));
        return (
            <div>
                <div>
                    <br/>
                    <Row style={{height: 50}}>

                    </Row>
                </div>
                <div style={{display: 'flex', justifyContent: 'center'}}>
                    <div>
                        <Col md={3}>

                            <div style={{alignText: 'center'}}>
                                <h1 style={{paddingLeft: '100px'}}>Systems</h1>
                                <Col md={3}>
                                    <Scroll
                                        style={{height: 500, width: 300}}
                                    >
                                        <Row>
                                            <Col>
                                                {systems}
                                            </Col>
                                        </Row>

                                    </Scroll>
                                </Col>
                            </div>
                        </Col>
                    </div>

                    <Col md={9}>
                        <PendingRequests getDashboard={this.getDashboardData}/>
                    </Col>
                </div>

            </div>
        )
    };
}