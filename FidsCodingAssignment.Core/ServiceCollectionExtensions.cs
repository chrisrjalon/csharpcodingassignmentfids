using FidsCodingAssignment.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FidsCodingAssignment.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCoreServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IFlightService, FlightService>();
        
        return serviceCollection;
    }
}