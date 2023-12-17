import { useState, useCallback } from 'react'
import { Tab, Tabs } from 'react-bootstrap'
import BikeTable from './components/BikeTable'
import RiderTable from './components/RiderTable'

import { getBikesAsync } from './api'

export default function App() {
  const [bikes, setBikes] = useState([])

  const loadBikes = useCallback(async () => {
    const data = await getBikesAsync()
    setBikes(data)
  }, []);

  return (
    <div className="App">
      <Tabs className="mb-3" defaultActiveKey="home">
        <Tab eventKey="home" title="Home">
          Tab content for Home
        </Tab>
        <Tab eventKey="bike" title="Bike">
          <BikeTable bikes={bikes} setBikes={setBikes} loadBikes={loadBikes} />
        </Tab>
        <Tab eventKey="rider" title="Rider">
          <RiderTable bikes={bikes} loadBikes={loadBikes} />
        </Tab>
      </Tabs>
    </div>
  );
}