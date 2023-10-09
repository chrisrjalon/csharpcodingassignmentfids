using FidsCodingAssignment.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FidsCodingAssignment.Controllers;

/// <summary>
/// Gates API controller.
/// </summary>
[Route("api/[controller]")]
public class GatesController : ApiController
{
    private readonly IGateService _gateService;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="GatesController"/> class.
    /// </summary>
    /// <param name="gateService"></param>
    public GatesController(IGateService gateService)
    {
        _gateService = gateService;
    }

    /// <summary>
    /// Gets the flight currently at a specific gate.
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