using System.Collections.Immutable;

namespace Reditus.Abstractions;

public interface IResult
{
    public bool IsSuccessful { get; }

    public bool IsFailed { get; }

    public IError? Error { get; }
}

public interface IResult<T> : IResult
{
    public T Value { get; set; }
}