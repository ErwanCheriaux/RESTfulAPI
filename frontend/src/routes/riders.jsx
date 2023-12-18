import RiderTable from "../components/RiderTable"
import { getBikesAsync, getRidersAsync } from "../api"

export async function loader() {
    const ridersData = await getRidersAsync()
    const bikesData = await getBikesAsync()
    return { ridersData, bikesData }
}

export default function Riders() {
    return (
        <>
            <RiderTable />
        </>
    )
}