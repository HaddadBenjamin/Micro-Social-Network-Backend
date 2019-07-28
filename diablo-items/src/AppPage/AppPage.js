import React from "react";
import { BrowserRouter as Router } from "react-router-dom";
import {
    MDBNavbar,
    MDBNavbarBrand,
    MDBNavbarNav,
    MDBNavItem,
    MDBNavLink,
    MDBNavbarToggler,
    MDBCollapse,
    MDBMask,
    MDBRow,
    MDBCol,
    MDBBtn,
    MDBView,
    MDBContainer,
    MDBFormInline,
    MDBAnimation
} from "mdbreact";
import "./AppPage.css";
import diablo from '../diablo.png';

class AppPage extends React.Component {
    state = {
        collapsed: false
    };

    handleTogglerClick = () => {
        this.setState({
            collapsed: !this.state.collapsed
        });
    };

    render() {
        const overlay = (
            <div
        id="sidenav-overlay"
        style={{ backgroundColor: "transparent" }}
        onClick={this.handleTogglerClick}
        />
    );
        return (
            <div id="apppage">
            <Router>
            <div>
            <MDBNavbar
        color="primary-color"
        dark
        expand="md"
        fixed="top"
        scrolling
        transparent
        >
        <MDBContainer>
        <MDBNavbarBrand>
        <strong className="white-text">MDB</strong>
            </MDBNavbarBrand>
            <MDBNavbarToggler onClick={this.handleTogglerClick} />
        <MDBCollapse isOpen={this.state.collapsed} navbar>
        <MDBNavbarNav left>
        <MDBNavItem active>
        <MDBNavLink to="#!">Home</MDBNavLink>
            </MDBNavItem>
            <MDBNavItem>
            <MDBNavLink to="#!">Link</MDBNavLink>
            </MDBNavItem>
            <MDBNavItem>
            <MDBNavLink to="#!">Profile</MDBNavLink>
            </MDBNavItem>
            </MDBNavbarNav>
            <MDBNavbarNav right>
        <MDBNavItem>
        <MDBFormInline waves>
        <div className="md-form my-0">
            <input
        className="form-control mr-sm-2"
        type="text"
        placeholder="Search"
        aria-label="Search"
            />
            </div>
            </MDBFormInline>
            </MDBNavItem>
            </MDBNavbarNav>
            </MDBCollapse>
            </MDBContainer>
            </MDBNavbar>
        {this.state.collapsed && overlay}
    </div>
        </Router>
        <MDBView>
        <MDBMask className="d-flex justify-content-center align-items-center gradient">
            <MDBContainer>
            <MDBRow>
            <MDBCol
        md="6"
        className="white-text text-center text-md-left mt-xl-5 mb-5"
            >
            <MDBAnimation type="fadeInLeft" delay=".3s">
            <h1 className="h1-responsive font-weight-bold mt-sm-5">
            ITEMS
        </h1>
        <hr className="hr-light" />
            <h6 className="mb-4">
            It’s in your best interest to bedeck yourself in quality pieces of equipment. Belts, rings, sandals and similar accoutrements aren’t just for looking good - these items are often enchanted to make you swifter or safer. Some can even enhance your skills.
        </h6>

        </MDBAnimation>
        </MDBCol>

        <MDBCol md="6" xl="5" className="mt-xl-5">
            <MDBAnimation type="fadeInRight" delay=".3s">
            <img
        src={diablo}
        alt=""
        className="img-fluid"
            />
            </MDBAnimation>
            </MDBCol>
            </MDBRow>
            </MDBContainer>
            </MDBMask>
            </MDBView>

            <MDBContainer>
        </MDBContainer>
        </div>
    );
    }
}

export default AppPage;