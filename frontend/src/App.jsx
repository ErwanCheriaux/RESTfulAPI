import { useState, useCallback } from 'react'
import { Tab, Tabs } from 'react-bootstrap'
import BikeTable from './components/BikeTable'
import RiderTable from './components/RiderTable'

export default function App() {
  const [bikes, setBikes] = useState([])

  const getBikes = useCallback(async () => {
    // Fetch data from your .NET API
    try {
      const response = await fetch(process.env.REACT_APP_SERVER_URL + '/bikes')
      if (response.ok) {
        const data = await response.json()
        setBikes(data)
      } else {
        console.error('Failed to fetch data from API')
      }
    } catch (error) {
      console.error('Error:', error)
    }
  }, []);

  return (
    <div className="App">
      <Tabs className="mb-3" defaultActiveKey="home">
        <Tab eventKey="home" title="Home">
          Tab content for Home
        </Tab>
        <Tab eventKey="bike" title="Bike">
          <BikeTable bikes={bikes} setBikes={setBikes} getBikes={getBikes} />
        </Tab>
        <Tab eventKey="rider" title="Rider">
          <RiderTable bikes={bikes} getBikes={getBikes} />
        </Tab>
      </Tabs>
    </div>
  );
}