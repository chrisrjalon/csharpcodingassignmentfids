using Autofac.Extras.Moq;
using FidsCodingAssignment.Common.Enumerations;
using FidsCodingAssignment.Common.Exceptions;
using FidsCodingAssignment.Common.Models;
using FidsCodingAssignment.Core.Models;
using FidsCodingAssignment.Core.Services;
using FidsCodingAssignment.Data.Models;
using FidsCodingAssignment.Data.Repositories;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace FidsCodingAssignment.Core.UnitTests.Services;

public class FlightServiceTests
{
    private AutoMock GetMock()
    {
        var mock = AutoMock.GetLoose();
        var mockFlightConfigOptions = mock.Mock<IOptions<FlightConfiguration>>();
        mockFlightConfigOptions
            .Setup(x => x.Value)
            .Returns(FlightConfig);

        return mock;
    }
    
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
            .ReturnsAsync(OutboundFlight);
        
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
            .ReturnsAsync(new[] {OutboundFlight, OutboundFlight2});
        
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
            .ReturnsAsync(new[] {InboundFlight, InboundFlight2});
        
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
            .ReturnsAsync(new[] {InboundFlight, InboundFlight2, OutboundFlight, OutboundFlight2});
        
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

    private FlightEntity OutboundFlight =>
        new()
        {
            Id = 541406104,
            FlightNumber = 505,
            AirlineCode = "SY",
            Bound = FlightBoundType.Outbound,
            FlightType = FlightMovementType.International,
            ScheduledTime = new DateTime(2023, 08, 08, 13, 00, 00)
        };
    
    private FlightEntity InboundFlight =>
        new()
        {
            Id = 541406100,
            FlightNumber = 500,
            AirlineCode = "SY",
            Bound = FlightBoundType.Inbound,
            FlightType = FlightMovementType.International,
            ScheduledTime = new DateTime(2023, 08, 08, 13, 00, 00)
        };
    
    private FlightEntity InboundFlight2 =>
        new()
        {
            Id = 541406101,
            FlightNumber = 501,
            AirlineCode = "SY",
            Bound = FlightBoundType.Inbound,
            FlightType = FlightMovementType.International,
            ScheduledTime = new DateTime(2023, 08, 08, 14, 00, 00)
        };
    
    private FlightEntity OutboundFlight2 =>
        new()
        {
            Id = 541406105,
            FlightNumber = 506,
            AirlineCode = "SY",
            Bound = FlightBoundType.Outbound,
            FlightType = FlightMovementType.International,
            ScheduledTime = new DateTime(2023, 08, 08, 14, 00, 00)
        };
    
    private FlightStatus OnTimeStatus =>
        new()
        {
            FlightId = 541406104,
            FlightNumber = 505,
            AirlineCode = "SY",
            Status = FlightStatusType.OnTime,
        };
    
    private FlightConfiguration FlightConfig =>
        new()
        {
            BoardingWindow = -90,
            BoardingDuration = 60,
            FlightAtGateWindow = 120
        };
}