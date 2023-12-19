import {
    NavLink,
} from "react-router-dom";

import {
    Nav,
    Navbar,
} from "react-bootstrap";

export default function MainNavigation() {
    return (
        <Navbar className="bg-body-tertiary">
            <Navbar.Brand href="/">MountainBike</Navbar.Brand>
            <Nav>
                <NavLink to="/bikes" className="nav-link" >Bikes</NavLink>
                <NavLink to="/riders" className="nav-link" >Riders</NavLink>
            </Nav>
        </Navbar>
    )
}