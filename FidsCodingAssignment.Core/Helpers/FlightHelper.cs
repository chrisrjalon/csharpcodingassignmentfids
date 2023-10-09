﻿using FidsCodingAssignment.Common.Enumerations;
using FidsCodingAssignment.Common.Models;
using FidsCodingAssignment.Core.Models;

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
        
        var boardingTime = flight.ScheduledTime.AddMinutes(flightConfiguration.BoardingWindow);
        var closedTime = boardingTime.AddMinutes(flightConfiguration.BoardingDuration);
        
        if (referenceTime.Value > boardingTime && referenceTime.Value < closedTime)
            return FlightStatusType.Boarding;
        
        if (referenceTime.Value > closedTime && referenceTime.Value < flight.ScheduledTime)
            return FlightStatusType.Closed;
        
        return FlightStatusType.OnTime;
    }
}