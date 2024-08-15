using System;
using System.Net;
using Reditus.Definitions;
using Reditus.Extensions.AspNetCore.Http.Abstractions;

namespace Reditus.Extensions.AspNetCore.Http.Definitions;

public class InternalServerError : Failure, IHttpFailure
{
    public string Type { get; } = "https://tools.ietf.org/html/rfc9110#section-15.6.1";

    public string Title { get; } = "Internal Server Error";

    public HttpStatusCode StatusCode => HttpStatusCode.InternalServerError;

    public InternalServerError()
    {
    }

    public InternalServerError(
        string message =
            "the server encountered an unexpected condition that prevented it from fulfilling the request.")
        : base(message)
    {
    }

    public InternalServerError(string message, Exception exception)
        : base(message, exception)
    {
    }
}