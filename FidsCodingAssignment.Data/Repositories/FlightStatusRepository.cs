using FidsCodingAssignment.Data.Contexts;
using FidsCodingAssignment.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FidsCodingAssignment.Data.Repositories;

public class FlightStatusRepository : RepositoryBase<FlightStatusEntity>, IFlightStatusRepository
{
    private readonly IQueryable<FlightStatusEntity> _flightStatuses;
    
    public FlightStatusRepository(IContext context) : base(context)
    {
        _flightStatuses = context.Set<FlightStatusEntity>();
    }

    public async Task<ICollection<FlightStatusEntity>?> GetFlightStatuses(int flightId)
    {
        return await _flightStatuses
            .Include(x => x.Flight)
            .Where(x => x.FlightId == flightId)
            .ToListAsync();
    }
}