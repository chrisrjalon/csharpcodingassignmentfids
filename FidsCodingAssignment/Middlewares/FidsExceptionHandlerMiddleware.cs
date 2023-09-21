using System.Net;
using FidsCodingAssignment.Common.Helpers;
using FidsCodingAssignment.Common.Models;

namespace FidsCodingAssignment.Middlewares;

public class FidsExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public FidsExceptionHandlerMiddleware(RequestDelegate next)
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
            var result = FidsApiResponse.Error("An unexpected error occurred. The developers have been notified.");
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(result);
        }
    }
}