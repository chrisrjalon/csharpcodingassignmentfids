﻿using FidsCodingAssignment.Common.Enumerations;
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
    Task<FlightStatus> GetFlightStatus(string airlineCode, int flightNumber);

    /// <summary>
    /// Get all flights that are delayed by a given delta.
    /// </summary>
    Task<ICollection<Flight>?> GetDelayedFlights(TimeSpan delta, DateTime? reference = null);
    
    /// <summary>
    /// Get the status history of a flight.
    /// </summary>
    Task<ICollection<FlightStatus>?> GetFlightStatusHistory(int flightId);

    /// <summary>
    /// Save flight actual departure or arrival time.
    /// </summary>
    Task RecordFlightActualTime(string airlineCode, int flightNumber, DateTime actualTime);
}