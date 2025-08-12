import React from 'react';
import { FormGroup, Label, Input, Button } from 'reactstrap';
import Coords from './Coords';

const Journeys = ({ journeys, onDeleteClick, onAxisChange, onOrientationChange, onInstructionsControlClick }) => {
    return <div className="my-4 p-4 border rounded bg-light">
        <h5>Journeys</h5>
        <div>
            {journeys.map((j, i) => <div key={j.id}>
                <>
                    <div className="d-flex justify-content-between align-items-center">
                        <h3>{j.id}</h3>
                        <div className='w-100 ms-5'>
                            <Coords
                                journeyId={j.id}
                                xValue={j.x}
                                onXChange={(e) => onAxisChange(i, e.target.value, true)}
                                yValue={j.y}
                                onYChange={(e) => onAxisChange(i, e.target.value, false)}
                            />
                            <FormGroup floating>
                                <Input
                                    type="select"
                                    name="orientation"
                                    id={`orientation-${j.id}`}
                                    value={j.orientation}
                                    onChange={(e) => onOrientationChange(i, e.target.value)}
                                    required
                                >
                                    <option value="" className="text-muted"></option>
                                    <option value="N">North</option>
                                    <option value="E">East</option>
                                    <option value="S">South</option>
                                    <option value="W">West</option>
                                </Input>
                                <Label for="orientation">Start Orientation</Label>
                            </FormGroup>
                            <FormGroup floating>
                                <Input
                                    className="w-100"
                                    type="text"
                                    disabled
                                    name="instructions"
                                    id="instructions"
                                    placeholder="Input Instructions"
                                    required
                                    value={j.instructions}
                                />
                                <Label for="instructions">Instructions</Label>
                                <div>
                                    <Button className="m-2" title="Right" onClick={() => onInstructionsControlClick(i, 'L')}>{'< '}Left</Button>
                                    <Button className="m-2" title="Left" onClick={() => onInstructionsControlClick(i, 'R')}>Right{' >'}</Button>
                                    <Button className="m-2" title="Forward" onClick={() => onInstructionsControlClick(i, 'F')} color="primary">Forward</Button>
                                    <Button className="m-2" title="del" onClick={() => onInstructionsControlClick(i, '<')} color="dark">{'Del'}</Button>
                                </div>
                            </FormGroup>
                        </div>
                    </div>
                    {journeys.length > 1 ? <div className="border m-4" /> : <></>}
                </>
            </div>)}
            {journeys.length <= 1 ? <></> : <Button className="w-100" onClick={onDeleteClick} color="danger">Delete</Button>}
        </div>
    </div>;
}

export default Journeys;