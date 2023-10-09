using System.Text.Json.Serialization;
using FidsCodingAssignment.Common.Enumerations;
using FidsCodingAssignment.Common.Models;
using FidsCodingAssignment.Core.Helpers;
using FidsCodingAssignment.Data.Models;
using DateTime = System.DateTime;

namespace FidsCodingAssignment.Core.Models;

public abstract class Flight
{
    /// <summary>
    /// Flight Id.
    /// </summary>
    public int FlightId { get; set; }
    
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
    public bool IsCodeShare => CodeShareFlights?.Any() ?? false || ParentFlightId.HasValue;
    
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
    
    protected virtual void SetStatus(FlightConfiguration flightConfiguration, DateTime? referenceTime = null)
    {
        FlightStatus = FlightHelper.GetFlightStatus(this, flightConfiguration, referenceTime);
    }
    
    public bool IsFlightDelayed(TimeSpan delta, DateTime? referenceTime = null)
    {
        referenceTime ??= DateTime.UtcNow;

        return ScheduledTime < referenceTime.Value.Add(delta);
    }
    
    public virtual void Map(FlightEntity flightEntity)
    {
        FlightId = flightEntity.Id;
        AirlineCode = flightEntity.AirlineCode;
        FlightNumber = flightEntity.FlightNumber;
        ParentFlightId = flightEntity.ParentFlightId;
        Bound = flightEntity.Bound;
        ScheduledTime = flightEntity.ScheduledTime;
        ActualTime = flightEntity.ActualTime;
        FlightType = flightEntity.FlightType;
        CodeShareFlights = flightEntity.CodeShareFlights?.Select(Create).ToList();
    }

    private static readonly Dictionary<FlightBoundType, Func<Flight>> FlightLookup = new()
    {
        {FlightBoundType.Outbound, () => new OutboundFlight()},
        {FlightBoundType.Inbound, () => new InboundFlight()}
    };
    
    public static Flight Create(FlightEntity flightEntity)
    {
        var flight = FlightLookup[flightEntity.Bound]();
        flight.Map(flightEntity);
        
        return flight;
    }
    
    public static Flight CreateWithStatus(FlightEntity flightEntity, FlightConfiguration flightConfiguration, DateTime? referenceTime = null)
    {
        var flight = Create(flightEntity);
        flight.SetStatus(flightConfiguration, referenceTime);
        
        return flight;
    }
}