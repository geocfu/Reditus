using System;

namespace Reditus.Extensions.AspNetCore.Http.Abstractions;

/// <summary>
/// Represents an interface for handling HTTP 409 Conflict failures.
/// A conflict typically occurs when a request cannot be completed due to a conflict with the current state of the resource.
/// This interface extends the <see cref="IHttpFailure"/> interface to provide a structure specific to conflict errors.
/// </summary>
public interface IConflict : IHttpFailure
{
    /// <summary>
    /// Creates an instance of <see cref="HttpResult"/> representing a general failure.
    /// This method provides a default instance without additional context.
    /// </summary>
    /// <returns>A new <see cref="HttpResult"/> indicating failure.</returns>
    public static abstract HttpResult CreateHttpResult();

    /// <summary>
    /// Creates an instance of <see cref="HttpResult"/> representing a failure with a specific error message.
    /// This method allows for customization of the failure result with a message that describes the error.
    /// </summary>
    /// <param name="message">The error message to include in the result.</param>
    /// <returns>A new <see cref="HttpResult"/> indicating failure with the specified message.</returns>
    public static abstract HttpResult CreateHttpResult(string message);

    /// <summary>
    /// Creates an instance of <see cref="HttpResult"/> representing a failure with a specific error message and exception.
    /// This method provides additional context by including the exception that caused the failure.
    /// </summary>
    /// <param name="message">The error message to include in the result.</param>
    /// <param name="exception">The exception that caused the failure.</param>
    /// <returns>A new <see cref="HttpResult"/> indicating failure with the specified message and exception.</returns>
    public static abstract HttpResult CreateHttpResult(string message, Exception exception);
}