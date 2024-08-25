using System;
using System.Net;
using Reditus.Definitions;
using Reditus.Extensions.AspNetCore.Http.Abstractions;

namespace Reditus.Extensions.AspNetCore.Http.Definitions;

/// <summary>
/// Represents a concrete implementation of a "Conflict" failure (HTTP 409).
/// This class extends the <see cref="Failure"/> base class and implements the <see cref="IHttpConflict"/> interface.
/// It is used to handle cases where a request cannot be processed due to a conflict with the current state of the resource.
/// </summary>
public class HttpConflict : Failure, IHttpConflict
{
    /// <inheritdoc />
    public string Type => "https://tools.ietf.org/html/rfc9110#section-15.5.10";

    /// <inheritdoc />
    public string Title => "Conflict";

    /// <inheritdoc />
    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpConflict"/> class with default settings.
    /// This constructor is typically used when no specific message or exception needs to be provided.
    /// </summary>
    public HttpConflict()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpConflict"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that describes the conflict.</param>
    public HttpConflict(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpConflict"/> class with a specified error message and exception.
    /// </summary>
    /// <param name="message">The error message that describes the conflict.</param>
    /// <param name="exception">The exception that caused the conflict.</param>
    public HttpConflict(string message, Exception exception)
        : base(message, exception)
    {
    }

    /// <inheritdoc />
    public static HttpResult CreateHttpResult()
    {
        var conflict = new HttpConflict();
        return HttpResult.CreateFail(conflict);
    }

    /// <inheritdoc />
    public static HttpResult CreateHttpResult(string message)
    {
        var conflict = new HttpConflict(message);
        return HttpResult.CreateFail(conflict);
    }

    /// <inheritdoc />
    public static HttpResult CreateHttpResult(string message, Exception exception)
    {
        var conflict = new HttpConflict(message, exception);
        return HttpResult.CreateFail(conflict);
    }
}