using RedBadgerMartianRobots;
using RedBadgerMartianRobots.Classes;
using RedBadgerMartianRobots.Classes.Data;
using RedBadgerMartianRobots.Models;

namespace Tests;

public class UnitTests : BaseTest
{
    [Fact]
    public void PlaceNewRobotPlacesRobotIfInBounds()
    {
        const Orientation orientation = Orientation.N;
        const int inBoundsX = 1;
        const int inBoundsY = 2;
        const int outOfBoundsX = 999;
        const int outOfBoundsY = 999;

        var model = new JourneyModel(new InputData { GridUpperCoords = TenTen });
        model.PlaceNewRobot(new Coords { X = inBoundsX, Y = inBoundsY }, orientation);

        var position = model.GetCurrentPosition();
        Assert.Equal(inBoundsX, position.Coords.X);
        Assert.Equal(inBoundsY, position.Coords.Y);
        Assert.Equal(orientation, position.Orientation);

        model.PlaceNewRobot(new Coords { X = outOfBoundsX, Y = outOfBoundsY }, orientation);
        // current position shouldn't change
        Assert.Equal(inBoundsX, position.Coords.X);
        Assert.Equal(inBoundsY, position.Coords.Y);
        Assert.Equal(orientation, position.Orientation);
    }

    [Fact]
    public void RobotIsFacingOutOfBoundsIsCorrect()
    {
        const Orientation orientation = Orientation.E;

        var facingInBoundsBot = new RobotData
        {
            StartPosition = new RobotPosition
            {
                Coords = new Coords { X = 3, Y = 4 },
                Orientation = orientation
            }
        };

        var facingOutOfBoundsBot = new RobotData
        {
            StartPosition = new RobotPosition
            {
                Coords = new Coords { X = 10, Y = 2 },
                Orientation = orientation
            }
        };

        var model = new JourneyModel(new InputData { GridUpperCoords = TenTen });
        model.PlaceNewRobot(facingInBoundsBot);
        Assert.False(model.RobotIsFacingOutOfBounds());

        model.PlaceNewRobot(facingOutOfBoundsBot);
        Assert.True(model.RobotIsFacingOutOfBounds());
    }

    [Fact]
    public void TurnRobotTurnsRobot()
    {
        var model = new JourneyModel(new InputData { GridUpperCoords = TenTen });
        
        model.PlaceNewRobot(BotFacing(Orientation.N));
        Assert.Throws<ArgumentOutOfRangeException>(() => model.TurnRobot(Instruction.F));
        model.TurnRobot(Instruction.R);
        Assert.Equal(Orientation.E, model.GetCurrentPosition().Orientation);
        model.TurnRobot(Instruction.L);
        Assert.Equal(Orientation.N, model.GetCurrentPosition().Orientation);
        
        model.PlaceNewRobot(BotFacing(Orientation.E));
        model.TurnRobot(Instruction.R);
        Assert.Equal(Orientation.S, model.GetCurrentPosition().Orientation);
        model.TurnRobot(Instruction.L);
        Assert.Equal(Orientation.E, model.GetCurrentPosition().Orientation);
        
                
        model.PlaceNewRobot(BotFacing(Orientation.S));
        model.TurnRobot(Instruction.R);
        Assert.Equal(Orientation.W, model.GetCurrentPosition().Orientation);
        model.TurnRobot(Instruction.L);
        Assert.Equal(Orientation.S, model.GetCurrentPosition().Orientation);
        
                
        model.PlaceNewRobot(BotFacing(Orientation.W));
        model.TurnRobot(Instruction.R);
        Assert.Equal(Orientation.N, model.GetCurrentPosition().Orientation);
        model.TurnRobot(Instruction.L);
        Assert.Equal(Orientation.W, model.GetCurrentPosition().Orientation);
    }
}