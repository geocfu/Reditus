using System;
using System.Net;
using Reditus.Definitions;
using Reditus.Extensions.AspNetCore.Http.Abstractions;

namespace Reditus.Extensions.AspNetCore.Http.Definitions
{
    /// <summary>
    /// Represents an HTTP-specific success response containing a value and an HTTP status code.
    /// This class implements the <see cref="IHttpSuccess{T}"/> interface and extends the <see cref="Success{T}"/> class.
    /// </summary>
    /// <typeparam name="T">The type of the value contained in the success response.</typeparam>
    public class HttpSuccess<T> : Success<T>, IHttpSuccess<T>
    {
        /// <summary>
        /// Gets the HTTP status code associated with the success response.
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpSuccess{T}"/> class with the specified value and HTTP status code.
        /// </summary>
        /// <param name="httpStatusCode">The HTTP status code associated with the success response.</param>
        /// <param name="value">The value to be included in the success response.</param>
        /// <exception cref="ArgumentNullException">Thrown if the value is null.</exception>
        public HttpSuccess(HttpStatusCode httpStatusCode, T value)
            : base(value)
        {
            HttpStatusCode = httpStatusCode;
        }
    }
}