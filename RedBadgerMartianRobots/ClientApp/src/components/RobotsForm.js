import React, { useState } from 'react';
import { Button, Form } from 'reactstrap';
import Coords from './Coords';
import Journeys from './Journeys';
import { sampleGridX, sampleGridY, sampleJourneys } from '../sampleData';

const defaultJourney = {
    id: 1,
    x: undefined,
    y: undefined,
    orientation: undefined,
    instructions: '',
}

const RobotsForm = ({ setResult }) => {
    const [erroredRequest, setErroredRequest] = useState(false);

    const [gridX, setGridX] = useState(undefined);
    const [gridY, setGridY] = useState(undefined);
    const [journeys, setJourneys] = useState([{ ...defaultJourney }]);

    const onUseSampleDataClick = () => {
        setGridX(sampleGridX);
        setGridY(sampleGridY)
        setJourneys(sampleJourneys);
    }

    const onSubmit = async (e) => {
        e.preventDefault();
        if (erroredRequest) setErroredRequest(false);
        await sendRobotData({
            gridUpperCoords: {
                x: gridX,
                y: gridY,
            },
            journeys: journeys.map((j) => ({
                startPosition: {
                    coords: {
                        x: j.x,
                        y: j.y,
                    },
                    orientation: j.orientation
                },
                instructions: j.instructions.split(''),
            }))
        });
    }

    const sendRobotData = async (data) => {
        try {
            const response = await fetch('/martianrobot', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(data)
            });

            if (!response.ok) {
                throw new Error(`Server error: ${response.status}`);
            }

            const result = await response.json();
            setResult(result);
        } catch (error) {
            console.error('Error posting to server:', error);
            setErroredRequest(true);
        }
    }

    const onAddClick = () => {
        const newJourney = { ...defaultJourney };
        newJourney.id = journeys.length + 1;
        setJourneys(prevJourneys => [...prevJourneys, newJourney])
    }

    const onDeleteClick = () => {
        journeys.splice(journeys.length - 1, 1); // remove last
        setJourneys(prevJourneys => [...journeys])
    }

    const onJourneyAxisChange = (i, value, isX) => {
        const newJourneys = [...journeys];
        if (isX) newJourneys[i].x = Number(value);
        else newJourneys[i].y = Number(value);
        setJourneys(newJourneys);
    }


    const onJourneyOrientationChange = (i, value) => {
        if (value !== 'N' && value !== 'E' && value !== 'S' && value !== 'W') return;
        const newJourneys = [...journeys];
        newJourneys[i].orientation = value;
        setJourneys(newJourneys);
    }

    const onInstructionsControlClick = (i, value) => {
        const newJourneys = [...journeys];
        const journey = { ...newJourneys[i] };

        if (value === '<') {
            journey.instructions = journey.instructions.substring(0, journey.instructions.length - 1);
        } else {
            journey.instructions += value;
        }

        newJourneys[i] = journey;
        setJourneys(newJourneys);
    }

    return <Form onSubmit={onSubmit}>
        <div className="d-flex justify-content-between">
            <h3 className="mb-4">Martian Robots</h3>
            <div className="h-50">
                <Button color="light" className="me-2" onClick={onUseSampleDataClick}>Load Sample Data</Button>
                <Button color="primary">Start</Button>
            </div>
        </div>
        {
            erroredRequest && <div className="p-3 border rounded bg-danger text-light d-flex justify-content-between align-items-center">
                <h6 className="m-0">An unexpected error has occured. Please try again...</h6>
                <Button className="m-0 p-0 text-light" color="" onClick={() => setErroredRequest(false)}>OK</Button>
            </div>
        }
        <Coords
            gridAxis
            journeyId={0}
            xValue={gridX}
            onXChange={(e) => setGridX(e.target.value)}
            yValue={gridY}
            onYChange={(e) => setGridY(e.target.value)}
        />
        <Journeys
            journeys={journeys}
            onAddJourneyClick={onAddClick}
            onDeleteJourneyClick={onDeleteClick}
            onAxisChange={onJourneyAxisChange}
            onOrientationChange={onJourneyOrientationChange}
            onInstructionsControlClick={onInstructionsControlClick}
        />
    </Form>;
}

export default RobotsForm;