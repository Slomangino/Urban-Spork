import React from 'react';
import {Col, Form, FormFeedback, FormGroup, Input, Label} from 'reactstrap';


export default class AddSystem extends React.Component {

    constructor() {
        super();

        this.state = {
        };
    }

    render() {
        return (
            <div>
                <Form>
                    <FormGroup row>
                        <Col md={6}>
                            <Label color={"muted"} for={"systemName"}>
                                System Name:
                            </Label>
                        </Col>

                        <Col md={6}>
                            <Input placeholder={"Enter System Name"} id={"systemName"} onChange={evt => {
                                this.props.onInputChange(evt)

                            }}/>
                            <FormFeedback>This feeld has to be filled</FormFeedback>
                        </Col>
                    </FormGroup>

                    <FormGroup row>
                        <Col md={6}>
                            <Label color={"muted"} for={"systemDescription"}>
                                System Description:
                            </Label>
                        </Col>

                        <Col md={6}>
                            <Input placeholder={'Enter Description'} id={"systemDescription"}
                                   onChange={evt => {
                                       this.props.onInputChange(evt)
                                   }}/>
                        </Col>
                    </FormGroup>

                    <FormGroup row>
                        <Col md={6}>
                            <Label color={"muted"} for={"SystemLogoURL"}>
                                System Logo URL:
                            </Label>
                        </Col>

                        <Col md={6}>
                            <Input placeholder={'Enter Logo Image URL'} id={"systemLogoURL"} onChange={evt => {
                                this.props.onInputChange(evt)
                            }}/>
                        </Col>
                    </FormGroup>

                </Form>
            </div>
        )
    }

}



