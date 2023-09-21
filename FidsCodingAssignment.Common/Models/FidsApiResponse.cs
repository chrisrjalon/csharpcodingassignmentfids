using FidsCodingAssignment.Common.Enumerations;

namespace FidsCodingAssignment.Common.Models;

public class FidsApiResponse<TResult>
{
    public TResult? Result { get; set; }
    
    private FidsApiResponse(TResult result)
    {
        Result = result;
    }
    
    public static FidsApiResponse<TResult> Success(TResult result)
    {
        return new FidsApiResponse<TResult>(result);
    }
}

public class FidsApiResponse
{
    public string? ErrorMessage { get; set; }

    public ExceptionCategoryType? ErrorCategory { get; set; }

    private FidsApiResponse(string errorMessage, ExceptionCategoryType errorCategory)
    {
        ErrorMessage = errorMessage;
        ErrorCategory = errorCategory;
    }
    
    public static FidsApiResponse Error(string errorMessage, ExceptionCategoryType errorCategory = ExceptionCategoryType.Error)
    {
        return new FidsApiResponse(errorMessage, errorCategory);
    }
}