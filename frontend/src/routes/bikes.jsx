import BikeTable from "../components/BikeTable"
import { getBikesAsync } from "../api"

export async function loader() {
    const data = await getBikesAsync()
    return data
}

export default function Bikes() {
    return (
        <>
            <BikeTable />
        </>
    )
}