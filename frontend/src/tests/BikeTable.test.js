import { render, screen } from '@testing-library/react'
import BikeTable from '../components/BikeTable'

describe('BikeTable', () => {
    test('renders BikeTable component', async () => {
        render(<BikeTable bikesData={[]} />)

        // Check that the table is rendered
        expect(screen.getByText('My bike list')).toBeInTheDocument()
        screen.getAllByText('Brand').forEach((element) => {
            expect(element).toBeInTheDocument()
        })
        screen.getAllByText('Model').forEach((element) => {
            expect(element).toBeInTheDocument()
        })
        screen.getAllByText('Year').forEach((element) => {
            expect(element).toBeInTheDocument()
        })
        screen.getAllByText('Material').forEach((element) => {
            expect(element).toBeInTheDocument()
        })
        screen.getAllByText('Color').forEach((element) => {
            expect(element).toBeInTheDocument()
        })
        screen.getAllByText('Size').forEach((element) => {
            expect(element).toBeInTheDocument()
        })
        screen.getAllByText('Serial number').forEach((element) => {
            expect(element).toBeInTheDocument()
        })
    })
})