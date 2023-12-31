import { render, screen } from '@testing-library/react'
import RiderTable from '../components/RiderTable'

describe('RiderTable', () => {
    test('renders RiderTable component', async () => {
        render(<RiderTable ridersData={[]} bikesData={[]} />)

        // Check that the table is rendered
        expect(screen.getByText('My rider list')).toBeInTheDocument()
        screen.getAllByText('Name').forEach((element) => {
            expect(element).toBeInTheDocument()
        })
        screen.getAllByText('Age').forEach((element) => {
            expect(element).toBeInTheDocument()
        })
        screen.getAllByText('Country').forEach((element) => {
            expect(element).toBeInTheDocument()
        })
    })
})