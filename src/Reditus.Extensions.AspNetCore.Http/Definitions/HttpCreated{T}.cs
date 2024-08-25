using System.Net;
using Reditus.Definitions;
using Reditus.Extensions.AspNetCore.Http.Abstractions;

namespace Reditus.Extensions.AspNetCore.Http.Definitions;

/// <summary>
/// Represents an HTTP 201 Created success response that includes a data payload.
/// This class extends <see cref="Success{T}"/> and implements <see cref="ICreated{T}"/>.
/// It provides a way to encapsulate a successful creation result along with the payload of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of the data payload returned in the HTTP 201 Created response.</typeparam>
public class HttpCreated<T> : Success<T>, ICreated<T>
{
    /// <inheritdoc />
    public HttpStatusCode StatusCode => HttpStatusCode.Created;

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpCreated{T}"/> class with the specified payload.
    /// This constructor is used to create an HTTP 201 Created response with a given payload of type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="value">The data payload to be included in the response.</param>
    public HttpCreated(T value)
        : base(value)
    {
    }

    /// <inheritdoc />
    public static HttpResult<T> CreateHttpResult(T value)
    {
        var created = new HttpCreated<T>(value);
        return HttpResult<T>.CreateSuccess(created);
    }
}