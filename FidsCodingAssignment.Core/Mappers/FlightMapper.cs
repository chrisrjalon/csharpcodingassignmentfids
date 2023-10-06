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

    public static OutboundFlight MapOutbound(this FlightEntity flight)
    {
        return new OutboundFlight
        {
            Id = flight.Id,
            AirlineCode = flight.AirlineCode,
            FlightNumber = flight.FlightNumber,
            IsCodeShare = flight.IsCodeShare,
            ParentFlightId = flight.ParentFlightId,
            Bound = flight.Bound,
            ScheduledTime = flight.ScheduledTime,
            ActualTime = flight.ActualTime,
            FlightType = flight.FlightType,
            Destination = flight.City,
            GateCode = flight.GateCode
        };
    }

    public static InboundFlight MapInbound(this FlightEntity flight)
    {
        return new InboundFlight()
        {
            Id = flight.Id,
            AirlineCode = flight.AirlineCode,
            FlightNumber = flight.FlightNumber,
            IsCodeShare = flight.IsCodeShare,
            ParentFlightId = flight.ParentFlightId,
            Bound = flight.Bound,
            ScheduledTime = flight.ScheduledTime,
            ActualTime = flight.ActualTime,
            FlightType = flight.FlightType,
            Origin = flight.City
        };
    }
    
    
}