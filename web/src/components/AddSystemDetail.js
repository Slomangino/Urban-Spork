import React from 'react';
import {Col, Form, FormGroup, Input, Label, Button} from 'reactstrap';
import Dropzone from 'react-dropzone'
import UrbanSporkAPI from "../api/UrbanSporkAPI";


export default class AddSystemDetailComponent extends React.Component {
    //state = {...this.props.userData};
    state = {
        dropZoneDisabled: true,
        files: []
    };

   // editedData = {...this.state};

    // handleOnChange = (e) => {
    //     console.log(e.target)
    // };

    render() {
        return(
            <div>
                <Form>
                    <FormGroup row>
                        <Label color={"muted"} sm={"3"} for={"systemName"}>
                            System Name:
                        </Label>
                        <Col sm={20}>
                            <Input id={"systemName"} onChange={this.onInputChange}/>
                        </Col>
                    </FormGroup>

                    <FormGroup row>
                        <Label color={"muted"} sm={"3"} for={"systemDescription"}>
                             System Description:
                        </Label>
                        <Col sm={20}>
                            <Input id={"systemDescription"}/>
                        </Col>
                    </FormGroup>

                    <FormGroup row>
                        <Label color={"muted"} sm={"3"} for={"systemDescription"}>
                            System Logo URL:
                        </Label>
                        <Col sm={20}>
                            <Input id={"systemLogo"}/>
                        </Col>
                    </FormGroup>
                </Form>
            </div>
        )
    }

}



