using System;
using System.Net;
using Reditus.Definitions;
using Reditus.Extensions.AspNetCore.Http.Abstractions;

namespace Reditus.Extensions.AspNetCore.Http.Definitions;

/// <summary>
/// Represents an HTTP 500 Internal Server Error failure response.
/// This class extends the <see cref="Failure"/> base class and implements the <see cref="IHttpInternalServerError"/> interface.
/// It is used to handle scenarios where the server encounters an unexpected condition that prevents it from fulfilling the request.
/// </summary>
public class HttpInternalServerError : Failure, IHttpInternalServerError
{
    /// <inheritdoc />
    public string Type => "https://tools.ietf.org/html/rfc9110#section-15.6.1";

    /// <inheritdoc />
    public string Title => "Internal Server Error";

    /// <inheritdoc />
    public HttpStatusCode StatusCode => HttpStatusCode.InternalServerError;

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpInternalServerError"/> class with a default error message.
    /// This constructor is typically used when no specific message or exception needs to be provided.
    /// </summary>
    public HttpInternalServerError()
        : base("The server encountered an unexpected condition that prevented it from fulfilling the request.")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpInternalServerError"/> class with the specified error message.
    /// </summary>
    /// <param name="message">The error message that describes the internal server error.</param>
    public HttpInternalServerError(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpInternalServerError"/> class with the specified error message and exception.
    /// </summary>
    /// <param name="message">The error message that describes the internal server error.</param>
    /// <param name="exception">The exception that caused the internal server error.</param>
    public HttpInternalServerError(string message, Exception exception)
        : base(message, exception)
    {
    }

    /// <inheritdoc />
    public static HttpResult CreateHttpResult()
    {
        var internalServerError = new HttpInternalServerError();
        return HttpResult.CreateFail(internalServerError);
    }

    /// <inheritdoc />
    public static HttpResult CreateHttpResult(string message)
    {
        var internalServerError = new HttpInternalServerError(message);
        return HttpResult.CreateFail(internalServerError);
    }

    /// <inheritdoc />
    public static HttpResult CreateHttpResult(string message, Exception exception)
    {
        var internalServerError = new HttpInternalServerError(message, exception);
        return HttpResult.CreateFail(internalServerError);
    }
}