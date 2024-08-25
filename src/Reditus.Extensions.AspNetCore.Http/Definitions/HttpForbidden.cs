using System;
using System.Net;
using Reditus.Definitions;
using Reditus.Extensions.AspNetCore.Http.Abstractions;

namespace Reditus.Extensions.AspNetCore.Http.Definitions;

/// <summary>
/// Represents an HTTP 403 Forbidden failure response.
/// This class extends the <see cref="Failure"/> base class and implements the <see cref="IHttpForbidden"/> interface.
/// It is used to handle cases where the server understands the request but refuses to authorize it.
/// </summary>
public class HttpForbidden : Failure, IHttpForbidden
{
    /// <inheritdoc />
    public string Type => "https://tools.ietf.org/html/rfc9110#section-15.5.4";

    /// <inheritdoc />
    public string Title => "Forbidden";

    /// <inheritdoc />
    public HttpStatusCode StatusCode => HttpStatusCode.Forbidden;

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpForbidden"/> class with default settings.
    /// This constructor is typically used when no specific message or exception needs to be provided.
    /// </summary>
    public HttpForbidden()
        : base("Access is not granted for the request resource.")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpForbidden"/> class with the specified error message.
    /// </summary>
    /// <param name="message">The error message that describes the forbidden access.</param>
    public HttpForbidden(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpForbidden"/> class with the specified error message and exception.
    /// </summary>
    /// <param name="message">The error message that describes the forbidden access.</param>
    /// <param name="exception">The exception that caused the forbidden access.</param>
    public HttpForbidden(string message, Exception exception)
        : base(message, exception)
    {
    }

    /// <inheritdoc />
    public static HttpResult CreateHttpResult()
    {
        var forbidden = new HttpForbidden();
        return HttpResult.CreateFail(forbidden);
    }

    /// <inheritdoc />
    public static HttpResult CreateHttpResult(string message)
    {
        var forbidden = new HttpForbidden(message);
        return HttpResult.CreateFail(forbidden);
    }

    /// <inheritdoc />
    public static HttpResult CreateHttpResult(string message, Exception exception)
    {
        var forbidden = new HttpForbidden(message, exception);
        return HttpResult.CreateFail(forbidden);
    }
}