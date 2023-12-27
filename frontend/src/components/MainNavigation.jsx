import {
    NavLink,
    useSubmit,
} from "react-router-dom"

import {
    Nav,
    Navbar,
} from "react-bootstrap"

import { getAuthToken } from "../utils/auth"

export default function MainNavigation() {
    const submit = useSubmit()
    function handleLogout() {
        submit(null, { action: '/logout', method: 'post' })
    }

    function isLoggedIn() {
        const token = getAuthToken()
        if (token === null) {
            return false
        }
        return true
    }

    return (
        <Navbar className="bg-body-tertiary justify-content-between">
            <Nav>
                <Navbar.Brand href="/">MountainBike</Navbar.Brand>
                <NavLink to="/bikes" className="nav-link" >Bikes</NavLink>
                <NavLink to="/riders" className="nav-link" >Riders</NavLink>
            </Nav>
            <Nav>
                {isLoggedIn() ?
                    <NavLink onClick={handleLogout} className="nav-link" >Logout</NavLink> :
                    <NavLink to="/auth" className="nav-link" >Login</NavLink>
                }
            </Nav>
        </Navbar>
    )
}