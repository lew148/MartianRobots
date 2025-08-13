using RedBadgerMartianRobots.Classes;
using RedBadgerMartianRobots.Classes.Data;

namespace RedBadgerMartianRobots.Models;

public class JourneyModel
{
    private readonly Coords _gridLowerCoords;
    private readonly Coords _gridUpperCoords;
    private readonly List<RobotData> _journeys;

    private readonly List<RobotPosition> _lostScents = new();
    private Coords _currentCoords;
    private Orientation _currentOrientation;

    public JourneyModel(InputData input)
    {
        _gridLowerCoords = new Coords { X = 0, Y = 0 };
        _gridUpperCoords = input.GridUpperCoords;
        _journeys = input.Journeys;

        // defaults
        _currentCoords = new Coords { X = _gridLowerCoords.X, Y = _gridLowerCoords.Y };
        _currentOrientation = Orientation.N;
    }

    public RobotPosition GetCurrentPosition() => new()
    {
        Coords = new Coords { X = _currentCoords.X, Y = _currentCoords.Y },
        Orientation = _currentOrientation
    };

    public IEnumerable<RobotResult> PerformJourneys()
    {
        foreach (var journey in _journeys)
        {
            var wasLost = false;

            if (!PlaceNewRobot(journey.StartPosition.Coords, journey.StartPosition.Orientation))
            {
                // robot was lost on placement
                yield return new RobotResult
                {
                    Lost = true,
                    EndPosition = new RobotPosition
                    {
                        Coords = new Coords { X = journey.StartPosition.Coords.X, Y = journey.StartPosition.Coords.Y },
                        Orientation = journey.StartPosition.Orientation
                    }
                };

                continue;
            }

            foreach (var instruction in journey.Instructions)
            {
                if (PerformInstruction(instruction)) continue;
                wasLost = true;
                break;
            }

            yield return new RobotResult
            {
                Lost = wasLost,
                EndPosition = new RobotPosition
                {
                    Coords = new Coords { X = _currentCoords.X, Y = _currentCoords.Y },
                    Orientation = _currentOrientation
                }
            };
        }
    }

    private bool PerformInstruction(Instruction instruction)
    {
        switch (instruction)
        {
            case Instruction.F:
            {
                if (RobotIsInLostScentPosition())
                {
                    // ignore
                    break;
                }

                if (RobotIsFacingOutOfBounds())
                {
                    _lostScents.Add(new RobotPosition
                    {
                        Coords = _currentCoords,
                        Orientation = _currentOrientation
                    });
                    return false;
                }

                MoveRobotForward();
                break;
            }
            case Instruction.R:
            case Instruction.L:
                TurnRobot(instruction);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(instruction), instruction, null);
        }

        return true;
    }

    private void MoveRobotForward()
    {
        switch (_currentOrientation)
        {
            case Orientation.N:
                _currentCoords.Y++;
                break;
            case Orientation.E:
                _currentCoords.X++;
                break;
            case Orientation.S:
                _currentCoords.Y--;
                break;
            case Orientation.W:
                _currentCoords.X--;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void TurnRobot(Instruction instruction)
    {
        if (instruction != Instruction.R && instruction != Instruction.L)
        {
            throw new ArgumentOutOfRangeException(nameof(instruction), instruction, null);
        }

        _currentOrientation = _currentOrientation switch
        {
            Orientation.N => instruction == Instruction.R ? Orientation.E : Orientation.W,
            Orientation.E => instruction == Instruction.R ? Orientation.S : Orientation.N,
            Orientation.S => instruction == Instruction.R ? Orientation.W : Orientation.E,
            Orientation.W => instruction == Instruction.R ? Orientation.N : Orientation.S,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public bool PlaceNewRobot(RobotData robotData) =>
        PlaceNewRobot(robotData.StartPosition.Coords, robotData.StartPosition.Orientation);

    public bool PlaceNewRobot(Coords coords, Orientation orientation)
    {
        if (RobotIsOutOfBounds(coords)) return false;
        _currentCoords = coords;
        _currentOrientation = orientation;
        return true;
    }

    private bool RobotIsOutOfBounds(Coords coords) => coords.X > _gridUpperCoords.X || coords.Y > _gridUpperCoords.Y ||
                                                      coords.X < _gridLowerCoords.X || coords.Y < _gridLowerCoords.Y;

    public bool RobotIsFacingOutOfBounds() =>
        (_currentCoords.X == _gridUpperCoords.X && _currentOrientation == Orientation.E) ||
        (_currentCoords.Y == _gridUpperCoords.Y && _currentOrientation == Orientation.N) ||
        (_currentCoords.X == _gridLowerCoords.X && _currentOrientation == Orientation.W) ||
        (_currentCoords.Y == _gridLowerCoords.Y && _currentOrientation == Orientation.S);

    private bool RobotIsInLostScentPosition() => _lostScents.Any(ls =>
        ls.Coords.X == _currentCoords.X && ls.Coords.Y == _currentCoords.Y && ls.Orientation == _currentOrientation);
}