using FidsCodingAssignment.Data.Contexts;
using FidsCodingAssignment.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FidsCodingAssignment.Data.Repositories;

public class FlightRepository : RepositoryBase<FlightEntity>, IFlightRepository
{
    private readonly IQueryable<FlightEntity> _flights;
    
    public FlightRepository(IContext context) : base(context)
    {
        _flights = context.Set<FlightEntity>();
    }
    
    public async Task<FlightEntity?> GetFlight(string airlineCode, int flightNumber)
    {
        return await _flights
            .Include(x => x.Gate)
            .Include(x => x.Airline)
            .FirstOrDefaultAsync(x => x.Airline!.Code == airlineCode && x.FlightNumber == flightNumber);
    }
}