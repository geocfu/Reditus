using System.Net;
using Reditus.Definitions;
using Reditus.Extensions.AspNetCore.Http.Abstractions;

namespace Reditus.Extensions.AspNetCore.Http.Definitions
{
    /// <summary>
    /// Represents an HTTP-specific success response, encapsulating an HTTP status code.
    /// This class implements the <see cref="IHttpSuccess"/> interface and extends the <see cref="Success"/> class.
    /// </summary>
    public class HttpSuccess : Success, IHttpSuccess
    {
        /// <summary>
        /// Gets the HTTP status code associated with the success response.
        /// Defaults to <see cref="HttpStatusCode.OK"/> if not specified.
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; } = HttpStatusCode.OK;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpSuccess"/> class with the default HTTP status code.
        /// The HTTP status code defaults to <see cref="HttpStatusCode.OK"/>.
        /// </summary>
        public HttpSuccess()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpSuccess"/> class with the specified HTTP status code.
        /// </summary>
        /// <param name="httpStatusCode">The HTTP status code associated with the success response.</param>
        public HttpSuccess(HttpStatusCode httpStatusCode)
        {
            HttpStatusCode = httpStatusCode;
        }
    }
}