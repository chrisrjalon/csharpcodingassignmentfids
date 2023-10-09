using System.Net;
using FidsCodingAssignment.Common.Helpers;
using FidsCodingAssignment.Common.Models.Results;

namespace FidsCodingAssignment.Middlewares;

/// <summary>
/// Middleware to handle unhandled exceptions.
/// </summary>
public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Initialize a new instance of <see cref="ExceptionHandlerMiddleware"/>.
    /// </summary>
    /// <param name="next"></param>
    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    /// <summary>
    /// Invoke the middleware.
    /// </summary>
    /// <param name="context"></param>
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            LogHelper.LogException(e);
            var result = Error.Unexpected();
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(result);
        }
    }
}