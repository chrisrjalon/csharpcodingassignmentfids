namespace FidsCodingAssignment.Common.Models;

public class FlightConfiguration
{
    /// <summary>
    /// Time in minutes for boarding to start before departure.
    /// </summary>
    public int BoardingWindow { get; set; }
    
    /// <summary>
    /// Time in minutes for boarding to complete.
    /// </summary>
    public int BoardingDuration { get; set; }
    
    /// <summary>
    /// Time in minutes for flight to be at the gate before departure.
    /// </summary>
    public int FlightAtGateWindow { get; set; }
}