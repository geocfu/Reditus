using System;
using System.Net;
using Reditus.Definitions;
using Reditus.Extensions.AspNetCore.Http.Abstractions;

namespace Reditus.Extensions.AspNetCore.Http.Definitions;

/// <summary>
/// Represents an HTTP 401 Unauthorized failure response.
/// This class extends the <see cref="Failure"/> base class and implements the <see cref="IHttpUnauthorized"/> interface.
/// It is used to indicate that the request cannot be processed because it lacks valid authentication credentials.
/// </summary>
public class HttpUnauthorized : Failure, IHttpUnauthorized
{
    /// <inheritdoc />
    public string Type => "https://tools.ietf.org/html/rfc9110#section-15.5.2";

    /// <inheritdoc />
    public string Title => "Unauthorized";

    /// <inheritdoc />
    public HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpUnauthorized"/> class with a default error message.
    /// This constructor is typically used when no specific message or exception needs to be provided.
    /// </summary>
    public HttpUnauthorized()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpUnauthorized"/> class with the specified error message.
    /// </summary>
    /// <param name="message">The error message that describes why the request is unauthorized.</param>
    public HttpUnauthorized(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpUnauthorized"/> class with the specified error message and exception.
    /// </summary>
    /// <param name="message">The error message that describes why the request is unauthorized.</param>
    /// <param name="exception">The exception that caused the "Unauthorized" error.</param>
    public HttpUnauthorized(string message, Exception exception)
        : base(message, exception)
    {
    }

    /// <inheritdoc />
    public static HttpResult CreateHttpResult()
    {
        var unauthorized = new HttpUnauthorized();
        return HttpResult.CreateFail(unauthorized);
    }

    /// <inheritdoc />
    public static HttpResult CreateHttpResult(string message)
    {
        var unauthorized = new HttpUnauthorized(message);
        return HttpResult.CreateFail(unauthorized);
    }

    /// <inheritdoc />
    public static HttpResult CreateHttpResult(string message, Exception exception)
    {
        var unauthorized = new HttpUnauthorized(message, exception);
        return HttpResult.CreateFail(unauthorized);
    }
}