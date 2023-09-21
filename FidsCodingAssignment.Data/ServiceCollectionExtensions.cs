using FidsCodingAssignment.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FidsCodingAssignment.Data;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IFlightRepository, FlightRepository>();
        serviceCollection.AddScoped<IFlightStatusRepository, FlightStatusRepository>();

        return serviceCollection;
    }
}