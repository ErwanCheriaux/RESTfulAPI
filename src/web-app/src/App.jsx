import { Tab, Tabs } from 'react-bootstrap';
import BikeTable from './Components/BikeTable';
import RiderTable from './Components/RiderTable';

export default function App() {
  return (
    <div className="App">
      <Tabs
        defaultActiveKey="home"
        // id="uncontrolled-tab-example"
        className="mb-3"
      >
        <Tab eventKey="home" title="Home">
          Tab content for Home
        </Tab>
        <Tab eventKey="bike" title="Bike">
          <BikeTable />
        </Tab>
        <Tab eventKey="rider" title="Rider">
          <RiderTable />
        </Tab>
      </Tabs>
    </div>
  );
}