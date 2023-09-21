using FidsCodingAssignment.Core.Models;
using FidsCodingAssignment.Data.Models;

namespace FidsCodingAssignment.Core.Mappers;

public static class LocationMapper
{
    public static Location MapLocation(this AirportEntity airport)
    {
        return new Location
        {
            City = airport.City,
            State = airport.State,
            Country = airport.Country
        };
    }
}