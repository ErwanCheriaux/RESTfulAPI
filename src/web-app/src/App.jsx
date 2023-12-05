import { useState } from 'react'
import { Tab, Tabs } from 'react-bootstrap';
import BikeTable from './Components/BikeTable';
import RiderTable from './Components/RiderTable';

export default function App() {
  const [bikes, setBikes] = useState([])

  return (
    <div className="App">
      <Tabs className="mb-3" defaultActiveKey="home">
        <Tab eventKey="home" title="Home">
          Tab content for Home
        </Tab>
        <Tab eventKey="bike" title="Bike">
          <BikeTable bikes={bikes} setBikes={setBikes} />
        </Tab>
        <Tab eventKey="rider" title="Rider">
          <RiderTable bikes={bikes} />
        </Tab>
      </Tabs>
    </div>
  );
}