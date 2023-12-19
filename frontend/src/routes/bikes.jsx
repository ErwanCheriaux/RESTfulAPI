import BikeTable from "../components/BikeTable"
import { getBikesAsync } from "../api"
import { useLoaderData } from "react-router-dom"

export async function loader() {
    const data = await getBikesAsync()
    return data
}

export default function Bikes() {
    return (
        <>
            <BikeTable bikesData={useLoaderData()} />
        </>
    )
}