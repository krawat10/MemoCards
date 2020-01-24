import React, {useState} from 'react';
import {Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink} from 'reactstrap';
import {Link} from 'react-router-dom';
import './NavMenu.css';
import {connect} from "react-redux";
import {login, logout, setLanguage} from "../actions";
import history from "../history";

const NavMenu = ({language, user, logout}) => {
    const [collapsed, setCollapsed] = useState(true);
    const [cssUrl, setCssUrl] = useState('/static/css/bright.css')

    const toggle = () => {
        setCollapsed(!collapsed)
    };

    const logoutClick = () => {
        logout();
        history.push('/');
    };

    return (
        <header>
            <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
                <Container>
                    <NavbarBrand tag={Link} to="/">MemoCards</NavbarBrand>
                    <NavbarToggler onClick={toggle} className="mr-2"/>
                    <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={collapsed} navbar>
                        <ul className="navbar-nav flex-grow">
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" to="/">{language['home']}</NavLink>
                            </NavItem>
                            <NavItem>
                                {user.isValid ? <NavLink onClick={() => logoutClick()} className="text-dark btn">
                                    {language['logout']}</NavLink> : null}
                            </NavItem>
                            <NavItem>
                                {!user.isValid ? <NavLink tag={Link} className="text-dark" to="/login">
                                    {language['login']}
                                </NavLink> : null}
                            </NavItem>
                            <NavItem>
                                {!user.isValid ? <NavLink tag={Link} className="text-dark"
                                                          to="/Register">{language['register']}</NavLink> : null}
                            </NavItem>
                                <NavItem>
                                    <NavLink onClick={() => setCssUrl('/static/css/bright.css')}
                                             className="btn text-dark">Bright</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink onClick={() => setCssUrl('/static/css/dark.css')}
                                             className="btn text-dark">Dark</NavLink>
                                </NavItem>
                        </ul>
                        <link rel="stylesheet" type="text/css" href={cssUrl}/>
                    </Collapse>
                </Container>
            </Navbar>
        </header>
    );
};

const mapStateToProps = state => ({
    language: state.language,
    user: state.user
});

const mapDispatchToProps = dispatch => ({
    logout: () => dispatch(logout())
});

export default connect(mapStateToProps, mapDispatchToProps)(NavMenu);