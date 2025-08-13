using RedBadgerMartianRobots;
using RedBadgerMartianRobots.Models;

namespace Tests;

public class InputDataTests : BaseTest
{
    [Fact]
    public void GetInputDataFromArgsIsCorrect()
    {
        var args = new List<string>
        {
            "5 3",
            "1 1 E",
            "RFL",
            "3 2 N",
            "FRR",
            "0 3 W",
            "LLF"
        };

        var result = InputDataModel.GetInputDataFromArgs(args);
        Assert.NotNull(result);
        Assert.Equal(5, result!.GridUpperCoords.X);
        Assert.Equal(3, result.GridUpperCoords.Y);
        Assert.Equal(3, result.Journeys.Count);
        
        var firstJourney = result.Journeys[0];
        Assert.Equal(1, firstJourney.StartPosition.Coords.X);
        Assert.Equal(1, firstJourney.StartPosition.Coords.Y);
        Assert.Equal(Orientation.E, firstJourney.StartPosition.Orientation);

        var instructions = firstJourney.Instructions;
        Assert.Equal(3, instructions.Count);
        Assert.Equal(Instruction.R, instructions[0]);
        Assert.Equal(Instruction.F, instructions[1]);
        Assert.Equal(Instruction.L, instructions[2]);
        
        var secondJourney = result.Journeys[1];
        Assert.Equal(3, secondJourney.StartPosition.Coords.X);
        Assert.Equal(2, secondJourney.StartPosition.Coords.Y);
        Assert.Equal(Orientation.N, secondJourney.StartPosition.Orientation);
        
        var thirdJourney = result.Journeys[2];
        Assert.Equal(0, thirdJourney.StartPosition.Coords.X);
        Assert.Equal(3, thirdJourney.StartPosition.Coords.Y);
        Assert.Equal(Orientation.W, thirdJourney.StartPosition.Orientation);
    }

    [Fact]
    public void InvalidInputReturnsNull()
    {
        var args = new List<string>
        {
            "N/A",
            "N/A",
            "N/A",
        };
        
        Assert.Null(InputDataModel.GetInputDataFromArgs(args));
    }
}