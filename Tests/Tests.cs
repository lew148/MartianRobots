using RedBadgerMartianRobots;
using RedBadgerMartianRobots.Classes;
using RedBadgerMartianRobots.Classes.Data;
using RedBadgerMartianRobots.Models;

namespace Tests;

public class Tests
{
    [Fact]
    public void InputDataIsStoredCorrectly()
    {
        // test string input reading
    }

    [Fact]
    public void SampleDataIsProcessedCorrectly()
    {
        var testData = new InputData
        {
            GridUpperCoords = new Coords { X = 5, Y = 3 },
            Journeys =
                new List<RobotData>
                {
                    new()
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
                    },
                    new()
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
                    },
                    new()
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
                    }
                }
        };

        var result = new JourneyModel(testData).PerformJourneys().ToList();
        Assert.NotEmpty(result);
        Assert.Equal(3, result.Count);

        var firstResult = result[0];
        Assert.False(firstResult.Lost);
        Assert.Equal("1 1 E", firstResult.GetResultString());

        var secondResult = result[1];
        Assert.True(secondResult.Lost);
        Assert.Equal("3 3 N LOST", secondResult.GetResultString());

        var thirdResult = result[2];
        Assert.False(thirdResult.Lost);
        Assert.Equal("2 3 S", thirdResult.GetResultString());
    }
}