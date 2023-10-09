using FidsCodingAssignment.Common.Models.Results;
using FidsCodingAssignment.Core.Models;

namespace FidsCodingAssignment.Core.Services;

public interface IFlightService : IService
{
    /// <summary>
    /// Get the current status of a flight.
    /// </summary>
    Task<Result<Flight>> GetFlight(string airlineCode, int flightNumber, DateTime? referenceTime = null);

    /// <summary>
    /// Get all flights that are delayed by a given delta.
    /// </summary>
    Task<Result<ICollection<Flight>?>> GetDelayedFlights(TimeSpan delta, DateTime? referenceTime = null);

    /// <summary>
    /// Save flight actual departure or arrival time.
    /// </summary>
    Task<Result> RecordFlightActualTime(string airlineCode, int flightNumber, DateTime actualTime);
}