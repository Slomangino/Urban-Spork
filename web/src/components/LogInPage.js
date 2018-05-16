import React from 'react';
import {Button, Col, Form, FormGroup, Input, Label} from "reactstrap";
import UrbanSporkAPI from '../api/UrbanSporkAPI';


export default class LogInPage extends React.Component {
    state = {
        admin: false,
        LogginList: [],
    };

    styles = {
        flex: 1,
        justifyContent: 'center',
         alignItems: 'center',
        margin:'auto'
        //paddingTop: '300px'
    };

    componentWillMount() {
        this.getLogginDropDown();
    };

    getLogginDropDown = () => {
        const Admins = UrbanSporkAPI.getLogginDropDown();
        Admins.then(data => this.setState({LogginList: data})).catch(() => this.setState({LogginList: []}));
    };

    onSelectHandle = (e) => {
        const selected = e.target.value;
        const selectedId = e.target.options[e.target.options.selectedIndex].id;
        const selectedName = e.target.options[e.target.options.selectedIndex].text;


        this.setState({id: selectedId});
        this.setState({name: selectedName});
        this.setState({admin: selected});
    };

    render() {
        const LogginUsers = this.state.LogginList.map((User, index) => (

            <option  id={User.userId} style={User.isAdmin? {color:"#FF0000"}: {color:"#000000"} } value={User.isAdmin} key={index} >{User.fullName}</option>

        ));
        return (

            <div style={{paddingTop: '230px'}}>

                <img style={{display: 'block', margin: 'auto',}} src="https://i.imgur.com/G7zI6wH.png" width="500" height="150"/>
                <br/>
                <Form style={this.styles} inline>

                    <br/>
                    <FormGroup>
                        <Label for="adminSelector">Select User</Label>
                        <Col>
                            <Input onChange={(e) => this.onSelectHandle(e)} type={'select'} name="adminSelector" id="adminSelector">
                                <option/>
                                {LogginUsers}
                            </Input>
                        </Col>

                    </FormGroup>
                    {' '}
                    <Button
                        onClick={()=> {
                            this.props.isAdmin(this.state.admin, this.state.id, this.state.name);
                        }}
                    >LogIn</Button>
                </Form>
            </div>
        )
    }
}