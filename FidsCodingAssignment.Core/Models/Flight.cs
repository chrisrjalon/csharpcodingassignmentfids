using FidsCodingAssignment.Common.Enumerations;

namespace FidsCodingAssignment.Core.Models;

public class Flight
{
    public int Id { get; set; }
    public int FlightNumber { get; set; }
    public string? AirlineCode { get; set; }
    public bool IsCodeShare { get; set; }
    public int? ParentFlightId { get; set; }
    public Flight? ParentFlight { get; set; }
    public FlightBound Bound { get; set; }
    public DateTime ScheduledDeparture { get; set; }
    public DateTime? ActualDeparture { get; set; }
    public DateTime ScheduledArrival { get; set; }
    public DateTime? ActualArrival { get; set; }
    public DateTime? ScheduledBoarding { get; set; }
    public DateTime? ActualBoarding { get; set; }
    public FlightMovementType FlightType { get; set; }
    public FlightStatusType FlightStatus { get; set; }
}