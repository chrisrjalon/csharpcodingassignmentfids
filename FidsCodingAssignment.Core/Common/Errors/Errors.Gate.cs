using FidsCodingAssignment.Common.Enumerations;
using FidsCodingAssignment.Common.Models.Results;

namespace FidsCodingAssignment.Core.Common.Errors;

public static partial class Errors
{
    public static class Gate
    {
        public static Error NotFound = Error.NotFound("Gate.NotFound", "Gate not found.");
        
        public static Error NoActiveFlight = Error.NotFound("Gate.NoActiveFlight", "No active flight found.", ErrorCategoryType.Warning);
    }
}