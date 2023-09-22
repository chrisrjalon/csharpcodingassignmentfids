using FidsCodingAssignment.Common.Enumerations;
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
            .FirstOrDefaultAsync(x => x.AirlineCode == airlineCode && x.FlightNumber == flightNumber);
    }

    public async Task<ICollection<FlightEntity>> GetActiveFlights()
    {
        return await Set
            .Where(x => x.ActualTime == null)
            .ToListAsync();
    }

    public async Task<ICollection<FlightEntity>> GetFlightsAssignedToGate(string gateCode)
    {
        return await Set
            .Where(x => 
                x.Bound == FlightBoundType.Outbound && 
                x.GateCode == gateCode)
            .ToListAsync();
    }
}