import React, { useState } from 'react';
import RobotsForm from './components/RobotsForm';
import { Container, Col, Button } from 'reactstrap';

const App = () => {
    const [result, setResult] = useState(undefined);

    const getOrientationString = (orientation) => {
        if (orientation === 'N') return 'North';
        if (orientation === 'E') return 'East';
        if (orientation === 'S') return 'South';
        if (orientation === 'W') return 'West';
        return '???';
    }

    return <Container className="p-5">
        <Col className="p-5 border rounded shadow" md={{ size: 8, offset: 2 }}>
            {result == null
                ? <RobotsForm setResult={setResult} />
                : <>
                    <div className="d-flex justify-content-between align-items-center">
                        <h2>Robot Journey Results</h2>
                        <Button onClick={() => setResult(undefined)}>Reset</Button>
                    </div>
                    <span>With a <strong>{`${result.grid.x} x ${result.grid.y}`}</strong> grid</span>
                    <div className="mt-4 d-flex justify-content-between align-items-center">
                        <div className="fw-bold">Robot #</div>
                        <div className="fw-bold">Coordinates</div>
                        <div className="fw-bold">Orientation</div>
                        <div className="fw-bold">Conclusion</div>
                    </div>
                    {result.results.map((r, i) => <div key={i} className="border rounded py-2 px-4 my-2 bg-light d-flex justify-content-between align-items-center">
                        <div><strong>{i+1}</strong></div>
                        <div>{"("}{r.endPosition.coords.x}, {r.endPosition.coords.y}{")"}</div>
                        <div>{getOrientationString(r.endPosition.orientation)}</div>
                        <div className={`${r.lost ? "text-danger" : "text-success"} fw-bold`}>{r.lost ? "LOST" : "SAFE"}</div>
                    </div>)}
                </>}
        </Col>
    </Container>
}

export default App;
