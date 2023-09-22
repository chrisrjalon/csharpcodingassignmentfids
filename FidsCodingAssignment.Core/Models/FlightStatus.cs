using FidsCodingAssignment.Common.Enumerations;

namespace FidsCodingAssignment.Core.Models;

public class FlightStatus
{
    public int FlightId { get; set; }

    public string AirlineCode { get; set; }

    public int FlightNumber { get; set; }

    public FlightStatusType Status { get; set; }
}