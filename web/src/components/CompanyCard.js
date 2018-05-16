import React from 'react';
import {Button, Card, CardBody,CardTitle, Row, Col} from 'reactstrap';
import FontAwesomeIcon from "@fortawesome/react-fontawesome";

export default class CompanyCard extends React.Component{

    render() {
        return (
            <Card  className="text-center"
                  style=
                      {{
                          borderWidth: 1,
                          borderColor: this.props.color,
                          borderLeftWidth: 3,
                          borderBottomWidth: 3
                      }}>
                <br/>
                <CardTitle>{this.props.cardTitle}</CardTitle>
                <CardBody>
                    <Row className="text-center" style={{justifyContent: "center"}}>
                    <FontAwesomeIcon icon={this.props.fontAwesomeIcon} size="8x"
                                     style={{
                                         align: "center",
                                         color: this.props.color
                                     }}/>
                    </Row>



                <br/>

                        <Row>
                            <Col md={6} sm={6}>
                                <span>
                                <Button color={this.props.buttonOneColor} onClick={this.props.addButtonOnClick}>{this.props.buttonOneText}</Button>
                                </span>
                            </Col>
                            <Col md={6} sm={6}>
                                <span>
                                <Button color={this.props.buttonTwoColor} onClick={this.props.removeButtonOnClick} style={this.props.style}>{this.props.buttonTwoText}</Button>
                                </span>
                            </Col>
                        </Row>
                </CardBody>

            </Card>
        )
    }
}