import { render, screen } from '@testing-library/react'
import App from '../App'

describe('App', () => {
  test('renders App component', async () => {
    render(<App />)

    expect(screen.getByText('MountainBike')).toBeInTheDocument()

    screen.getAllByText('Bikes').forEach((element) => {
      expect(element).toBeInTheDocument()
    })

    screen.getAllByText('Riders').forEach((element) => {
      expect(element).toBeInTheDocument()
    })

    expect(screen.getByText('Login')).toBeInTheDocument()
  })
})
