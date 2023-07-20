import {HistoricalSensorData} from "./components/HistoricalSensorData";
import {Metrics} from "./components/Metrics";
import React, {useState} from "react";
import {RemoteControl} from "./components/RemoteControl";
import {Graph} from "./components/Graph";
import {Col, Container, Row} from "reactstrap";
import {Footer} from "./components/Footer";
import {Header} from "./components/Header";
import Theme from "./Theme";
import {OptionsModal} from "./components/OptionsModal";

function App() {
  const [showOptions, setShowOptions] = useState(false);

  const toggleOptionsModal = () => setShowOptions(!showOptions);

  return (
    <Theme>
      <Header toggleOptions={toggleOptionsModal} />
      <Container className='main'>
        <OptionsModal isOpen={showOptions} toggle={toggleOptionsModal} />
        <Row>
          <Col className='order-lg-2'>
            <RemoteControl/>
            <br/>
            <Graph/>
            <br/>
            <Metrics/>
          </Col>
          <Col className='order-lg-1'>
            <Row>
              <HistoricalSensorData/>
            </Row>
          </Col>
        </Row>
      </Container>
      <Footer/>
    </Theme>
  );
}

export default App;
