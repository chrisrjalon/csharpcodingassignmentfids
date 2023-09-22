using FidsCodingAssignment.Common.Models;
using FidsCodingAssignment.Core.Helpers;
using FidsCodingAssignment.Core.Models;

namespace FidsCodingAssignment.Core.Extensions;

public static class FlightExtensions
{
    public static void SetStatuses(this Flight flight,
        FlightConfiguration flightConfiguration,
        DateTime? timeReference = null)
    {
        flight.FlightStatus = FlightHelper.GetFlightStatus(flight, flightConfiguration, timeReference);

        if (flight is OutboundFlight outboundFlight)
            outboundFlight.IsAtGate = FlightHelper.IsFlightAtGate(flight.ScheduledTime, flightConfiguration, timeReference);
    }
}