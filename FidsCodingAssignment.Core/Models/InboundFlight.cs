using FidsCodingAssignment.Data.Models;

namespace FidsCodingAssignment.Core.Models;

public class InboundFlight : Flight
{
    /// <summary>
    /// Flight origin.
    /// </summary>
    public string? Origin { get; set; }

    public override void Map(FlightEntity flightEntity)
    {
        base.Map(flightEntity);
        Origin = flightEntity.City;
    }
}