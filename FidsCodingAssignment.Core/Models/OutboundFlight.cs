using System.Text.Json.Serialization;
using FidsCodingAssignment.Common.Enumerations;

namespace FidsCodingAssignment.Core.Models;

public class OutboundFlight : Flight
{
    /// <summary>
    /// Flag indicating whether the flight is at the gate.
    /// </summary>
    public bool IsAtGate { get; set; }

    /// <summary>
    /// Flag indicating whether the flight is boarding.
    /// </summary>
    public bool IsBoarding => FlightStatus == FlightStatusType.Boarding;

    /// <summary>
    /// Flight destination.
    /// </summary>
    public string Destination { get; set; }
    
    /// <summary>
    /// Code of the gate the flight is assigned to.
    /// </summary>
    public string GateCode { get; set; }
}