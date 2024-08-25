using System;
using Reditus.Abstractions;

namespace Reditus.Extensions.AspNetCore.Http.Abstractions;

/// <summary>
/// Represents a result of an HTTP operation that can either be a success or a failure.
/// This interface extends <see cref="IResult"/> by specifying that the success and failure objects are HTTP-specific.
/// It provides access to either a success or failure object depending on the outcome of the operation.
/// </summary>
public interface IHttpResult : IResult
{
    /// <summary>
    /// Gets the HTTP-specific success object attached to the result, if the operation was successful.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the operation was not successful, indicating that there is no success object available.
    /// </exception>
    public new IHttpSuccess Success { get; }

    /// <summary>
    /// Gets the HTTP-specific failure object attached to the result, if the operation failed.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the operation was successful, indicating that there is no failure object available.
    /// </exception>
    public new IHttpFailure Failure { get; }
}