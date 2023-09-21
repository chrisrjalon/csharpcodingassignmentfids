using System.Text.Json.Serialization;

namespace FidsCodingAssignment.Core.Models;

public class OutboundFlight : Flight
{
    /// <summary>
    /// Estimated boarding time for departing flights.
    /// </summary>
    public DateTime? ScheduledBoardingTime { get; set; }
    
    /// <summary>
    /// Actual boarding time for departing flights.
    /// </summary>
    public DateTime? ActualBoardingTime { get; set; }

    /// <summary>
    /// Flight destination details.
    /// </summary>
    public Location? Destination { get; set; }
    
    /// <summary>
    /// Code of the gate the flight is assigned to.
    /// </summary>
    public string? GateCode => Gate?.Code;
    
    /// <summary>
    /// Navigation property for the gate.
    /// </summary>
    [JsonIgnore]
    public Gate? Gate { get; set; }
}