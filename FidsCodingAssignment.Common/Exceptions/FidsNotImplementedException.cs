using FidsCodingAssignment.Common.Enumerations;

namespace FidsCodingAssignment.Common.Exceptions;

public class FidsNotImplementedException : FidsException
{
    private FidsNotImplementedException(string errorMessage, ExceptionCategoryType category = ExceptionCategoryType.Error) 
        : base(errorMessage, category)
    {
    }

    public FidsNotImplementedException(Type type, string instanceName) :
        this($"The type {type.Name} does not implement {instanceName}.")
    {
        
    }
}