import BikeTable from "../components/BikeTable"
import { getBikesAsync } from "../utils/api"
import { useLoaderData, useRouteLoaderData } from "react-router-dom"

export async function loader() {
    const data = await getBikesAsync()
    return data
}

export default function Bikes() {
    return <BikeTable
        bikesData={useLoaderData()}
        token={useRouteLoaderData('root')}
    />
}