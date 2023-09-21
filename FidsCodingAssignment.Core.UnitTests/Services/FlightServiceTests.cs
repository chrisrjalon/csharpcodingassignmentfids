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
    public async Task GetFlight_BlankAirlineCode_ThrowsException()
    {
        using var mock = AutoMock.GetLoose();
        var mockFlightRepo = mock.Mock<IFlightRepository>();

        mockFlightRepo
            .Setup(x => x.GetFlight(string.Empty, It.IsAny<int>()))
            .ReturnsAsync((FlightEntity?) null);
        
        var service = mock.Create<FlightService>();
        
        await Assert.ThrowsAsync<FidsNotFoundException>(() => service.GetFlight(string.Empty, It.IsAny<int>()));
    }
}