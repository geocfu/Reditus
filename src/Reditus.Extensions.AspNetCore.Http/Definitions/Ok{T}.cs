using System.Net;
using Reditus.Abstractions;
using Reditus.Definitions;
using Reditus.Extensions.AspNetCore.Http.Abstractions;

namespace Reditus.Extensions.AspNetCore.Http.Definitions;

/// <summary>
/// Represents an HTTP 200 OK success response with a value.
/// This class extends the <see cref="Success{T}"/> base class and implements the <see cref="IOk{T}"/> interface.
/// It is used to indicate that the request was successful and includes a value of type <typeparamref name="T"/> in the response.
/// </summary>
/// <typeparam name="T">The type of the value included in the response.</typeparam>
public class Ok<T> : Success<T>, IOk<T>
{
    /// <inheritdoc />
    public HttpStatusCode StatusCode => HttpStatusCode.OK;

    /// <summary>
    /// Initializes a new instance of the <see cref="Ok{T}"/> class with the specified value.
    /// This constructor is used to create a success response that includes the provided value.
    /// </summary>
    /// <param name="value">The value to include in the response, representing the successful result of the request.</param>
    public Ok(T value)
        : base(value)
    {
    }


    public static HttpResult<T> CreateSuccesfulHttpResult(T value)
    {
        throw new System.NotImplementedException();
    }
}