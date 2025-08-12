namespace RedBadgerMartianRobots.Classes;

public class RobotResult
{
    public RobotPosition EndPosition { get; set; }
    public bool Lost { get; set; }

    public string GetResultString() =>
        $"{EndPosition.Coords.X} {EndPosition.Coords.Y} {EndPosition.Orientation}{(Lost ? " LOST" : string.Empty)}";
}