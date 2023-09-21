namespace FidsCodingAssignment.Common.Models;

public class FidsApiResponse<T>
{
    public T? Result { get; set; }

    public string? ErrorMessage { get; set; }

    public FidsApiResponse(T result)
    {
        Result = result;
    }
    
    public FidsApiResponse(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }
}