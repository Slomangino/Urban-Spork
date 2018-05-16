import React from 'react';
import {Col, Form, FormFeedback, FormGroup, Input, Label} from 'reactstrap';


export default class EditSystem extends React.Component {

    state = {
        Systems: this.props.systems,
        systemName: "Select System Above",
        systemDescription: "Select System Above",
        systemLogoUrl: "Select System Above",
        systemId:""
    }

    handleOnSystemChange = (event) => {

        const selectedOptionIndex =  event.target.options.selectedIndex;

        const option = event.target.options[selectedOptionIndex];

        const system = this.state.Systems.find(x => x.id === option.id);

        this.setState({systemName: system.name});
        this.setState({systemDescription: system.description});
        this.setState({systemLogoUrl: system.image});
        this.setState({systemId: system.id});

        this.props.idChanged(system.id);
    }

    getAllSystems = () => {

        let templateList =  this.state.Systems.map((System, index) => (
            <option id={System.id} key={index + 1} >{System.name}</option>
        ));

        templateList.unshift(<option key={0} >Select System</option>);

        return templateList;
    };

    // defaultValue={this.state.systemName}


    render() {
        return (
            <div>
                <Form>
                    <FormGroup row>
                        <Col md={6}>
                            <Label for="SelectSystem">
                                Select System
                            </Label>
                        </Col>

                        <Col md={6}>
                            <Input type="select"  id="SelectSystem" onChange={e => {this.handleOnSystemChange(e)}}>
                                {this.getAllSystems()}

                            </Input>
                        </Col>

                    </FormGroup>
                    <br/>
                    <FormGroup row>
                        <Col md={6}>
                            <Label  color={"muted"} for={"systemName"}>
                                System Name:
                            </Label>
                        </Col>

                        <Col md={6}>
                            <Input placeholder={this.state.systemName} id={"systemName"} onChange={evt => {
                                this.props.onInputChange(evt)}}/>
                        </Col>
                    </FormGroup>

                    <FormGroup row>
                        <Col md={6}>
                            <Label color={"muted"} for={"systemDescription"}>
                                System Description:
                            </Label>
                        </Col>

                        <Col md={6}>
                            <Input placeholder={this.state.systemDescription} id={"systemDescription"}
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
                            <Input placeholder={this.state.systemLogoUrl} id={"systemLogoURL"} onChange={evt => {
                                this.props.onInputChange(evt)
                            }}/>
                        </Col>
                    </FormGroup>

                </Form>
            </div>
        )
    }

}



