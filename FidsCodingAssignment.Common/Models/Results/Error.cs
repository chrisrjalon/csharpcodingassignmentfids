using FidsCodingAssignment.Common.Enumerations;

namespace FidsCodingAssignment.Common.Models.Results;

public readonly record struct Error
{
    public string Code { get; }
    public string Description { get; }
    public ErrorType Type { get; }

    public ErrorCategoryType Category { get; }
    public int NumericType { get; }
    
    private Error(string code, string description, ErrorType type, ErrorCategoryType category)
    {
        Code = code;
        Description = description;
        Type = type;
        NumericType = (int)type;
        Category = category;
    }
    
    public static Error Failure(
        string code = "General.Failure",
        string description = "A failure has occurred.") =>
        new(code, description, ErrorType.Failure, ErrorCategoryType.Error);
    
    public static Error Unexpected(
        string code = "General.Unexpected",
        string description = "An unexpected error has occurred.") =>
        new(code, description, ErrorType.Unexpected, ErrorCategoryType.Error);
    
    public static Error Validation(
        string code = "General.Validation",
        string description = "A validation error has occurred.") =>
        new(code, description, ErrorType.Validation, ErrorCategoryType.Error);
    
    public static Error Conflict(
        string code = "General.Conflict",
        string description = "A conflict error has occurred.") =>
        new(code, description, ErrorType.Conflict, ErrorCategoryType.Error);
    
    public static Error NotFound(
        string code = "General.NotFound",
        string description = "A not found error has occurred.",
        ErrorCategoryType category = ErrorCategoryType.Error) =>
        new(code, description, ErrorType.NotFound, category);
    
    public static Error Unauthorized(
        string code = "General.Unauthorized",
        string description = "An unauthorized error has occurred.") =>
        new(code, description, ErrorType.Unauthorized, ErrorCategoryType.Error);
}