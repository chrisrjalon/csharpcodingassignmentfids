using FidsCodingAssignment.Common.Enumerations;
using FidsCodingAssignment.Common.Exceptions;
using FidsCodingAssignment.Data.Models;

namespace FidsCodingAssignment.Data.Extensions;

public static class FlightEntityExtensions
{
    public static void SetFlightActualTime(this FlightEntity flight, DateTime actualTime)
    {
        switch (flight.Bound)
        {
            case FlightBoundType.Inbound:
                flight.ActualArrival = actualTime;
                return;
            case FlightBoundType.Outbound:
                flight.ActualDeparture = actualTime;
                return;
            default:
                throw new FidsNotImplementedException(typeof(FlightBoundType), flight.Bound.ToString());
        }
        
        
    }
}