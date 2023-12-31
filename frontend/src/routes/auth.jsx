import { json, redirect } from "react-router"
import AuthForm from "../components/AuthForm"

export async function action({ request }) {
    const searchParams = new URL(request.url).searchParams
    const mode = searchParams.get('mode') || 'login'

    if (mode !== 'login' && mode !== 'signup') {
        throw json({ message: 'Unsupported mode.' }, { status: 422 })
    }

    const data = await request.formData()
    const authData = {
        email: data.get('email'),
        password: data.get('password'),
    }

    const response = await fetch(`${process.env.REACT_APP_SERVER_URL}/${mode}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(authData),
    })

    if (response.status === 422 || response.status === 401) {
        return response
    }

    if (!response.ok) {
        throw json({ message: 'Could not authenticate user.' }, { status: 500 })
    }

    const resData = await response.json()
    const token = resData.token

    localStorage.setItem('token', token)

    const decodedJwt = parseJwt(token)
    const expiration = new Date(decodedJwt.exp * 1000)
    localStorage.setItem('expiration', expiration.toISOString())

    return redirect('/')
}

export default function Authentication() {
    return <AuthForm />
}

const parseJwt = (token) => {
    try {
        return JSON.parse(atob(token.split(".")[1]));
    } catch (e) {
        return null;
    }
};