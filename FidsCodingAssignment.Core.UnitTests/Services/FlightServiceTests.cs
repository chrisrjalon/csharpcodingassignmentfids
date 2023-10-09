using Autofac.Extras.Moq;
using FidsCodingAssignment.Common.Enumerations;
using FidsCodingAssignment.Core.Common.Errors;
using FidsCodingAssignment.Core.Services;
using FidsCodingAssignment.Core.UnitTests.TestData;
using FidsCodingAssignment.Data.Models;
using FidsCodingAssignment.Data.Repositories;
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
        
        var result = await service.GetFlight(It.IsAny<string>(), It.IsAny<int>());
        
        Assert.True(result.IsError);
        Assert.NotNull(result.Errors);
        Assert.Collection(result.Errors,
            e1 => Assert.Equal(Errors.Flight.NotFound.Code, e1.Code));
    }
    
    [Fact]
    public async Task GetFlightStatus_FlightStatusBoarding_ReturnsFlightStatus()
    {
        using var mock = GetMock();
        var mockFlightRepo = mock.Mock<IFlightRepository>();

        mockFlightRepo
            .Setup(x => x.GetFlight(It.IsAny<string>(), It.IsAny<int>()))
            .ReturnsAsync(FlightData.OutboundFlightEntity);
        
        var service = mock.Create<FlightService>();
        var result = await service.GetFlight(It.IsAny<string>(), It.IsAny<int>());
        
        Assert.False(result.IsError);
        Assert.Equal(541406104, result.Value.FlightId);
        Assert.Equal(FlightStatusType.Delayed, result.Value.FlightStatus);
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
        
        Assert.Null(result.Value);
    }
    
    [Fact]
    public async Task GetDelayedFlights_DelayedOutboundFlights_ReturnDelayedFlights()
    {
        using var mock = GetMock();
        var mockFlightRepo = mock.Mock<IFlightRepository>();
        
        mockFlightRepo
            .Setup(x => x.GetActiveFlights())
            .ReturnsAsync(new[] {FlightData.OutboundFlightEntity, FlightData.OutboundFlightEntity2});
        
        var service = mock.Create<FlightService>();
        var reference = new DateTime(2023, 08, 08, 11, 30, 00);
        var result = await service.GetDelayedFlights(TimeSpan.FromMinutes(120), reference);

        Assert.False(result.IsError);
        Assert.NotNull(result.Value);
        Assert.Single(result.Value);
        Assert.Collection(result.Value,
            f1 =>
            {
                Assert.Equal(541406104, f1.FlightId);
            });
    }
    
    [Fact]
    public async Task GetDelayedFlights_DelayedInboundFlights_ReturnDelayedFlights()
    {
        using var mock = AutoMock.GetLoose();
        var mockFlightRepo = mock.Mock<IFlightRepository>();
        
        mockFlightRepo
            .Setup(x => x.GetActiveFlights())
            .ReturnsAsync(new[] {FlightData.InboundFlightEntity, FlightData.InboundFlightEntity2});
        
        var service = mock.Create<FlightService>();
        var reference = new DateTime(2023, 08, 08, 11, 30, 00);
        var result = await service.GetDelayedFlights(TimeSpan.FromMinutes(120), reference);

        Assert.False(result.IsError);
        Assert.NotNull(result.Value);
        Assert.Single(result.Value);
        Assert.Collection(result.Value,
            f1 =>
            {
                Assert.Equal(541406100, f1.FlightId);
            });
    }
    
    [Fact]
    public async Task GetDelayedFlights_DelayedInboundAndOutboundFlights_ReturnDelayedFlights()
    {
        using var mock = AutoMock.GetLoose();
        var mockFlightRepo = mock.Mock<IFlightRepository>();
        
        mockFlightRepo
            .Setup(x => x.GetActiveFlights())
            .ReturnsAsync(new[] {FlightData.InboundFlightEntity, FlightData.InboundFlightEntity2, FlightData.OutboundFlightEntity, FlightData.OutboundFlightEntity2});
        
        var service = mock.Create<FlightService>();
        var reference = new DateTime(2023, 08, 08, 11, 30, 00);
        var result = await service.GetDelayedFlights(TimeSpan.FromMinutes(120), reference);

        Assert.False(result.IsError);
        Assert.NotNull(result.Value);
        Assert.Collection(result.Value,
            f1 =>
            {
                Assert.Equal(541406100, f1.FlightId);
            },
            f2 =>
            {
                Assert.Equal(541406104, f2.FlightId);
            });
    }
}