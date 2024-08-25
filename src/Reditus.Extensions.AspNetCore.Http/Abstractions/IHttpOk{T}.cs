namespace Reditus.Extensions.AspNetCore.Http.Abstractions;

/// <summary>
/// Represents an HTTP 200 OK success response with a value.
/// This interface extends <see cref="IHttpSuccess{T}"/> to specify that the success corresponds to an HTTP 200 status code and includes a value of type <typeparamref name="T"/>.
/// It is used to indicate that the request was successful and that a response body with a value is being returned.
/// </summary>
/// <typeparam name="T">The type of the value included in the response.</typeparam>
public interface IHttpOk<T> : IHttpSuccess<T>
{
    /// <summary>
    /// Creates a successful <see cref="HttpResult{T}"/> for an HTTP 200 OK response.
    /// This method allows for the creation of a success result that includes a value of type <typeparamref name="T"/>
    /// and corresponds to the HTTP 200 status code.
    /// Typically used when a request is successfully processed and a response with a body is returned to the client.
    /// </summary>
    /// <param name="value">The value to include in the HTTP 200 OK response. This is the data being returned as part of the response body.</param>
    /// <returns>A successful <see cref="HttpResult{T}"/> representing an HTTP 200 OK response with the specified value.</returns>
    public static abstract HttpResult<T> CreateHttpResult(T value);
}