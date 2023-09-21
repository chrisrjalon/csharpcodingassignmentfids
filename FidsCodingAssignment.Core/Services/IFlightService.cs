using FidsCodingAssignment.Common.Enumerations;
using FidsCodingAssignment.Core.Models;

namespace FidsCodingAssignment.Core.Services;

public interface IFlightService : IService
{
    /// <summary>
    /// Create or update a flight.
    /// </summary>
    Task SaveFlight(Flight flight);
    
    /// <summary>
    /// Get a flight by airline code and flight number.
    /// </summary>
    Task<Flight> GetFlight(string airlineCode, int flightNumber);
    
    /// <summary>
    /// Get the current status of a flight.
    /// </summary>
    Task<FlightStatus> GetFlightStatus(int flightId);

    /// <summary>
    /// Get all flights that are delayed by a given delta.
    /// </summary>
    /// <returns></returns>
    Task<ICollection<Flight>> GetDelayedFlights(TimeSpan delta, DateTime? reference = null);
    
    /// <summary>
    /// Get the status history of a flight.
    /// </summary>
    Task<ICollection<FlightStatus>?> GetFlightStatusHistory(int flightId);
    
    /// <summary>
    /// Save flight actual departure or arrival time.
    /// </summary>
    /// <param name="flightId"></param>
    /// <param name="actualTime"></param>
    /// <returns></returns>
    Task RecordFlightActualTime(int flightId, DateTime actualTime);
}