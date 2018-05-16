import React from 'react';
import {Col, Form, FormGroup, Input, Label} from 'reactstrap';

export default class RemoveDepartments extends React.Component {


       state = {
            Departments: this.props.departments,
        };



    onInputChange = (data) => {
        console.log("onInputChange");
        this.props.AddButton(data.target.value);

    };

    updateDepartmentField = (department) => {
        this.props.DepartmentSelected(department.target.value);
    };

    getAllDepartments = () => {

        let options =  this.state.Departments.map((Department, index) => (

            <option key={index + 1} >{Department.name}</option>

        ));

        options.unshift(<option key={0} ></option>)

        return options
    };

    render() {
        return(
            <div>
                <Form>
                    <FormGroup row>
                        <Col md={6}>
                            <Label for="SelectDepartment">
                                Select Department
                            </Label>
                        </Col>

                        <Col md={6}>
                            <Input type="select"  id="SelectDepartment" onChange={e => {this.updateDepartmentField(e)}}>
                                {this.getAllDepartments()}

                            </Input>
                        </Col>

                    </FormGroup>

                </Form>
            </div>
        )
    }

}



