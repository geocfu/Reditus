using Reditus.Abstractions;
using Reditus.Definitions.Errors;

namespace Reditus;

public class Result : IResult
{
    public bool IsSuccessful => Error is null;

    public bool IsFailed => !IsSuccessful;

    public IError? Error { get; }

    protected Result()
    {
    }

    protected Result(IError error)
    {
        Error = error;
    }

    private static Result SuccessfulResult => new();

    private static Result FailedResult => new(new Error("An Error occured."));

    public static Result Successful => SuccessfulResult;

    public static Result Failed()
    {
        return FailedResult;
    }
}