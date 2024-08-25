using System;
using System.Net;
using Reditus.Definitions;
using Reditus.Extensions.AspNetCore.Http.Abstractions;

namespace Reditus.Extensions.AspNetCore.Http.Definitions;

/// <summary>
/// Represents an HTTP 404 Not Found failure response.
/// This class extends the <see cref="Failure"/> base class and implements the <see cref="IHttpNotFound"/> interface.
/// It is used to indicate that the requested resource could not be found on the server.
/// </summary>
public class HttpNotFound : Failure, IHttpNotFound
{
    /// <inheritdoc />
    public string Type => "https://tools.ietf.org/html/rfc9110#section-15.5.5";

    /// <inheritdoc />
    public string Title => "Not Found";

    /// <inheritdoc />
    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpNotFound"/> class with a default error message.
    /// This constructor is typically used when no specific message or exception needs to be provided.
    /// </summary>
    public HttpNotFound()
        : base("The requested resource was not found")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpNotFound"/> class with the specified error message.
    /// </summary>
    /// <param name="message">The error message that describes why the resource was not found.</param>
    public HttpNotFound(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpNotFound"/> class with the specified error message and exception.
    /// </summary>
    /// <param name="message">The error message that describes why the resource was not found.</param>
    /// <param name="exception">The exception that caused the "Not Found" error.</param>
    public HttpNotFound(string message, Exception exception)
        : base(message, exception)
    {
    }

    /// <inheritdoc />
    public static HttpResult CreateHttpResult()
    {
        var notFound = new HttpNotFound();
        return HttpResult.CreateFail(notFound);
    }

    /// <inheritdoc />
    public static HttpResult CreateHttpResult(string message)
    {
        var notFound = new HttpNotFound(message);
        return HttpResult.CreateFail(notFound);
    }

    /// <inheritdoc />
    public static HttpResult CreateHttpResult(string message, Exception exception)
    {
        var notFound = new HttpNotFound(message, exception);
        return HttpResult.CreateFail(notFound);
    }
}