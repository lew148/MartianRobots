import React, { useState } from 'react';
import { Row, Col, Button, Form } from 'reactstrap';
import Coords from './Coords';
import Journeys from './Journeys';

const defaultJourney = {
    id: 1,
    x: undefined,
    y: undefined,
    orientation: undefined,
    instructions: '',
}

const RobotsForm = ({ setResult }) => {
    const [gridX, setGridX] = useState(undefined);
    const [gridY, setGridY] = useState(undefined);
    const [journeys, setJourneys] = useState([{ ...defaultJourney }]);

    const onSubmit = async (e) => {
        e.preventDefault();
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
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            });

            if (!response.ok) {
                throw new Error(`Server error: ${response.status}`);
            }

            const result = await response.json();
            console.log('Server response:', result);
            setResult(result);
        } catch (error) {
            console.error('Error posting to server:', error);
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
            <Button color="primary" className="h-50">Start</Button>
        </div>
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