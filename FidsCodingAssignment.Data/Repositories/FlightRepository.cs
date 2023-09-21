using FidsCodingAssignment.Data.Contexts;
using FidsCodingAssignment.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FidsCodingAssignment.Data.Repositories;

public class FlightRepository : RepositoryBase<FlightEntity>, IFlightRepository
{
    public FlightRepository(IContext context) : base(context)
    {
    }
    
    public async Task<FlightEntity?> GetFlight(string airlineCode, int flightNumber)
    {
        return await Set
            .Include(x => x.Gate)
            .Include(x => x.Airline)
            .FirstOrDefaultAsync(x => x.Airline!.Code == airlineCode && x.FlightNumber == flightNumber);
    }
}