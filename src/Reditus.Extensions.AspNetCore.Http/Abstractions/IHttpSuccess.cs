using System.Net;
using Reditus.Abstractions;

namespace Reditus.Extensions.AspNetCore.Http.Abstractions;

public interface IHttpSuccess : ISuccess
{
    /// <summary>
    ///
    /// </summary>
    HttpStatusCode StatusCode { get; }
}