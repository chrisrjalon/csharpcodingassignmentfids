using FidsCodingAssignment.Common.Enumerations;
using FidsCodingAssignment.Common.Models.Results;
using Microsoft.AspNetCore.Mvc;

namespace FidsCodingAssignment.Controllers;

/// <summary>
/// Base API controller.
/// </summary>
[ApiController]
public abstract class ApiController : ControllerBase
{
    /// <summary>
    /// Convert errors to a ProblemDetails response.
    /// </summary>
    /// <param name="errors"></param>
    /// <returns></returns>
    protected IActionResult Problem(IEnumerable<Error> errors)
    {
        HttpContext.Items["errors"] = errors;
        
        var firstError = errors.First();
        
        var statusCode = firstError.Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };
        
        return Problem(statusCode: statusCode, title: firstError.Code, detail: firstError.Description, type: firstError.Category.ToString());
    }
}