using FidsCodingAssignment.Core.Models;
using FidsCodingAssignment.Data.Models;

namespace FidsCodingAssignment.Core.Mappers;

public static class FlightMapper
{
    public static Flight Map(this FlightEntity flight)
    {
        return new Flight
        {
            Id = flight.Id,
            FlightNumber = flight.FlightNumber,
            IsCodeShare = flight.IsCodeShare,
            ParentFlightId = flight.ParentFlightId,
            ParentFlight = flight.ParentFlight?.Map(),
            Bound = flight.Bound,
            ScheduledTime = flight.ScheduledTime,
            ActualTime = flight.ActualTime,
            ScheduledBoardingTime = flight.ScheduledBoardingTime,
            ActualBoardingTime = flight.ActualBoardingTime,
            OriginAirport = flight.OriginAirport?.Map(),
            DestinationAirport = flight.DestinationAirport?.Map(),
            FlightType = flight.FlightType,
            FlightStatus = flight.FlightStatus,
            Gate = flight.Gate?.Map(),
            Airline = flight.Airline?.Map(),
        };
    }
}