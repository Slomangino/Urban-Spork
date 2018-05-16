import React from 'react';
import UserTable from "./UserTable";
import UrbanSporkAPI from '../api/UrbanSporkAPI';
import moment from 'moment';
import PermissionRequestModal from "./PermissionRequestModal";

export default class PendingRequests extends React.Component{
    state = {
        pipi: " hi",
        modalIsOpen: false,
        requestData: []
    };

    componentWillMount() {
        this.getRequestData();
    }


    // componentWillUpdate(nextProps, nextState) {
    //     console.log('component Will update');
    //     const requestData = UrbanSporkAPI.getPendingRequests();
    //     requestData.then(data => this.setState({requestData: data}))
    // }


    styles = {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center',
        textAlign: 'center'
    };

    columns = [
        // {accessor: 'id', Header: 'User ID'},
        {accessor: 'forFullName', Header: 'Name'},
        {accessor: 'permissionName', Header: 'Request'},
        {
            accessor: 'dateOfRequest',
            Header: 'Date',
            Cell: ({value}) => moment.utc(value).format('ddd MMM D YYYY').toString()
        },
        // {accessor: 'reason', Header: 'Request Notes'}
    ];

    getRequestData = () => {
        const requestData = UrbanSporkAPI.getPendingRequests();
        requestData.then(data => this.setState({requestData: data})).catch(() => this.setState({requestData: []}));
    };

    toggleModalIsOpen = () => {
        this.setState({SystemModalIsOpen: !this.state.SystemModalIsOpen});
    };

    setSelectedRequestData = (selectedRequestData) => this.setState({selectedRequestData: selectedRequestData});


    openRequestDialogModal = (selectedRequestData) => {
        this.setSelectedRequestData(selectedRequestData);
        this.toggleModalIsOpen();
    };

    render() {
        return (
            <div>

                <div style={this.styles}>
                    <h1>Pending Requests</h1>
                </div>
                <div>
                    <UserTable onRowClick={this.openRequestDialogModal} columns={this.columns} data={this.state.requestData}/>
                </div>
                <PermissionRequestModal isOpen={this.state.SystemModalIsOpen} toggle={this.toggleModalIsOpen} refreshDashboard={this.props.getDashboard} refreshData={this.getRequestData} requestData={this.state.selectedRequestData}/>
            </div>
        )
    }

}


