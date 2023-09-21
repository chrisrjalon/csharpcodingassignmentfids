﻿using System.Text.Json.Serialization;
using FidsCodingAssignment.Common.Enumerations;

namespace FidsCodingAssignment.Core.Models;

public class FlightStatus
{
    /// <summary>
    /// Flight status Id.
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Flight Id for the flight status.
    /// </summary>
    public int FlightId { get; set; }
    
    /// <summary>
    /// Flight status.
    /// </summary>
    public FlightStatusType Status { get; set; }
    
    /// <summary>
    /// Date and time the flight status is effective from.
    /// </summary>
    public DateTime EffectiveFrom { get; set; }
    
    /// <summary>
    /// Date and time the flight status is effective to.
    /// </summary>
    public DateTime? EffectiveTo { get; set; }
    
    /// <summary>
    /// Remarks for the flight status.
    /// </summary>
    public string? Remarks { get; set; }
    
    /// <summary>
    /// Navigation property for the flight.
    /// </summary>
    [JsonIgnore]
    public Flight? Flight { get; set; }
}