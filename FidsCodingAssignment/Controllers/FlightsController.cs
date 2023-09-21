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
    
    [HttpGet("{flightId}/status")]
    public async Task<IActionResult> GetFlightStatus(int flightId)
    {
        try
        {
            var flightStatus = await _flightService.GetFlightStatus(flightId);
            return Ok(FidsApiResponse<FlightStatus>.Success(flightStatus));
        }
        catch (FidsException ex)
        {
            return BadRequest(FidsApiResponse<FlightStatus>.Error(ex.Message, ex.Category));
        }
    }
    
    [HttpGet("delayed/{delta}")]
    public async Task<IActionResult> GetDelayedFlights(long delta)
    {
        try
        {
            var delayedFlights = await _flightService.GetDelayedFlights(TimeSpan.FromMinutes(delta));
            return Ok(FidsApiResponse<ICollection<Flight>>.Success(delayedFlights));
        }
        catch (FidsException ex)
        {
            return BadRequest(FidsApiResponse<ICollection<Flight>>.Error(ex.Message, ex.Category));
        }
    }
}