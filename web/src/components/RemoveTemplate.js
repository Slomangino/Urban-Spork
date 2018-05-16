import React from 'react';
import {Col, Form, FormGroup, Input, Label} from 'reactstrap';

export default class RemoveTemplate extends React.Component {

    constructor(props){
        super();

        this.state = {
            Templates: props.templates,
        };
    }


    onInputChange = (data) => {
        console.log("onInputChange");
        this.props.AddButton(data.target.value);

    };

    handleOnTemplateChange = (e) => {

        const selectedOptionIndex =  e.target.options.selectedIndex;

        const option = e.target.options[selectedOptionIndex];

        const template = {
            id: option.id,
            name: e.target.value
        }

        this.props.TemplateSelected(template);
    };

    getAllTemplates = () => {

        let templateList =  this.state.Templates.map((Template, index) => (
            <option id={Template.id} key={index + 1} >{Template.name}</option>
        ));

        templateList.unshift(<option key={0} >Select Template</option>);

        return templateList;
    };

    render() {
        return(
            <div>
                <Form>
                    <FormGroup row>
                        <Col md={6}>
                            <Label for="SelectTemplate">
                                Select Template
                            </Label>
                        </Col>

                        <Col md={6}>
                            <Input type="select"  id="SelectTemplate" onChange={e => {this.handleOnTemplateChange(e)}}>
                                {this.getAllTemplates()}

                            </Input>
                        </Col>

                    </FormGroup>

                </Form>
            </div>
        )
    }

}