using RedBadgerMartianRobots.Classes;
using RedBadgerMartianRobots.Classes.Data;

namespace RedBadgerMartianRobots.Models;

public class InputDataModel
{
    private static InputData? GetInputDataFromArgs(List<string> args)
    {
        try
        {
            if (args.Count < 3 || (args.Count - 1) % 2 != 0) return null; // not enough data or invalid inputs

            var input = new InputData();
            var coordLine = args[0].Split(' ');
            args = args.Skip(1).ToList();
            input.GridUpperCoords = GetCoordsFromInputData(coordLine[0], coordLine[1]);
            input.Journeys = new List<RobotData>();

            while (args.Count != 0)
            {
                var cordsAndOrientation = args[0].Split(' ');
                var instructions = args[1];
                args = args.Skip(2).ToList();

                input.Journeys.Add(new RobotData
                {
                    StartPosition = new RobotPosition
                    {
                        Coords = GetCoordsFromInputData(cordsAndOrientation[0], cordsAndOrientation[1]),
                        Orientation = Enum.Parse<Orientation>(cordsAndOrientation[2]),
                    },
                    Instructions = instructions
                        .ToCharArray()
                        .Select(i => Enum.Parse<Instruction>(i.ToString()))
                        .ToList()
                });
            }

            return input;
        }
        catch
        {
            return null;
        }
    }

    private static Coords GetCoordsFromInputData(string x, string y) => new() { X = int.Parse(x), Y = int.Parse(y) };
}