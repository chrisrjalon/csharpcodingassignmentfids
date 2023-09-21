using FidsCodingAssignment.Core.Models;
using FidsCodingAssignment.Data.Models;

namespace FidsCodingAssignment.Core.Mappers;

public static class FlightStatusMapper
{
    public static FlightStatus? Map(this FlightStatusEntity? flightStatus)
    {
        if (flightStatus == null)
            return null;

        return new FlightStatus
        {
            Id = flightStatus.Id,
            FlightId = flightStatus.FlightId,
            Flight = flightStatus.Flight?.Map(),
            Status = flightStatus.Status,
            EffectiveFrom = flightStatus.DateCreated,
            EffectiveTo = flightStatus.DateDeleted,
            Remarks = flightStatus.Remarks
        };
    }
}