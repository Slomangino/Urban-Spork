import React from 'react';
import {Button, Modal, ModalHeader, ModalBody, ModalFooter} from 'reactstrap';
import {connect} from "react-redux";
// import StaticUserDetail from './StaticUserDetail';
// import EditUserDetail from "./EditUserDetail";
import UrbanSporkAPI from "../api/UrbanSporkAPI";


class PermissionRequestModal extends React.Component {

    // componentWillReceiveProps(nextProps) {
    //     if(nextProps.isOpen){
    //         const userData = UrbanSporkAPI.getUserFullData(nextProps.userId);
    //         userData.then(data => this.setState({userData: data}))
    //     }
    // }
    //
    state = {
        // edit: false
    };


    // handleOnEdit = () => {
    //     this.setState({edit: true})
    // };
    //
    handleOnCancel = () => {
            this.props.toggle();
    };

    handleOnClose = () => {
        // this.setState({edit: false});
        this.props.toggle();
    };

    handleOnGrantReguest = () => {

        let payload = {
            ForId: this.props.requestData.forId,
            ById: this.props.managerId ,
            PermissionsToGrant:{
                [this.props.requestData.permissionId]:{
                    EventType:"GrantPermission",
                    IsPending:true,
                    Reason:"Need Access",
                    RequestedBy:this.props.requestData.byId,
                    RequestedFor:this.props.requestData.forId,
                }
            }

        };
        console.log(payload);
        UrbanSporkAPI.grantPermission(payload).then(()=> {
                this.props.refreshData();
                this.props.refreshDashboard();
        }

        );
        this.props.toggle();

    };

    handleOnDenyReguest = () => {
        let payload = {
        ForId: this.props.requestData.forId,
        ById: this.props.managerId ,
        PermissionsToDeny:{
            [this.props.requestData.permissionId]:{
                EventType:"DenyPermission",
                IsPending:true,
                Reason:"Need Access",
                RequestedBy:this.props.requestData.byId,
                RequestedFor:this.props.requestData.forId,
            }
        }

    };
        console.log(payload);
        UrbanSporkAPI.denyPermission(payload).then(()=> {
                this.props.refreshData();
                this.props.refreshDashboard();
            }

        );
        this.props.toggle();


    };


    render() {
        return (
            <div>
                <Modal isOpen={this.props.isOpen} toggle={this.handleOnClose}>
                    <ModalHeader toggle={this.handleOnClose}>Permission Request</ModalHeader>
                    <ModalBody>
                        {/*If requestData exists do something*/}
                        {this.props.requestData && <p><h5>Access to:</h5> {this.props.requestData.permissionName}</p>}
                        <hr/>
                        {this.props.requestData && <p><h5>Reason:</h5> {this.props.requestData.reason}</p>}
                        {/*{this.state.userData? (this.state.edit? <EditUserDetail  userData={this.state.userData}/>: <StaticUserDetail userData={this.state.userData}/>):null}*/}
                    </ModalBody>
                    <ModalFooter>
                        <Button color="danger" onClick={this.handleOnDenyReguest}>Deny Request</Button>
                        {' '}
                        <Button color="success" onClick={this.handleOnGrantReguest}>Grant Request</Button>
                        {' '}
                        <Button color="secondary" onClick={this.handleOnCancel}>Cancel</Button>
                    </ModalFooter>
                </Modal>
            </div>
        );
    }
}
const mapStateToProps = (state) => {
    return {
        managerId: state.manager.id
    }
};
export default connect(mapStateToProps)(PermissionRequestModal);