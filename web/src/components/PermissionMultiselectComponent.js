import React from 'react';
import {connect} from "react-redux";
import FilteredMultiSelect from 'react-filtered-multiselect'


class PermissionMultiselectComponent extends React.Component {


    handleAddPermission = (selectedPermissions) => {
        this.props.setPermissions(selectedPermissions)
    };

    handleDeselect = (deselectedOptions) => {
        let selectedPermissions = this.props.selectedPermissions.slice();
        deselectedOptions.forEach(option => {
            selectedPermissions.splice(selectedPermissions.indexOf(option), 1)
        });
        this.props.setPermissions(selectedPermissions)
    };

    render(){
        return (
            <div>
                <div style={{display: 'flex', justifyContent: 'space-around'}}>
                    <div style={{minWidth: "150px"}}>
                        <FilteredMultiSelect
                            onChange={this.handleAddPermission}
                            options={this.props.allPermissions}
                            selectedOptions={this.props.selectedPermissions}
                            textProp="permissionName"
                            valueProp="permissionID"
                            buttonText={"Add"}
                            classNames={{
                                button: "btn btn btn-block btn-default",
                                buttonActive: "btn btn btn-block btn-primary",
                                select: "form-control",
                                filter: "form-control"
                            }}
                        />
                    </div>
                    <div style={{minWidth: "150px"}}>
                        <FilteredMultiSelect
                            onChange={(userPermissions) => this.handleDeselect(userPermissions)}

                            options={this.props.selectedPermissions}

                            classNames={{
                                button: "btn btn btn-block btn-default",
                                buttonActive: "btn btn btn-block btn-danger",
                                select: "form-control",
                                filter: "form-control"
                            }}

                            textProp="permissionName"
                            valueProp="permissionID"
                            buttonText={"Remove"}
                        />
                    </div>
                </div>
            </div>
        )
    }
}

const mapStateToProps = (state) => {
    return {
        allPermissions: state.permissions
    }
};

export default connect(mapStateToProps)(PermissionMultiselectComponent);