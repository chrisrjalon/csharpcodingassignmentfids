using FidsCodingAssignment.Common.Enumerations;
using FidsCodingAssignment.Common.Models;
using FidsCodingAssignment.Data.Models;

namespace FidsCodingAssignment.Core.UnitTests.TestData;

public class FlightData
{
    public static FlightConfiguration FlightConfig =>
        new()
        {
            BoardingWindow = -90,
            BoardingDuration = 60,
            FlightAtGateWindow = 120
        };
    
    public static FlightEntity OutboundFlight =>
        new()
        {
            Id = 541406104,
            FlightNumber = 505,
            AirlineCode = "SY",
            Bound = FlightBoundType.Outbound,
            FlightType = FlightMovementType.International,
            ScheduledTime = new DateTime(2023, 08, 08, 13, 00, 00),
            GateCode = "E36"
        };
    
    public static FlightEntity OutboundFlight2 =>
        new()
        {
            Id = 541406105,
            FlightNumber = 506,
            AirlineCode = "SY",
            Bound = FlightBoundType.Outbound,
            FlightType = FlightMovementType.International,
            ScheduledTime = new DateTime(2023, 08, 08, 14, 00, 00),
            GateCode = "E37"
        };
    
    public static FlightEntity InboundFlight =>
        new()
        {
            Id = 541406100,
            FlightNumber = 500,
            AirlineCode = "SY",
            Bound = FlightBoundType.Inbound,
            FlightType = FlightMovementType.International,
            ScheduledTime = new DateTime(2023, 08, 08, 13, 00, 00)
        };
    
    public static FlightEntity InboundFlight2 =>
        new()
        {
            Id = 541406101,
            FlightNumber = 501,
            AirlineCode = "SY",
            Bound = FlightBoundType.Inbound,
            FlightType = FlightMovementType.International,
            ScheduledTime = new DateTime(2023, 08, 08, 14, 00, 00),
        };
}