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
            AirlineCode = flight.Airline?.Code,
            IsCodeShare = flight.IsCodeShare,
            ParentFlightId = flight.ParentFlightId,
            ParentFlight = flight.ParentFlight?.Map(),
            Bound = flight.Bound,
            ScheduledTime = flight.ScheduledTime,
            ActualTime = flight.ActualTime,
            ScheduledBoardingTime = flight.ScheduledBoardingTime,
            ActualBoardingTime = flight.ActualBoardingTime,
            OriginAirportId = flight.OriginAirportId,
            OriginAirport = flight.OriginAirport?.Map(),
            DestinationAirportId = flight.DestinationAirportId,
            DestinationAirport = flight.DestinationAirport?.Map(),
            FlightType = flight.FlightType,
            FlightStatus = flight.FlightStatus,
            GateId = flight.GateId,
            Gate = flight.Gate?.Map(),
            AirlineId = flight.AirlineId,
            Airline = flight.Airline?.Map(),
        };
    }
}