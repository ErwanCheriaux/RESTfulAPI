import { render, screen } from '@testing-library/react'
import App from '../App'

describe('App', () => {
  test('renders App component', async () => {
    render(<App />)

    // Check that the tabs are rendered
    expect(screen.getByText('Home')).toBeInTheDocument()
    screen.getAllByText('Bike').forEach((element) => {
      expect(element).toBeInTheDocument()
    })
    screen.getAllByText('Rider').forEach((element) => {
      expect(element).toBeInTheDocument()
    })
  })
})
