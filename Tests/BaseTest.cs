using RedBadgerMartianRobots;
using RedBadgerMartianRobots.Classes;
using RedBadgerMartianRobots.Classes.Data;
using RedBadgerMartianRobots.Models;

namespace Tests;

public abstract class BaseTest
{
    protected static List<RobotResult> GetResults(InputData data) => new JourneyModel(data).PerformJourneys().ToList();

    protected static readonly Coords SampleGrid = new() { X = 5, Y = 3 };
    protected static readonly Coords TenTen = new() { X = 10, Y = 10 };
    protected static readonly Coords ZeroZero = new() { X = 0, Y = 0 };

    protected static InputData Grid(Coords upperCoords, RobotData journey) =>
        Grid(upperCoords, new List<RobotData> { journey });

    protected static InputData Grid(Coords upperCoords, List<RobotData> journeys) => new()
    {
        GridUpperCoords = upperCoords,
        Journeys = journeys.ToList()
    };

    protected static RobotData BotFacing(Orientation orientation) => new()
    {
        StartPosition = new RobotPosition
        {
            Orientation = orientation,
            Coords = ZeroZero
        }
    };

    protected static RobotData SampleBot1 => new()
    {
        StartPosition = new RobotPosition
        {
            Coords = new Coords { X = 1, Y = 1 },
            Orientation = Orientation.E
        },
        Instructions =
            new List<Instruction>
            {
                Instruction.R,
                Instruction.F,
                Instruction.R,
                Instruction.F,
                Instruction.R,
                Instruction.F,
                Instruction.R,
                Instruction.F
            }
    };

    protected static RobotData SampleBot2 => new()
    {
        StartPosition = new RobotPosition
        {
            Coords = new Coords { X = 3, Y = 2 },
            Orientation = Orientation.N
        },
        Instructions =
            new List<Instruction>
            {
                Instruction.F,
                Instruction.R,
                Instruction.R,
                Instruction.F,
                Instruction.L,
                Instruction.L,
                Instruction.F,
                Instruction.F,
                Instruction.R,
                Instruction.R,
                Instruction.F,
                Instruction.L,
                Instruction.L
            }
    };

    protected static RobotData SampleBot3 => new()
    {
        StartPosition = new RobotPosition
        {
            Coords = new Coords { X = 0, Y = 3 },
            Orientation = Orientation.W
        },
        Instructions =
            new List<Instruction>
            {
                Instruction.L,
                Instruction.L,
                Instruction.F,
                Instruction.F,
                Instruction.F,
                Instruction.L,
                Instruction.F,
                Instruction.L,
                Instruction.F,
                Instruction.L
            }
    };

    protected static RobotData ExtraBot1 => new()
    {
        StartPosition = new RobotPosition
        {
            Coords = new Coords { X = 5, Y = 3 },
            Orientation = Orientation.S
        },
        Instructions =
            new List<Instruction>
            {
                Instruction.R,
                Instruction.R,
                Instruction.F,
                Instruction.F,
                Instruction.F,
                Instruction.L,
                Instruction.F
            }
    };

    protected static RobotData ExtraBot2 => new()
    {
        StartPosition = new RobotPosition
        {
            Coords = new Coords { X = 0, Y = 0 },
            Orientation = Orientation.E
        },
        Instructions =
            new List<Instruction>
            {
                Instruction.L,
                Instruction.F,
                Instruction.R,
                Instruction.F,
                Instruction.L,
                Instruction.F,
                Instruction.R,
                Instruction.F
            }
    };

    protected static RobotData ExtraBot3 => new()
    {
        StartPosition = new RobotPosition
        {
            Coords = new Coords { X = 0, Y = 1 },
            Orientation = Orientation.S
        },
        Instructions =
            new List<Instruction>
            {
                Instruction.F,
                Instruction.R,
                Instruction.F,
                Instruction.R,
                Instruction.F
            }
    };

    protected static RobotData OutOfBoundsBot => new()
    {
        StartPosition = new RobotPosition
        {
            Coords = new Coords { X = 99999, Y = 99999 },
            Orientation = Orientation.N
        }
    };
}