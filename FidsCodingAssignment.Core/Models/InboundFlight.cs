using FidsCodingAssignment.Common.Enumerations;
using FidsCodingAssignment.Data.Models;

namespace FidsCodingAssignment.Core.Models;

public class InboundFlight : Flight
{
    /// <summary>
    /// Flight origin.
    /// </summary>
    public string? Origin { get; set; }

    protected override FlightStatusType FinalStatus => FlightStatusType.Arrived;

    public override void Map(FlightEntity flightEntity)
    {
        base.Map(flightEntity);
        Origin = flightEntity.City;
    }
}