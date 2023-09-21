using FidsCodingAssignment.Common.Enumerations;

namespace FidsCodingAssignment.Common.Exceptions;

public class FidsException : Exception
{
    public ExceptionCategory Category { get; }

    public FidsException(string errorMessage, ExceptionCategory category = ExceptionCategory.Error)
        : base(errorMessage)
    {
        Category = category;
    }
}