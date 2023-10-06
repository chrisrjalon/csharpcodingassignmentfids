using FidsCodingAssignment.Common.Enumerations;
using FidsCodingAssignment.Common.Models.Results;
using Microsoft.AspNetCore.Mvc;

namespace FidsCodingAssignment.Controllers;

[ApiController]
public abstract class ApiController : ControllerBase
{
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