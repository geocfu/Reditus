using System.Net;
using Reditus.Abstractions;

namespace Reditus.Extensions.AspNetCore.Http.Abstractions;

/// <summary>
/// Represents a base interface for HTTP failure responses.
/// Provides the structure for handling common properties like type, title, and status code
/// that are typically used in problem details for HTTP error responses.
/// This interface extends the <see cref="IFailure"/> interface to include additional HTTP-specific properties.
/// </summary>
public interface IHttpFailure : IFailure
{
    /// <summary>
    /// Gets a URI reference that identifies the type of the problem.
    /// This is typically used to provide documentation or further information about the error.
    /// Example: "https://tools.ietf.org/html/rfc9110#section-15.5.1".
    /// </summary>
    public string Type { get; }

    /// <summary>
    /// Gets a short, human-readable title that summarizes the nature of the problem.
    /// Example: "Bad Request" or "Conflict".
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// Gets the HTTP status code associated with this failure.
    /// Example: <see cref="HttpStatusCode.BadRequest"/> (400) or <see cref="HttpStatusCode.Conflict"/> (409).
    /// </summary>
    public HttpStatusCode StatusCode { get; }
}