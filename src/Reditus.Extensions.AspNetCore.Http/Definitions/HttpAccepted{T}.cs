using System.Net;
using Reditus.Definitions;
using Reditus.Extensions.AspNetCore.Http.Abstractions;

namespace Reditus.Extensions.AspNetCore.Http.Definitions;

/// <summary>
/// Represents an HTTP 202 Accepted success response with a data payload.
/// This class implements the <see cref="IHttpAccepted{T}"/> interface and extends the <see cref="Success{T}"/> class.
/// It specifies that the success response corresponds to an HTTP 202 (Accepted) status code and includes a result of type <typeparamref name="T"/>.
/// It is used to indicate that the request has been accepted for processing, and the response includes additional information about the request.
/// </summary>
/// <typeparam name="T">The type of the data payload included in the HTTP 202 Accepted response.</typeparam>
public class HttpAccepted<T> : Success<T>, IHttpAccepted<T>
{
    /// <inheritdoc />
    public HttpStatusCode StatusCode => HttpStatusCode.OK;

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpAccepted{T}"/> class with the specified data payload.
    /// </summary>
    /// <param name="value">The data payload to include in the HTTP 202 Accepted response.</param>
    public HttpAccepted(T value)
        : base(value)
    {
    }

    /// <inheritdoc />
    public static HttpResult<T> CreateHttpResult(T value)
    {
        var accepted = new HttpAccepted<T>(value);
        return HttpResult<T>.CreateSuccess(accepted);
    }
}