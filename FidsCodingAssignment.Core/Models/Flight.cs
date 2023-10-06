using System.Text.Json.Serialization;
using FidsCodingAssignment.Common.Enumerations;
using DateTime = System.DateTime;

namespace FidsCodingAssignment.Core.Models;

public abstract class Flight
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
    [JsonConverter(typeof(JsonStringEnumConverter))]
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
    /// Domestic or international flight.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public FlightMovementType FlightType { get; set; }

    /// <summary>
    /// Current status of the flight.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public FlightStatusType FlightStatus { get; set; }

    /// <summary>
    /// Collection of code share flights.
    /// </summary>
    public ICollection<Flight>? CodeShareFlights { get; set; }
}