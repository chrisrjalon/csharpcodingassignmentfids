namespace FidsCodingAssignment.Common.Models.Results;

public interface IResult
{
    List<Error>? Errors { get; }
    
    bool IsError { get; }
}