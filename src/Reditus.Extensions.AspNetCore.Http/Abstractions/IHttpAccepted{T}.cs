namespace Reditus.Extensions.AspNetCore.Http.Abstractions;

/// <summary>
/// Represents an HTTP 202 Accepted success response that includes a data payload.
/// This interface extends <see cref="IHttpSuccess{T}"/> to specify that the success response has an HTTP status code of 202 (Accepted)
/// and contains a result of type <typeparamref name="T"/>. It is used to indicate that the request has been accepted for processing,
/// but the processing has not been completed yet.
/// </summary>
/// <typeparam name="T">The type of the data payload returned in the HTTP 202 Accepted response.</typeparam>
public interface IAccepted<T> : IHttpSuccess<T>
{
    /// <summary>
    /// Creates a successful <see cref="HttpResult{T}"/> for an HTTP 202 Accepted response.
    /// This method allows for the creation of a success result that includes a value of type <typeparamref name="T"/> 
    /// and corresponds to the HTTP 202 status code.
    /// Typically used when a request has been accepted for processing, but the result of the processing is not yet available,
    /// and a response with a body including the current state or additional information is being returned.
    /// </summary>
    /// <param name="value">The value to include in the HTTP 202 Accepted response. This is the data being returned as part of the response body.</param>
    /// <returns>A successful <see cref="HttpResult{T}"/> representing an HTTP 202 Accepted response with the specified value.</returns>
    public static abstract HttpResult<T> CreateHttpResult(T value);
}