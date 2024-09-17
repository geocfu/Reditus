using System.Collections.ObjectModel;
using System.Net;
using Reditus.Abstractions;

namespace Reditus.Extensions.AspNetCore.Http.Abstractions
{
    /// <summary>
    /// Represents a base interface for HTTP failure responses.
    /// This interface extends the <see cref="IFailure"/> interface to include additional HTTP-specific properties.
    /// It is intended to provide details about failures in HTTP requests, including the status code and errors.
    /// </summary>
    public interface IHttpFailure : IFailure
    {
        /// <summary>
        /// Gets the HTTP status code associated with this failure.
        /// This property indicates the specific HTTP status code that describes the type of failure.
        /// Example: <see cref="HttpStatusCode.BadRequest"/> (400) for validation errors,
        /// or <see cref="HttpStatusCode.Conflict"/> (409) for data conflicts.
        /// </summary>
        HttpStatusCode HttpStatusCode { get; }

        /// <summary>
        /// Gets a collection of error messages related to the failure, if any.
        /// This property provides detailed error information, often used for validation errors or more complex failures.
        /// The dictionary contains keys representing the error source (e.g., field names) and arrays of related error messages.
        /// This value may be null if there are no specific errors to report.
        /// </summary>
        ReadOnlyDictionary<string, string[]> Errors { get; }
    }
}