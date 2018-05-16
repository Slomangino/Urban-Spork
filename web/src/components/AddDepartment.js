import React from 'react';
import {Col, Form, FormGroup, Input, Label} from 'reactstrap';



export default class AddDepartment extends React.Component {

    constructor(){
        super();

        this.state = {
            InputPlaceholder: 'Enter department name',
        };
    }


    onInputChange = (e) => {
        this.props.updateDepartment(e.target.value);
    };

    render() {
        return(
            <div>
                <Form>
                    <FormGroup row>
                        <Col md={6}>
                            <Label color={"muted"} for={"Name"}>
                                Department Name:
                            </Label>
                        </Col>

                        <Col md={6} >
                            <Input placeholder={this.state.InputPlaceholder} id={"Name"} onChange={e => {this.onInputChange(e)}}/>
                        </Col>
                    </FormGroup>

                </Form>
            </div>
        )
    }

}



