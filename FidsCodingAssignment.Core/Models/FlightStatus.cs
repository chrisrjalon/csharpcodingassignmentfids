using System.Text.Json.Serialization;
using FidsCodingAssignment.Common.Enumerations;

namespace FidsCodingAssignment.Core.Models;

public class FlightStatus
{
    public int FlightId { get; set; }

    public string AirlineCode { get; set; } = null!;

    public int FlightNumber { get; set; }

    public DateTime ScheduledTime { get; set; }
    
    public DateTime? ActualTime { get; set; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public FlightBoundType Bound { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public FlightMovementType FlightType { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public FlightStatusType Status { get; set; }
}