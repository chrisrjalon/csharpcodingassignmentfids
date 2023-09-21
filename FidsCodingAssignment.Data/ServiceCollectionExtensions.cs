using FidsCodingAssignment.Data.Contexts;
using FidsCodingAssignment.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FidsCodingAssignment.Data;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IFlightRepository, FlightRepository>();
        serviceCollection.AddScoped<IFlightStatusRepository, FlightStatusRepository>();
        serviceCollection.AddScoped<IGateRepository, GateRepository>();
        serviceCollection.AddScoped<IGateStatusRepository, GateStatusRepository>();
        serviceCollection.AddScoped<IContext, FidsDbContext>();

        return serviceCollection;
    }
}