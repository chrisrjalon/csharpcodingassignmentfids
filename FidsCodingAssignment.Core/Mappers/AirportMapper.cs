using FidsCodingAssignment.Core.Models;
using FidsCodingAssignment.Data.Models;

namespace FidsCodingAssignment.Core.Mappers;

public static class AirportMapper
{
    public static Airport Map(this AirportEntity airport)
    {
        return new Airport
        {
            Id = airport.Id,
            Code = airport.Code,
            City = airport.City,
            State = airport.State,
            Country = airport.Country
        };
    }
}