using System;
using System.Net;
using Reditus.Definitions;
using Reditus.Extensions.AspNetCore.Abstractions;

namespace Reditus.Extensions.AspNetCore.Definitions
{
    public class InternalServerError : Failure, IHttpFailure
    {
        public HttpStatusCode Status => HttpStatusCode.InternalServerError;

        public InternalServerError()
        {
        }

        public InternalServerError(string message)
            : base(message)
        {
        }

        public InternalServerError(string message, Exception exception)
            : base(message, exception)
        {
        }
    }
}