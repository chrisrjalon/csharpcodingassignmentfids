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
        var flightStatus = await _flightService.GetFlightStatus(flightId);

        return Ok(new FidsApiResponse<FlightStatus>(flightStatus));
    }
}