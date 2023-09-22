using FidsCodingAssignment.Common.Exceptions;
using FidsCodingAssignment.Common.Models;
using FidsCodingAssignment.Core.Models;
using FidsCodingAssignment.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FidsCodingAssignment.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GatesController : ControllerBase
{
    private readonly IGateService _gateService;
    
    public GatesController(IGateService gateService)
    {
        _gateService = gateService;
    }

    /// <summary>
    /// Gets the active flight currently assigned to the gate.
    /// </summary>
    /// <param name="gateCode">Unique code assigned to the gate.</param>
    /// <returns>Flight details.</returns>
    [HttpGet("{gateCode}/active-flight")]
    public async Task<FidsApiResponse<Flight?>> GetActiveFlight(string gateCode)
    {
        try
        {
            var flight = await _gateService.GetActiveFlight(gateCode);
            return FidsApiResponse<Flight?>.Success(flight);
        }
        catch (FidsException ex)
        {
            return FidsApiResponse<Flight?>.Error(ex.Message, ex.Category);
        }
    }
}