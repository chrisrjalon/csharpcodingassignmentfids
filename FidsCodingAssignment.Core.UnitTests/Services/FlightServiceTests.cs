using Autofac.Extras.Moq;
using FidsCodingAssignment.Common.Enumerations;
using FidsCodingAssignment.Common.Exceptions;
using FidsCodingAssignment.Core.Services;
using FidsCodingAssignment.Data.Models;
using FidsCodingAssignment.Data.Repositories;
using Moq;
using Xunit;

namespace FidsCodingAssignment.Core.UnitTests.Services;

public class FlightServiceTests
{
    [Fact]
    public async Task GetFlight_NoCorrespondingFlight_ThrowsException()
    {
        using var mock = AutoMock.GetLoose();
        var mockFlightRepo = mock.Mock<IFlightRepository>();

        mockFlightRepo
            .Setup(x => x.GetFlight(It.IsAny<string>(), It.IsAny<int>()))
            .ReturnsAsync((FlightEntity?) null);
        
        var service = mock.Create<FlightService>();
        
        await Assert.ThrowsAsync<FidsNotFoundException>(() => service.GetFlight(It.IsAny<string>(), It.IsAny<int>()));
    }

    [Fact]
    public async Task GetFlight_FlightFound_ReturnsFlight()
    {
        using var mock = AutoMock.GetLoose();
        var mockFlightRepo = mock.Mock<IFlightRepository>();

        mockFlightRepo
            .Setup(x => x.GetFlight("SY", 505))
            .ReturnsAsync(SimpleFlightEntity);
        
        var service = mock.Create<FlightService>();
        
        var flight = await service.GetFlight("SY", 505);
        
        Assert.Equal(541406104, flight.Id);
    }

    private FlightEntity SimpleFlightEntity =>
        new()
        {
            Id = 541406104,
            FlightNumber = 505,
            AirlineId = 1001,
            Airline = new AirlineEntity
            {
                Id = 1001,
                Code = "SY",
                Name = "SUN COUNTRY"
            },
            FlightStatus = FlightStatusType.Departed,
            Bound = FlightBound.OutBound,
            FlightType = FlightMovementType.International,
            ScheduledDeparture = new DateTime(2023, 08, 08, 13, 00, 00),
            ActualDeparture = new DateTime(2023, 08, 08, 12, 49, 00),
        };
}