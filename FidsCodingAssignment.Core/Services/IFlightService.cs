using FidsCodingAssignment.Common.Enumerations;
using FidsCodingAssignment.Core.Models;

namespace FidsCodingAssignment.Core.Services;

public interface IFlightService : IService
{
    /// <summary>
    /// Get a flight by airline code and flight number.
    /// </summary>
    Task<Flight> GetFlight(string airlineCode, int flightNumber);
    
    /// <summary>
    /// Get the current status of a flight.
    /// </summary>
    Task<FlightStatusType> GetFlightStatus(int flightId);
    
    /// <summary>
    /// Get the status history of a flight.
    /// </summary>
    Task<ICollection<FlightStatus>?> GetFlightStatusHistory(int flightId);
}