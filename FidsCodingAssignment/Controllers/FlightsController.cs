using FidsCodingAssignment.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FidsCodingAssignment.Controllers;

[Route("api/[controller]")]
public class FlightsController : ApiController
{
    private readonly IFlightService _flightService;
    
    public FlightsController(IFlightService flightService)
    {
        _flightService = flightService;
    }
    
    /// <summary>
    /// Gets the current status of a flight.
    /// </summary>
    /// <param name="airlineCode">Code of the airline operating the flight.</param>
    /// <param name="flightNumber">Flight number assigned to the flight.</param>
    /// <returns>A flight status.</returns>
    [HttpGet("{airlineCode}/{flightNumber}/status")]
    public async Task<IActionResult> GetFlightStatus(string airlineCode, int flightNumber)
    {
        var result = await _flightService.GetFlightStatus(airlineCode, flightNumber);
        return result.Match(
            Ok,
            Problem);
    }
    
    /// <summary>
    /// Gets a list of flights that are delayed by a given delta.
    /// </summary>
    /// <param name="delta">Number of minutes to use as the delay threshold.</param>
    /// <returns>Collection of flights that are delayed per the given delta.</returns>
    [HttpGet("delayed/{delta}")]
    public async Task<IActionResult> GetDelayedFlights(long delta)
    {
        var result = await _flightService.GetDelayedFlights(TimeSpan.FromMinutes(delta));
        return result.Match(
            Ok,
            Problem);
    }
    
    /// <summary>
    /// Records the actual departure and arrival times of a flight.
    /// </summary>
    /// <param name="airlineCode">Code of the airline operating the flight.</param>
    /// <param name="flightNumber">Flight number assigned to the flight.</param>
    /// <param name="actualTime">The actual departure or arrival time of the flight.</param>
    /// <returns></returns>
    [HttpPut("{airlineCode}/{flightNumber}/actual-time")]
    public async Task<IActionResult> RecordFlightActualTime(string airlineCode, int flightNumber, [FromBody] DateTime actualTime)
    {
        var result = await _flightService.RecordFlightActualTime(airlineCode, flightNumber, actualTime);
        return result.Match(
            Ok,
            Problem);
    }
}