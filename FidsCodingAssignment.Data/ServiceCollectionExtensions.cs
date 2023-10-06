using FidsCodingAssignment.Data.Contexts;
using FidsCodingAssignment.Data.Repositories;
using FidsCodingAssignment.TestData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FidsCodingAssignment.Data;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddData(this IServiceCollection services)
    {
        services.AddDbContext<FidsDbContext>(options =>
        {
            options.UseInMemoryDatabase("FidsDb");
        });

        services.AddSingleton<TestDataService>();
        services.AddScoped<IFlightRepository, FlightRepository>();
        services.AddScoped<IContext, FidsDbContext>();

        return services;
    }
}