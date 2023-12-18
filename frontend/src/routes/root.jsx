import {
    Outlet,
} from "react-router-dom";

import {
    Nav
} from "react-bootstrap";

export default function Root() {
    return (
        <>
            <Nav variant="underline">
                <Nav.Item>
                    <Nav.Link href="/bikes">Bikes</Nav.Link>
                </Nav.Item>
                <Nav.Item>
                    <Nav.Link href="/riders">Riders</Nav.Link>
                </Nav.Item>
            </Nav>
            <div>
                <Outlet />
            </div>
        </>
    );
}