using System.Text.Json.Serialization;
using FidsCodingAssignment.Common.Enumerations;

namespace FidsCodingAssignment.Core.Models;

public class Flight
{
    /// <summary>
    /// Flight Id.
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Flight number.
    /// </summary>
    public int FlightNumber { get; set; }
    
    /// <summary>
    /// Code of the airline operating the flight.
    /// </summary>
    public string? AirlineCode { get; set; }
    
    /// <summary>
    /// Flag indicating whether this is a CodeShare flight.
    /// </summary>
    public bool IsCodeShare { get; set; }
    
    /// <summary>
    /// Parent flight Id.
    /// </summary>
    public int? ParentFlightId { get; set; }
    
    /// <summary>
    /// Inbound or outbound flight.
    /// </summary>
    public FlightBoundType Bound { get; set; }
    
    /// <summary>
    /// Scheduled arrival or departure time of the flight.
    /// </summary>
    public DateTime ScheduledTime { get; set; }
    
    /// <summary>
    /// Actual arrival or departure time of the flight.
    /// </summary>
    public DateTime? ActualTime { get; set; }

    /// <summary>
    /// Estimated boarding time for departing flights.
    /// </summary>
    public DateTime? ScheduledBoardingTime { get; set; }
    
    /// <summary>
    /// Actual boarding time for departing flights.
    /// </summary>
    public DateTime? ActualBoardingTime { get; set; }
    
    /// <summary>
    /// Flight origin airport Id.
    /// </summary>
    public int OriginAirportId { get; set; }

    /// <summary>
    /// Flight destination airport Id.
    /// </summary>
    public int DestinationAirportId { get; set; }
    
    /// <summary>
    /// Domestic or international flight.
    /// </summary>
    public FlightMovementType FlightType { get; set; }
    
    /// <summary>
    /// Current status of the flight.
    /// </summary>
    public FlightStatusType FlightStatus { get; set; }
    
    /// <summary>
    /// Id of the airline operating the flight.
    /// </summary>
    public int AirlineId { get; set; }
    
    /// <summary>
    /// Id of the gate the flight is assigned to.
    /// </summary>
    public int? GateId { get; set; }
    
    /// <summary>
    /// Navigation property for the airline.
    /// </summary>
    [JsonIgnore]
    public Airline? Airline { get; set; }
    
    /// <summary>
    /// Navigation property for the gate.
    /// </summary>
    [JsonIgnore]
    public Gate? Gate { get; set; }

    /// <summary>
    /// Navigation property for the origin airport.
    /// </summary>
    [JsonIgnore]
    public Airport? OriginAirport { get; set; }
    
    /// <summary>
    /// Navigation property for the destination airport.
    /// </summary>
    [JsonIgnore]
    public Airport? DestinationAirport { get; set; }
    
    /// <summary>
    /// Navigation property for the parent flight.
    /// </summary>
    [JsonIgnore]
    public Flight? ParentFlight { get; set; }
}