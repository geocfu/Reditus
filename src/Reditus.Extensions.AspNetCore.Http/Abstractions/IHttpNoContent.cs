namespace Reditus.Extensions.AspNetCore.Http.Abstractions;

/// <summary>
/// Represents an HTTP 204 No Content success response.
/// This interface extends <see cref="IHttpSuccess"/> to specify that the success corresponds to an HTTP 204 status code.
/// It is used to indicate that the request was successfully processed, but there is no content to return.
/// </summary>
public interface IHttpNoContent : IHttpSuccess
{
    /// <summary>
    /// Creates a successful <see cref="HttpResult"/> for an HTTP 204 No Content response.
    /// This method allows for the creation of a success result that corresponds to the HTTP 204 status code.
    /// Typically used when a request is successful but there is no additional content to return in the response.
    /// </summary>
    /// <returns>A successful <see cref="HttpResult"/> representing an HTTP 204 No Content response.</returns>
    public static abstract HttpResult CreateHttpResult();
}