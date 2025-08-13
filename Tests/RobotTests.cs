using System.Diagnostics;
using RedBadgerMartianRobots.Classes.Data;

namespace Tests;

public class RobotTests : BaseTest
{
    [Fact]
    public void OutOfBoundsRobotIsLostAndIgnored()
    {
        var result = GetResults(Grid(TenTen, new List<RobotData>
        {
            OutOfBoundsBot,
            SampleBot1
        }));

        Assert.Equal(2, result.Count);
        Assert.True(result[0].Lost);

        var secondBot = result[1];
        Assert.False(secondBot.Lost);
        Assert.Equal("1 1 E", secondBot.GetResultString());
    }

    [Fact]
    public void SampleDataIsProcessedCorrectly()
    {
        var result = GetResults(Grid(SampleGrid, new List<RobotData>
        {
            SampleBot1,
            SampleBot2,
            SampleBot3
        }));

        Assert.NotEmpty(result);
        Assert.Equal(3, result.Count);

        var firstBot = result[0];
        Assert.False(firstBot.Lost);
        Assert.Equal("1 1 E", firstBot.GetResultString());

        var secondBot = result[1];
        Assert.True(secondBot.Lost);
        Assert.Equal("3 3 N LOST", secondBot.GetResultString());

        var thirdBot = result[2];
        Assert.False(thirdBot.Lost);
        Assert.Equal("2 3 S", thirdBot.GetResultString());
    }

    [Fact]
    public void BotsAccountForLostScents()
    {
        var results = GetResults(Grid(TenTen, new List<RobotData>
        {
            ExtraBot3,
            ExtraBot3
        }));
        
        Assert.Equal(2, results.Count);
        Assert.True(results[0].Lost);
        Assert.False(results[1].Lost);
        Assert.Equal("0 1 N", results[1].GetResultString());
    }

    [Fact]
    public void LoadTest()
    {
        var bigData = Enumerable.Repeat(new List<RobotData>
            {
                SampleBot1,
                SampleBot2,
                SampleBot3,
                OutOfBoundsBot,
                ExtraBot1,
                ExtraBot2,
                ExtraBot3,
            }, 1000000)
            .SelectMany(x => x)
            .ToList();

        var timer = Stopwatch.StartNew();
        GetResults(Grid(TenTen, bigData));
        timer.Stop();

        Assert.True(timer.ElapsedMilliseconds < 10000);
    }
}