using System;
using System.Net;
using Reditus.Definitions;
using Reditus.Extensions.AspNetCore.Http.Abstractions;

namespace Reditus.Extensions.AspNetCore.Http.Definitions;

/// <summary>
///
/// </summary>
public class Conflict : Failure, IHttpFailure
{
    public string Type { get; } = "https://tools.ietf.org/html/rfc9110#section-15.5.10";

    public string Title { get; } = "Conflict";
    
    /// <summary>
    ///
    /// </summary>
    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

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