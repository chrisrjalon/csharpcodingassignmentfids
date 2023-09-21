using FidsCodingAssignment.Common.Enumerations;

namespace FidsCodingAssignment.Common.Exceptions;

public class FidsException : Exception
{
    public ExceptionCategoryType Category { get; }

    public FidsException(string errorMessage, ExceptionCategoryType category = ExceptionCategoryType.Error)
        : base(errorMessage)
    {
        Category = category;
    }
}