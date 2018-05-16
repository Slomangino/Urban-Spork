import React from 'react';
import {NavLink as RouterNavLink} from 'react-router-dom';
import {DropdownItem, DropdownMenu, DropdownToggle, Nav, NavItem, NavLink, UncontrolledDropdown} from 'reactstrap';
import {connect} from "react-redux";
import FontAwesomeIcon from "@fortawesome/react-fontawesome";
import {faUserCircle} from '@fortawesome/fontawesome-free-solid'

// Changed the name of reactstrap's Navlink to NavLinkStrap so as to avoid confusion.

const NonAdminHeader = (props) => (
    <div>
        <header className={"header"}>

            <div className={"header-top"}>
                <div style={{paddingTop: '10px'}}>
                    <img src="https://i.imgur.com/G7zI6wH.png" width="210" height="80"/>
                </div>
                <h2 style={{color: "#CBD4D2", marginTop: "40px", cursor: 'pointer'}} onClick={()=> window.location.href= '/'}>
                    <span>
                        <h6><FontAwesomeIcon icon={faUserCircle} style={{color: "white"}}/>  {props.managerId}</h6>
                    </span>
                </h2>
            </div>

            <Nav tabs justified>
                <NavItem>
                    <NavLink to={"/"} exact={true} width="300px" activeClassName={"is-active"} tag={RouterNavLink}>Home</NavLink>
                </NavItem>

            </Nav>

        </header>
    </div>

);

const mapStateToProps = (state) => {
    return {
        managerId: state.manager.name
    }
};
export default connect(mapStateToProps)(NonAdminHeader);

