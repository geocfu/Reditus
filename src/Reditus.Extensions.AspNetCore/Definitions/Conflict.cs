using System;
using System.Net;
using Reditus.Definitions;
using Reditus.Extensions.AspNetCore.Abstractions;

namespace Reditus.Extensions.AspNetCore.Definitions
{
    /// <summary>
    ///
    /// </summary>
    public class Conflict : Failure, IHttpFailure
    {
        /// <summary>
        ///
        /// </summary>
        public HttpStatusCode Status => HttpStatusCode.Conflict;

        /// <summary>
        ///
        /// </summary>
        public Conflict()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        public Conflict(string message)
            : base(message)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public Conflict(string message, Exception exception)
            : base(message, exception)
        {
        }
    }
}