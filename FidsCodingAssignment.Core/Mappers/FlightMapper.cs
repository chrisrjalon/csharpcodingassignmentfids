using FidsCodingAssignment.Core.Models;
using FidsCodingAssignment.Data.Models;

namespace FidsCodingAssignment.Core.Mappers;

public static class FlightMapper
{
    public static Flight? Map(this FlightEntity? flight)
    {
        if (flight == null)
            return null;

        return new Flight
        {
            Id = flight.Id,
            FlightNumber = flight.FlightNumber,
            AirlineCode = flight.Airline?.Code,
            IsCodeShare = flight.IsCodeShare,
            ParentFlightId = flight.ParentFlightId,
            ParentFlight = flight.ParentFlight?.Map(),
            Bound = flight.Bound,
            ScheduledDeparture = flight.ScheduledDeparture,
            ActualDeparture = flight.ActualDeparture,
            ScheduledArrival = flight.ScheduledArrival,
            ActualArrival = flight.ActualArrival,
            ScheduledBoarding = flight.ScheduledBoarding,
            ActualBoarding = flight.ActualBoarding,
            FlightType = flight.FlightType,
            FlightStatus = flight.FlightStatus
        };
    }

    public static FlightEntity? Map(this Flight? flight)
    {
        if (flight == null)
            return null;

        return new FlightEntity
        {
            Id = flight.Id,
            FlightNumber = flight.FlightNumber,
            IsCodeShare = flight.IsCodeShare,
            ParentFlightId = flight.ParentFlightId,
            ParentFlight = flight.ParentFlight?.Map(),
            Bound = flight.Bound,
            ScheduledDeparture = flight.ScheduledDeparture,
            ActualDeparture = flight.ActualDeparture,
            ScheduledArrival = flight.ScheduledArrival,
            ActualArrival = flight.ActualArrival,
            ScheduledBoarding = flight.ScheduledBoarding,
            ActualBoarding = flight.ActualBoarding,
            FlightType = flight.FlightType,
            FlightStatus = flight.FlightStatus
        };
    }
}