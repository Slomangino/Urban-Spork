import React from 'react';
import {Col, Form, FormGroup, Input, Label} from 'reactstrap';

export default class AddPosition extends React.Component {

    constructor(props){
        super();

        this.state = {
            InputPlaceholder: 'Enter title of position',
            Departments: props.department,
        };
    }


    onInputChange = (data) => {
        this.props.AddButton(data.target.value);
    };

    updateDepartment = (department) => {

        this.props.DepartmentSelected(department);
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
                            <Input type="select"  id="SelectDepartment" onChange={e => {this.updateDepartment(e)}}>
                                {this.getAllDepartments()}
                            </Input>
                            <br/>
                        </Col>

                        <FormGroup row>
                            <Col ms={6}>
                                <Label color={"muted"}  for={"Title"}>
                                    Position Title
                                </Label>
                            </Col>
                            <Col md={7}>
                                <Input placeholder={"Enter title of position"} id={"Title"} onChange={e => {this.onInputChange(e)}}/>
                            </Col>
                        </FormGroup>
                    </FormGroup>

                </Form>
            </div>
        )
    }

}



