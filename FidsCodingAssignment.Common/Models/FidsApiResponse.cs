using FidsCodingAssignment.Common.Enumerations;

namespace FidsCodingAssignment.Common.Models;

public class FidsApiResponse<TResult>
{
    public TResult? Result { get; set; }
    
    public string? ErrorMessage { get; set; }

    public ExceptionCategoryType? ErrorCategory { get; set; }

    public bool IsSuccess { get; }
    
    private FidsApiResponse(TResult result)
    {
        Result = result;
        IsSuccess = true;
    }
    
    private FidsApiResponse(string errorMessage, ExceptionCategoryType errorCategory)
    {
        ErrorMessage = errorMessage;
        ErrorCategory = errorCategory;
    }
    
    public static FidsApiResponse<TResult> Success(TResult result)
    {
        return new FidsApiResponse<TResult>(result);
    }
    
    public static FidsApiResponse<TResult> Error(string errorMessage, ExceptionCategoryType errorCategory = ExceptionCategoryType.Error)
    {
        return new FidsApiResponse<TResult>(errorMessage, errorCategory);
    }
}

public class EmptyFidsResponse
{
    public string? ErrorMessage { get; set; }

    public ExceptionCategoryType? ErrorCategory { get; set; }

    public bool IsSuccess { get; }

    private EmptyFidsResponse()
    {
        IsSuccess = true;
    }
    
    private EmptyFidsResponse(string errorMessage, ExceptionCategoryType errorCategory)
    {
        ErrorMessage = errorMessage;
        ErrorCategory = errorCategory;
    }
    
    public static EmptyFidsResponse Success()
    {
        return new EmptyFidsResponse();
    }
    
    public static EmptyFidsResponse Error(string errorMessage, ExceptionCategoryType errorCategory = ExceptionCategoryType.Error)
    {
        return new EmptyFidsResponse(errorMessage, errorCategory);
    }
}