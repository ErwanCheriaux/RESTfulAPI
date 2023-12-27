import {
    NavLink,
    useSubmit,
} from "react-router-dom"

import {
    Nav,
    Navbar,
} from "react-bootstrap"

export default function MainNavigation() {
    const submit = useSubmit()
    function handleLogout() {
        submit(null, { action: '/logout', method: 'post' })
    }

    return (
        <Navbar className="bg-body-tertiary justify-content-between">
            <Nav>
                <Navbar.Brand href="/">MountainBike</Navbar.Brand>
                <NavLink to="/bikes" className="nav-link" >Bikes</NavLink>
                <NavLink to="/riders" className="nav-link" >Riders</NavLink>
            </Nav>
            <Nav>
                <NavLink to="/auth" className="nav-link" >Login</NavLink>
                <NavLink onClick={handleLogout} className="nav-link" >Logout</NavLink>
            </Nav>
        </Navbar>
    )
}