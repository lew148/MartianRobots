using Microsoft.AspNetCore.Mvc;
using RedBadgerMartianRobots.Classes.Data;
using RedBadgerMartianRobots.Models;

namespace RedBadgerMartianRobots.Controllers;

[ApiController]
[Route("[controller]")]
public class MartianRobotController : ControllerBase
{
    private readonly ILogger<MartianRobotController> _logger;

    public MartianRobotController(ILogger<MartianRobotController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public IActionResult GetResult([FromBody] InputData data)
    {
        if (data.Journeys.Count == 0) return BadRequest();
        _logger.Log(LogLevel.Information, "Received request");
        return Ok(new { grid = data.GridUpperCoords, results = new JourneyModel(data).PerformJourneys().ToList() });
    }
}