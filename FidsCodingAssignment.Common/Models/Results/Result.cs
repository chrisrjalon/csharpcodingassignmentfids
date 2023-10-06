namespace FidsCodingAssignment.Common.Models.Results;

public abstract class ResultBase : IResult
{
    private readonly List<Error>? _errors;
    
    public List<Error>? Errors => IsError ? _errors! : new List<Error>();
    
    public bool IsError { get; }
    
    protected ResultBase()
    {
        IsError = false;
    }
    
    protected ResultBase(List<Error> errors)
    {
        _errors = errors;
        IsError = true;
    }
    
    protected ResultBase(Error error)
    {
        _errors = new List<Error> { error };
        IsError = true;
    }
}

public class Result : ResultBase
{
    private Result()
    {
    }
    
    private Result(List<Error> errors)
        : base(errors)
    {
    }
    
    private Result(Error error)
        : base(error)
    {
    }

    public static Result Success => new();
    
    public static implicit operator Result(Error error)
    {
        return new Result(error);
    }
    
    public static implicit operator Result(List<Error> errors)
    {
        return new Result(errors);
    }
    
    public static implicit operator Result(Error[] errors)
    {
        return new Result(errors.ToList());
    }
    
    public TResult Match<TResult>(Func<TResult> onValue, Func<List<Error>, TResult> onError)
    {
        return IsError ? onError(Errors!) : onValue();
    }
}

public class Result<TValue> : ResultBase
{
    private readonly TValue? _value;
    
    public TValue Value => _value!;
    
    private Result(Error error)
        : base(error)
    {
    }
    
    private Result(List<Error> errors)
        : base(errors)
    {
    }
    
    private Result(TValue value)
    {
        _value = value;
    }
    
    public static implicit operator Result<TValue>(TValue value)
    {
        return new Result<TValue>(value);
    }
    
    public static implicit operator Result<TValue>(Error error)
    {
        return new Result<TValue>(error);
    }
    
    public static implicit operator Result<TValue>(List<Error> errors)
    {
        return new Result<TValue>(errors);
    }
    
    public static implicit operator Result<TValue>(Error[] errors)
    {
        return new Result<TValue>(errors.ToList());
    }
    
    public TResult Match<TResult>(Func<TValue, TResult> onValue, Func<List<Error>, TResult> onError)
    {
        return IsError ? onError(Errors!) : onValue(Value);
    }
}