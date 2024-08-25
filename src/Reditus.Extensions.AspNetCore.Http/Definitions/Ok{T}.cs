using System.Net;
using Reditus.Definitions;
using Reditus.Extensions.AspNetCore.Http.Abstractions;

namespace Reditus.Extensions.AspNetCore.Http.Definitions;

/// <summary>
/// Represents an HTTP 200 OK success response with a value.
/// This class extends the <see cref="Success{T}"/> base class and implements the <see cref="IHttpOk{T}"/> interface.
/// It is used to indicate that the request was successful and includes a value of type <typeparamref name="T"/> in the response.
/// </summary>
/// <typeparam name="T">The type of the value included in the response.</typeparam>
public class HttpOk<T> : Success<T>, IHttpOk<T>
{
    /// <inheritdoc />
    public HttpStatusCode StatusCode => HttpStatusCode.OK;

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpOk{T}"/> class with the specified value.
    /// This constructor is used to create a success response that includes the provided value.
    /// </summary>
    /// <param name="value">The value to include in the response, representing the successful result of the request.</param>
    public HttpOk(T value)
        : base(value)
    {
    }

    /// <inheritdoc />
    public static HttpResult<T> CreateHttpResult(T value)
    {
        var ok = new HttpOk<T>(value);
        return HttpResult<T>.CreateSuccess(ok);
    }
}