namespace FidsCodingAssignment.Core.Models;

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
    /// Time in minutes for flight to be considered at the gate before and after scheduled time.
    /// </summary>
    public int FlightAtGateWindow { get; set; }
}