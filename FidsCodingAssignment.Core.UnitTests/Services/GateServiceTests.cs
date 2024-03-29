﻿using FidsCodingAssignment.Core.Models;
using FidsCodingAssignment.Core.Services;
using FidsCodingAssignment.Core.UnitTests.TestData;
using FidsCodingAssignment.Data.Models;
using FidsCodingAssignment.Data.Repositories;
using Moq;
using Xunit;

namespace FidsCodingAssignment.Core.UnitTests.Services;

public class GateServiceTests : ServiceBaseTests
{
    [Fact]
    public async Task GetActiveFlight_NoFlightsAssigned_ThrowsException()
    {
        using var mock = GetMock();
        var mockFlightRepo = mock.Mock<IFlightRepository>();

        mockFlightRepo
            .Setup(x => x.GetFlightsAssignedToGate("E36"))!
            .ReturnsAsync((ICollection<FlightEntity>?) null);
        
        var service = mock.Create<GateService>();
        
        var result = await service.GetActiveFlight("E36");
        
        Assert.Null(result.Value);
    }
    
    [Fact]
    public async Task GetActiveFlight_WithActiveFlight_ThrowsException()
    {
        using var mock = GetMock();
        var mockFlightRepo = mock.Mock<IFlightRepository>();

        mockFlightRepo
            .Setup(x => x.GetFlightsAssignedToGate("E36"))!
            .ReturnsAsync(new[] {FlightData.OutboundFlightEntity});
        
        var service = mock.Create<GateService>();
        var referenceTime = new DateTime(2023, 08, 08, 12, 00, 00);
        var result = await service.GetActiveFlight("E36", referenceTime);
        
        Assert.False(result.IsError);
        Assert.True(result.Value is OutboundFlight);
        
        var outboundFlight = (result.Value as OutboundFlight)!;
        Assert.True(outboundFlight.IsAtGate);
    }
}