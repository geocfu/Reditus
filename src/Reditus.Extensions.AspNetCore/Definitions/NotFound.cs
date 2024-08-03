using System;
using System.Net;
using Reditus.Definitions;
using Reditus.Extensions.AspNetCore.Abstractions;

namespace Reditus.Extensions.AspNetCore.Definitions
{
    /// <summary>
    ///
    /// </summary>
    public class NotFound : Failure, IHttpFailure
    {
        /// <summary>
        ///
        /// </summary>
        public string Type { get; } = "https://tools.ietf.org/html/rfc9110#section-15.5.5";

        /// <summary>
        ///
        /// </summary>
        public string Title { get; } = "Not Found";

        /// <summary>
        ///
        /// </summary>
        public HttpStatusCode Status { get; } = HttpStatusCode.NotFound;

        /// <summary>
        ///
        /// </summary>
        public NotFound()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        public NotFound(string message = "The request resource was not found.") : base(message)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public NotFound(string message, Exception exception) : base(message, exception)
        {
        }
    }
}