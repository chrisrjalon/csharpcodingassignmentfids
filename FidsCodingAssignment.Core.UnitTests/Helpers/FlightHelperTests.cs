using FidsCodingAssignment.Core.Helpers;
using FidsCodingAssignment.Core.Mappers;
using FidsCodingAssignment.Core.UnitTests.TestData;
using Xunit;

namespace FidsCodingAssignment.Core.UnitTests.Helpers;

public class FlightHelperTests
{
    [Fact]
    public void GetFlightStatus_FlightDeparted_ReturnsDeparted()
    {
        var flight = FlightData.InboundFlight;
        
        var result = FlightHelper.GetFlightStatus(flight.Map(), FlightData.FlightConfig);
    }
}