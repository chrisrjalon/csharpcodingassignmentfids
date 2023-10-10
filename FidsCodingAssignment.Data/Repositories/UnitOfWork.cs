using Microsoft.Extensions.DependencyInjection;

namespace FidsCodingAssignment.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly IServiceProvider _serviceProvider;
    
    public IFlightRepository Flights => _serviceProvider.GetRequiredService<IFlightRepository>()!;

    public UnitOfWork(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
}