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
    public async Task GetFlight_FlightNotFound_ThrowsException()
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
            .ReturnsAsync(OutboundFlight);
        
        var service = mock.Create<FlightService>();
        
        var flight = await service.GetFlight("SY", 505);
        
        Assert.Equal(541406104, flight.Id);
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
        await Assert.ThrowsAsync<FidsNotFoundException>(() => service.GetFlightStatus(It.IsAny<int>()));
    }
    
    [Fact]
    public async Task GetFlightStatus_FlightNoStatus_ThrowsException()
    {
        using var mock = AutoMock.GetLoose();
        var mockFlightRepo = mock.Mock<IFlightRepository>();
        var mockFlightStatusRepo = mock.Mock<IFlightStatusRepository>();
        
        mockFlightRepo
            .Setup(x => x.Get(It.IsAny<int>()))
            .ReturnsAsync(OutboundFlight);
        
        mockFlightStatusRepo
            .Setup(x => x.GetCurrentFlightStatus(It.IsAny<int>()))
            .ReturnsAsync((FlightStatusEntity?) null);
        
        var service = mock.Create<FlightService>();
        await Assert.ThrowsAsync<FidsException>(() => service.GetFlightStatus(It.IsAny<int>()));
    }
    
    [Fact]
    public async Task GetFlightStatus_FlightWithStatus_ReturnsFlightStatus()
    {
        using var mock = AutoMock.GetLoose();
        var mockFlightRepo = mock.Mock<IFlightRepository>();
        var mockFlightStatusRepo = mock.Mock<IFlightStatusRepository>();
        
        mockFlightRepo
            .Setup(x => x.Get(It.IsAny<int>()))
            .ReturnsAsync(OutboundFlight);
        
        mockFlightStatusRepo
            .Setup(x => x.GetCurrentFlightStatus(It.IsAny<int>()))
            .ReturnsAsync(OnTimeFlightStatus);
        
        var service = mock.Create<FlightService>();
        var result = await service.GetFlightStatus(It.IsAny<int>());
        
        Assert.Equal(123456789, result.Id);
        Assert.Equal(FlightStatusType.OnTime, result.Status);
    }
    
    [Fact]
    public async Task GetDelayedFlights_NoActiveFlights_ReturnsEmptyList()
    {
        using var mock = AutoMock.GetLoose();
        var mockFlightRepo = mock.Mock<IFlightRepository>();
        
        mockFlightRepo
            .Setup(x => x.GetActiveFlights())
            .ReturnsAsync(Array.Empty<FlightEntity>());
        
        var service = mock.Create<FlightService>();
        var result = await service.GetDelayedFlights(It.IsAny<TimeSpan>());
        
        Assert.Empty(result);
    }
    
    [Fact]
    public async Task GetDelayedFlights_DelayedOutboundFlights_ReturnDelayedFlights()
    {
        using var mock = AutoMock.GetLoose();
        var mockFlightRepo = mock.Mock<IFlightRepository>();
        
        mockFlightRepo
            .Setup(x => x.GetActiveFlights())
            .ReturnsAsync(new[] {OutboundFlight, OutboundFlight2});
        
        var service = mock.Create<FlightService>();
        var reference = new DateTime(2023, 08, 08, 11, 30, 00);
        var result = await service.GetDelayedFlights(TimeSpan.FromMinutes(120), reference);

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
            AirlineId = 1001,
            Airline = new AirlineEntity
            {
                Id = 1001,
                Code = "SY",
                Name = "SUN COUNTRY"
            },
            FlightStatus = FlightStatusType.OnTime,
            Bound = FlightBoundType.Outbound,
            FlightType = FlightMovementType.International,
            ScheduledDeparture = new DateTime(2023, 08, 08, 13, 00, 00)
        };
    
    private FlightEntity InboundFlight =>
        new()
        {
            Id = 541406100,
            FlightNumber = 500,
            AirlineId = 1001,
            Airline = new AirlineEntity
            {
                Id = 1001,
                Code = "SY",
                Name = "SUN COUNTRY"
            },
            FlightStatus = FlightStatusType.OnTime,
            Bound = FlightBoundType.Inbound,
            FlightType = FlightMovementType.International,
            ScheduledArrival = new DateTime(2023, 08, 08, 13, 00, 00)
        };
    
    private FlightEntity InboundFlight2 =>
        new()
        {
            Id = 541406101,
            FlightNumber = 501,
            AirlineId = 1001,
            Airline = new AirlineEntity
            {
                Id = 1001,
                Code = "SY",
                Name = "SUN COUNTRY"
            },
            FlightStatus = FlightStatusType.OnTime,
            Bound = FlightBoundType.Inbound,
            FlightType = FlightMovementType.International,
            ScheduledArrival = new DateTime(2023, 08, 08, 14, 00, 00)
        };
    
    private FlightEntity OutboundFlight2 =>
        new()
        {
            Id = 541406105,
            FlightNumber = 506,
            AirlineId = 1001,
            Airline = new AirlineEntity
            {
                Id = 1001,
                Code = "SY",
                Name = "SUN COUNTRY"
            },
            FlightStatus = FlightStatusType.OnTime,
            Bound = FlightBoundType.Outbound,
            FlightType = FlightMovementType.International,
            ScheduledDeparture = new DateTime(2023, 08, 08, 14, 00, 00)
        };
    
    private FlightStatusEntity OnTimeFlightStatus =>
        new()
        {
            Id = 123456789,
            FlightId = 541406104,
            Flight = OutboundFlight,
            Status = FlightStatusType.OnTime,
            DateCreated = new DateTime(2023, 08, 08, 13, 00, 00),
            IsActive = true
        };
}