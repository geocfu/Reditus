using System.Net;
using Reditus.Abstractions;

namespace Reditus.Extensions.AspNetCore.Http.Abstractions;

/// <summary>
///
/// </summary>
public interface IHttpFailure : IFailure
{
    public string Type { get; }

    public string Title { get; }

    HttpStatusCode StatusCode { get; }
}