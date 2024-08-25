namespace Reditus.Extensions.AspNetCore.Http.Abstractions;

/// <summary>
/// Represents an HTTP 201 Created success response that includes a data payload.
/// This interface extends <see cref="IHttpSuccess{T}"/> to specify that the success response has an HTTP status code of 201 (Created) 
/// and contains a result of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of the data payload returned in the HTTP 201 Created response.</typeparam>
public interface ICreated<T> : IHttpSuccess<T>
{
    /// <summary>
    /// Creates a successful <see cref="HttpResult{T}"/> for an HTTP 201 Created response.
    /// This method allows for the creation of a success result that includes a data payload of type <typeparamref name="T"/> 
    /// and corresponds to the HTTP 201 status code.
    /// Typically used when a resource has been successfully created and the response includes the newly created resource.
    /// </summary>
    /// <param name="value">The data payload to include in the HTTP 201 Created response.</param>
    /// <returns>A successful <see cref="HttpResult{T}"/> representing an HTTP 201 Created response with the specified data payload.</returns>
    public static abstract HttpResult<T> CreateHttpResult(T value);
}