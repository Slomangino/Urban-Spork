import React from 'react';
import {Col, Form, FormGroup, Input, Label} from 'reactstrap';
import UrbanSporkAPI from '../api/UrbanSporkAPI';

export default class RemovePosition extends React.Component {

    constructor(props){
        super();

        this.state = {
            InputPlaceholder: 'Enter title of position',
            Departments: props.department,
            Positions: [],
            PositionId: ""
        };

        this.onInputChange = this.onInputChange.bind(this);
        this.updateDepartment = this.updateDepartment.bind(this);
    }

    onInputChange = (data) => {
        this.props.AddButton(data.target.value);

    };

    handleOnPositionChange = (e) => {
        const selectedOptionIndex =  e.target.options.selectedIndex;
        const option = e.target.options[selectedOptionIndex];
        this.props.positionId(option.id);
        this.props.position(e.target.value);
    }

    // updatePositionList = (department) => {
    //     return UrbanSporkAPI.getPositionByDepartment(department.target.value);
    // };

    updateDepartment = (department) => {

        this.props.DepartmentSelected(department);
        // let positions = this.updatePositionList(department);

        let positions = UrbanSporkAPI.getPositionByDepartment(department.target.value);


        positions.then((positions) => {
            this.setState({Positions: positions}),
            this.getAllPositions
        });
    };

    getAllPositions = () => {

        let positionList = this.state.Positions.map((Position, index) => (

            <option id={Position.id} key={index + 1} >{Position.positionName}</option>

        ));

        positionList.unshift(<option key={0} ></option>)
        return positionList;
    };

    getAllDepartments = () => {

        let options = this.state.Departments.map((Department, index) => (
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
                        </Col>

                    </FormGroup>
                    <br/>
                    <FormGroup row>
                        <Col md={6}>
                            <Label color={"muted"}  for={"Title"}>
                                Position Title:
                            </Label>
                        </Col>

                            <Col md={6}>
                                <Input type="select"  id="SelectPosition" onChange={e => {this.handleOnPositionChange(e)}}>
                                    {this.getAllPositions()}
                                </Input>
                            </Col>

                    </FormGroup>
                </Form>
            </div>
        )
    }

}



