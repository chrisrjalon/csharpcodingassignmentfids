using System.Text.Json.Serialization;
using FidsCodingAssignment.Common.Enumerations;
using FidsCodingAssignment.Data.Models;

namespace FidsCodingAssignment.Core.Models;

public class OutboundFlight : Flight
{
    /// <summary>
    /// Flag indicating whether the flight is at the gate.
    /// </summary>
    public bool IsAtGate { get; private set; }
    
    /// <summary>
    /// Gets the flag indicating whether the flight is boarding.
    /// </summary>
    public bool IsBoarding => BoardingStatus == BoardingStatusType.Boarding;
    
    /// <summary>
    /// Gets the flag indicating whether the flight boarding is closed.
    /// </summary>
    public bool IsBoardingClosed => BoardingStatus == BoardingStatusType.Closed;
    
    /// <summary>
    /// Boarding status of the flight.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public BoardingStatusType? BoardingStatus { get; set; }

    /// <summary>
    /// Flight destination.
    /// </summary>
    public string? Destination { get; set; }
    
    /// <summary>
    /// Code of the gate the flight is assigned to.
    /// </summary>
    public string? GateCode { get; set; }

    protected override FlightStatusType FinalStatus => FlightStatusType.Departed;

    private bool IsFlightAtGate(FlightConfiguration flightConfiguration, DateTime? referenceTime = null)
    {
        referenceTime ??= DateTime.UtcNow;
        
        var flightAtGateEarliestTime = ScheduledTime.AddMinutes(-flightConfiguration.FlightAtGateWindow);
        var flightAtGateLatestTime = ScheduledTime.AddMinutes(flightConfiguration.FlightAtGateWindow);
        
        return referenceTime.Value > flightAtGateEarliestTime && referenceTime.Value < flightAtGateLatestTime;
    }
    
    public void SetFlightAtGate(int flightAtGateWindow, DateTime? referenceTime = null)
    {
        referenceTime ??= DateTime.UtcNow;

        var flightAtGateEarliestTime = ScheduledTime.AddMinutes(-flightAtGateWindow);
        var flightAtGateLatestTime = ScheduledTime.AddMinutes(flightAtGateWindow);
        
        IsAtGate = referenceTime.Value >= flightAtGateEarliestTime && referenceTime.Value <= flightAtGateLatestTime;
    }

    public void SetBoardingStatus(int boardingWindow, int boardingDuration, DateTime? referenceTime = null)
    {
        referenceTime ??= DateTime.UtcNow;
        
        var boardingTimeStart = ScheduledTime.AddMinutes(boardingWindow);
        var boardingTimeEnd = boardingTimeStart.AddMinutes(boardingDuration);
        
        if (referenceTime.Value >= boardingTimeStart && referenceTime.Value <= boardingTimeEnd)
        {
            BoardingStatus = BoardingStatusType.Boarding;
            return;
        }

        if (boardingTimeEnd < referenceTime.Value && ActualTime == null)
        {
            BoardingStatus = BoardingStatusType.Closed;
            return;
        }
        
        BoardingStatus = null;
    }

    public override void Map(FlightEntity flightEntity)
    {
        base.Map(flightEntity);
        Destination = flightEntity.City;
        GateCode = flightEntity.GateCode;
    }

    protected override void SetStatus(FlightConfiguration flightConfiguration, DateTime? referenceTime = null)
    {
        referenceTime ??= DateTime.UtcNow;
        
        base.SetStatus(flightConfiguration, referenceTime);
        SetBoardingStatus(flightConfiguration.BoardingWindow, flightConfiguration.BoardingDuration, referenceTime);
        SetFlightAtGate(flightConfiguration.FlightAtGateWindow, referenceTime);
    }
}