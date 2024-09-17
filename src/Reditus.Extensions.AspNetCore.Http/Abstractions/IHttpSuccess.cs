using System.Net;
using Reditus.Abstractions;

namespace Reditus.Extensions.AspNetCore.Http.Abstractions;

/// <summary>
/// Represents a generic interface for HTTP success responses.
/// This interface extends <see cref="ISuccess"/> and adds an HTTP-specific property for the status code.
/// It is used for handling HTTP responses that are successful but do not necessarily carry a payload or a specific result.
/// </summary>
public interface IHttpSuccess : ISuccess
{
    /// <summary>
    /// Gets the HTTP status code associated with this success.
    /// Example: <see cref="HttpStatusCode.OK"/> (200) or <see cref="HttpStatusCode.NoContent"/> (204).
    /// </summary>
    public HttpStatusCode HttpStatusCode { get; }
}