import {
    NavLink,
    Outlet,
} from "react-router-dom";

import {
    Nav,
    Navbar,
} from "react-bootstrap";

export default function Root() {
    return (
        <>
            <Navbar className="bg-body-tertiary">
                <Navbar.Brand exact href="/">MountainBike</Navbar.Brand>
                <Nav>
                    <NavLink to="/bikes" className="nav-link" >Bikes</NavLink>
                    <NavLink to="/riders" className="nav-link" >Riders</NavLink>
                </Nav>
            </Navbar>
            <div>
                <Outlet />
            </div>
        </>
    );
}