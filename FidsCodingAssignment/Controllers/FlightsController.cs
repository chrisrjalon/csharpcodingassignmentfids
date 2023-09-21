using FidsCodingAssignment.Common.Exceptions;
using FidsCodingAssignment.Common.Models;
using FidsCodingAssignment.Core.Models;
using FidsCodingAssignment.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FidsCodingAssignment.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FlightsController : ControllerBase
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
    public async Task<FidsApiResponse<FlightStatus>> GetFlightStatus(string airlineCode, int flightNumber)
    {
        try
        {
            var flightStatus = await _flightService.GetFlightStatus(airlineCode, flightNumber);
            return FidsApiResponse<FlightStatus>.Success(flightStatus);
        }
        catch (FidsException ex)
        {
            return FidsApiResponse<FlightStatus>.Error(ex.Message, ex.Category);
        }
    }
    
    /// <summary>
    /// Gets a list of flights that are delayed by a given delta.
    /// </summary>
    /// <param name="delta">Number of minutes to use as the delay threshold.</param>
    /// <returns>Collection of flights that are delayed per the given delta.</returns>
    [HttpGet("delayed/{delta}")]
    public async Task<FidsApiResponse<ICollection<Flight>?>> GetDelayedFlights(long delta)
    {
        try
        {
            var delayedFlights = await _flightService.GetDelayedFlights(TimeSpan.FromMinutes(delta));
            return FidsApiResponse<ICollection<Flight>?>.Success(delayedFlights);
        }
        catch (FidsException ex)
        {
            return FidsApiResponse<ICollection<Flight>?>.Error(ex.Message, ex.Category);
        }
    }
    
    /// <summary>
    /// Records the actual departure and arrival times of a flight.
    /// </summary>
    /// <param name="airlineCode">Code of the airline operating the flight.</param>
    /// <param name="flightNumber">Flight number assigned to the flight.</param>
    /// <param name="actualTime">The actual departure or arrival time of the flight.</param>
    /// <returns></returns>
    [HttpPost("{airlineCode}/{flightNumber}/actual-time")]
    public async Task<EmptyFidsResponse> RecordFlightActualTime(string airlineCode, int flightNumber, [FromBody] DateTime actualTime)
    {
        try
        {
            await _flightService.RecordFlightActualTime(airlineCode, flightNumber, actualTime);
            return EmptyFidsResponse.Success();
        }
        catch (FidsException ex)
        {
            return EmptyFidsResponse.Error(ex.Message, ex.Category);
        }
    }
}