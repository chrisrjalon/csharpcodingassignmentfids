using Autofac.Extras.Moq;
using FidsCodingAssignment.Common.Enumerations;
using FidsCodingAssignment.Common.Exceptions;
using FidsCodingAssignment.Common.Models;
using FidsCodingAssignment.Core.Models;
using FidsCodingAssignment.Core.Services;
using FidsCodingAssignment.Core.UnitTests.TestData;
using FidsCodingAssignment.Data.Models;
using FidsCodingAssignment.Data.Repositories;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace FidsCodingAssignment.Core.UnitTests.Services;

public class FlightServiceTests : ServiceBaseTests
{
    [Fact]
    public async Task GetFlightStatus_FlightNotFound_ThrowsException()
    {
        using var mock = AutoMock.GetLoose();
        var mockFlightRepo = mock.Mock<IFlightRepository>();
        
        mockFlightRepo
            .Setup(x => x.Get(It.IsAny<int>()))
            .ReturnsAsync((FlightEntity?) null);
        
        var service = mock.Create<FlightService>();
        await Assert.ThrowsAsync<FidsNotFoundException>(() => service.GetFlightStatus(It.IsAny<string>(), It.IsAny<int>()));
    }
    
    [Fact]
    public async Task GetFlightStatus_FlightStatusBoarding_ReturnsFlightStatus()
    {
        using var mock = GetMock();
        var mockFlightRepo = mock.Mock<IFlightRepository>();

        mockFlightRepo
            .Setup(x => x.GetFlight(It.IsAny<string>(), It.IsAny<int>()))
            .ReturnsAsync(FlightData.OutboundFlight);
        
        var service = mock.Create<FlightService>();
        var result = await service.GetFlightStatus(It.IsAny<string>(), It.IsAny<int>());
        
        Assert.Equal(541406104, result.FlightId);
        Assert.Equal(FlightStatusType.Delayed, result.Status);
    }
    
    [Fact]
    public async Task GetDelayedFlights_NoActiveFlights_ReturnsEmptyList()
    {
        using var mock = GetMock();
        var mockFlightRepo = mock.Mock<IFlightRepository>();
        
        mockFlightRepo
            .Setup(x => x.GetActiveFlights())!
            .ReturnsAsync((ICollection<FlightEntity>?) null);
        
        var service = mock.Create<FlightService>();
        var result = await service.GetDelayedFlights(It.IsAny<TimeSpan>());
        
        Assert.Null(result);
    }
    
    [Fact]
    public async Task GetDelayedFlights_DelayedOutboundFlights_ReturnDelayedFlights()
    {
        using var mock = GetMock();
        var mockFlightRepo = mock.Mock<IFlightRepository>();
        
        mockFlightRepo
            .Setup(x => x.GetActiveFlights())
            .ReturnsAsync(new[] {FlightData.OutboundFlight, FlightData.OutboundFlight2});
        
        var service = mock.Create<FlightService>();
        var reference = new DateTime(2023, 08, 08, 11, 30, 00);
        var result = await service.GetDelayedFlights(TimeSpan.FromMinutes(120), reference);

        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Collection(result,
            f1 =>
            {
                Assert.Equal(541406104, f1.Id);
            });
    }
    
    [Fact]
    public async Task GetDelayedFlights_DelayedInboundFlights_ReturnDelayedFlights()
    {
        using var mock = AutoMock.GetLoose();
        var mockFlightRepo = mock.Mock<IFlightRepository>();
        
        mockFlightRepo
            .Setup(x => x.GetActiveFlights())
            .ReturnsAsync(new[] {FlightData.InboundFlight, FlightData.InboundFlight2});
        
        var service = mock.Create<FlightService>();
        var reference = new DateTime(2023, 08, 08, 11, 30, 00);
        var result = await service.GetDelayedFlights(TimeSpan.FromMinutes(120), reference);

        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Collection(result,
            f1 =>
            {
                Assert.Equal(541406100, f1.Id);
            });
    }
    
    [Fact]
    public async Task GetDelayedFlights_DelayedInboundAndOutboundFlights_ReturnDelayedFlights()
    {
        using var mock = AutoMock.GetLoose();
        var mockFlightRepo = mock.Mock<IFlightRepository>();
        
        mockFlightRepo
            .Setup(x => x.GetActiveFlights())
            .ReturnsAsync(new[] {FlightData.InboundFlight, FlightData.InboundFlight2, FlightData.OutboundFlight, FlightData.OutboundFlight2});
        
        var service = mock.Create<FlightService>();
        var reference = new DateTime(2023, 08, 08, 11, 30, 00);
        var result = await service.GetDelayedFlights(TimeSpan.FromMinutes(120), reference);

        Assert.NotNull(result);
        Assert.Collection(result,
            f1 =>
            {
                Assert.Equal(541406100, f1.Id);
            },
            f2 =>
            {
                Assert.Equal(541406104, f2.Id);
            });
    }
}