using System.Text.Json.Serialization;
using FidsCodingAssignment.Common.Enumerations;

namespace FidsCodingAssignment.Core.Models;

public class GateStatus
{
    /// <summary>
    /// Gate status Id.
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Gate Id for the gate status.
    /// </summary>
    public int GateId { get; set; }
    
    /// <summary>
    /// Flight Id of the flight assigned to the gate.
    /// </summary>
    public int FlightId { get; set; }
    
    /// <summary>
    /// Gate status.
    /// </summary>
    public GateStatusType Status { get; set; }
    
    /// <summary>
    /// Date and time the gate status is effective from.
    /// </summary>
    public DateTime EffectiveFrom { get; set; }
    
    /// <summary>
    /// Date and time the gate status is effective to.
    /// </summary>
    public DateTime? EffectiveTo { get; set; }
    
    /// <summary>
    /// Remarks for the gate status.
    /// </summary>
    public string? Remarks { get; set; }
    
    /// <summary>
    /// Navigation property for the flight assigned to the gate.
    /// </summary>
    [JsonIgnore]
    public Flight? Flight { get; set; }

    /// <summary>
    /// Navigation property for the gate.
    /// </summary>
    [JsonIgnore]
    public Gate? Gate { get; set; }
    
}