using FidsCodingAssignment.Common.Models.Results;
using FidsCodingAssignment.Core.Models;
using FidsCodingAssignment.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FidsCodingAssignment.Controllers;

[Route("api/[controller]")]
public class GatesController : ApiController
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
    public async Task<IActionResult> GetActiveFlight(string gateCode)
    {
        var result = await _gateService.GetActiveFlight(gateCode);
        return result.Match(
            Ok,
            Problem);
    }
}