using FidsCodingAssignment.Data.Models;

namespace FidsCodingAssignment.Data.Repositories;

public interface IFlightRepository : IRepository<FlightEntity>
{
    Task<FlightEntity?> GetFlight(string airlineCode, int flightNumber);
}