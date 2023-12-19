import RiderTable from "../components/RiderTable"
import { getBikesAsync, getRidersAsync } from "../utils/api"
import { useLoaderData } from "react-router-dom"

export async function loader() {
    const ridersData = await getRidersAsync()
    const bikesData = await getBikesAsync()
    return { ridersData, bikesData }
}

export default function Riders() {
    const { ridersData, bikesData } = useLoaderData()
    return <RiderTable ridersData={ridersData} bikesData={bikesData} />
}