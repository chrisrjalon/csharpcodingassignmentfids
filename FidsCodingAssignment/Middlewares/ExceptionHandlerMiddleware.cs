using System.Net;
using FidsCodingAssignment.Common.Helpers;
using FidsCodingAssignment.Common.Models.Results;

namespace FidsCodingAssignment.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
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