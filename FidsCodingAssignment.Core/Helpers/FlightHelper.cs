using FidsCodingAssignment.Common.Enumerations;
using FidsCodingAssignment.Common.Models;
using FidsCodingAssignment.Core.Models;
using FidsCodingAssignment.Data.Models;

namespace FidsCodingAssignment.Core.Helpers;

public static class FlightHelper
{
    public static FlightStatusType GetFlightStatus(
        Flight flight, 
        FlightConfiguration flightConfiguration,
        DateTime? referenceTime = null)
    {
        referenceTime ??= DateTime.UtcNow;
        
        if (flight.ActualTime.HasValue)
        {
            return flight.Bound == FlightBoundType.Outbound
                ? FlightStatusType.Departed
                : FlightStatusType.Arrived;
        }
        
        if (referenceTime.Value > flight.ScheduledTime)
            return FlightStatusType.Delayed;
        
        if (flight.Bound == FlightBoundType.Inbound)
            return FlightStatusType.OnTime;
        
        var boardingTime = flight.ScheduledTime.AddMinutes(-flightConfiguration.BoardingWindow);
        var closedTime = boardingTime.AddMinutes(flightConfiguration.BoardingDuration);
        
        if (referenceTime.Value > boardingTime && referenceTime.Value < closedTime)
            return FlightStatusType.Boarding;
        
        if (referenceTime.Value > closedTime && referenceTime.Value < flight.ScheduledTime)
            return FlightStatusType.Closed;
        
        return FlightStatusType.OnTime;
    }
    
    public static bool IsFlightAtGate(DateTime scheduledTime, FlightConfiguration flightConfiguration, DateTime? referenceTime = null)
    {
        referenceTime ??= DateTime.UtcNow;
        
        var flightAtGateEarliestTime = scheduledTime.AddMinutes(-flightConfiguration.FlightAtGateWindow);
        var flightAtGateLatestTime = flightAtGateEarliestTime.AddMinutes(flightConfiguration.FlightAtGateWindow);
        
        return referenceTime.Value > flightAtGateEarliestTime && referenceTime.Value < flightAtGateLatestTime;
    }
}