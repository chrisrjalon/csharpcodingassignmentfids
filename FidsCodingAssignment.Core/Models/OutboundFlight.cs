using FidsCodingAssignment.Common.Enumerations;
using FidsCodingAssignment.Common.Models;
using FidsCodingAssignment.Data.Models;

namespace FidsCodingAssignment.Core.Models;

public class OutboundFlight : Flight
{
    /// <summary>
    /// Flag indicating whether the flight is at the gate.
    /// </summary>
    public bool IsAtGate { get; private set; }
    
    /// <summary>
    /// Flag indicating whether the flight is boarding.
    /// </summary>
    public bool IsBoarding => FlightStatus == FlightStatusType.Boarding;

    /// <summary>
    /// Flight destination.
    /// </summary>
    public string? Destination { get; set; }
    
    /// <summary>
    /// Code of the gate the flight is assigned to.
    /// </summary>
    public string? GateCode { get; set; }

    private bool IsFlightAtGate(FlightConfiguration flightConfiguration, DateTime? referenceTime = null)
    {
        base.SetStatus(flightConfiguration, referenceTime);

        referenceTime ??= DateTime.UtcNow;
        
        var flightAtGateEarliestTime = ScheduledTime.AddMinutes(-flightConfiguration.FlightAtGateWindow);
        var flightAtGateLatestTime = flightAtGateEarliestTime.AddMinutes(flightConfiguration.FlightAtGateWindow);
        
        return referenceTime.Value > flightAtGateEarliestTime && referenceTime.Value < flightAtGateLatestTime;
    }

    protected override void SetStatus(FlightConfiguration flightConfiguration, DateTime? referenceTime = null)
    {
        base.SetStatus(flightConfiguration, referenceTime);
        IsAtGate = IsFlightAtGate(flightConfiguration, referenceTime);
    }

    public override void Map(FlightEntity flightEntity)
    {
        base.Map(flightEntity);
        Destination = flightEntity.City;
        GateCode = flightEntity.GateCode;
    }
}