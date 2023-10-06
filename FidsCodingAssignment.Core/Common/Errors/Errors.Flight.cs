using FidsCodingAssignment.Common.Models.Results;

namespace FidsCodingAssignment.Core.Common.Errors;

public static partial class Errors
{
    public static class Flight
    {
        public static Error NotFound = Error.NotFound("Flight.NotFound", "Flight not found.");
    }
}