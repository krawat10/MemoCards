import React, {useState} from 'react';
import {Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink} from 'reactstrap';
import {Link} from 'react-router-dom';
import './NavMenu.css';
import {connect} from "react-redux";

const NavMenu = ({language}) => {
  const [collapsed, setCollapsed] = useState(true);
  const [cssUrl, setCssUrl] = useState('/static/css/bright.css')


    const toggle = () => {setCollapsed(!collapsed)}

    return (
      <header>
        <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
          <Container>
            <NavbarBrand tag={Link} to="/">MemoCards</NavbarBrand>
            <NavbarToggler onClick={toggle} className="mr-2" />
            <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={collapsed} navbar>
              <ul className="navbar-nav flex-grow">
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/">{language['home']}</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/login">{language['login']}</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/Register">{language['register']}</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink onClick={() => setCssUrl('/static/css/bright.css')} className="text-dark">Bright</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink onClick={() => setCssUrl('/static/css/dark.css')} className="text-dark">Dark</NavLink>
                </NavItem>
                <link rel="stylesheet" type="text/css" href={cssUrl} />
              </ul>
            </Collapse>
          </Container>
        </Navbar>
      </header>
    );
};

const mapStateToProps = state => ({
  language: state.language
});

export default connect(mapStateToProps)(NavMenu);