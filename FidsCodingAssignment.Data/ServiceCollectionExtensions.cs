using FidsCodingAssignment.Data.Repositories;
using FidsCodingAssignment.TestData;
using Microsoft.Extensions.DependencyInjection;

namespace FidsCodingAssignment.Data;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddData(this IServiceCollection services)
    {
        services.AddSingleton<TestDataService>();
        services.AddScoped<IFlightRepository, TestFlightRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}