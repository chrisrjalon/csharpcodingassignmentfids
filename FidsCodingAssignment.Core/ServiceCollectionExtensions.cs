using FidsCodingAssignment.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FidsCodingAssignment.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddScoped<IFlightService, FlightService>();
        services.AddScoped<IGateService, GateService>();
        
        return services;
    }
}