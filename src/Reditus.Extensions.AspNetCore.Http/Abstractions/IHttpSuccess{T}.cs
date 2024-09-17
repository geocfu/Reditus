using System.Net;
using Reditus.Abstractions;

namespace Reditus.Extensions.AspNetCore.Http.Abstractions;

/// <summary>
/// Represents a generic interface for HTTP success responses.
/// This interface extends <see cref="ISuccess{T}"/> and adds an HTTP-specific property for the status code.
/// It is designed to handle successful HTTP responses where the response body contains a result of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of the successful result contained in the response.</typeparam>
public interface IHttpSuccess<out T> : ISuccess<T>
{
    /// <summary>
    /// Gets the HTTP status code associated with this success.
    /// Example: <see cref="HttpStatusCode.OK"/> (200) or <see cref="HttpStatusCode.Created"/> (201).
    /// </summary>
    public HttpStatusCode HttpStatusCode { get; }
}