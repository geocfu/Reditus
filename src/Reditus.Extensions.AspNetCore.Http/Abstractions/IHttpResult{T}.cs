using System;
using Reditus.Abstractions;

namespace Reditus.Extensions.AspNetCore.Http.Abstractions;

public interface IHttpResult<out T> : IResult<T>
{
    /// <summary>
    /// Gets the Success, if any, attached to the Result.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when Result is Failed.</exception>
    new IHttpSuccess<T> Success { get; }

    /// <summary>
    /// Gets the Failure, if any, attached to the Result.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when Result is Successful.</exception>
    new IHttpFailure Failure { get; }
}