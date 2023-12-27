import {
    Form,
    NavLink,
    useRouteLoaderData,
} from "react-router-dom"

import {
    Nav,
    Navbar,
} from "react-bootstrap"

export default function MainNavigation() {
    const token = useRouteLoaderData('root')

    return (
        <Navbar className="bg-body-tertiary justify-content-between">
            <Nav>
                <Navbar.Brand href="/">MountainBike</Navbar.Brand>
                <NavLink to="/bikes" className="nav-link" >Bikes</NavLink>
                <NavLink to="/riders" className="nav-link" >Riders</NavLink>
            </Nav>
            <Nav>
                {token ?
                    <Form action="/logout" method="post">
                        <button className="nav-link">Logout</button>
                    </Form> :
                    <NavLink to="/auth" className="nav-link" >Login</NavLink>
                }
            </Nav>
        </Navbar>
    )
}