import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Glyphicon, Nav, Navbar, NavItem } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import './NavMenu.css';

export class NavMenu extends Component {
  displayName = NavMenu.name

  logout() {
    localStorage.clear();
  }

  render() {
    let userName = localStorage.getItem('loggedUserName');
    return (
      <Navbar inverse fixedTop fluid collapseOnSelect>
        <Navbar.Header>
          <Navbar.Brand>
            <Link to={'/'}>Учет призывников</Link>
          </Navbar.Brand>
          <Navbar.Toggle />
        </Navbar.Header>
        <Navbar.Header>
          <Navbar.Brand>
            {userName}
          </Navbar.Brand>
          <Navbar.Toggle />
        </Navbar.Header>
        <Navbar.Collapse>
          <Nav>
            <LinkContainer to={'/'} exact>
              <NavItem>
                <Glyphicon glyph='home' /> Home
              </NavItem>
            </LinkContainer>
            <LinkContainer to={'/counter'}>
              <NavItem>
                <Glyphicon glyph='education' /> Counter
              </NavItem>
            </LinkContainer>
            <LinkContainer to={'/fetchdata'}>
              <NavItem>
                <Glyphicon glyph='th-list' /> Fetch data
              </NavItem>
            </LinkContainer>
            <LinkContainer to={'/trooptypespage'}>
              <NavItem>
                <Glyphicon glyph='th-list' /> Рода войск
              </NavItem>
            </LinkContainer>
            <LinkContainer to={'/troopkindspage'}>
              <NavItem>
                <Glyphicon glyph='th-list' /> Виды войск
              </NavItem>
            </LinkContainer>
            <LinkContainer to={'/userPage'}>
              <NavItem>
                <Glyphicon glyph='user' /> Личный кабинет
              </NavItem>
            </LinkContainer>
            <LinkContainer to={'/'}>
              <NavItem onClick={() => this.logout()}>
                <Glyphicon glyph='off' />
                  Выйти из системы
              </NavItem>
            </LinkContainer>
          </Nav>
        </Navbar.Collapse>
      </Navbar>
    );
  }
}
