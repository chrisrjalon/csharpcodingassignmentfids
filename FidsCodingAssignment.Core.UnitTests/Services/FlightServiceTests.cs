using Autofac.Extras.Moq;
using FidsCodingAssignment.Common.Enumerations;
using FidsCodingAssignment.Core.Common.Errors;
using FidsCodingAssignment.Core.Models;
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
    public async Task GetFlight_FlightNotFound_ThrowsException()
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
    public async Task GetFlight_FlightStatusBoarding_ReturnsFlightStatus()
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
        Assert.Equal(BoardingStatusType.Closed, ((OutboundFlight)result.Value).BoardingStatus);
    }
    
    [Fact]
    public async Task GetDelayedFlights_NoActiveFlights_ReturnsEmptyList()
    {
        using var mock = GetMock();
        var mockFlightRepo = mock.Mock<IFlightRepository>();
        
        mockFlightRepo
            .Setup(x => x.GetActiveFlights())!
            .ReturnsAsync(Array.Empty<FlightEntity>());
        
        var service = mock.Create<FlightService>();
        var result = await service.GetDelayedFlights(It.IsAny<TimeSpan>());
        
        Assert.Empty(result.Value!);
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
        var result = await service.GetDelayedFlights(TimeSpan.FromMinutes(30));

        Assert.False(result.IsError);
        Assert.NotNull(result.Value);
        Assert.Collection(result.Value,
            f1 =>
            {
                Assert.Equal(541406105, f1.FlightId);
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
        var result = await service.GetDelayedFlights(TimeSpan.FromMinutes(30));

        Assert.False(result.IsError);
        Assert.NotNull(result.Value);
        Assert.Collection(result.Value,
            f1 =>
            {
                Assert.Equal(541406101, f1.FlightId);
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
        var result = await service.GetDelayedFlights(TimeSpan.FromMinutes(30));

        Assert.False(result.IsError);
        Assert.NotNull(result.Value);
        Assert.Collection(result.Value,
            df1 =>
            {
                Assert.Equal(541406101, df1.FlightId);
            },
            df2 =>
            {
                Assert.Equal(541406105, df2.FlightId);
            });
    }
}