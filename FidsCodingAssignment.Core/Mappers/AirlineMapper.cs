using FidsCodingAssignment.Core.Models;
using FidsCodingAssignment.Data.Models;

namespace FidsCodingAssignment.Core.Mappers;

public static class AirlineMapper
{
    public static Airline Map(this AirlineEntity airline)
    {
        return new Airline
        {
            Id = airline.Id,
            Code = airline.Code,
            Name = airline.Name
        };
    }
}