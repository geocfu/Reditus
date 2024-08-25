using System;
using System.Net;
using Reditus.Definitions;
using Reditus.Extensions.AspNetCore.Http.Abstractions;

namespace Reditus.Extensions.AspNetCore.Http.Definitions;

/// <summary>
/// Represents an HTTP 422 Unprocessable Entity failure response.
/// This class extends the <see cref="Failure"/> base class and implements the <see cref="IHttpUnprocessableEntity"/> interface.
/// It is used to indicate that the server understands the request and its content type but is unable to process the contained instructions.
/// </summary>
public class HttpUnprocessableEntity : Failure, IHttpUnprocessableEntity
{
    /// <inheritdoc />
    public string Type => "https://tools.ietf.org/html/rfc9110#section-15.5.21";

    /// <inheritdoc />
    public string Title => "Unprocessable Entity";

    /// <inheritdoc />
    public HttpStatusCode StatusCode => HttpStatusCode.UnprocessableEntity;

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpUnprocessableEntity"/> class with a default error message.
    /// This constructor is typically used when no specific message or exception needs to be provided.
    /// </summary>
    public HttpUnprocessableEntity()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpUnprocessableEntity"/> class with the specified error message.
    /// </summary>
    /// <param name="message">The error message that describes why the request is unprocessable.</param>
    public HttpUnprocessableEntity(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpUnprocessableEntity"/> class with the specified error message and exception.
    /// </summary>
    /// <param name="message">The error message that describes why the request is unprocessable.</param>
    /// <param name="exception">The exception that caused the "Unprocessable Entity" error.</param>
    public HttpUnprocessableEntity(string message, Exception exception)
        : base(message, exception)
    {
    }

    /// <inheritdoc />
    public static HttpResult CreateHttpResult()
    {
        var unprocessableEntity = new HttpUnprocessableEntity();
        return HttpResult.CreateFail(unprocessableEntity);
    }

    /// <inheritdoc />
    public static HttpResult CreateHttpResult(string message)
    {
        var unprocessableEntity = new HttpUnprocessableEntity(message);
        return HttpResult.CreateFail(unprocessableEntity);
    }

    /// <inheritdoc />
    public static HttpResult CreateHttpResult(string message, Exception exception)
    {
        var unprocessableEntity = new HttpUnprocessableEntity(message, exception);
        return HttpResult.CreateFail(unprocessableEntity);
    }
}