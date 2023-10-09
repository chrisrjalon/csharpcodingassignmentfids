using FidsCodingAssignment.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FidsCodingAssignment.Data.Extensions;

internal static class FlightExtensions
{
    internal static IQueryable<FlightEntity> IncludeDefault(this IQueryable<FlightEntity> query)
    {
        return query
            .Include(x => x.ParentFlight)
            .Include(x => x.CodeShareFlights);
    }
}