using FidsCodingAssignment.Data.Contexts;
using FidsCodingAssignment.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FidsCodingAssignment.Data.Repositories;

public class FlightStatusRepository : RepositoryBase<FlightStatusEntity>, IFlightStatusRepository
{
    public FlightStatusRepository(IContext context) : base(context)
    {
    }
    
    public async Task<ICollection<FlightStatusEntity>?> GetFlightStatuses(int flightId)
    {
        return await Set
            .Include(x => x.Flight)
            .Where(x => x.FlightId == flightId)
            .ToListAsync();
    }

    public async Task<FlightStatusEntity?> GetCurrentFlightStatus(int flightId)
    {
        return await Set
            .Include(x => x.Flight)
            .FirstOrDefaultAsync(x => x.FlightId == flightId && x.IsActive);
    }
}