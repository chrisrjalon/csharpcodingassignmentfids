using FidsCodingAssignment.Common.Enumerations;

namespace FidsCodingAssignment.Common.Exceptions;

public class FidsNotFoundException : FidsException
{
    public FidsNotFoundException(string entity) 
        : base($"Cannot find {entity} or you do not have access.")
    {
    }
}