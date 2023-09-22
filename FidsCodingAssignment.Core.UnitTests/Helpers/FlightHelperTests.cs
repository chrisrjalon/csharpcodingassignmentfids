using FidsCodingAssignment.Common.Enumerations;
using FidsCodingAssignment.Core.Helpers;
using FidsCodingAssignment.Core.Mappers;
using FidsCodingAssignment.Core.UnitTests.TestData;
using Xunit;

namespace FidsCodingAssignment.Core.UnitTests.Helpers;

public class FlightHelperTests
{
    [Fact]
    public void GetFlightStatus_InboundFlightOnTime_ReturnsDeparted()
    {
        var flight = FlightData.InboundFlight;

        var referenceTime = new DateTime(2023, 08, 08, 12, 00, 00);
        var result = FlightHelper.GetFlightStatus(flight.Map(), FlightData.FlightConfig, referenceTime);
        
        Assert.Equal(FlightStatusType.OnTime, result);
    }
    
    [Fact]
    public void GetFlightStatus_InboundFlightDelayed_ReturnsDeparted()
    {
        var flight = FlightData.InboundFlight;

        var referenceTime = new DateTime(2023, 08, 08, 14, 00, 00);
        var result = FlightHelper.GetFlightStatus(flight.Map(), FlightData.FlightConfig, referenceTime);
        
        Assert.Equal(FlightStatusType.Delayed, result);
    }
    
    [Fact]
    public void GetFlightStatus_InboundFlightArrived_ReturnsDeparted()
    {
        var flight = FlightData.InboundFlight;
        flight.ActualTime = new DateTime(2023, 08, 08, 13, 00, 00);

        var referenceTime = new DateTime(2023, 08, 08, 14, 00, 00);
        var result = FlightHelper.GetFlightStatus(flight.Map(), FlightData.FlightConfig, referenceTime);
        
        Assert.Equal(FlightStatusType.Arrived, result);
    }
    
    [Fact]
    public void GetFlightStatus_OutboundFlightOnTime_ReturnsDeparted()
    {
        var flight = FlightData.OutboundFlight;

        var referenceTime = new DateTime(2023, 08, 08, 10, 00, 00);
        var result = FlightHelper.GetFlightStatus(flight.Map(), FlightData.FlightConfig, referenceTime);
        
        Assert.Equal(FlightStatusType.OnTime, result);
    }
    
    [Fact]
    public void GetFlightStatus_OutboundFlightBoarding_ReturnsDeparted()
    {
        var flight = FlightData.OutboundFlight;

        var referenceTime = new DateTime(2023, 08, 08, 12, 00, 00);
        var result = FlightHelper.GetFlightStatus(flight.Map(), FlightData.FlightConfig, referenceTime);
        
        Assert.Equal(FlightStatusType.Boarding, result);
    }
    
    [Fact]
    public void GetFlightStatus_OutboundFlightClosed_ReturnsDeparted()
    {
        var flight = FlightData.OutboundFlight;

        var referenceTime = new DateTime(2023, 08, 08, 12, 45, 00);
        var result = FlightHelper.GetFlightStatus(flight.Map(), FlightData.FlightConfig, referenceTime);
        
        Assert.Equal(FlightStatusType.Closed, result);
    }
    
    [Fact]
    public void GetFlightStatus_OutboundFlightDeparted_ReturnsDeparted()
    {
        var flight = FlightData.OutboundFlight;
        flight.ActualTime = new DateTime(2023, 08, 08, 13, 05, 00);

        var referenceTime = new DateTime(2023, 08, 08, 12, 00, 00);
        var result = FlightHelper.GetFlightStatus(flight.Map(), FlightData.FlightConfig, referenceTime);
        
        Assert.Equal(FlightStatusType.Departed, result);
    }
}