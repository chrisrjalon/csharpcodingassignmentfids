using FidsCodingAssignment.Common.Enumerations;

namespace FidsCodingAssignment.Core.Models;

public class FlightStatus
{
    public int Id { get; set; }
    public int FlightId { get; set; }
    public Flight? Flight { get; set; }
    public FlightStatusType Status { get; set; }
    public DateTime EffectiveFrom { get; set; }
    public DateTime? EffectiveTo { get; set; }
}