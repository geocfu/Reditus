using System;
using System.Net;
using Reditus.Definitions;
using Reditus.Extensions.AspNetCore.Http.Abstractions;

namespace Reditus.Extensions.AspNetCore.Http.Definitions;

/// <summary>
/// Represents an HTTP 204 No Content success response.
/// This class extends the <see cref="Success"/> base class and implements the <see cref="IHttpNoContent"/> interface.
/// It is used to indicate that the request was successfully processed, but there is no content to return.
/// </summary>
public class HttpNoContent : Success, IHttpNoContent
{
    /// <inheritdoc />
    public HttpStatusCode StatusCode => HttpStatusCode.NoContent;

    /// <inheritdoc />
    public static HttpResult CreateHttpResult()
    {
        var noContent = new HttpNoContent();
        return HttpResult.CreateSuccess(noContent);
    }
}