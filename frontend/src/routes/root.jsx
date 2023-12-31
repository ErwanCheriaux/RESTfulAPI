import { useEffect } from "react"
import {
    Outlet,
    useLoaderData,
    useSubmit,
} from "react-router-dom"
import MainNavigation from "../components/MainNavigation"
import { getTokenDuration } from "../utils/auth"

export default function Root() {
    const token = useLoaderData()
    const submit = useSubmit()
    useEffect(() => {
        if (!token) {
            return
        }

        if (token === 'EXPIRED') {
            submit(null, { action: '/logout', method: 'post' })
            return
        }

        const tokenDuration = getTokenDuration()
        console.log(`token duration: ${Math.round(tokenDuration / 600) / 100} min`)

        setTimeout(() => {
            submit(null, { action: '/logout', method: 'post' })
        }, tokenDuration)
    }, [token, submit])

    return (
        <>
            <MainNavigation token={token} />
            <div>
                <Outlet />
            </div>
        </>
    )
}