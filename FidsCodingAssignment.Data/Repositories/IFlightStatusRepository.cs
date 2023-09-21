using FidsCodingAssignment.Data.Models;

namespace FidsCodingAssignment.Data.Repositories;

public interface IFlightStatusRepository : IRepository<FlightStatusEntity>
{
    Task<ICollection<FlightStatusEntity>?> GetFlightStatuses(int flightId);
}