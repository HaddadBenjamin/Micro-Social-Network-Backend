import React from "react";
import { BrowserRouter as Router } from "react-router-dom";
import {
    MDBNavbar,
    MDBNavbarBrand,
    MDBNavbarNav,
    MDBNavItem,
    MDBNavbarToggler,
    MDBCollapse,
    MDBMask,
    MDBRow,
    MDBCol,
    MDBView,
    MDBContainer,
    MDBFormInline,
    MDBAnimation
} from "mdbreact";
import "./AppPage.css";
import SearchItem from "../Items/SearchItem";
import {DefaultSearchItemDto} from "../Items/SearchItemDto";

class AppPage extends React.Component
{
    state =
    {
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
        <strong className="white-text">Diablo II - Documentation</strong>
            </MDBNavbarBrand>
            <MDBNavbarToggler onClick={this.handleTogglerClick} />
        <MDBCollapse isOpen={this.state.collapsed} navbar>
        <MDBNavbarNav left>
        <MDBNavItem active>

        <MDBFormInline waves>
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
        md="5"
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

        <MDBCol md="5" xl="6" className="mt-xl-5 offset-md-1">
            <MDBAnimation type="fadeInRight" delay=".3s">
              <SearchItem search={ DefaultSearchItemDto} />
            </MDBAnimation>
        </MDBCol>
    </MDBRow>
    </MDBContainer>
    </MDBMask>
    </MDBView>

        </div>
    );
    }
}

export default AppPage;