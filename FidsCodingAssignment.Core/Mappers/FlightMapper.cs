using FidsCodingAssignment.Common.Enumerations;
using FidsCodingAssignment.Core.Models;
using FidsCodingAssignment.Data.Models;

namespace FidsCodingAssignment.Core.Mappers;

public static class FlightMapper
{
    public static Flight Map(this FlightEntity flight)
    {
        return flight.Bound switch
        {
            FlightBoundType.Outbound => flight.MapOutbound(),
            FlightBoundType.Inbound => flight.MapInbound(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private static OutboundFlight MapOutbound(this FlightEntity flight)
    {
        return new OutboundFlight
        {
            Id = flight.Id,
            FlightNumber = flight.FlightNumber,
            IsCodeShare = flight.IsCodeShare,
            ParentFlightId = flight.ParentFlightId,
            ParentFlight = flight.ParentFlight?.Map(),
            Bound = flight.Bound,
            ScheduledTime = flight.ScheduledTime,
            ActualTime = flight.ActualTime,
            FlightType = flight.FlightType,
            FlightStatus = flight.FlightStatus,
            Airline = flight.Airline?.Map(),
            Gate = flight.Gate?.Map(),
            ScheduledBoardingTime = flight.ScheduledBoardingTime,
            ActualBoardingTime = flight.ActualBoardingTime,
            Destination = flight.DestinationAirport?.MapLocation(),
        };
    }

    private static InboundFlight MapInbound(this FlightEntity flight)
    {
        return new InboundFlight()
        {
            Id = flight.Id,
            FlightNumber = flight.FlightNumber,
            IsCodeShare = flight.IsCodeShare,
            ParentFlightId = flight.ParentFlightId,
            ParentFlight = flight.ParentFlight?.Map(),
            Bound = flight.Bound,
            ScheduledTime = flight.ScheduledTime,
            ActualTime = flight.ActualTime,
            FlightType = flight.FlightType,
            FlightStatus = flight.FlightStatus,
            Airline = flight.Airline?.Map(),
            Origin = flight.OriginAirport?.MapLocation(),
        };
    }
    
    
}