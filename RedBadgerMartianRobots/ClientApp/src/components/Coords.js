import React from 'react';
import { Col, FormGroup, Label, Input } from 'reactstrap';

const Coords = ({ journeyId, gridAxis = false, xValue, onXChange, yValue, onYChange }) => {
    return <div className="d-flex justify-content-between align-items-center my-2">
        <Col>
            <FormGroup floating noMargin>
                <Input name="x" id={`x-${journeyId}`} placeholder="Enter X Axis" type="number" step="1" required value={xValue} onChange={onXChange} />
                <Label className="m-0 text-muted" for="x">{gridAxis ? 'Grid Upper X' : 'Start X'}</Label>
            </FormGroup>
        </Col>
        <div className="m-2" />
        <Col>
            <FormGroup floating noMargin>
                <Input name="y" id={`y-${journeyId}`} placeholder="Enter Y Axis" type="number" step="1" required value={yValue} onChange={onYChange} />
                <Label className="m-0 text-muted" for="y">{gridAxis ? 'Grid Upper Y' : 'Start Y'}</Label>
            </FormGroup>
        </Col>
    </div>
}

export default Coords;












